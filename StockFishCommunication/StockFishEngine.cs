using System.Diagnostics;
using System.Xml.Linq;

namespace StockFishCommunication
{
    public class StockFishEngine : IDisposable
    {
        private Process stockfish;

        public StockFishEngine()
        {
            stockfish = new Process();
            stockfish.StartInfo.FileName = @"D:\Projects\MiniChess5x6_C#_Blazor_WebApp\fairy-stockfish\fairy-stockfish-largeboard_x86-64.exe";
            stockfish.StartInfo.UseShellExecute = false;
            stockfish.StartInfo.RedirectStandardInput = true;
            stockfish.StartInfo.RedirectStandardOutput = true;
            stockfish.StartInfo.CreateNoWindow = true;

            stockfish.Start();
            SendCommand("uci");
            SendCommand("setoption name UCI_Variant value gardner");
            SendCommand("isready");
            WaitForReady();
        }
        private void WaitForReady()
        {
            string output;
            while ((output = ReadOutput()) != null)
            {
                if (output == "readyok")
                {
                    break;
                }
            }
        }
        public void SendCommand(string command)
        {
            if (!stockfish.HasExited)
            {
                stockfish.StandardInput.WriteLine(command);
                stockfish.StandardInput.Flush();
            }
        }

        public string ReadOutput()
        {
            if (!stockfish.HasExited)
            {
                return stockfish.StandardOutput.ReadLine();
            }
            return null;
        }

        public string GetBestMove(string fen)
        {
            SendCommand($"position fen {fen}");
            SendCommand("go movetime 1000");  // Time limit 1 s
            string output;
            while ((output = ReadOutput()) != null)
            {
                if (output.StartsWith("bestmove"))
                {
                    return output.Split(' ')[1];
                }
            }
            return null;
        }

        public void Dispose()
        {
            if (!stockfish.HasExited)
            {
                SendCommand("quit");
                stockfish.WaitForExit();
            }
            stockfish.Dispose();
        }
    }
}
