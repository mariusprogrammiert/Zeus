using System;

namespace Zeus
{
    [Serializable]
    public class MessageModel
    {
        public enum Type { MyName, MyMove, RoundOver, GameOver, ChatMessage };
        public enum Move { None, Rock, Paper, Scissors };

        public Type messageType { get; set; }
        public string playerServer { get; set; }
        public string playerClient { get; set; }
        public int pointsServer { get; set; }
        public int pointsClient { get; set; }
        public Move moveServer { get; set; }
        public Move moveClient { get; set; }
        public string roundWinner { get; set; }
        public string chatMessage { get; set; }

        public MessageModel()
        {

        }
        public MessageModel(Type messageType, string playerServer, string playerClient, int pointsServer, int pointsClient, Move moveServer, Move moveClient, string roundWinner)
        {
            this.messageType = messageType;
            this.playerServer = playerServer;
            this.playerClient = playerClient;
            this.pointsServer = pointsServer;
            this.pointsClient = pointsClient;
            this.moveServer = moveServer;
            this.moveClient = moveClient;
            this.roundWinner = roundWinner;
        }

        public void setMoves(Move moveServer, Move moveClient)
        {
            messageType = Type.MyMove;
            this.moveServer = moveServer;
            this.moveClient = moveClient;
        }

        public void setNames(string playerServer, string playerClient)
        {
            messageType = Type.MyName;
            this.playerServer = playerServer;
            this.playerClient = playerClient;
        }

        public void setRoundOver(int pointsServer, int pointsClient, Move moveServer, Move moveClient, string roundWinner)
        {
            messageType = Type.RoundOver;
            this.moveServer = moveServer;
            this.moveClient = moveClient;
            this.pointsServer = pointsServer;
            this.pointsClient = pointsClient;
            this.roundWinner = roundWinner;
        }

        public void setGameOver(string roundWinner)
        {
            messageType = Type.GameOver;
            this.roundWinner = roundWinner;
        }

        public void setChatMessage(string message)
        {
            messageType = Type.ChatMessage;
            this.chatMessage = message;
        }
    }
}
