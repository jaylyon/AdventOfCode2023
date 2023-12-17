using System.Text.RegularExpressions;

namespace AOC2023
{
    internal class Day05p2
    {
        private static readonly string input = File.ReadAllText(@"Input/05.txt");

        public List<List<Mapping>> Maps { get; set; } = new();

        public Day05p2()
        {
            foreach (var match in Regex.Matches(input, @"(?<name>[\w-]+) map:(?<mappings>[\d\s\n]+)").Cast<Match>())
            {
                var maps = match.Groups["mappings"].Value.Split(Environment.NewLine)
                    .Where(l => !string.IsNullOrEmpty(l)).Select(line => new Mapping(line)).ToList();
                Maps.Add(maps);
            }
        }

        internal long Part2()
        {
            var firstLine = input[..input.IndexOf(Environment.NewLine)];
            var seedRanges = Regex.Matches(firstLine, @"(?<location>\d+) (?<length>\d+)")
                .Select(match => new SeedRange()
                {
                    From = long.Parse(match.Groups["location"].Value),
                    To = long.Parse(match.Groups["location"].Value) + long.Parse(match.Groups["length"].Value) - 1L
                }).ToList();

            // Thanks to my friend Thomas for this little bit of wizardry
            foreach (var maps in Maps)
            {
                for (var i = 0; i < seedRanges.Count; i++)
                {
                    var seed = seedRanges[i];
                    List<SeedRange> toAdd = new();
                    foreach (var map in maps.Where(map => seed.From <= map.To && seed.To >= map.From))
                    {
                        if (map.From <= seed.From && map.To >= seed.To)
                        {
                            seed.From += map.Shift;
                            seed.To += map.Shift;
                            break;
                        }
                        if (map.From <= seed.From && map.To < seed.To)
                        {
                            toAdd.Add(new SeedRange() { From = map.To + 1, To = seed.To });
                            seed.From += map.Shift;
                            seed.To = map.To + map.Shift;
                            break;
                        }
                        if (map.From > seed.From && map.To >= seed.To)
                        {
                            toAdd.Add(new SeedRange() { From = seed.From, To = map.From - 1 });
                            seed.From = map.From + map.Shift;
                            seed.To += map.Shift;
                            break;
                        }

                        if (map.From <= seed.From || map.To >= seed.To) continue;
                        toAdd.Add(new SeedRange() { From = seed.From, To = map.From - 1 });
                        toAdd.Add(new SeedRange() { From = map.From + 1, To = seed.To });
                        seed.From = map.From + map.Shift;
                        seed.To = map.To + map.Shift;
                        break;
                    }
                    seedRanges.AddRange(toAdd);
                }
            }
            return seedRanges.Select(s => s.From).Where(v => v > 0).Min();
        }

        internal class Mapping
        {
            internal long From { get; }
            internal long To { get; }
            internal long Length { get; }
            internal long Max { get; }
            internal long Shift { get; }

            internal Mapping(string line)
            {
                var numbers = line.Split(' ').Select(long.Parse).ToArray();
                From = numbers[1];
                To = numbers[1] + numbers[2] - 1;
                Length = numbers[2];
                Shift = numbers[0] - numbers[1];
                Max = From + Length;
            }
        }

        internal class SeedRange
        {
            public long From { get; set; }
            public long To { get; set; }
        }
    }
}
