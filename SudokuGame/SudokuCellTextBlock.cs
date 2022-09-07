using System.Windows;
using System.Windows.Controls;

namespace SudokuGame
{
    class SudokuCellTextBlock : TextBlock
    {
        public SudokuCellTextBlock()
        {
            FontSize = 18;
            TextAlignment = TextAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
