using System;
using System.Windows.Forms;

namespace Zeus
{
    public partial class GameView : Form
    {
        LogicController logic;
        public GameView(string connectionMode, string playerName, string key, int port, int rounds, string host)
        {
            InitializeComponent();
            logic = new LogicController(connectionMode, playerName, key, port, rounds, host);

            logic.UpdateStatus += UpdateStatus;
            logic.UpdateOpponentText += UpdateOpponentText;
            logic.NewChatMessage += NewChatMessage;
            logic.UpdateNames += UpdateNames;
            logic.UpdatePoints += UpdatePoints;
            logic.LockButtons += LockButtons;
            logic.UnlockButtons += UnlockButtons;
            logic.LockChat += LockChat;
            logic.UnlockChat += UnlockChat;

            logic.initController();
        }

        private void LockChat()
        {
            tbMessage.BeginInvoke(new Action(() =>
            {
                tbMessage.Enabled = false;
            }));
        }

        private void UnlockChat()
        {
            tbMessage.BeginInvoke(new Action(() =>
            {
                tbMessage.Enabled = true;
            }));
        }

        private void NewChatMessage(string newMessage)
        {
            lbChatMessages.BeginInvoke(new Action(() =>
            {
                lbChatMessages.Items.Add(newMessage);

                int visibleItems = lbChatMessages.ClientSize.Height / lbChatMessages.ItemHeight;
                lbChatMessages.TopIndex = Math.Max(lbChatMessages.Items.Count - visibleItems + 1, 0);
            }));
        }

        private void LockButtons()
        {
            bScissors.BeginInvoke(new Action(() =>
            {
                bScissors.Enabled = false;
            }));

            bRock.BeginInvoke(new Action(() =>
            {
                bRock.Enabled = false;
            }));

            bPaper.BeginInvoke(new Action(() =>
            {
                bPaper.Enabled = false;
            }));
        }

        private void UnlockButtons()
        {
            bScissors.BeginInvoke(new Action(() =>
            {
                bScissors.Enabled = true;
            }));

            bRock.BeginInvoke(new Action(() =>
            {
                bRock.Enabled = true;
            }));

            bPaper.BeginInvoke(new Action(() =>
            {
                bPaper.Enabled = true;
            }));
        }

        private void UpdateStatus(string newStatus)
        {
            lStatus.BeginInvoke(new Action(() =>
            {
                lStatus.Text = newStatus;
            }));
        }

        private void UpdateOpponentText(string newText)
        {
            lOpponent.BeginInvoke(new Action(() =>
            {
                lOpponent.Text = newText;
            }));
        }

        private void UpdateNames(string playerLocal, string playerRemote)
        {
            gbPlayer.BeginInvoke(new Action(() =>
            {
                gbPlayer.Text = playerLocal;
            }));

            gbOpponent.BeginInvoke(new Action(() =>
            {
                gbOpponent.Text = playerRemote;
            }));
        }

        private void UpdatePoints(int pointsLocal, int pointsRemote)
        {
            lPointsPlayer.BeginInvoke(new Action(() =>
            {
                lPointsPlayer.Text = "Punkte: " + Convert.ToString(pointsLocal);
            }));

            lPointsOpponent.BeginInvoke(new Action(() =>
            {
                lPointsOpponent.Text = "Punkte: " + Convert.ToString(pointsRemote);
            }));
        }

        private void bScissors_Click(object sender, EventArgs e)
        {
            logic.clickedScissors();
        }

        private void bRock_Click(object sender, EventArgs e)
        {
            logic.clickedRock();
        }

        private void bPaper_Click(object sender, EventArgs e)
        {
            logic.clickedPaper();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                logic.typedChatMessage(tbMessage.Text);
                tbMessage.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
