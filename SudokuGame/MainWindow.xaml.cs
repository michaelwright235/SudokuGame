using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[][] grid;
        private int[][] originalGrid;
        private int[][] userGridInput;
        private string _userName;
        private string UserName { get { return _userName; } set { _userName = TopUserName.Text = value; } }
        private SudokuGrid sudokuGrid = new SudokuGrid();
        private List<SudokuCellTextBox> textBoxes = new List<SudokuCellTextBox>();
        public MainWindow()
        {
            InitializeComponent();
            Closing += CloseWithoutSavingDialog;
        }

        // Создание новой игры - очистка предыдущих значений и занесение новых
        private void NewGame(int difficulty)
        {
            // очистка окна
            textBoxes.Clear();
            mainSudokuGrid.Children.Clear();

            userGridInput = new int[9][];
            for (int i = 0; i < 9; i++)
                userGridInput[i] = new int[9];
            
            sudokuGrid.Generate(difficulty);
            grid = sudokuGrid.Grid;
            originalGrid = sudokuGrid.OriginalGrid;

            CreateCells();
        }

        // Создание клеток с неизменяемым и изменяемым текстом 
        private void CreateCells()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    double thinLine = 1;
                    double thickLine = 3;

                    // Создаем видимые "квадраты" с рамкой
                    Thickness thickness = new Thickness(thinLine, thinLine, 0, 0);
                    if (i == 0)
                        thickness.Top = thickLine;

                    if (j == 0)
                        thickness.Left = thickLine;

                    if (j == 2 || j == 5 || j == 8)
                        thickness.Right = thickLine;

                    if (i == 2 || i == 5 || i == 8)
                        thickness.Bottom = thickLine;


                    Border border = new Border() { BorderThickness = thickness, BorderBrush = Brushes.Black };

                    if (grid[i][j] != 0) // Если квадрат заполнен
                    {
                        SudokuCellTextBlock textBlock = new SudokuCellTextBlock();
                        textBlock.Text = grid[i][j] + "";

                        border.Child = textBlock;
                        Grid.SetColumn(border, j);
                        Grid.SetRow(border, i);
                        mainSudokuGrid.Children.Add(border);
                    }
                    else // Если квадрат незаполнен
                    {
                        SudokuCellTextBox textBox = new SudokuCellTextBox()
                        {
                            Row = i,
                            Column = j,
                            SudokuDigit = originalGrid[i][j],
                        };
                        if (userGridInput[i][j] != 0) textBox.Text = userGridInput[i][j]+"";
                        textBox.TextChanged += UpdateGrid;
                        textBoxes.Add(textBox);
                        border.Child = textBox;
                        Grid.SetColumn(border, j);
                        Grid.SetRow(border, i);
                        mainSudokuGrid.Children.Add(border);
                    }

                }
        }
        
        // Обновляем сетку при каждом изменении клетки для правильного сохранения в файл
        private void UpdateGrid(object sender, RoutedEventArgs e)
        {
            string str = (sender as SudokuCellTextBox).Text;
            int row = (sender as SudokuCellTextBox).Row;
            int column = (sender as SudokuCellTextBox).Column;

            if (str.Length == 0)
            {
                userGridInput[row][column] = 0;
                return;
            }
            int digit = int.Parse(str);
            userGridInput[row][column] = digit;
        }

        // Проверка клеток на правильность
        private void CheckSolution(object sender, RoutedEventArgs e)
        {
            if (grid == null)
            {
                MessageBox.Show("Вы еще не начали игру!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int error = 0;
            foreach (SudokuCellTextBox tb in textBoxes)
                if (tb.CheckWrongDigit() < 0) error = -1;
            if (error == -1) System.Media.SystemSounds.Hand.Play();
        }

        // Кнопка создания новой игры
        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            NewGameDialog dialog = new NewGameDialog();
            if (dialog.ShowDialog() == true)
                NewGame(dialog.Difficulty);
            UserName = dialog.UserName;
        }

        // Сохранение игры в файл
        private void SaveGameButtonClick(object sender, RoutedEventArgs e)
        {
            if (grid == null)
            {
                MessageBox.Show("Вы еще не начали игру!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Файлы dat (*.dat)|*.dat|Все файлы (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                SudokuSaver.Save(grid, originalGrid, userGridInput, UserName, saveFileDialog.FileName);
            }
        }

        // Открытие игры из файла
        private void OpenGameButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы dat (*.dat)|*.dat|Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                ArrayList result = SudokuSaver.Open(openFileDialog.FileName);
                if (result == null)
                {
                    MessageBox.Show("Не удалось открыть файл", "Ошибка");
                    return;
                }

                textBoxes.Clear();
                mainSudokuGrid.Children.Clear();
                UserName = (string)result[0];
                grid = (int[][])result[1];
                originalGrid = (int[][])result[2];
                userGridInput = (int[][])result[3];
                CreateCells();
            }
        }

        // Кнопка "О программе"
        private void AboutButtonClick(object sender, RoutedEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog();
        }

        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            HelpDialog dialog = new HelpDialog();
            dialog.ShowDialog();
        }

        // Кнопка выхода
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Сообщение при закрытии программы (о возможности сохранить игру)
        private void CloseWithoutSavingDialog(object sender, CancelEventArgs e)
        {
            if (grid == null)
                return;

            MessageBoxResult result = MessageBox.Show("Не хотели бы вы сохранить игру перед выходом?", "Сохранение", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SaveGameButtonClick(new object(), new RoutedEventArgs());
            }
            else if(result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
