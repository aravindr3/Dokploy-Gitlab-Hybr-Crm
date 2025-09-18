using System.Security.Cryptography;
using OtpNet;

namespace HyBrForex.Application.Helpers;

public static class TotpGenerator
{
    public static string GenerateSecretKey()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[20];
        rng.GetBytes(bytes);
        return Base32Encoding.ToString(bytes);
    }

    public static string GenerateQrCodeUrl(string appName, string userEmail, string secretKey)
    {
        return $"otpauth://totp/{appName}:{userEmail}?secret={secretKey}&issuer={appName}";
    }

    public static bool ValidateCode(string secretKey, string code)
    {
        var totp = new Totp(Base32Encoding.ToBytes(secretKey));
        return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2));
    }
}