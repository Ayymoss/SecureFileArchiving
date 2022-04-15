using System.Security.Cryptography;

namespace FileEncryption;

public static class KeyConversion
{
    public static (string Key, string IVBase64) SymmetricEncryptDecrypt()
    {
        var key = GetEncodedRandomString(32); // 256
        var cipher = CreateCipher(key);
        var ivBase64 = Convert.ToBase64String(cipher.IV);
        return (key, ivBase64);
    }
 
    private static string GetEncodedRandomString(int length)
    {
        var base64 = Convert.ToBase64String(GenerateRandomBytes(length));
        return base64;
    }
 
    public static Aes CreateCipher(string keyBase64)
    {
        // Default values: Keysize 256, Padding PKC27
        var cipher = Aes.Create();
        cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC
 
        cipher.Padding = PaddingMode.ISO10126;
        cipher.Key = Convert.FromBase64String(keyBase64);
 
        return cipher;
    }
 
    private static byte[] GenerateRandomBytes(int length)
    {
        var byteArray = new byte[length];
        RandomNumberGenerator.Fill(byteArray);
        return byteArray;
    }
}
