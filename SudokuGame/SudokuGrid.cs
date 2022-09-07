using System;

namespace SudokuGame
{
    /// <summary>
    /// Модуль генерации судоку
    /// </summary>
    class SudokuGrid
    {
        public const int DIFFICULTY_SIMPLE = 20;
        public const int DIFFICULTY_MEDIUM = 35;
        public const int DIFFICULTY_HARD = 50;
        private const int MIX_COUNT = 10;
        public int[][] Grid { get; private set; } = new int[9][];
        public int[][] OriginalGrid { get; private set; } = new int[9][];

        private Random rand = new Random();
        private int difficulty;

        public void Generate(int _difficulty)
        {
            difficulty = _difficulty;
            InitializeGrid();
            BasicGrid();
            Mix();
            for(int i = 0; i<9; i++) OriginalGrid[i] = (int[])Grid[i].Clone();
            RemoveDigits();
        }

        // Инициализация сетки
        private void InitializeGrid()
        {
            for(int i = 0; i<9; i++)
                Grid[i] = new int[9];
        }

        // Основная сетка
        private void BasicGrid()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Grid[i][j] = ((i * 3 + i / 3 + j) % (3 * 3) + 1);
        }

        // Транспонирование
        private void Transposing()
        {
            int[][] trans = new int[9][];
            for (int i = 0; i < 9; i++)
                trans[i] = new int[9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    trans[i][j] = Grid[j][i];
                }
            Grid = trans;
        }

        // Меняем местами рандомные в одной из трех частей
        private void SwapRowsInLines()
        {
            // получение случайного района и случайной строки
            int area = rand.Next(0, 3);
            int line1 = rand.Next(0, 3);

            // номер 1 строки для обмена
            int N1 = area * 3 + line1;
            
            int line2 = rand.Next(0, 3);

            while (line1 == line2)
                line2 = rand.Next(0, 3);

            // номер 2 строки для обмена
            int N2 = area * 3 + line2;

            for(int i = 0; i<9; i++)
                for (int j = 0; j < 9; j++)
                {
                    int tmp;
                    tmp = Grid[N1][j];
                    Grid[N1][j] = Grid[N2][j];
                    Grid[N2][j] = tmp;
                }
        }

        // Меняем местами рандомные столбцы в одной из трех частей
        private void SwapColumsInLines()
        {
            Transposing();
            SwapRowsInLines();
            Transposing();
        }

        // Меняем местами две рандомные строчки
        private void SwapRowsArea()
        {
            // получение случайного района
            int area1 = rand.Next(0, 3);
            int area2 = rand.Next(0, 3);

            while (area1 == area2)
			    area2 = rand.Next(0, 3);
            
            for(int i = 0; i < 3; i++)
            {
                int N1 = area1 * 3 + i;
                int N2 = area2 * 3 + i;
                for (int j = 0; j < 9; j++)
                    for (int k = 0; k < 9; k++)
                    {
                        int tmp;
                        tmp = Grid[N1][k];
                        Grid[N1][k] = Grid[N2][k];
                        Grid[N2][k] = tmp;
                    }
            }
        }

        // Меняем местами два рандомных столбца
        private void SwapColumnsArea()
        {
            Transposing();
            SwapRowsArea();
            Transposing();
        }

        // В случайном порядке запускаем функции
        private void Mix()
        {
            for (int i = 0; i < MIX_COUNT; i++)
            {
                int funcId = rand.Next(0, 5);
                switch(funcId)
                {
                    case 0: 
                        Transposing();
                        break;
                    case 1:
                        SwapRowsInLines();
                        break;
                    case 2:
                        SwapColumsInLines();
                        break;
                    case 3:
                        SwapRowsArea();
                        break;
                    case 4:
                        SwapColumnsArea();
                        break;
                }
            }
        }

        // Случайным образом удаляем цифры
        private void RemoveDigits()
        {
            int count = difficulty;
            int N = 9;

            while (count != 0)
            {
                int i = 0, j = 0;
                int cellId = (int)Math.Floor(rand.NextDouble() * N * N);

                i = (cellId / N);
                j = cellId % 9;
                if (i == 9)
                    i = i - 1;

                if (Grid[i][j] != 0)
                {
                    Grid[i][j] = 0;
                    count--;
                }
            }
        }

    }
}
