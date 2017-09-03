// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System.Linq;

    public class Move
    {
        public Move(string start, string end, Player player)
        {
            this.Player = player;
            this.GameBoard = this.Player.GameBoard;
            this.StartCoordinates = Coordinates.FromString(start);
            this.EndCoordinates = Coordinates.FromString(end);
        }

        private Coordinates StartCoordinates { get; }

        private Coordinates EndCoordinates { get; }

        private GameBoard GameBoard { get; }

        private Player Player { get; }

        public string Display()
        {
            var output = string.Empty;
            output += this.StartCoordinates.ToSAN();
            output += " -> ";
            output += this.EndCoordinates.ToSAN();
            return output;
        }

        public void Make()
        {
            if (!this.IsValid())
            {
                throw new InvalidMoveException();
            }

            var startPiece = this.GameBoard.GetPanel(this.StartCoordinates).Piece;
            startPiece.MoveTo(this.EndCoordinates);
            this.GameBoard.Game.WhoseMove = this.Player.Opponent;

            // checks for castling
            switch (startPiece.Name)
            {
                case "King":
                    if (this.StartCoordinates.Column == 4)
                    {
                        this.Player.KingMoved = true;
                        break;
                    }
                    else break;
                case "Rook":
                    switch (this.StartCoordinates.Column)
                    {
                        case 0:
                            if (this.StartCoordinates.Row == this.Player.BaseRow)
                            {
                                this.Player.LeftRookMoved = true;
                                break;
                            }
                            else break;
                        case 7:
                            if (this.StartCoordinates.Row == this.Player.BaseRow)
                            {
                                this.Player.RightRookMoved = true;
                                break;
                            }
                            else break;
                    }

                break;
            }

            // castling
            // left = queenSide
            if (startPiece.Name == "King" && this.StartCoordinates.Column == 4 && this.EndCoordinates.Column == 2
                && this.StartCoordinates.Row == this.Player.BaseRow && this.EndCoordinates.Row == this.Player.BaseRow)
            {
                var p = this.GameBoard.GetPanel(this.Player.BaseRow, 0);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(this.GameBoard.GetPanel(this.Player.BaseRow, 3).Coordinates);
                this.Player.Castled = true;
            }

            // left = kingSide
            if (startPiece.Name == "King" && this.StartCoordinates.Column == 4 && this.EndCoordinates.Column == 6
                && this.StartCoordinates.Row == this.Player.BaseRow && this.EndCoordinates.Row == this.Player.BaseRow)
            {
                var p = this.GameBoard.GetPanel(this.Player.BaseRow, 7);
                var rook = p?.Piece ?? throw new InvalidMoveException();
                rook.MoveTo(this.GameBoard.GetPanel(this.Player.BaseRow, 5).Coordinates);
                this.Player.Castled = true;
            }

            // en passant
            if (startPiece.Name == "Pawn" && this.GameBoard.EnPassantCoordinates != null)
            {
                if (this.EndCoordinates.Row == this.GameBoard.EnPassantCoordinates.Row
                    && this.EndCoordinates.Column == this.GameBoard.EnPassantCoordinates.Column)
                {
                    if (this.Player.Color == Color.White)
                    {
                        this.GameBoard.GetPanel(this.EndCoordinates.Row + 1, this.EndCoordinates.Column).Piece
                            ?.Remove();
                    }
                    else
                    {
                        this.GameBoard.GetPanel(this.EndCoordinates.Row - 1, this.EndCoordinates.Column).Piece
                            ?.Remove();
                    }
                }
            }

            if (startPiece.Name == "Pawn" && this.StartCoordinates.Row == 6 && this.EndCoordinates.Row == 4)
            {
                this.GameBoard.EnPassantCoordinates =
                    this.GameBoard.GetPanel(this.EndCoordinates.Row + 1, this.EndCoordinates.Column).Coordinates;
            }
            else if (startPiece.Name == "Pawn" && this.StartCoordinates.Row == 1 && this.EndCoordinates.Row == 3)
            {
                this.GameBoard.EnPassantCoordinates = this.GameBoard
                    .GetPanel(this.EndCoordinates.Row - 1, this.EndCoordinates.Column).Coordinates;
            }
            else
            {
                this.GameBoard.EnPassantCoordinates = null;
            }

            if (this.Player.Color == Color.Black)
            {
                this.GameBoard.Game.MoveCount++;
            }

            if (startPiece.Name == "Pawn" || this.GameBoard.GetPanel(this.EndCoordinates).IsPiece)
            {
                this.GameBoard.Game.FiftyMovesCount = 0;
            }
            else
            {
                this.GameBoard.Game.FiftyMovesCount++;
            }
        }

        private bool IsValid()
        {
            if (this.Player != this.GameBoard.Game.WhoseMove)
            {
                return false;
            }

            if (!this.GameBoard.GetPanel(this.StartCoordinates)?.IsPiece ?? false)
            {
                return false;
            }

            var startPiece = this.GameBoard.GetPanel(this.StartCoordinates).Piece;
            if (!this.Player.Pieces.Contains(startPiece))
            {
                return false;
            }

            var moves = startPiece.GetAvailableMoves();
            var endCoordsCheck = moves.Any(c => c.Row == this.EndCoordinates.Row && c.Column == this.EndCoordinates.Column);
            return this.StartCoordinates.Valid && this.EndCoordinates.Valid && endCoordsCheck;
        }
    }
}
