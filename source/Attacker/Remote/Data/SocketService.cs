using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Remote.Data
{
    public class SocketService
    {
        private TcpListener? _tcpListener;

        public void Start(string ip, int port)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(AsyncCallback, _tcpListener);
            Console.WriteLine("���A���w�ҰʡA�}�l��ť");
        }

        /// <summary>
        /// �D�P�B�����Ȥ�ݳs�u
        /// </summary>
        private void AsyncCallback(IAsyncResult asyncResult)
        {
            if (asyncResult.AsyncState is not TcpListener listener) return;
            TcpClient client = listener.EndAcceptTcpClient(asyncResult);
            Console.WriteLine("�Ȥ�ݤw�s�u");

            // �}�l�����Ȥ�ݸ��
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            var read = stream.Read(buffer, 0, buffer.Length);
            var receive = Encoding.UTF8.GetString(buffer, 0, read);
            Console.WriteLine($"������Ȥ�ݰT���G{receive}");

            // �}�l�ǰe��Ƶ��Ȥ��
            string message = "�١A�ڬO���A��";
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            stream.Write(bytes, 0, bytes.Length);
            Console.WriteLine($"�o�e���Ȥ�ݪ��T��: {message}");

            // �����s�u
            stream.Close();
            client.Close();

            // �����U�@�ӰT��
            listener.BeginAcceptTcpClient(AsyncCallback, listener);
        }

        public void Close()
        {
            _tcpListener?.Stop();
        }
    }
}