using System.Text;

namespace AOC2023
{
    internal class Day14
    {

        public Day14()
        {
            var input = File.ReadAllText(@"Input\14.txt");
            var lines = input.Split(Environment.NewLine);
            length = lines.Length;
            width = lines[0].Length;
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var c in line)
                {
                    if (c != '.')
                    {
                        var rock = new Rock(x, y, c);
                        Rocks.Add(rock);
                    }
                    x++;
                }
                y++;
            }
        }

        private List<Rock> Rocks = new();
        private int length;
        private int width;

        internal int Part1()
        {
            Tilt('N');
            return Rocks.Where(r => r.Type == 'O').Sum(r => length - r.Y);
        }

        internal long Part2()
        {
            var directions = new[] { 'N', 'W', 'S', 'E' };
            const int n = 1000000000;
            HashSet<string> mapHashSet = new();
            List<string> mapCache = new();
            List<long> results = new();
            int i;
            var map = string.Empty;
            for (i = 1; i <= n; i++)
            {
                foreach (var direction in directions)
                {
                    Tilt(direction);
                }
                map = BuildMap();
                if (mapHashSet.Contains(map))
                {
                    break;
                }
                mapCache.Add(map);
                mapHashSet.Add(map);
                results.Add(Rocks.Where(r => r.Type == 'O').Sum(r => length - r.Y));
            }

            var repeatedIndex = mapCache.IndexOf(map);
            var patternLength = results.Count - repeatedIndex;
            var index = repeatedIndex + ((n - repeatedIndex - 1) % patternLength);

            return results[index];
        }

        private void Tilt(char direction)
        {
            switch (direction)
            {
                case 'N':
                    Parallel.For(0, width, x =>
                    {
                        var rocks = Rocks.Where(r => r.X == x).OrderBy(r => r.Y).ToArray();
                        for (var i = 0; i < rocks.Length; i++)
                        {
                            if (rocks[i].Type == '#') continue;
                            if (i == 0)
                            {
                                rocks[i].Y = 0;
                            }
                            else
                            {
                                rocks[i].Y = rocks[i - 1].Y + 1;
                            }
                        }
                    });
                    break;
                case 'S':
                    Parallel.For(0, width, x =>
                    {
                        var rocks = Rocks.Where(r => r.X == x).OrderByDescending(r => r.Y).ToArray();
                        for (var i = 0; i < rocks.Length; i++)
                        {
                            if (rocks[i].Type == '#') continue;
                            if (i == 0)
                            {
                                rocks[i].Y = length - 1;
                            }
                            else
                            {
                                rocks[i].Y = rocks[i - 1].Y - 1;
                            }
                        }
                    });
                    break;
                case 'W':
                    Parallel.For(0, length, y =>
                    {
                        var rocks = Rocks.Where(r => r.Y == y).OrderBy(r => r.X).ToArray();
                        for (var i = 0; i < rocks.Length; i++)
                        {
                            if (rocks[i].Type == '#') continue;
                            if (i == 0)
                            {
                                rocks[i].X = 0;
                            }
                            else
                            {
                                rocks[i].X = rocks[i - 1].X + 1;
                            }
                        }
                    });
                    break;
                case 'E':
                    Parallel.For(0, length, y =>
                    {
                        var rocks = Rocks.Where(r => r.Y == y).OrderByDescending(r => r.X).ToArray();
                        for (var i = 0; i < rocks.Length; i++)
                        {
                            if (rocks[i].Type == '#') continue;
                            if (i == 0)
                            {
                                rocks[i].X = width - 1;
                            }
                            else
                            {
                                rocks[i].X = rocks[i - 1].X - 1;
                            }
                        }
                    });
                    break;
            }
        }

        private class Rock
        {
            public Rock(int x, int y, char type)
            {
                X = x; Y = y;
                Type = type;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public char Type { get; }
            public override string ToString()
            {
                return $"{X},{Y},{Type};";
            }
        }

        private string BuildMap()
        {
            StringBuilder sb = new();
            foreach (var rock in Rocks.OrderBy(r => r.X).ThenBy(r => r.Y))
            {
                sb.Append(rock);
            }
            return sb.ToString();
        }
    }
}