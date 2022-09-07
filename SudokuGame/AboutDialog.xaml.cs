using System.Text;
using System.Windows;

namespace SudokuGame
{
    /// <summary>
    /// Логика взаимодействия для AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sudoku Game");
            sb.AppendLine("Version 1.0");
            sb.AppendLine("By Michael Wright");
            AboutText.Text = sb.ToString();
        }
    }
}
