using System.Text;
using System.Security.Cryptography;


public class PasswordEncryptionController {
        byte[] salt = RandomNumberGenerator.GetBytes(8);
        byte[] initVector = RandomNumberGenerator.GetBytes(16);
        int iterations = 5;
    public PasswordEncryptionController() {}

    public void run(string sourceFile, string destFile, string password, string command) {
        if (!File.Exists(sourceFile)) throw new Exception("Source file does not exist");

        UnicodeEncoding byteConverter = new UnicodeEncoding();  

        if (command == "0") {
            string data = File.ReadAllText(sourceFile);
            byte[] bytesData = byteConverter.GetBytes(data);
            byte[] encryptedData = encrypt(bytesData, password);
            File.WriteAllBytes(destFile, encryptedData);
        }

        if (command == "1") {   
            byte[] data = File.ReadAllBytes(destFile);
            byte[] decryptedData = decrypt(data, password);
            File.WriteAllText(destFile, byteConverter.GetString(decryptedData));
        }
    }

    private byte[] encrypt(byte[] data, string password) {
        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        Aes encAlg = Aes.Create();
        encAlg.IV = initVector;
        encAlg.Key = k1.GetBytes(16);
        MemoryStream encryptionStream = new MemoryStream();
        CryptoStream encrypt = new CryptoStream(encryptionStream,
            encAlg.CreateEncryptor(), CryptoStreamMode.Write);
        encrypt.Write(data, 0, data.Length);
        encrypt.FlushFinalBlock();
        encrypt.Close();
        k1.Reset();
        return encryptionStream.ToArray();
    }

    private byte[] decrypt(byte[] data, string password) {
        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        Aes decAlg = Aes.Create();
        decAlg.Key = k1.GetBytes(16);
        decAlg.IV = initVector;
        MemoryStream decryptionStreamBacking = new MemoryStream();
        CryptoStream decrypt = new CryptoStream(
            decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
        decrypt.Write(data, 0, data.Length);
        decrypt.Flush();
        decrypt.Close();
        k1.Reset();
        return decryptionStreamBacking.ToArray();
    }

}