using System;
using System.Windows.Forms;
using BISFT;

namespace BISFT
{
    public partial class ChatbotForm : Form
    {
        private ChatbotService chatbot;

        public ChatbotForm()
        {
            InitializeComponent();
            chatbot = new ChatbotService();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
                e.SuppressKeyPress = true; // prevent ding sound
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                txtOutput.AppendText("🧑‍💻 You: " + userInput + Environment.NewLine);

                string reply = await chatbot.AskChatbotAsync(userInput);

                txtOutput.AppendText("🤖 Bot: " + reply + Environment.NewLine);
                txtInput.Clear();
            }
        }


        private void ChatbotForm_Load(object sender, EventArgs e)
        {

        }
    }
}
