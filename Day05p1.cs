using System.Text.RegularExpressions;

namespace AOC2023
{
    internal class Day05p1
    {
        private static readonly string input = File.ReadAllText(@"Input/05.txt");

        private long[]? seeds;

        internal long Part1()
        {
            seeds = Regex.Match(input, @"seeds: (?<seeds>[\d\n\s]*)").Groups["seeds"].Value.Split(' ').Select(long.Parse).ToArray();
            foreach (Match match in Regex.Matches(input, @"(?<name>[\w-]+) map:(?<mappings>[\d\s\n]+)"))
            {
                Map map = new() { };
                foreach (string line in match.Groups["mappings"].Value.Split(Environment.NewLine).Where(l => !String.IsNullOrEmpty(l)))
                {
                    var vals = line.Split(' ').Select(long.Parse).ToArray();
                    map.Mappings.Add(new Mapping() { To = vals[0], From = vals[1], Length = vals[2] });
                }
                Maps.Add(map);
            }

            long minLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                long x = seed;
                foreach (var map in Maps)
                {
                    x = map.GetMappedValue(x);
                }

                minLocation = Math.Min(minLocation, x);
            }
            return minLocation;
        }

        private List<Map> Maps = new();

        private class Map
        {
            public List<Mapping> Mappings { get; } = new();

            public long GetMappedValue(long source)
            {
                var mapping = Mappings.FirstOrDefault(m => m.From <= source && m.Max > source);
                if (mapping == default) return source;
                return mapping.To + source - mapping.From;
            }
        }

        private class Mapping
        {
            internal long To { get; init; }
            internal long From { get; init; }
            internal long Length { get; init; }
            
            private long max;

            internal long Max
            {
                get
                {
                    if (max != default) return max;
                    max = From + Length;
                    return max;
                }
            }
        }
    }
}
