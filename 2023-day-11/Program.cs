namespace _2023_day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool useTestData = false;
            IList<string> data = useTestData ? GetTestInput() : GetPuzzleInput();

            bool[] rowsWithStars = new bool[data.Count];
            bool[] colsWithStars = new bool[data[0].Length];
            List<KeyValuePair<int, int>> galaxies = [];
            
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] == '#')
                    {
                        rowsWithStars[i] = true;
                        colsWithStars[j] = true;
                        galaxies.Add(new(i, j));
                    }
                }
            }

            Dictionary<KeyValuePair<int, int>, int> rowCalculations = [];
            Dictionary<KeyValuePair<int, int>, int> colCalculations = [];

            long result = 0;
            int connections = 0;
            Func<bool, int> distanceCalc = t => t ? 1 : 1000000;

            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    KeyValuePair<int, int> rowRange = GetRange(galaxies[i].Key, galaxies[j].Key);
                    KeyValuePair<int, int> colRange = GetRange(galaxies[i].Value, galaxies[j].Value);

                    if (rowCalculations.TryGetValue(rowRange, out int rowValue))
                    {
                        result += rowValue;
                    }
                    else
                    {
                        rowCalculations[rowRange] = rowsWithStars[rowRange.Key..rowRange.Value].Sum(distanceCalc);
                        result += rowCalculations[rowRange];
                    }

                    if (colCalculations.TryGetValue(colRange, out int colValue))
                    {
                        result += colValue;
                    }
                    else
                    {
                        colCalculations[colRange] = colsWithStars[colRange.Key..colRange.Value].Sum(distanceCalc);
                        result += colCalculations[colRange];
                    }
                }
            }

            Console.WriteLine($"Distance sum: {result}. ");
        }

        static IList<string> GetPuzzleInput()
        {
            string file = Path.Combine(Environment.CurrentDirectory, "puzzle-input.txt");
            using StreamReader sr = new StreamReader(file);
            List<string> input = [];

            while (!sr.EndOfStream)
            {
                input.Add(sr.ReadLine()!);
            }

            return input;
        }

        static IList<string> GetTestInput()
        {
            return @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....".Split('\n');
        }

        static KeyValuePair<int, int> GetRange(int a, int b)
        {
            if (a > b)
                return new(b, a);
            return new(a, b);
        }
    }
}
