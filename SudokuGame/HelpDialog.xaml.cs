using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SudokuGame
{
    /// <summary>
    /// Логика взаимодействия для HelpDialog.xaml
    /// </summary>
    public partial class HelpDialog : Window
    {
        public HelpDialog()
        {
            InitializeComponent();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Математическая головоломка под названием «Судоку» родом из Японии. Она получила широкое распространение во всем мире благодаря своей увлекательности. Для ее решения потребуется сконцентрировать внимание, память, задействовать логическое мышление.");
            sb.AppendLine("");
            sb.AppendLine("За основу головоломки взят латинский квадрат. Поле для игры выполнено в форме именно этой геометрической фигуры, каждая сторона которой состоит из 9 клеток. Большой квадрат заполнен маленькими квадратными блоками, подквадратами, со стороной в три клетки. В начале игры в определенные из них уже вписаны цифры-«подсказки».");
            sb.AppendLine("");
            sb.AppendLine("Необходимо заполнить все оставшиеся пустые ячейки натуральными числами от 1 до 9.");
            sb.AppendLine("");
            sb.AppendLine("Сделать это нужно так, чтобы цифры не повторялись:");
            sb.AppendLine("");
            sb.AppendLine(" - в каждом столбце,");
            sb.AppendLine(" - в каждой строке,");
            sb.AppendLine(" - в любом из малых квадратов.");
            sb.AppendLine("");
            sb.AppendLine("Таким образом в каждой строке и каждом столбце большого квадрата будут расположены цифры от одного до десяти, любой малый квадрат также будет содержать эти цифры без повторений.");
            sb.AppendLine("");
            sb.AppendLine("Игра имеет единственное правильное решение. Есть различные уровни сложности: простую головоломку, с большим количеством заполненных клеток, можно решить за несколько минут. На сложную, где расставлено малое количество цифр, можно потратить несколько часов.");

            HelpText.Text = sb.ToString();


        }
    }
}
