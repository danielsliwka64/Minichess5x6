﻿namespace StockFishCommunication
{
    public class StockFishService
    {
        public static string bestMove { get; set; }
        public static string examplePosition { get; set; }

        //"rnbqk/ppppp/5/PPPPP/RNBQK w Qq - 0 1";
        //r1bqkb1r/pppppppp/2n2n2/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3"; // Przykładowa pozycja 

        public static async Task OnInitializedAsync()
        {
            using (var stockfish = new StockFishEngine())
            {
                string currentFen = examplePosition;
                bestMove = stockfish.GetBestMove(currentFen);
            
            }
        }

        static async Task Main(string[] args)
        {
            await Console.Out.WriteLineAsync($"Start position: {examplePosition}");
            await Console.Out.WriteLineAsync("----------Start-----------");
            await OnInitializedAsync();
            await Console.Out.WriteLineAsync($"Najlepszy ruch: {bestMove}");
            await Console.Out.WriteLineAsync("-------------End-----------");
        }
    }
}
