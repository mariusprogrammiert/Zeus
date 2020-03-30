using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading;

namespace Zeus
{
    class LogicController
    {
        public event Action<string> UpdateStatus;
        public event Action<string> UpdateOpponentText;
        public event Action<string> NewChatMessage;
        public event Action<string, string> UpdateNames;
        public event Action<int, int> UpdatePoints;
        public event Action LockButtons;
        public event Action UnlockButtons;
        public event Action LockChat;
        public event Action UnlockChat;

        string connectionMode;
        string playerName, serverName, clientName;
        int port, currentRound, rounds, pointsServer, pointsClient;
        string host;
        bool isGameOver;
        Thread connectionThread;
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        SymmetricAlgorithm cryptAlgorithm;
        Rfc2898DeriveBytes deriveBytes;
        MessageModel.Move serverMove, clientMove;
        ICryptoTransform encryptor, decryptor;

        public LogicController(string connectionMode, string playerName, string key, int port, int rounds, string host)
        {
            this.connectionMode = connectionMode;
            this.playerName = playerName;
            this.port = port;
            this.rounds = rounds;
            this.host = host;
            isGameOver = false;
            currentRound = 0;
            pointsServer = 0;
            pointsClient = 0;
            serverMove = MessageModel.Move.None;
            clientMove = MessageModel.Move.None;

            cryptAlgorithm = Rijndael.Create();
            deriveBytes = new Rfc2898DeriveBytes(key, new byte[] { 0x5a, 0x6f, 0x6d, 0x69, 0x75, 0x6d, 0x20, 0x43, 0x58, 0x6c, 0x6f, 0x32, 0x69, 0x64, 0x6b });
            cryptAlgorithm.Padding = PaddingMode.ISO10126;
            cryptAlgorithm.Key = deriveBytes.GetBytes(32);
            cryptAlgorithm.IV = deriveBytes.GetBytes(16);
            encryptor = cryptAlgorithm.CreateEncryptor();
            decryptor = cryptAlgorithm.CreateDecryptor();
        }

        public void initController()
        {
            if (connectionMode.Equals("server"))
            {
                connectionThread = new Thread(StartServer);
            }
            else
            {
                connectionThread = new Thread(StartClient);
            }
            connectionThread.IsBackground = true;
            connectionThread.Start();
        }

        public void clickedScissors()
        {
            LockButtons();
            newMove(MessageModel.Move.Scissors);
        }

        public void clickedRock()
        {
            LockButtons();
            newMove(MessageModel.Move.Rock);
        }

        public void clickedPaper()
        {
            LockButtons();
            newMove(MessageModel.Move.Paper);
        }

        public void typedChatMessage(string text)
        {
            MessageModel chatMessage = new MessageModel();
            text = playerName + ": " + text;
            NewChatMessage(text);
            chatMessage.setChatMessage(text);
            sendMessage(chatMessage);
        }

        private void newMove(MessageModel.Move move)
        {
            UpdateStatus("Warte auf Gegner...");
            if (connectionMode.Equals("client"))
            {
                MessageModel moveMessage = new MessageModel();
                moveMessage.setMoves(MessageModel.Move.None, move);
                sendMessage(moveMessage);
            }
            else
            {
                serverMove = move;
                processMoves();
            }
        }

        private void processMessage(MessageModel message)
        {
            // Spielzug
            if (message.messageType == MessageModel.Type.MyMove)
            {
                if (connectionMode.Equals("server"))
                {
                    clientMove = message.moveClient;
                    processMoves();
                }
            }

            if (message.messageType == MessageModel.Type.RoundOver)
            {
                // Wird nur vom Client empfangen
                UpdateOpponentText(getMoveText(message.moveServer));
                UpdatePoints(message.pointsClient, message.pointsServer);
                UpdateStatus(message.roundWinner + " hat die Runde gewonnen!");

                UnlockButtons();
            }

            if (message.messageType == MessageModel.Type.GameOver)
            {
                isGameOver = true;
                UpdateStatus("Spiel vorbei! - " + message.roundWinner + " hat das Spiel gewonnen!");

                LockButtons();
            }

            if (message.messageType == MessageModel.Type.ChatMessage)
            {
                NewChatMessage(message.chatMessage);
            }
        }

