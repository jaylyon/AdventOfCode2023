namespace AOC2023
{
    internal class Day13
    {
        private readonly List<List<string>> patterns = new() { new() };

        public Day13()
        {
            var input = File.ReadAllText(@"Input\13.txt");
            foreach (var line in input.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(line))
                {
                    patterns.Add(new());
                }
                else
                {
                    patterns.Last().Add(line);
                }
            }
        }

        public int Part1()
        {
            return Solve(0);
        }

        public int Part2()
        {
            return Solve(1);
        }

        public int Solve(int targetSmudges)
        {
            var result = 0;
            foreach (var pattern in patterns)
            {
                result += GetPoints(targetSmudges, pattern[0].Length, 1, n => pattern.Select(row => row[n]));
                result += GetPoints(targetSmudges, pattern.Count, 100, n => pattern[n]);
            }
            return result;
        }

        private static int GetPoints(int targetSmudges, int length, int multiplier, Func<int, IEnumerable<char>> selector)
        {
            var result = 0;
            for (var i = 1; i < length; i++)
            {
                var smudges = 0;
                for (int fore = i, aft = i - 1; fore < length && aft >= 0; fore++, aft--)
                {
                    smudges += selector(fore).Zip(selector(aft)).Count(pairing => pairing.First != pairing.Second);
                }
                if (smudges == targetSmudges) result += multiplier * i;
            }
            return result;
        }
    }
}