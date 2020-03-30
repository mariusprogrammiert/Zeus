using System;
using System.Windows.Forms;

namespace Zeus
{
    public partial class SettingsView : Form
    {
        GameView gameView;
        public SettingsView()
        {
            InitializeComponent();
        }

        private void hideForm()
        {
            if (String.IsNullOrEmpty(tbPlayerName.Text))
            {
                tbPlayerName.Text = "Anonymous";
            }
            Hide();
        }

        private void GameView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void bStartServer_Click(object sender, EventArgs e)
        {
            hideForm();
            gameView = new GameView("server", tbPlayerName.Text, tbKey.Text, Convert.ToInt32(tbPort.Text), Convert.ToInt32(tbRounds.Value), null);
            gameView.FormClosed += GameView_FormClosed;
            gameView.Show();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            hideForm();
            gameView = new GameView("client", tbPlayerName.Text, tbKey.Text, Convert.ToInt32(tbPort.Text), -1, tbHost.Text);
            gameView.FormClosed += GameView_FormClosed;
            gameView.Show();
        }

        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
