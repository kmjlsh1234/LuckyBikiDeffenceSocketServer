using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace LuckyBikiDeffenceSocketServer
{
    class GameManager
    {
        public GameData gameData;

        public TcpClient player1;
        public TcpClient player2;

        public NetworkStream player1Stream;
        public NetworkStream player2Stream;

        private byte[] buffer = new byte[1024];  // 데이터 받을 버퍼
        private int bytesRead;

        public bool isGameFinish = false;

        public GameManager(TcpClient player1, TcpClient player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.player1Stream = player1.GetStream();
            this.player2Stream = player2.GetStream();
        }

        public void EnterRoom()
        {
            //TODO : PLAYER1,2 PROFILE 가져오기
            //BROADCAST : 입장 완료 메시지 보내기
            BroadCastMessage(Constants.ENTER_ROOM);

        }

        public void GameStart()
        {
            //1초마다 실행되는 타이머
            Timer timer = new Timer(1000);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            while (!isGameFinish)
            {
                //player1 요청 처리
                bytesRead = player1Stream.Read(buffer, 0, buffer.Length);
                if(bytesRead > 0)
                {
                    string player1Message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"player1 message : {player1Message}");
                    HandleRequest(player1Message);
                }

                //player2 요청 처리
                bytesRead = player2Stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string player2Message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"player2 message : {player2Message}");
                    HandleRequest(player2Message);
                }


            }
        }

        public void HandleRequest(string message)
        {
            switch (message)
            {
                //킬
                //보스 킬
                //영웅 소환
                //영웅 이동
                //영웅 합체
                //게임 종료
                default:
                    break;
            }
        }

        public void BroadCastMessage(string message)
        {
            // 서버가 클라이언트에게 주는 데이터
            byte[] data = Encoding.UTF8.GetBytes(message);

            if (player1.Connected)
            {
                player1Stream.Write(data, 0, data.Length);
            }
            else
            {
                //게임 종료 메시지 보내기
                player1.Close();
            }
            player2Stream.Write(data, 0, data.Length);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!isGameFinish)
            {
                string message = Constants.TIMER + Constants.SPLITTER + gameData.timer;
                BroadCastMessage(message);
            }
        }
    }
}
