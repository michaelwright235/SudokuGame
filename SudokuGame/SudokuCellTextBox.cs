using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SudokuGame
{
    class SudokuCellTextBox : TextBox
    {
        public SudokuCellTextBox()
        {
            PreviewMouseDown += CellOnMouseDown;
            LostFocus += CellOnLostFocus;
            KeyDown += CellOnKeyDown;

            TextChanged += CellOnTextChanged;
            MaxLength = 1;
            BorderThickness = new Thickness(0);
            Cursor = Cursors.Hand;
            TextAlignment = TextAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            FontSize = 18;
            Foreground = Brushes.Blue;
            VerticalAlignment = VerticalAlignment.Stretch;
            CaretBrush = Brushes.Transparent;
            PreviewMouseDown += CellOnMouseDown;
            LostFocus += CellOnLostFocus;
            KeyDown += CellOnKeyDown;
            TextChanged += CellOnTextChanged;
        }

        public int Row { get; set; } = 0;
        public int Column { get; set; } = 0;

        public int SudokuDigit { get; set; } = 0;

        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D1 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad1 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }

        private bool IsArrows(Key inKey)
        {
            return inKey == Key.Left || inKey == Key.Right || inKey == Key.Up || inKey == Key.Down;
        }

        private string LeaveOnlyNumbers(string inString)
        {
            string tmp = inString;
            foreach (char c in inString.ToCharArray())
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), "^[0-9]*$"))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp;
        }

        /* События */
        private void CellOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).Background = Brushes.LightSkyBlue;
        }

        private void CellOnLostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = Brushes.Transparent;
        }

        private void CellOnKeyDown(object sender, KeyEventArgs e)
        {
            // Разрешаем вводить только цифры 1-9
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
            if (!e.Handled)
                // Если нажимают цифру, то тогда заменяем ей то, что было в ячейке до этого
                (sender as TextBox).Text = "";
        }

        private void CellOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = LeaveOnlyNumbers(Text);
        }

        // Подсвечиваем ячейку красным, если ответ неправильный
        public int CheckWrongDigit()
        {
            if (Text.Length == 0) return 0;
            int digit = int.Parse(Text);
            if (digit == SudokuDigit) return SudokuDigit;

            SolidColorBrush myBrush = new SolidColorBrush();
            ColorAnimation animation = new ColorAnimation
            {
                From = Colors.Transparent,
                To = Colors.Pink,
                Duration = new Duration(TimeSpan.FromSeconds(0.7)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2)
            };
            myBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            Background = myBrush;

            return -1;
        }
    }
}
