using ASPAssignment.DataAccess.Models;
using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Repositories;

namespace ASPAssignment.Business
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenService _tokenService;

        public AdminService(IAdminRepository adminRepository, ITokenService tokenService)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAdminAsync(LoginDto dto)
        {
            if (dto.TypeRole == "Admin")
            {
                var user = await _adminRepository.GetByEmailAsync(dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Sai Email hoặc mật khẩu.");
                }
                RefreshToken refreshToken = await _tokenService.GenerateRefreshAdminToken(user);
                return new AuthResponseDto
                {
                    AccessToken = await _tokenService.GenerateAccessAdminToken(user),
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
