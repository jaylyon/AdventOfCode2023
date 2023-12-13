﻿using System.Net;

namespace AOC2023
{
    internal class Day11
    {
        private readonly string input = File.ReadAllText(@"Input\11.txt");

        public Day11()
        {
            var rawlines = input.Split(Environment.NewLine);
            var lines = new List<string>();
            var i = 0;
            foreach (var line in rawlines)
            {
                lines.Add(line);
                if (!line.Contains("#")) emptyRows.Add(i);
                i++;
            }
            for (int x = 0; x < lines[0].Length; x++)
            {
                bool isEmpty = true;
                foreach (var line in lines)
                {
                    if (line.ToCharArray()[x] == '#')
                    {
                        isEmpty = false;
                    }
                }
                if (isEmpty) emptyColumns.Add(x);
            }

            char[,] image = new char[lines[0].Length,lines.Count];
            for (int y = 0; y < image.GetLength(1); y++)
            {
                var chars = lines[y].ToArray<char>();
                for (int x = 0; x < chars.Length; x++)
                {
                    image[x, y] = chars[x];
                }
            }

            int id = 0;
            for (int y = 0;y < image.GetLength(1);y++)
            {
                for (int x = 0;x < image.GetLength(0);x++)
                {
                    if (image[x,y] == '#') 
                    {
                        id++;
                        galaxies.Add(new Galaxy()
                        {
                            Id = id,
                            X = x,
                            Y = y
                        });
                    }
                }
            }
            foreach (var g in galaxies)
            {
                foreach (var otherg in galaxies)
                {
                    if (g.Id < otherg.Id)
                    {
                        pairs.Add(new Tuple<Galaxy, Galaxy>(g, otherg));
                    }
                }
            }
        }

        private List<long> emptyRows = new();
        private List<long> emptyColumns = new();
        private List<Galaxy> galaxies = new();
        private HashSet<Tuple<Galaxy, Galaxy>> pairs = new();

        internal long Part1()
        {
            long sum = 0;
            foreach (var pair in pairs)
            {
                long x = Math.Abs(pair.Item1.X - pair.Item2.X) + emptyColumns.Count(c => c > pair.Item1.X && c < pair.Item2.X || c > pair.Item2.X && c < pair.Item1.X);
                long y = Math.Abs(pair.Item1.Y - pair.Item2.Y) + emptyRows.Count(r => r > pair.Item1.Y && r < pair.Item2.Y || r > pair.Item2.Y && r < pair.Item1.Y) ;
                sum += x + y;
            }
            return sum;
        }

        internal long Part2()
        {
            long sum = 0;
            foreach (var pair in pairs)
            {
                long x = Math.Abs(pair.Item1.X - pair.Item2.X) + emptyColumns.Count(c => c > pair.Item1.X && c < pair.Item2.X || c > pair.Item2.X && c < pair.Item1.X) * (1000000 - 1);
                long y = Math.Abs(pair.Item1.Y - pair.Item2.Y) + emptyRows.Count(r => r > pair.Item1.Y && r < pair.Item2.Y || r > pair.Item2.Y && r < pair.Item1.Y) * (1000000 - 1);
                sum += x + y;
            }
            return sum;
        }

        private class Galaxy : IComparable<Galaxy>
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public int CompareTo(Galaxy? other)
            {
                return other.Id - Id;
            }
        }
    }
}