// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameBoard
    {
        public GameBoard(Game game)
        {
            this.Game = game;
            this.Panels = new List<Panel>();
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    this.Panels.Add(new Panel(i, j));
                }
            }

            this.EnPassantCoordinates = null;
        }

        // ReSharper disable StyleCop.SA1305
        public GameBoard(string fen)
        {
            this.Game = new Game(this);
            this.Panels = new List<Panel>();
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    this.Panels.Add(new Panel(i, j));
                }
            }

            this.EnPassantCoordinates = null;

            var split = fen.Split(" ");
            var board = split[0] ?? throw new InvalidFenException(1);
            var whoseMove = split[1] ?? throw new InvalidFenException(2);
            var castling = split[2] ?? throw new InvalidFenException(3);
            var enPassant = split[3] ?? throw new InvalidFenException(4);
            var fiftyMoves = split[4] ?? throw new InvalidFenException(5);
            var moveCount = split[5] ?? throw new InvalidFenException(6);

            if (!int.TryParse(moveCount, out var moveCountResult))
            {
                throw new InvalidFenException(6);
            }

            this.Game.MoveCount = moveCountResult;

            if (!int.TryParse(fiftyMoves, out var fiftyMovesResult))
            {
                throw new InvalidFenException(5);
            }

            this.Game.FiftyMovesCount = fiftyMovesResult;

            Coordinates enPassantCoordinates = null;
            if (enPassant != "-")
            {
                enPassantCoordinates = Coordinates.FromString(enPassant);
            }

            this.EnPassantCoordinates = enPassantCoordinates;

            if (castling.Contains("k"))
            {
                this.Game.BlackPlayer.KingMoved = false;
                this.Game.BlackPlayer.RightRookMoved = false;
                this.Game.BlackPlayer.Castled = false;
            }

            if (castling.Contains("q"))
            {
                this.Game.BlackPlayer.KingMoved = false;
                this.Game.BlackPlayer.LeftRookMoved = false;
                this.Game.BlackPlayer.Castled = false;
            }

            if (castling.Contains("K"))
            {
                this.Game.WhitePlayer.KingMoved = false;
                this.Game.WhitePlayer.RightRookMoved = false;
                this.Game.WhitePlayer.Castled = false;
            }

            if (castling.Contains("Q"))
            {
                this.Game.WhitePlayer.KingMoved = false;
                this.Game.WhitePlayer.LeftRookMoved = false;
                this.Game.WhitePlayer.Castled = false;
            }

            const string Possible = "kqKQ-";
            if (!castling.Any(ch => Possible.Contains(ch)))
            {
                throw new InvalidFenException(3);
            }

            switch (whoseMove.ToLower())
            {
                case "w":
                    this.Game.WhoseMove = this.Game.WhitePlayer;
                    break;
                case "b":
                    this.Game.WhoseMove = this.Game.BlackPlayer;
                    break;
                default:
                    throw new InvalidFenException(2);
            }

            var index = 0;
            foreach (var c in board)
            {
                if (index >= 64)
                {
                    continue;
                }

                switch (c)
                {
                    case '1':
                        if (index < 63)
                        {
                            index++;
                        }

                        break;
                    case '2':
                        if (index < 62)
                        {
                            index += 2;
                        }

                        break;
                    case '3':
                        if (index < 61)
                        {
                            index += 3;
                        }

                        break;
                    case '4':
                        if (index < 60)
                        {
                            index += 4;
                        }

                        break;
                    case '5':
                        if (index < 59)
                        {
                            index += 5;
                        }

                        break;
                    case '6':
                        if (index < 58)
                        {
                            index += 6;
                        }

                        break;
                    case '7':
                        if (index < 57)
                        {
                            index += 7;
                        }

                        break;
                    case '8':
                        if (index < 56)
                        {
                            index += 8;
                        }

                        break;
                    case 'P':
                        this.Panels.ElementAt(index).Piece = new Pawn(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'N':
                        this.Panels.ElementAt(index).Piece = new Knight(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'B':
                        this.Panels.ElementAt(index).Piece = new Bishop(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'R':
                        this.Panels.ElementAt(index).Piece = new Rook(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'Q':
                        this.Panels.ElementAt(index).Piece = new Queen(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'K':
                        this.Panels.ElementAt(index).Piece = new King(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.White);
                        index++;
                        break;
                    case 'p':
                        this.Panels.ElementAt(index).Piece = new Pawn(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case 'n':
                        this.Panels.ElementAt(index).Piece = new Knight(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case 'b':
                        this.Panels.ElementAt(index).Piece = new Bishop(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case 'r':
                        this.Panels.ElementAt(index).Piece = new Rook(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case 'q':
                        this.Panels.ElementAt(index).Piece = new Queen(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case 'k':
                        this.Panels.ElementAt(index).Piece = new King(
                            this,
                            this.Panels.ElementAt(index).Coordinates,
                            Color.Black);
                        index++;
                        break;
                    case '/':
                        continue;
                    default:
                        throw new InvalidFenException(1);
                }
            }
        }

        public List<Panel> Panels { get; }

        public Game Game { get; }

        public Coordinates EnPassantCoordinates { get; set; }

        public Panel GetPanel(int row, int col)
        {
            return this.GetPanel(new Coordinates(row, col));
        }

        public Panel GetPanel(Coordinates coords)
        {
            if (!coords.Valid)
            {
                return null;
            }

            var pan = this.Panels.FirstOrDefault(p => (p.Coordinates.Row == coords.Row && p.Coordinates.Column == coords.Column));
            return pan;
        }

        public string Output()
        {
            var output = new StringBuilder();
            const string Line = " +-+-+-+-+-+-+-+-+";
            output.AppendLine("  a b c d e f g h");
            output.AppendLine(Line);
            for (var r = 0; r < 8; r++)
            {
                output.Append(8 - r + "|");
                for (var c = 0; c < 8; c++)
                {
                    output.Append(this.GetPanel(r, c).ShowPanel());
                    output.Append("|");
                }

                output.Append(8 - r);

                output.AppendLine();
                output.AppendLine(Line);
            }

            output.Append("  a b c d e f g h");
            return output.ToString();
        }

        // ReSharper disable once InconsistentNaming
        public string ToFEN()
        {
            var output = string.Empty;

            // board
            var blankSquares = 0;
            for (var index = 0; index < 64; index++)
            {
                if (this.Panels.ElementAt(index).IsPiece)
                {
                    var piece = this.Panels.ElementAt(index).Piece;
                    if (blankSquares > 0)
                    {
                        output += blankSquares.ToString();
                        blankSquares = 0;
                    }

                    output += piece.Show();
                }
                else
                {
                    blankSquares++;
                }

                if (index % 8 != 7)
                {
                    continue;
                }

                if (blankSquares > 0)
                {
                    output += blankSquares.ToString();
                    output += "/";
                    blankSquares = 0;
                }
                else
                {
                    if (index > 0 && index != 63)
                    {
                        output += "/";
                    }
                }
            }

            // whose move
            if (this.Game.WhoseMove.Color == Color.White)
            {
                output += " w ";
            }
            else
            {
                output += " b ";
            }

            // castling
            var castling = false;
            if (!this.Game.WhitePlayer.KingMoved && !this.Game.WhitePlayer.RightRookMoved && !this.Game.WhitePlayer.Castled)
            {
                output += "K";
                castling = true;
            }

            if (!this.Game.WhitePlayer.KingMoved && !this.Game.WhitePlayer.LeftRookMoved && !this.Game.WhitePlayer.Castled)
            {
                output += "Q";
                castling = true;
            }

            if (!this.Game.BlackPlayer.KingMoved && !this.Game.BlackPlayer.RightRookMoved && !this.Game.BlackPlayer.Castled)
            {
                output += "k";
                castling = true;
            }

            if (!this.Game.BlackPlayer.KingMoved && !this.Game.BlackPlayer.LeftRookMoved && !this.Game.BlackPlayer.Castled)
            {
                output += "q";
                castling = true;
            }

            if (castling)
            {
                output += " ";
            }
            else
            {
                output += "- ";
            }

            // enpassant
            if (this.EnPassantCoordinates == null)
            {
                output += "- ";
            }
            else
            {
                output += $"{this.EnPassantCoordinates.ToSAN()} ";
            }

            // 50 moves
            output += $"{this.Game.FiftyMovesCount} ";

            // moves
            output += this.Game.MoveCount;

            return output;
        }
    }
}
