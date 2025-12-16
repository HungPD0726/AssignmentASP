using ASPAssignment.DataAccess.Models;
using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Repositories;

namespace ASPAssignment.Business
{
    public class LandlordService : ILandlordService
    {
        private readonly ILandlordRepository _landlordRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LandlordService(ILandlordRepository landlordRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _landlordRepository = landlordRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterLandlordAsync(RegisterDto dto)
        {
            // 1. Kiểm tra số điện thoại bắt buộc
            if (string.IsNullOrEmpty(dto.PhoneNumber))
            {
                throw new Exception("Số điện thoại là bắt buộc cho chủ nhà.");
            }

            // 2. Kiểm tra Email tồn tại trong DB_Users và DB_Landlords
            if (await _landlordRepository.GetByEmailAsync(dto.Email) != null)
            {
                throw new Exception("Email đã được đăng ký trong hệ thống Landlord.");
            }

            if (await _userRepository.GetByEmailAsync(dto.Email) != null)
            {
                throw new Exception("Email đã được đăng ký trong hệ thống User.");
            }

            // 3. Hash mật khẩu
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // 4. Tạo Landlord và lưu vào DB_Landlords
            var newLandlord = new Landlord
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = passwordHash,
                VerificationStatus = "Pending" // Mặc định là Pending
            };
            await _landlordRepository.AddAsync(newLandlord);

            // 5. Tạo Access Token và Refresh Token cho Landlord mới
            var refreshToken = await _tokenService.GenerateRefreshLandlordToken(newLandlord);
            var accessToken = await _tokenService.GenerateAccessLandlordToken(newLandlord);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                TypeRole = "Landlord"
            };
        }

        public async Task<AuthResponseDto> LoginLandlordAsync(LoginDto dto)
        {
            if (dto.TypeRole == "Landlord")
            {
                var user = await _landlordRepository.GetByEmailAsync(dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Sai Email hoặc mật khẩu.");
                }
                RefreshToken refreshToken = await _tokenService.GenerateRefreshLandlordToken(user);
                return new AuthResponseDto
                {
                    AccessToken = await _tokenService.GenerateAccessLandlordToken(user),
                    RefreshToken = refreshToken.Token,
                    TypeRole = refreshToken.NameRole
                };
            }
            else
            {
                throw new UnauthorizedAccessException("Sai Vai trò đăng nhập.");
            }
        }
    }
}