        private void processMoves()
        {
            if ((serverMove != MessageModel.Move.None) && (clientMove != MessageModel.Move.None))
            {
                // Beide Spieler haben ihren Spielzug durchgeführt
                int winner = determineWinner(serverMove, clientMove);

                MessageModel roundOverMessage = new MessageModel();
                if (winner == 1)
                {
                    // Spieler auf Server-Seite hat gewonnen

                    pointsServer++;
                    roundOverMessage.setRoundOver(pointsServer, pointsClient, serverMove, clientMove, serverName);
                    UpdateStatus(serverName + " hat die Runde gewonnen!");
                }
                else if (winner == 2)
                {
                    // Spieler auf Client-Seite hat gewonnen

                    pointsClient++;
                    roundOverMessage.setRoundOver(pointsServer, pointsClient, serverMove, clientMove, clientName);
                    UpdateStatus(clientName + " hat die Runde gewonnen!");
                }
                else if (winner == 0)
                {
                    // Unentschieden

                    roundOverMessage.setRoundOver(pointsServer, pointsClient, serverMove, clientMove, "Niemand");
                    UpdateStatus("Niemand hat die Runde gewonnen!");
                    currentRound--;
                }
                UpdateOpponentText(getMoveText(clientMove));
                UpdatePoints(pointsServer, pointsClient);
                sendMessage(roundOverMessage);

                currentRound++;
                if (currentRound >= rounds)
                {
                    // Letzte Runde vorbei

                    isGameOver = true;
                    MessageModel gameOverMessage = new MessageModel();

                    if (pointsServer > pointsClient)
                    {
                        gameOverMessage.setGameOver(serverName);
                        UpdateStatus("Spiel vorbei! - " + serverName + " hat das Spiel gewonnen!");
                    }
                    else if (pointsServer < pointsClient)
                    {
                        gameOverMessage.setGameOver(clientName);
                        UpdateStatus("Spiel vorbei! - " + clientName + " hat das Spiel gewonnen!");
                    }
                    else
                    {
                        gameOverMessage.setGameOver("Niemand");
                        UpdateStatus("Spiel vorbei! - Niemand hat das Spiel gewonnen!");
                    }

                    sendMessage(gameOverMessage);
                }
                else
                {
                    serverMove = MessageModel.Move.None;
                    clientMove = MessageModel.Move.None;
                    UnlockButtons();
                }
            }
        }

        private void sendMessage(MessageModel message)
        {
            // Danke an https://stackoverflow.com/a/2316483
            byte[] userDataBytes;
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, message);
            userDataBytes = encryptByteArray(memoryStream.ToArray());

            byte[] userDataLen = BitConverter.GetBytes((Int32)userDataBytes.Length);
            // Zuerst die Länge der Daten senden und danach die Daten
            stream.Write(userDataLen, 0, 4);
            stream.Write(userDataBytes, 0, userDataBytes.Length);
        }

        private void readMessages()
        {
            // Danke an https://stackoverflow.com/a/2316483
            try
            {
                while (true)
                {
                    byte[] readMsgLen = new byte[4];
                    stream.Read(readMsgLen, 0, 4);

                    int dataLen = BitConverter.ToInt32(readMsgLen, 0);
                    byte[] readMsgData = new byte[dataLen];
                    stream.Read(readMsgData, 0, dataLen);
                    MemoryStream ms = new MemoryStream(decryptByteArray(readMsgData));
                    BinaryFormatter bf = new BinaryFormatter();
                    ms.Position = 0;
                    MessageModel message = (MessageModel)bf.Deserialize(ms);
                    processMessage(message);
                }
            }
            catch (IOException)
            {
                if (!isGameOver)
                {
                    UpdateStatus("Fehler: Verbindung zum Gegenspieler verloren!");
                }
                else
                {
                    UpdateStatus("Hinweis: Der Gegenspieler hat das Spiel geschlossen!");
                }
                LockButtons();
                LockChat();
            }
        }

