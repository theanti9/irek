using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace irek.Server
{
    public class Listener
    {
        private int port;
        private Socket server;
        private Byte[] data = new Byte[2048];
        static ManualResetEvent allDone = new ManualResetEvent(false);

        public Listener(int _port)
        {
            port = _port;
        }

        public void Run()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
            server.Bind(iep);

            Console.WriteLine("Server Initialized.");
            server.Listen(5);
            Console.WriteLine("Listening...");
            while (true)
            {
                allDone.Reset();
                server.BeginAccept(new AsyncCallback(AcceptCon), server);
                allDone.WaitOne();
            }

        }

        private void AcceptCon(IAsyncResult iar)
        {
            allDone.Set();
            Socket s = (Socket)iar.AsyncState;
            Socket s2 = s.EndAccept(iar);
            SocketStateObject state = new SocketStateObject();
            state.workSocket = s2;
            s2.BeginReceive(state.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(Read), state);
        }

        private void Read(IAsyncResult iar)
        {
            try
            {
                SocketStateObject state = (SocketStateObject)iar.AsyncState;
                Socket s = state.workSocket;

                int read = s.EndReceive(iar);

                if (read > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
                    if (s.Available > 0)
                    {
                        s.BeginReceive(state.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(Read), state);
                        return;
                    }
                }
                if (state.sb.Length > 1)
                {
                    string requestString = state.sb.ToString();
                    // HANDLE REQUEST HERE
                    
                    // Temporary response
                    string resp = "<h1>It Works!</h1>";
                    string head = "HTTP/1.1 200 OK\r\nContent-Type: text/html;\r\nServer: irek\r\nContent-Length:"+resp.Length+"\r\n\r\n";
                    byte[] answer = Encoding.ASCII.GetBytes(head+resp);
                    // end temp.
                    state.workSocket.BeginSend(answer, 0, answer.Length, SocketFlags.None, new AsyncCallback(Send), state.workSocket);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Send(IAsyncResult iar)
        {
            try
            {
                SocketStateObject state = (SocketStateObject)iar.AsyncState;
                int sent = state.workSocket.EndSend(iar);
                state.workSocket.Shutdown(SocketShutdown.Both);
                state.workSocket.Close();
            }
            catch (Exception)
            {
                
            }
            return;
        }
    }
}
