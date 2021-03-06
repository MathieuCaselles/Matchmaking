﻿using System;
using System.Net.Sockets;
using System.Text;

namespace Matchmaking
{
    class Client
    {
        // Client  socket.  
        private Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        private byte[] buffer = new byte[BufferSize];
        // Received data string.  
        private StringBuilder sb = new StringBuilder();

        private StringBuilder pseudo = new StringBuilder();

        public Client(Socket workSocket)
        {
            this.workSocket = workSocket;
        }

        public Socket getWorkSocket()
        {
            return this.workSocket;
        }

        public void appendStringData(int bytesRead)
        {
            this.sb.Append(Encoding.UTF8.GetString(
                   this.buffer, 0, bytesRead));
        }

        public void clearStringData()
        {
            this.sb.Clear();
        }

        public String getStringData()
        {
            return this.sb.ToString();
        }

        public byte[] getBuffer()
        {
            return this.buffer;
        }

        public String getPseudo()
        {
            return this.pseudo.ToString();
        }

        public void setPseudo(int pseudo)
        {
            this.pseudo.Clear();
            this.pseudo.Append(Encoding.UTF8.GetString(
                   this.buffer, 0, pseudo));
        }

        public override String ToString()
        {
            return this.pseudo.ToString();
        }
    }
}
