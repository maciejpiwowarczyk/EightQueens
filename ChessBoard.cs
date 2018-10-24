using System;

namespace EightQueens
{
    class ChessBoard
    {
        enum FieldTypes
        {
            Empty,
            Queen,
            Checked,
        }

        public int Size { get; private set; }
        FieldTypes[][] Board { get; set; }

        public ChessBoard Transpose()
        {
            var board = this.Copy();
            for (int i=0; i<Size; i++)
            for (int j=0; j<i; j++)
            {            
                board.Swap(i, j);
            }
            return board;
        }

        private void Swap(int x, int y)
        {
            var tmp = Board[x][y];
            Board[x][y] = Board[y][x];
            Board[y][x] = tmp;
        }

        public ChessBoard(int size)
        {
            Size = size;
            Board = new FieldTypes[size][];
            for (int i=0; i<size; i++) Board[i] = new FieldTypes[size];
        }

        ChessBoard Copy()
        {
            var board = new ChessBoard(this.Size);
            for (int i=0; i<Size; i++)
            for (int j=0; j<Size; j++)
            {
                board.Board[i][j] = this.Board[i][j];
            }
            return board;
        }

        public ChessBoard ClearChecked()
        {
            var board = this.Copy();
            for (int i=0; i<Size; i++)
            for (int j=0; j<Size; j++)
            {
                if (board.Board[i][j] == FieldTypes.Checked) board.Board[i][j] = FieldTypes.Empty;
            }
            return board;
        }

        public bool CanPutQueen(int x, int y)
        {
            return Board[x][y] == FieldTypes.Empty;
        }

        public ChessBoard PutQueen(int x, int y)
        {            
            if (CanPutQueen(x, y) == false) throw new InvalidOperationException($"Cannot put queen on ({x}, {y})");

            var board = this.Copy();
            board.Board[x][y] = FieldTypes.Queen;

            int i=0; 
            for (i=x+1; i<Size; i++)
            {
                board.Board[i][y] = FieldTypes.Checked;
            }
            for (i=y+1; i<Size; i++)
            {
                board.Board[x][i] = FieldTypes.Checked;
            }
            
            i=1;
            while (x+i < Size && y+i < Size)
            {
                board.Board[x+i][y+i] = FieldTypes.Checked;
                i++;
            }

            i=1;
            while (x-i >= 0 && y+i < Size)
            {
                board.Board[x-i][y+i] = FieldTypes.Checked;
                i++;
            }
            return board;

        }

        public void Print()
        {
            for (int i=0; i<Size; i++)
            {                
                for (int j=0; j<Size; j++)
                {
                    Console.Write($"{ToPrint(Board[i][j])} ");
                }
                Console.WriteLine();
            }
        }

        private string ToPrint(FieldTypes fieldType)
        {
            switch (fieldType)
            {
                case FieldTypes.Empty: return "_";
                case FieldTypes.Queen: return "Q";
                case FieldTypes.Checked: return "X";
                default: throw new ArgumentOutOfRangeException($"fieldType: {fieldType}");
            }
        }
    }
}
