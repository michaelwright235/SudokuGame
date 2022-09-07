using System;
using System.IO;
using System.Collections;

namespace SudokuGame
{
    /// <summary>
    /// Модуль сохранения текущего судоку в файл и его открытия из файла
    /// </summary>
    class SudokuSaver
    {
        const string START = "SGMW!"; // Защита от открытия чужих файлов

        // Сохранение
        /* (string)UserName; [int32,int32,bool]...*/
        public static bool Save(int[][] grid, int[][] originalGrid, int[][]userGridInput, string UserName, string fileName)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
                {
                    writer.Write(START);
                    writer.Write(UserName);
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                        {
                            bool editable = false;
                            if (grid[i][j] == 0) editable = true;
                            writer.Write(originalGrid[i][j]);
                            writer.Write(userGridInput[i][j]);
                            writer.Write(editable);
                        }
                }
                return true;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Открытие
        public static ArrayList Open(string fileName)
        {
            ArrayList result = new ArrayList();
            string UserName;
            int[][] grid = new int[9][], originalGrid = new int[9][], userGridInput = new int[9][];
            for (int i = 0; i < 9; i++) // инициализация
            {
                grid[i] = new int[9];
                originalGrid[i] = new int[9];
                userGridInput[i] = new int[9];
            }

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    string start = reader.ReadString();
                    if (start != START) return null;

                    UserName = reader.ReadString();
                    for(int i = 0; i<9; i++)
                        for(int j = 0; j<9; j++)
                        {
                            if (!(reader.PeekChar() > -1)) break;

                            int digit = reader.ReadInt32();
                            int userInputDigit = reader.ReadInt32();
                            originalGrid[i][j] = digit;

                            userGridInput[i][j] = userInputDigit;
                            bool editable = reader.ReadBoolean();
                            if (!editable) grid[i][j] = digit;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            result.Add(UserName);
            result.Add(grid);
            result.Add(originalGrid);
            result.Add(userGridInput);
            return result;

        }
    }
}