        private void StartServer()
        {
            MessageModel message = new MessageModel();

            try
            {
                server = TcpListener.Create(port);
                server.Start();

                UpdateStatus("Warte auf anderen Spieler...");
                client = server.AcceptTcpClient();
                stream = client.GetStream();

                // Empfange Spielernamen vom Client
                byte[] readMsgLen = new byte[4];
                stream.Read(readMsgLen, 0, 4);

                int dataLen = BitConverter.ToInt32(readMsgLen, 0);
                byte[] readMsgData = new byte[dataLen];
                stream.Read(readMsgData, 0, dataLen);
                MemoryStream memoryStream = new MemoryStream(decryptByteArray(readMsgData));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Position = 0;
                message = (MessageModel)binaryFormatter.Deserialize(memoryStream);

                serverName = playerName;
                clientName = message.playerClient;
                UpdateNames(serverName, clientName);

                UpdateStatus("Gegner ist da!");

                // Sende eigenen Spielernamen
                message.playerServer = playerName;
                sendMessage(message);

                UpdateStatus("Mache deinen Zug!");
                UnlockButtons();
                UnlockChat();

                readMessages();
            }
            catch (SocketException)
            {
                UpdateStatus("Fehler: Server konnte nicht gestartet werden!");
            }
            catch (SerializationException)
            {
                UpdateStatus("Fehler: Unbekanntes Datenformat empfangen! - Ist der Schlüssel richtig?");
            }
            catch (ArgumentOutOfRangeException)
            {
                UpdateStatus("Fehler: Ungültiger Port!");
            }
        }

        private void StartClient()
        {
            MessageModel message = new MessageModel();
            message.setNames("", playerName);

            UpdateStatus("Verbinde mit Server...");
            try
            {
                client = new TcpClient(host, port);
                UpdateStatus("Verbindung hergestellt!");
                stream = client.GetStream();

                //senden
                sendMessage(message);

                //lesen
                byte[] readMsgLen = new byte[4];
                stream.Read(readMsgLen, 0, 4);

                int dataLen = BitConverter.ToInt32(readMsgLen, 0);
                byte[] readMsgData = new byte[dataLen];
                stream.Read(readMsgData, 0, dataLen);
                MemoryStream memoryStream = new MemoryStream(decryptByteArray(readMsgData));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Position = 0;
                message = (MessageModel)binaryFormatter.Deserialize(memoryStream);

                serverName = message.playerServer;
                clientName = message.playerClient;
                UpdateNames(clientName, serverName);

                UpdateStatus("Mache deinen Zug!");
                UnlockButtons();
                UnlockChat();

                readMessages();
            }
            catch (SocketException)
            {
                UpdateStatus("Fehler: Verbindung fehlgeschlagen!");
            }
            catch (IOException)
            {
                UpdateStatus("Fehler: Unbekanntes Datenformat empfangen! - Ist der Schlüssel richtig?");
            }
        }

        private int determineWinner(MessageModel.Move first, MessageModel.Move second)
        {
            // Unentschieden
            if (first == second)
            {
                return 0;
            }
            if (first == MessageModel.Move.Scissors)
            {
                if (second == MessageModel.Move.Paper)
                {
                    return 1;
                }
                if (second == MessageModel.Move.Rock)
                {
                    return 2;
                }
            }
            if (first == MessageModel.Move.Rock)
            {
                if (second == MessageModel.Move.Paper)
                {
                    return 2;
                }
                if (second == MessageModel.Move.Scissors)
                {
                    return 1;
                }
            }
            if (first == MessageModel.Move.Paper)
            {
                if (second == MessageModel.Move.Scissors)
                {
                    return 2;
                }
                if (second == MessageModel.Move.Rock)
                {
                    return 1;
                }
            }
            return -1;
        }

        private string getMoveText(MessageModel.Move move)
        {
            if (move == MessageModel.Move.Paper)
            {
                return "Papier";
            }
            else if (move == MessageModel.Move.Rock)
            {
                return "Stein";
            }
            else if (move == MessageModel.Move.Scissors)
            {
                return "Schere";
            }
            else
            {
                return "Fehler";
            }
        }

        private byte[] encryptByteArray(byte[] clearBytes)
        {
            // Danke an https://stackoverflow.com/a/42834299
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                cryptoStream.Close();
            }
            catch (CryptographicException)
            {
                UpdateStatus("Fehler bei der Verschlüsselung!");
            }

            return memoryStream.ToArray();
        }

        private byte[] decryptByteArray(byte[] cipherBytes)
        {
            // Danke an https://stackoverflow.com/a/42834299
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.Close();
            }
            catch (CryptographicException)
            {
                UpdateStatus("Fehler bei der Entschlüsselung!");
            }

            return memoryStream.ToArray();
        }
    }
}
