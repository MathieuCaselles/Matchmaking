﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Matchmaking
{
    public class Serveur
    {

        public static int nbrPartie = 0;

        public static void StartServer()
        {
            // Establish the local endpoint for the socket.  
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                Console.WriteLine("Serveur démarré.");
                while (true)
                {
                    Console.WriteLine($"Nombre de parties en cours : {nbrPartie}");

                    //attente d'un joueur
                    Socket firstClient = listener.Accept();

                    //création objet client
                    Client client1 = new Client(firstClient);
                    client1.setPseudo(firstClient.Receive(client1.getBuffer()));

                    //signale au joueur d'attendre un adversaire
                    JObject obj = new JObject();
                    obj.Add("message", "En attente d'un adversaire...");
                    String reponse = obj.ToString(Formatting.None) + "\n";
                    client1.getWorkSocket().Send(Encoding.UTF8.GetBytes(reponse));

                    //attente d'un joueur
                    Socket secondClient = listener.Accept();

                    //création d'un client
                    Client client2 = new Client(secondClient);
                    client2.setPseudo(secondClient.Receive(client2.getBuffer()));

                    //lancement de la partie
                    Task.Run(() => {
                        new PartieEnCours(client1, client2);
                    });
                    nbrPartie++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

       

    }
}
