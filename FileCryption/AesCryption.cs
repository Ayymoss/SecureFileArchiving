using System.Text;

namespace FileEncryption;

public static class AesCryption
{
    public static string Encrypt(string text, string iv, string key)
    {
        var cipher = KeyConversion.CreateCipher(key);
        cipher.IV = Convert.FromBase64String(iv);
 
        var cryptTransform = cipher.CreateEncryptor();
        var plaintext = Encoding.UTF8.GetBytes(text);
        var cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);
 
        return Convert.ToBase64String(cipherText);
    }

    public static string Decrypt(string encryptedText, string iv, string key)
    {
        var cipher = KeyConversion.CreateCipher(key);
        cipher.IV = Convert.FromBase64String(iv);
 
        var cryptTransform = cipher.CreateDecryptor();
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        var plainBytes = cryptTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
 
        return Encoding.UTF8.GetString(plainBytes);
    }
}
