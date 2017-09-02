// ReSharper disable StyleCop.SA1600
// ReSharper disable StyleCop.SA1402

namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Pieces
    {
        public string Name { get; protected set; }

        public Color Color { get; protected set; }

        protected Coordinates Coordinates { get; set; }

        protected char Character { private get; set; }

        protected GameBoard GameBoard { get; set; }

        protected Player Player { get; private set; }

        public char Show()
        {
            var r = this.Character;
                 switch (this.Color)
                 {
                     case Color.White:
                         r = char.ToUpper(r);
                         break;
                     case Color.Black:
                         r = char.ToLower(r);
                         break;
                     default:
                         // ReSharper disable once UnthrowableException
                         throw new Exception();
                 }

            return r;
        }

        public List<Panel> GetOpponentPanels()
        {
            var opponentPanels = new List<Panel>();
            foreach (var piece in this.Player.Opponent.Pieces)
            {
                var pieceMoves = piece.Name == "King" ? piece.Cross(1).Concat(piece.Diagonal(1)).ToList() : piece.GetAvailableMoves();
                foreach (var coords in pieceMoves)
                {
                    opponentPanels.Add(this.GameBoard.GetPanel(coords));
                }
            }

            return opponentPanels.Distinct().ToList();
        }

        public void MoveTo(Coordinates coords)
        {
            this.Remove();
            this.Place(coords);
        }

        public abstract List<Coordinates> GetAvailableMoves();

        protected List<Coordinates> Cross(int radius = 7)
        {
            var output = new List<Coordinates>();
            for (var i = 1; i <= radius; i++)
            {
                var r = this.Coordinates.Row - i;
                var c = this.Coordinates.Column;
                var pan = this.GameBoard.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = this.Coordinates.Row + i;
                var c = this.Coordinates.Column;
                var pan = this.GameBoard.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = this.Coordinates.Row;
                var c = this.Coordinates.Column - i;
                var pan = this.GameBoard.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = this.Coordinates.Row;
                var c = this.Coordinates.Column + i;
                var pan = this.GameBoard.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return output;
        }

        protected List<Coordinates> Diagonal(int radius = 7)
        {
            var board = this.GameBoard;
            var coords = this.Coordinates;
            var output = new List<Coordinates>();
            for (var i = 1; i <= radius; i++)
            {
                var r = coords.Row - i;
                var c = coords.Column - i;
                var pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = coords.Row - i;
                var c = coords.Column + i;
                var pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = coords.Row + i;
                var c = coords.Column + i;
                var pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (var i = 1; i <= radius; i++)
            {
                var r = coords.Row + i;
                var c = coords.Column - i;
                var pan = board.GetPanel(r, c);
                if (pan?.Coordinates?.Valid ?? false)
                {
                    if (!pan.IsPiece)
                    {
                        output.Add(pan.Coordinates);
                        continue;
                    }

                    if (this.IsOpponent(pan))
                    {
                        output.Add(pan.Coordinates);
                        break;
                    }

                    if (!this.IsOpponent(pan))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return output;
        }

        protected void Place(Coordinates coords)
        {
            var panel = this.GameBoard.GetPanel(coords);
            panel.Piece = this;
            this.Coordinates = panel.Coordinates;
        }

        protected bool IsOpponent(Panel pan)
        {
            if (!pan?.IsPiece ?? false)
            {
                return false;
            }

            return pan != null && pan.Piece.Color != this.Color;
        }

        protected void SetPlayer()
        {
            if (this.GameBoard.Game.Player1.Color == this.Color)
            {
                this.Player = this.GameBoard.Game.Player1;
            }
            else if (this.GameBoard.Game.Player2.Color == this.Color)
            {
                this.Player = this.GameBoard.Game.Player2;
            }
        }

        private void Remove()
        {
            var panel = this.GameBoard.GetPanel(this.Coordinates);
            panel.Piece = null;
        }
    }

    public class Rook : Pieces
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

    public class Bishop : Pieces
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

    public class Queen : Pieces
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

    public class Knight : Pieces
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

    public class Pawn : Pieces
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
                if ((upL?.Coordinates?.Valid ?? false) && this.IsOpponent(upL))
                {
                    output.Add(upL.Coordinates);
                }

                var upR = this.GameBoard.GetPanel(this.Coordinates.Row - 1, this.Coordinates.Column + 1);
                if ((upR?.Coordinates?.Valid ?? false) && this.IsOpponent(upR))
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
                if ((downL?.Coordinates?.Valid ?? false) && this.IsOpponent(downL))
                {
                    output.Add(downL.Coordinates);
                }

                var downR = this.GameBoard.GetPanel(this.Coordinates.Row + 1, this.Coordinates.Column + 1);
                if ((downR?.Coordinates?.Valid ?? false) && this.IsOpponent(downR))
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

            // en passant?
            return output;
        }
    }

    public class King : Pieces
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
