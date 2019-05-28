using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;


namespace Scripts.Networking {
    public class ClientUDP {

        public int playerMode=1;
        public bool waiting = true;
        public Vector3 currentPos;
        public InputValues currentInputs;

        #region private members 	
        private static UdpClient socketConnection;
        private Thread clientReceiveThread;
        private bool running = false;
        private bool gameStarted = false;
        private int port;
        #endregion

        #region singleton
        private static ClientUDP instance = null;

        private ClientUDP() {
            this.playerMode = 1;
            this.currentInputs = new InputValues();
        }

        public static ClientUDP Instance {
            get {
                if (instance == null) {
                    instance = new ClientUDP();
                }
                return instance;
            }
        }
        #endregion

        public void ConnectToServer(string ip, int port) {
            try {
                Debug.LogError("Before connecting");
                socketConnection = new UdpClient(ip, port);
                Debug.LogError("2 connecting");
                Message connectMe = new Message("Player", "Connect me", Message.CONNECTION,null,new Vector3());
                Debug.LogError("3 connecting");
                string msg = JsonConvert.SerializeObject(connectMe);
                Debug.LogError("4 connecting");

                SendData(Encoding.ASCII.GetBytes(msg));
                Debug.LogError("After connecting");

                running = true;
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e) {
                Debug.Log("On client connect exception " + e);
            }
        }

        private void ListenForData() {
            if (socketConnection == null) {
                return;
            }

            Byte[] receiveBytes;
            var timeToWait = TimeSpan.FromSeconds(5);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //IPEndPoint object will allow us to read datagrams sent from any source.
            IAsyncResult asyncResult;

            while (running) {

                asyncResult = socketConnection.BeginReceive(null, null);

                if (gameStarted) {
                    timeToWait = TimeSpan.FromSeconds(5);
                }
                else { 
                    timeToWait = TimeSpan.FromMinutes(5);
                }
                asyncResult.AsyncWaitHandle.WaitOne(timeToWait);

                if (asyncResult.IsCompleted) {
                    try {
                        receiveBytes = new Byte[1024];
                        receiveBytes = socketConnection.EndReceive(asyncResult, ref RemoteIpEndPoint);
                        
                        Message message = JsonConvert.DeserializeObject<Message>(Encoding.ASCII.GetString(receiveBytes));

                        Debug.Log("msg received");
                        switch (message.type) {
                            case Message.WAIT:
                                waiting = true;
                                break;
                            case Message.ROLE:
                                playerMode = int.Parse(message.msg);
                                waiting = false;
                                gameStarted = true;
                                break;
                            case Message.DATA:
                                currentPos = message.position;
                                currentInputs = message.inputValues;
                                break;
                        }
                        Debug.Log(message.name + "> " + message.msg + " type :" + message.type); // TODO change here to correspond to the ui

                        // EndReceive worked and we have received data and remote endpoint
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.ToString());
                        // EndReceive failed and we ended up here
                    }
                }
                else {

                    // Handle the timeout (pause the game)

                    // The operation wasn't completed before the timeout and we're off the hook
                }

            }

            socketConnection.Close();

        }

        public void SendData(Byte[] data) {
            if (socketConnection == null) return;
            if (data.Length == 0) return;

            try {
                Debug.LogError("Before sending");
                socketConnection.Send(data, data.Length);
                Debug.LogError("after sending");

                Debug.Log("Client sent his message - should be received by server");
            }
            catch (SocketException socketException) {
                Debug.Log("Socket exception: " + socketException);
            }
        }


        //void Start() {
        //    try {
        //        socketConnection = new UdpClient("localhost", port);
        //        Debug.Log("client created");
        //        running = true;

        //        // Receive thread
        //        clientReceiveThread = new Thread(new ThreadStart(ListenForData));
        //        clientReceiveThread.IsBackground = true;
        //        clientReceiveThread.Start();

        //        // Send thread

        //        // Make a name request before sending messages
        //        string name = "Zerkit";
        //        //

        //        Message message = new Message(name, "msg");
        //        int i = 0;
        //        while (i<3) { // TODO change this to correspond to the ui
        //            message.msg = "new message";
        //            string msg = JsonConvert.SerializeObject(message);

        //            SendData(Encoding.ASCII.GetBytes(msg));
        //            i++;
        //        }
        //    }
        //    catch (Exception e) {
        //        Debug.Log("On client connect exception " + e);
        //    }
        //}

    }
}