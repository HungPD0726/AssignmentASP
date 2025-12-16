using ASPAssignment.DataAccess.Models;
using ASPAssignment.DataAccess.Repositories;
using ASPAssignment.Business.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASPAssignment.Business
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILandlordRepository _landlordRepository;
        private readonly IAdminRepository _adminRepository;

        public TokenService(IConfiguration config,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            ILandlordRepository landlordRepository,
            IAdminRepository adminRepository)
        {
            _config = config;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _landlordRepository = landlordRepository;
            _adminRepository = adminRepository;
        }

        public async Task<string> GenerateAccessStudentToken(User user)
        {
            var jwt = _config.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName),
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["AccessTokenMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateAccessLandlordToken(Landlord landlord)
        {
            var jwt = _config.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, landlord.LandlordID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, landlord.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, landlord.FullName),
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["AccessTokenMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateAccessAdminToken(Admin admin)
        {
            var jwt = _config.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, admin.AdminID.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, admin.AdminName),
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["AccessTokenMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshStudentToken(User user)
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            string result = Convert.ToBase64String(bytes);
            RefreshToken refreshToken = new RefreshToken
            {
                NameRole = "Student",
                RequestNameRoleId = user.UserID,
                Token = result,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtSettings:RefreshTokenMinutes"))
            };
            await _refreshTokenRepository.RevokedAllTokenAsync("Student", user.UserID);
            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken> GenerateRefreshLandlordToken(Landlord landlord)
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            string result = Convert.ToBase64String(bytes);
            RefreshToken refreshToken = new RefreshToken
            {
                NameRole = "Landlord",
                RequestNameRoleId = landlord.LandlordID,
                Token = result,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtSettings:RefreshTokenMinutes"))
            };
            await _refreshTokenRepository.RevokedAllTokenAsync("Landlord", landlord.LandlordID);
            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken> GenerateRefreshAdminToken(Admin admin)
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            string result = Convert.ToBase64String(bytes);
            RefreshToken refreshToken = new RefreshToken
            {
                NameRole = "Admin",
                RequestNameRoleId = admin.AdminID,
                Token = result,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtSettings:RefreshTokenMinutes"))
            };
            await _refreshTokenRepository.RevokedAllTokenAsync("Admin", admin.AdminID);
            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
        {
            var stored = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (stored == null || stored.IsRevoked || stored.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token không hợp lệ hoặc đã hết hạn.");
            }

            // Xác định role và tạo token mới
            if (stored.NameRole == "Student")
            {
                var user = await _userRepository.GetByIdAsync(stored.RequestNameRoleId)
                    ?? throw new UnauthorizedAccessException("Người dùng không tồn tại.");

                var newAccess = await GenerateAccessStudentToken(user);

                return new AuthResponseDto
                {
                    AccessToken = newAccess,
                    RefreshToken = refreshToken, // Giữ nguyên refresh token cũ
                    TypeRole = "Student"
                };
            }
            else if (stored.NameRole == "Landlord")
            {
                var landlord = await _landlordRepository.GetByIdAsync(stored.RequestNameRoleId)
                    ?? throw new UnauthorizedAccessException("Landlord không tồn tại.");

                var newAccess = await GenerateAccessLandlordToken(landlord);

                return new AuthResponseDto
                {
                    AccessToken = newAccess,
                    RefreshToken = refreshToken, // Giữ nguyên refresh token cũ
                    TypeRole = "Landlord"
                };
            }
            else if (stored.NameRole == "Admin")
            {
                var admin = await _adminRepository.GetByIdAsync(stored.RequestNameRoleId)
                    ?? throw new UnauthorizedAccessException("Admin không tồn tại.");

                var newAccess = await GenerateAccessAdminToken(admin);

                return new AuthResponseDto
                {
                    AccessToken = newAccess,
                    RefreshToken = refreshToken, // Giữ nguyên refresh token cũ
                    TypeRole = "Admin"
                };
            }
            else
            {
                throw new UnauthorizedAccessException("Loại vai trò không hợp lệ.");
            }
        }
    }
}
