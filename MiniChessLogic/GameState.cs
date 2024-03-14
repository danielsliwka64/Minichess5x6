using MiniChessLogic.Moves;
using MiniChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        //public Result Result { get; private set; } = null;
        private int noCaptureOrPawnMoves = 0;
        private string stateString;
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }
        public IEnumerable<Move> LegalMoveForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates;
        }
        public void MakeMove(Move move)
        {
            //Board.SetPawnSkipPosition(CurrentPlayer, null);
            bool captureOrPawn = move.Execute(Board);
            if (captureOrPawn)
            {
                noCaptureOrPawnMoves = 0;
                //stateHistory.Clear();
            }
            else
            {
                noCaptureOrPawnMoves++;
            }
            CurrentPlayer = CurrentPlayer.Opponent();
            //UpdateStateString();
            //CheckForGameOver();
        }
    }
}
