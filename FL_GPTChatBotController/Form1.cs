using System.Net.Sockets;
using System.Text;

namespace FL_GPTChatBotController
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        public StreamWriter streamWriter;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();
        }

        private void customSpeakRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (customSpeakRadioButton.Checked)
            {
                groupBox1.Enabled = true;
                languageSelectComboBox.Enabled = false;
                streamWriter.WriteLine("{\"type\":\"recognizer\",\"message\":\"stop\"}");
                streamWriter.Flush();

            } else
            {
                groupBox1.Enabled = false;
                languageSelectComboBox.Enabled = true;
                streamWriter.WriteLine("{\"type\":\"recognizer\",\"message\":\"start\"}");
                streamWriter.Flush();
                languageSelectComboBox.SelectedIndex = 0;
                streamWriter.WriteLine("{\"type\":\"setLanguage\",\"message\":\"" + languageSelectComboBox.Items[languageSelectComboBox.SelectedIndex] + "\"}");
                streamWriter.Flush();
            }
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 3169);
                var netStream = client.GetStream();
                streamWriter = new StreamWriter(netStream, Encoding.ASCII);
                MessageBox.Show("Connected to server");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        void Test(object sender, EventArgs e)
        {
            MessageBox.Show("{\"type\":\"speech\",\"message\":\"" + textBox1.Text + "\"}");
        }

        private void SendTextToVoice(object sender, EventArgs e)
        {
            if (client == null) return;
            try
            {
                streamWriter.WriteLine("{\"type\":\"speech\",\"message\":\"" + textBox1.Text + "\"}");
                streamWriter.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void SetConversationLanguage(object sender, EventArgs e)
        {
            if (client == null) return;
            try
            {
                streamWriter.WriteLine("{\"type\":\"setLanguage\",\"message\":\"" + languageSelectComboBox.Items[languageSelectComboBox.SelectedIndex] + "\"}");
                streamWriter.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}
