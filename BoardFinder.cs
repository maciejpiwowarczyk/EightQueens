using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace EightQueens
{
    class BoardFinder
    {
        private List<ChessBoard> winningBoards = new List<ChessBoard>();
        private object winningBoardsLock = new object();
        private int numberOfOperations;        
        private Stopwatch sw;

        private void AddWinner(ChessBoard winningBoard)
        {
            lock (winningBoardsLock)
            {
                var wb = winningBoard.ClearChecked();
                winningBoards.Add(wb);
                winningBoards.Add(wb.Transpose());
            }
        }

        public void Find(int boardSize)
        {
            sw = new Stopwatch();
            sw.Start();
            //var tasks = new List<Task>();
            for (int i=0; i<boardSize/2; i++)
            {
                var tmp = i;
                //var task = Task.Run(() => {
                var board = new ChessBoard(boardSize);
                FindBoard(board.PutQueen(tmp, 0), 0);
                //});
                //tasks.Add(task);
            }
            //Task.WaitAll(tasks.ToArray());
            sw.Stop();
        }

        private void FindBoard(ChessBoard board, int y)
        {            
            if (y >= board.Size-1)
            {
                AddWinner(board);
                return;
            }

            Interlocked.Increment(ref numberOfOperations);
            for (int i=0; i<board.Size; i++)
            {
                if (board.CanPutQueen(i, y+1))
                {
                    FindBoard(board.PutQueen(i, y+1), y+1);
                }
            }
        }

        internal void PrintSummary()
        {
            foreach (var board in winningBoards)
            {
                board.Print();
                Console.WriteLine("------------------------");
            }
            Console.WriteLine($"Found {winningBoards.Count} boards in {numberOfOperations} operations in {sw.ElapsedMilliseconds} ms");
        }
    }
}
