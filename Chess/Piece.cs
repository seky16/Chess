// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Piece
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

        public void Remove()
        {
            var panel = this.GameBoard.GetPanel(this.Coordinates);
            panel.Piece = null;
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
    }
}