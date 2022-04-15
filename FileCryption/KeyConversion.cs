using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace FileCryption;

/*
    base64: 8EggtuUpXmM5jV9r6VzHlNFtPIsAR09ZdFGkCEfsrno= length: 44
    convertBase64ToString: ?H ??)^c9?_k?\u?m<? GOYtQG?z length: 32
    Key: 8EggtuUpXmM5jV9r6VzHlNFtPIsAR09ZdFGkCEfsrno= length: 44
 */

public static class KeyConversion
{
    public static (string Key, string IVBase64) SymmetricEncryptDecrypt(string userSalt)
    {
        var key = GetEncodedRandomString(userSalt);

        var cipher = CreateCipher(key);
        var ivBase64 = Convert.ToBase64String(cipher.IV);
        return (key, ivBase64);
    }

    private static string GetEncodedRandomString(string userKey)
    {
        var userKeyBytes = Encoding.ASCII.GetBytes(userKey);
        
        var base64 = userKey.Length != 32
            ? userKeyBytes.Concat(Encoding.ASCII.GetBytes(new string('0', 32 - userKey.Length))).ToArray()
            : userKeyBytes;

        return Convert.ToBase64String(base64);
    }

    public static Aes CreateCipher(string keyBase64)
    {
        // Default values: Keysize 256, Padding PKC27
        var cipher = Aes.Create();
        cipher.Mode = CipherMode.CBC; // Ensure the integrity of the ciphertext if using CBC

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
