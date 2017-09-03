// ReSharper disable StyleCop.SA1600
// ReSharper disable StyleCop.SA1402

namespace Chess
{
    using System.Collections.Generic;
    using System.Linq;

    public class Rook : Piece
    {
        public Rook(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "Rook";
            this.Character = 'R';
            this.Place(coords);
            this.SetPlayer();
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            return this.Cross();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "Bishop";
            this.Character = 'B';
            this.Place(coords);
            this.SetPlayer();
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            return this.Diagonal();
        }
    }

    public class Queen : Piece
    {
        public Queen(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "Queen";
            this.Character = 'Q';
            this.Place(coords);
            this.SetPlayer();
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            return this.Cross().Concat(this.Diagonal()).ToList();
        }
    }

    public class Knight : Piece
    {
        public Knight(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "Knight";
            this.Character = 'N';
            this.Place(coords);
            this.SetPlayer();
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            var board = this.GameBoard;
            var r = this.Coordinates.Row;
            var c = this.Coordinates.Column;
            var output = new List<Coordinates>();
            var possible = new List<Coordinates>()
            {
                board.GetPanel(r - 2, c + 1)?.Coordinates,
                board.GetPanel(r - 1, c + 2)?.Coordinates,
                board.GetPanel(r + 1, c + 2)?.Coordinates,
                board.GetPanel(r + 2, c + 1)?.Coordinates,
                board.GetPanel(r + 2, c - 1)?.Coordinates,
                board.GetPanel(r + 1, c - 2)?.Coordinates,
                board.GetPanel(r - 1, c - 2)?.Coordinates,
                board.GetPanel(r - 2, c - 1)?.Coordinates
            };

            foreach (var coords in possible)
            {
                if (!(coords?.Valid ?? false))
                {
                    continue;
                }

                var pan = board.GetPanel(coords);
                if (!pan.IsPiece)
                {
                    output.Add(pan.Coordinates);
                }
                else if (this.IsOpponent(pan))
                {
                    output.Add(pan.Coordinates);
                }
            }

            return output;
        }
    }

    public class Pawn : Piece
    {
        public Pawn(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "Pawn";
            this.Character = 'P';
            this.Place(coords);
            this.SetPlayer();
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            var output = new List<Coordinates>();
            if (this.Color == Color.White)
            {
                var up = this.GameBoard.GetPanel(this.Coordinates.Row - 1, this.Coordinates.Column);
                if ((up?.Coordinates?.Valid ?? false) && !up.IsPiece)
                {
                    output.Add(up.Coordinates);
                }

                var upL = this.GameBoard.GetPanel(this.Coordinates.Row - 1, this.Coordinates.Column - 1);
                if ((upL?.Coordinates?.Valid ?? false) && (this.IsOpponent(upL) || upL.Coordinates == this.GameBoard.EnPassantCoordinates))
                {
                    output.Add(upL.Coordinates);
                }

                var upR = this.GameBoard.GetPanel(this.Coordinates.Row - 1, this.Coordinates.Column + 1);
                if ((upR?.Coordinates?.Valid ?? false) && (this.IsOpponent(upR) || upR.Coordinates == this.GameBoard.EnPassantCoordinates))
                {
                    output.Add(upR.Coordinates);
                }

                if (this.Coordinates.Row != 6)
                {
                    return output;
                }

                var up2 = this.GameBoard.GetPanel(this.Coordinates.Row - 2, this.Coordinates.Column);
                if (up2.Coordinates.Valid && !up2.IsPiece)
                {
                    output.Add(up2.Coordinates);
                }
            }
            else
            {
                var down = this.GameBoard.GetPanel(this.Coordinates.Row + 1, this.Coordinates.Column);
                if ((down?.Coordinates?.Valid ?? false) && !down.IsPiece)
                {
                    output.Add(down.Coordinates);
                }

                var downL = this.GameBoard.GetPanel(this.Coordinates.Row + 1, this.Coordinates.Column - 1);
                if ((downL?.Coordinates?.Valid ?? false) && (this.IsOpponent(downL) || downL.Coordinates == this.GameBoard.EnPassantCoordinates))
                {
                    output.Add(downL.Coordinates);
                }

                var downR = this.GameBoard.GetPanel(this.Coordinates.Row + 1, this.Coordinates.Column + 1);
                if ((downR?.Coordinates?.Valid ?? false) && (this.IsOpponent(downR) || downR.Coordinates == this.GameBoard.EnPassantCoordinates))
                {
                    output.Add(downR.Coordinates);
                }

                if (this.Coordinates.Row != 1)
                {
                    return output;
                }

                var down2 = this.GameBoard.GetPanel(this.Coordinates.Row + 2, this.Coordinates.Column);
                if (down2.Coordinates.Valid && !down2.IsPiece)
                {
                    output.Add(down2.Coordinates);
                }
            }

            return output;
        }
    }

    public class King : Piece
    {
        public King(GameBoard board, Coordinates coords, Color color)
        {
            this.GameBoard = board;
            this.Color = color;
            this.Coordinates = coords;
            this.Name = "King";
            this.Character = 'K';
            this.Place(coords);
            this.SetPlayer();
        }

        public bool IsInCheck
        {
            get
            {
                var pan = this.GameBoard.GetPanel(this.Coordinates);
                var opponentPanels = this.GetOpponentPanels();
                return opponentPanels.Contains(pan);
            }
        }

        public override List<Coordinates> GetAvailableMoves()
        {
            var output = new List<Coordinates>();
            var possible = this.Cross(1).Concat(this.Diagonal(1)).ToList();
            foreach (var coords in possible)
            {
                if (!coords.Valid)
                {
                    continue;
                }

                var pan = this.GameBoard.GetPanel(coords);
                var opponentPanels = this.GetOpponentPanels();
                if (!opponentPanels.Contains(pan))
                {
                    output.Add(pan.Coordinates);
                }
            }

            if (this.Player.CanLeftCastle)
            {
                output.Add(this.GameBoard.GetPanel(this.Player.BaseRow, 2).Coordinates);
            }

            if (this.Player.CanRightCastle)
            {
                output.Add(this.GameBoard.GetPanel(this.Player.BaseRow, 6).Coordinates);
            }

            return output;
        }
    }
}
