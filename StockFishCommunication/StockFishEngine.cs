using System.Diagnostics;

namespace StockFishCommunication
{
    public class StockFishEngine : IDisposable
    {
        private Process stockfish;

        public StockFishEngine()
        {
            stockfish = new Process();
            stockfish.StartInfo.FileName = @"D:\Projects\MiniChess5x6_C#_Blazor_WebApp\stockfish\stockfish-windows-x86-64-sse41-popcnt.exe";
            stockfish.StartInfo.UseShellExecute = false;
            stockfish.StartInfo.RedirectStandardInput = true;
            stockfish.StartInfo.RedirectStandardOutput = true;
            stockfish.StartInfo.CreateNoWindow = true;

            stockfish.Start();
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
