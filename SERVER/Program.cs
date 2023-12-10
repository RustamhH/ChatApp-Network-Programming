


// server

using System.Net;
using System.Net.Sockets;

#pragma warning disable

var ip = IPAddress.Parse("192.168.100.8");
var port = 27001;

var listener = new TcpListener(ip, port);

Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();


listener.Start(10);


while (true)
{
    var client = listener.AcceptTcpClient();

    Task.Run(() =>
    {
        bool isfirst = true;
        var clientStream = client.GetStream();

        var binaryReader = new BinaryReader(clientStream);
        
        var readString = "";
        var tousername = "";
        var index = 0;
        
        var username = "";
        
        if (isfirst)
        {
            username = binaryReader.ReadString();
            Console.WriteLine($"{username} Connected.... ");

            clients.Add(username, client);

            isfirst = false;
        }



        while (true)
        {
            readString = binaryReader.ReadString();
            index = readString.IndexOf(" ");
            tousername = readString.Substring(0, index);


            var qebulEden = clients.FirstOrDefault(c => c.Key.ToString() == tousername).Value;
            if (qebulEden is not null)
            {

                var qebulEdenStream = qebulEden.GetStream();
                var binaryWriter = new BinaryWriter(qebulEdenStream);


                var sendMsg = username+ ":  " + readString.Substring(index + 1);

                binaryWriter.Write(sendMsg);
            }

        }

    });

}
