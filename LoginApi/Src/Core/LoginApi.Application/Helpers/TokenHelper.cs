using System;
using System.Security.Cryptography;

namespace HyBrForex.Application.Helpers;

public static class TokenHelper
{
    public static string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}