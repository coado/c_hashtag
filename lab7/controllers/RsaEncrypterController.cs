using System.Security.Cryptography;
using System.Text;


public class RsaEncrypterController {
    KeypairController keypairController = new KeypairController();
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    public RsaEncrypterController() {

    }
    public void encryptFile(string sourceFile, string destFile) {
        if (!File.Exists(sourceFile)) throw new Exception("Source file does not exist");

        Keypair keypair = keypairController.getKeypair();

        string text = File.ReadAllText(sourceFile);
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        byte[] bytesData = byteConverter.GetBytes(text);  

        rsa.FromXmlString(keypair.PublicKey);
        byte[] hashBuffer = rsa.Encrypt(bytesData, false);

        File.WriteAllBytes(destFile, hashBuffer);
    }

    public void decryptFile(string sourceFile, string destFile) {
        if (!File.Exists(sourceFile)) throw new Exception("Source file does not exist");

        Keypair keypair = keypairController.getKeypair();
        byte[] dataToDecrypt = File.ReadAllBytes(sourceFile);

        rsa.FromXmlString(keypair.PrivateKey);
        byte[] data = rsa.Decrypt(dataToDecrypt, false);

        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        File.WriteAllText(destFile, byteConverter.GetString(data));
    }

    public void generateKeypair() {
        string publicKey = rsa.ToXmlString(false);    
        string privateKey = rsa.ToXmlString(true);

        keypairController.saveKeypair(publicKey, privateKey);   
    }


}