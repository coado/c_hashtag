using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server {
    public static void Main() {

        System.Console.WriteLine("Server started");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        //Choosing first address from list
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        //Socket listening on TCP/IP
        Socket serverSocket = new Socket(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        //Port reservation
        serverSocket.Bind(localEndPoint);
        //Begin listening
        serverSocket.Listen(100);
        //Awaiting connections
        Socket clientSocket = serverSocket.Accept();
        // Buffer for message, max 1024 bytes
        byte[] bufor = new byte[1024];

        //Blocking call, waiting for message
        int received = clientSocket.Receive(bufor, SocketFlags.None);
        String clientMessage = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine(clientMessage);
        string odpowiedz = "Received: " + clientMessage;
        var echoBytes = Encoding.UTF8.GetBytes(odpowiedz);
        clientSocket.Send(echoBytes, 0);
        try {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
        catch { }
    }

}