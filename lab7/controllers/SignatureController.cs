using System.Security.Cryptography;
using System.Text;


public class SignatureController {
    KeypairController keypairController = new KeypairController();
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();


    public SignatureController() {}

    public void sign(string sourceFile, string destFile) {
        if (!File.Exists(sourceFile)) throw new Exception("Source file does not exist");

        Keypair keypair = keypairController.getKeypair();
        string data = File.ReadAllText(sourceFile);
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        byte[] bytesData = byteConverter.GetBytes(data);

        if (File.Exists(destFile)) {
            rsa.FromXmlString(keypair.PublicKey);
            
            byte[] signature = File.ReadAllBytes(destFile);
            bool isSignatureValid = rsa.VerifyData(bytesData, new SHA256CryptoServiceProvider(), signature);

            if (isSignatureValid) {
                Console.WriteLine("Signature valid");
            } else {
                Console.WriteLine("Signature invalid");
            }

        } else {
            rsa.FromXmlString(keypair.PrivateKey);
            byte[] signature = rsa.SignData(bytesData, new SHA256CryptoServiceProvider());
            File.WriteAllBytes(destFile, signature);
        }
    }
}