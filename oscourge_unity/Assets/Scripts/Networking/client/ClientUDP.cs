#pragma warning disable 35

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

        #region game state
        public int gameState; // Positive corresponds to server states / Negative simple states client sided
        public const int RESTART = -2; // Happens during playing (short period of time)
        public const int OFFLINE = -1; // When the game is locally played
        public const int INIT = 1;
        public const int PLAYING = 2;
        public const int PAUSE = 3;
        public const int CONNECTION_ERROR = 4;
        #endregion

        #region current values
        public int playerMode;
        public Vector3 currentPos;
        public InputValues currentInputs;
        public byte[] valuesToSend;
        #endregion

        #region private members 	
        private static UdpClient socketConnection; // socket to send/receive messages
        private Thread clientReceiveThread;
        #endregion

        #region singleton
        private static ClientUDP instance = null;

        private ClientUDP() {
            playerMode = 1;
            gameState = OFFLINE;
            currentInputs = new InputValues();
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
                changeGameState(INIT);

                socketConnection = new UdpClient(ip, port); // creation of the socket
                sendTypedMessage(Message.CONNECTION);

                // Creation of the receiving thread
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e) {
                Debug.LogError("On client connect exception " + e);
            }
        }

        private void ListenForData() {
            if (socketConnection == null) return;

            Byte[] receiveBytes;

            // IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // permits the implementation of the timeout
            IAsyncResult asyncResult;

            TimeSpan fiveSecondWait = TimeSpan.FromSeconds(5);
            TimeSpan fiveMinuteWait = TimeSpan.FromMinutes(5);
            TimeSpan timeToWait;

            Debug.Log("Starting receiving thread");
            while (gameState != OFFLINE) {

                asyncResult = socketConnection.BeginReceive(null, null);

                if (gameState == PLAYING) {
                    timeToWait = fiveSecondWait;
                }
                else {
                    timeToWait = fiveMinuteWait;
                }
                asyncResult.AsyncWaitHandle.WaitOne(timeToWait);

                if (asyncResult.IsCompleted) {
                    try {
                        receiveBytes = new Byte[1024];
                        receiveBytes = socketConnection.EndReceive(asyncResult, ref RemoteIpEndPoint);

                        Message message = JsonConvert.DeserializeObject<Message>(Encoding.ASCII.GetString(receiveBytes));

                        switch (message.type) {
                            case Message.WAIT:
                                Debug.Log("type : waiting");
                                break;
                            case Message.ROLE:
                                Debug.Log("type : role");
                                playerMode = int.Parse(message.msg);
                                changeGameState(PLAYING);
                                break;
                            case Message.DATA:
                                currentPos = message.position;
                                currentInputs = message.inputValues;
                                break;
                            case Message.PAUSE:
                                Debug.Log("type : pause");
                                changeGameState(PAUSE);
                                break;
                            case Message.RESUME:
                                Debug.Log("type : resume");
                                changeGameState(PLAYING);
                                break;
                            case Message.HANDSHAKE:
                                Debug.Log("type : handshake");
                                sendTypedMessage(Message.HANDSHAKE);
                                break;
                            case Message.CONNECTION_ERROR:
                                Debug.Log("type : connection_error");
                                changeGameState(CONNECTION_ERROR);
                                sendTypedMessage(Message.RECONNECTION);
                                break;
                            case Message.RESTART:
                                Debug.Log("type : restart");
                                changeGameState(RESTART);
                                break;
                            case Message.ENDGAME:
                                Debug.Log("type : endGame");
                                break;
                            default:
                                Debug.LogError("wrong type of message");
                                break;
                        }

                        // EndReceive worked and we have received data and remote endpoint
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.ToString());
                        // EndReceive failed and we ended up here
                    }
                }
                else {

                    // Handle the timeout (pause the game)

                    //changeGameState(CONNECTION_ERROR);
                    //sendTypedMessage(Message.CONNECTION_ERROR);

                    //GameManager[] gameManager = UnityEngine.Object.FindObjectsOfType<GameManager>();
                    //if (gameManager.Length == 0) {
                    //    Debug.Log("No game manager found !");
                    //}
                    //if (gameManager.Length != 1) {
                    //    Debug.Log("Multiple game manager found !");
                    //}
                    //else {
                    //    gameManager[0].DisconnectedToggle();
                    //}

                    // The operation wasn't completed before the timeout and we're off the hook
                }

            }

            socketConnection.Close();

        }

        public void GiveUp() {
            changeGameState(OFFLINE);
            sendTypedMessage(Message.ENDGAME);
            socketConnection.Close();
        }

        public void changeGameState(int state) {
            gameState = state;
        }

        public void sendTypedMessage(int type) {
            string msg = JsonConvert.SerializeObject(new Message(type));
            SendData(Encoding.ASCII.GetBytes(msg));
        }

        public void SendData(Byte[] data) {
            if (socketConnection == null) return;
            if (data.Length == 0) return;

            try {
                socketConnection.Send(data, data.Length);
            }
            catch (SocketException socketException) {
                Debug.Log("Socket exception: " + socketException);
            }
        }

    }
}
