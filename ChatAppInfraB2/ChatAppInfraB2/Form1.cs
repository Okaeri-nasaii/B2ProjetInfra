using System;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatAppInfraB2
{
    public partial class Form1 : Form
    {
        public static string portServ = "8888";
        public static string ipServ = "127.0.0.1";
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public Form1()
        {
            InitializeComponent();
            // Load(@"..\..\.env");
        }
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=');

                if (parts.Length != 2)
                    continue;
                if (parts[0] == "IP_ADDRESS")
                {
                    
                    if (parts[1] != "")
                        ipServ = parts[1];
                }
                if (parts[0] == "PORT")
                {
                    Console.WriteLine(parts[0], parts[1]);
                    if (parts[1] != "")
                        portServ = parts[1];
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = inStream.Length;
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readData = "" + returndata;
                msg();
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + readData;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            readData = "Connected to Chat Server ...";
            msg();
            clientSocket.Connect(ipServ, Convert.ToInt32(portServ));
            serverStream = clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox3.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            msg();
        }
    }
}
