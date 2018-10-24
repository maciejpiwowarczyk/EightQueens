using System.Threading.Tasks;

namespace EightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            // var board = new ChessBoard(4).PutQueen(2, 1);
            // board.Print();
            // board.Transpose().Print();

            var finder = new BoardFinder();
            finder.Find(8);
            finder.PrintSummary();
        }
    }
}
