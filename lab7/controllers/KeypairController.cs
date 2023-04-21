using System.Text;



public class KeypairController {
    string filePublicKey = "publicKey.dat";
    string filePrivateKey = "privateKey.dat";

    public KeypairController() {

    }

    public void saveKeypair(string publicKey, string privateKey) {
        File.WriteAllText(filePublicKey, publicKey);
        File.WriteAllText(filePrivateKey, privateKey);   
    }

    public Keypair getKeypair() {
        if (!File.Exists(filePublicKey) || !File.Exists(filePrivateKey)) throw new Exception("Keypair not generated");
        string publicKey = File.ReadAllText(filePublicKey);
        string privateKey = File.ReadAllText(filePrivateKey);
        return new Keypair(publicKey, privateKey);
    }
    
}