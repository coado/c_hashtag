using System.Security.Cryptography;
using System.Text;


public class SHA256Controller {

    public SHA256Controller() {}

    public string hash(string text) {
        Encoding enc = Encoding.UTF8;
        var hashBuilder = new StringBuilder();
        using var hash = SHA256.Create();
        byte[] result = hash.ComputeHash(enc.GetBytes(text));
        foreach (var b in result)
            hashBuilder.Append(b.ToString("x2"));
        return hashBuilder.ToString();
    }
}