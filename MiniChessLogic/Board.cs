using MiniChessLogic.Pieces;

namespace MiniChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[6, 5];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }
        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }
        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }
        private void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);

            this[5, 0] = new Rook(Player.White);
            this[5, 1] = new Knight(Player.White);
            this[5, 2] = new Bishop(Player.White);
            this[5, 3] = new Queen(Player.White);
            this[5, 4] = new King(Player.White);

            for (int c = 0; c < 5; c++)
            {
                this[1, c] = new Pawn(Player.Black);
                this[4, c] = new Pawn(Player.White);
            }
        }
        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 6 && pos.Column >= 0 && pos.Column < 5;
        }
        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
