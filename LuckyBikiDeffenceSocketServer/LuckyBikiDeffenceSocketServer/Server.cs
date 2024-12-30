using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuckyBikiDeffenceSocketServer
{
    class Server
    {
        TcpListener listener;
        Queue<TcpClient> playerQueue = new Queue<TcpClient>();
        
        public int port = 8081;

        public void Start()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            
            listener = new TcpListener(endPoint);
            listener.Start();

            Console.WriteLine("server is listening . . .");
            while (true)
            {
                //대기중인 서버 소켓이 Aceept()를 실행하고, 서버는 클라이언트와 연결이 성공된 소켓을 하나 더 만든다.
                TcpClient client = listener.AcceptTcpClient();
                playerQueue.Enqueue(client);

                //플레이어 두 명을 매칭
                if(playerQueue.Count >= 2)
                {
                    MatchPlayer();
                }
            }
        }

        public void MatchPlayer()
        {
            TcpClient client1 = playerQueue.Dequeue();
            TcpClient client2 = playerQueue.Dequeue();

            GameManager gameManager = new GameManager(client1, client2);

            Thread thread = new Thread(() => gameManager.EnterRoom());

            thread.Start();
        }

        public void StartGame(TcpClient player, TcpClient partner)
        {
            // 네트워크 스트림 가져오기
            NetworkStream playerStream = player.GetStream();
            NetworkStream partnerStream = partner.GetStream();

            while(true)
            {
                break;
            }

            //gameFinish
            player.Close();
            partner.Close();
        }

        public void Close()
        {
            
        }
    }
}
