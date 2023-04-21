using System.Security.Cryptography;
using System.Text;

public class VerifyController {
    SHA256Controller sha256 = new SHA256Controller();
    SHA512Controller sha512 = new SHA512Controller();
    MD5Controller md5 = new MD5Controller();

    public VerifyController() {}

    public void verify(string sourceFile, string destFile, string alg) {
        if (!File.Exists(sourceFile)) throw new Exception("Source file does not exist");

        string data = File.ReadAllText(sourceFile);

        if (File.Exists(destFile)) {
            string hashedData = this.generateHash(data, alg);
            string savedData = File.ReadAllText(destFile);

            if (hashedData == savedData) {
                Console.WriteLine("Verification succed");
            } else {
                Console.WriteLine("Verification failed");
            }
        } else {
            string hashedData = this.generateHash(data, alg);
            File.WriteAllText(destFile, hashedData);
        }
    }

    private string generateHash(string text, string alg) {
        if (alg == "SHA256") {
            return sha256.hash(text);
        }

        if (alg == "SHA512") {
            return sha512.hash(text);
        }

        if (alg == "MD5") {
            return md5.hash(text);
        }

        throw new Exception("Provided alghoritm does not exist");
        
    }

}