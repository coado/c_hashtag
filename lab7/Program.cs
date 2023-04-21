using System;
using System.Security.Cryptography;


class Program {
    static public void Main() {
        Program program = new Program();
        program.zad4();

    }   


    public void zad1() {
        RsaEncrypterController rsaEncrypterController = new RsaEncrypterController();

        while (true) {
            string command = Console.ReadLine();
            if (command == "end") break;
            string[] reqParams = command.Split(" ");

            if (reqParams[0] == "0") {
                rsaEncrypterController.generateKeypair();
                Console.WriteLine("Keypair generated");
            }

            if (reqParams[0] == "1") {
                rsaEncrypterController.encryptFile(reqParams[1], reqParams[2]);
                Console.WriteLine("File encrypted");
            }

            if (reqParams[0] == "2") {
                rsaEncrypterController.decryptFile(reqParams[1], reqParams[2]);
                Console.WriteLine("File decrypted");
            }
        }

    }

    public void zad2() {
        VerifyController verify = new VerifyController();
        verify.verify("./data.txt", "adaa.txt", "SHA256");
    }

    public void zad3() {
        SignatureController signatureController = new SignatureController();
        signatureController.sign("./data.txt", "./signature.txt");
    }

    public void zad4() {
        PasswordEncryptionController passwordEncryptionController = new PasswordEncryptionController();
        while(true) {
            string command = Console.ReadLine();
            if (command == "end") break;
            passwordEncryptionController.run("./data.txt", "./passwEncr.txt", "qwerty", command);
        }
    }
}
