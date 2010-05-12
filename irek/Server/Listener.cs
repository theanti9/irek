using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using irek.Request;
using irek.Configuration;
using libirek.Urls;
namespace irek.Server
{
    public class Listener
    {
        private int port;
        private Socket server;
        private Byte[] data = new Byte[2048];
        static ManualResetEvent allDone = new ManualResetEvent(false);
        public Config config;
        public List<UrlMapItem> GlobalUrlMap;
		public Hashtable ModuleList;
        /// <summary>
        /// Initializes a new instance of the <see cref="Listener"/> class.
        /// </summary>
        /// <param name="cfg">The Config Object.</param>
        public Listener(Config cfg, List<UrlMapItem> UrlMap, Hashtable modulelist)
        {
            port = int.Parse(cfg.Get("port"));
            config = cfg;
            GlobalUrlMap = UrlMap;
            ServicePointManager.DefaultConnectionLimit = 20;
			ModuleList = modulelist;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
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

        /// <summary>
        /// Accepts the con.
        /// </summary>
        /// <param name="iar">The Async Result.</param>
        private void AcceptCon(IAsyncResult iar)
        {
            allDone.Set();
            Socket s = (Socket)iar.AsyncState;
            Socket s2 = s.EndAccept(iar);
            SocketStateObject state = new SocketStateObject();
            state.workSocket = s2;
            s2.BeginReceive(state.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(Read), state);
        }

        /// <summary>
        /// Reads the data from the server.
        /// </summary>
        /// <param name="iar">The Async Result</param>
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
                    /*
                    SocketStateObject nextState = new SocketStateObject();
                    nextState.workSocket = s;
                    s.BeginReceive(state.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(Read), nextState);
                    */
                }
                if (state.sb.Length > 1)
                {
                    string requestString = state.sb.ToString();
                    // HANDLE REQUEST HERE
                    byte[] answer = RequestHandler.Handle(requestString, ref config, ref GlobalUrlMap, ref ModuleList);
                    // Temporary response
                    /*
                    string resp = "<h1>It Works!</h1>";
                    string head = "HTTP/1.1 200 OK\r\nContent-Type: text/html;\r\nServer: irek\r\nContent-Length:"+resp.Length+"\r\n\r\n";
                    byte[] answer = Encoding.ASCII.GetBytes(head+resp);
                    // end temp.
                    */
                    state.workSocket.BeginSend(answer, 0, answer.Length, SocketFlags.None, new AsyncCallback(Send), state);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Sends the response.
        /// </summary>
        /// <param name="iar">The Async Results.</param>
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
