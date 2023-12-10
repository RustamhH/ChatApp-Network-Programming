// client

using System.Net.Sockets;
using System.Net;

var port = 27001;


var client = new TcpClient("192.168.100.8",port);



var stream = client.GetStream();

var binaryReader = new BinaryReader(stream);
var binaryWriter = new BinaryWriter(stream);
Task.Run(async () =>
{

    Console.Write("Your Name: ");
    var name = Console.ReadLine();
    binaryWriter.Write(name);

    while (true)
    {
        Console.Write($"To: ");

        var ToWho = Console.ReadLine();

        Console.Write($"Message: ");

        var Message = Console.ReadLine();
        binaryWriter.Write(ToWho + " " + Message);
    }

});

while (true)
{
    var Mes = binaryReader.ReadString();
    if (!string.IsNullOrEmpty(Mes)) Console.WriteLine(Mes);

}

