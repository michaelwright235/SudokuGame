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
    /// Логика взаимодействия для NewGameDialog.xaml
    /// </summary>
    public partial class NewGameDialog : Window
    {
        public NewGameDialog()
        {
            InitializeComponent();
        }

        public bool Valid { get; private set; } = false;
        public string UserName { get { return UserNameTextBox.Text; } }
        public int Difficulty { get {
                ComboBoxItem name = (ComboBoxItem)DifficultyComboBox.SelectedItem;
                switch(name.Name)
                {
                    case "simple": return SudokuGrid.DIFFICULTY_SIMPLE;
                    case "medium": return SudokuGrid.DIFFICULTY_MEDIUM;
                    case "hard":   return SudokuGrid.DIFFICULTY_HARD;
                    default: return SudokuGrid.DIFFICULTY_SIMPLE;
                }
            } }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(UserName.Length !=0)
            {
                DialogResult = true;
            }
            else
            {
                UserNameTextBox.BorderBrush = Brushes.Red;
            }
        }
    }
}
