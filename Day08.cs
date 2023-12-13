using System.Text.RegularExpressions;

namespace AOC2023
{
    internal class Day08
    {
        private static readonly string input = File.ReadAllText(@"Input\08.txt");

        private int[]? directions;

        private Dictionary<string, string[]> nodes = new();

        public Day08()
        {
            var lines = input.Split(Environment.NewLine);
            directions = lines.First().ToArray().Select(d => d == 'L' ? 0 : 1).ToArray();
            foreach (var match in Regex.Matches(input, @"(?<node>\w{3}) = \((?<left>\w{3}), (?<right>\w{3})\)").Cast<Match>())
            {
                var node = match.Groups["node"].Value;
                string[] instructions = { match.Groups["left"].Value, match.Groups["right"].Value };
                nodes.Add(node, instructions);
            }
        }

        internal long Part1()
        {
            return GetSteps("AAA", n => n=="ZZZ");
        }

        internal long Part2()
        {
            var currentNodes = nodes.Where(n => n.Key.EndsWith("A")).Select(n => n.Key).ToArray();
            HashSet<long> numbersOfSteps = new();
            foreach (var node in currentNodes)
            {
                numbersOfSteps.Add(GetSteps(node, n => n.EndsWith("Z")));
            }
            return LeastCommonMultiple(numbersOfSteps);
        }

        private long GetSteps(string node, Func<string,bool> test)
        {
            long steps = 0;
            long numberOfDirections = directions.Length;
            while (!test(node))
            {
                var d = directions[steps % numberOfDirections];
                node = nodes[node][d];
                steps++;
            }
            return steps;
        }

        private static long LeastCommonMultiple(HashSet<long> numbers)
        {
            return numbers.Aggregate((s, val) => s * (int)val / (int)GreatestCommonDenominator(s, val));
            
            static long GreatestCommonDenominator(long n1, long n2)
            {
                return n2 == 0 ? n1 : GreatestCommonDenominator(n2, n1 % n2);
            }
        }
    }
}