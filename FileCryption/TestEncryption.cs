namespace FileEncryption;

public class FileEncryption
{
    private static void Run()
    {
        {
            
            const string text = "I have a big dog. You've got a cat. We all love animals!";
            
            Console.WriteLine("-- Encrypt Decrypt symmetric --");
            Console.WriteLine("");
 

            var (key, ivBase64) = KeyConversion.SymmetricEncryptDecrypt();
 
            var encryptedText = AesCryption.Encrypt(text, ivBase64, "key");
 
            Console.WriteLine("-- Key --");
            Console.WriteLine(key);
            Console.WriteLine("-- IVBase64 --");
            Console.WriteLine(ivBase64);
 
            Console.WriteLine("");
            Console.WriteLine("-- Encrypted Text --");
            Console.WriteLine(encryptedText);
 
            var decryptedText = AesCryption.Decrypt(encryptedText, ivBase64, key);
 
            Console.WriteLine("-- Decrypted Text --");
            Console.WriteLine(decryptedText);
        }
    }
}
