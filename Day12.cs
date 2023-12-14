using System.Collections.Immutable;
using MemoCache = System.Collections.Generic.Dictionary<(string, System.Collections.Immutable.ImmutableStack<int>), long>;

namespace AOC2023
{
    internal class Day12
    {
        // Immutable collections used for DTOs - remember that's important
        // Memoized recursion is a thing

        private readonly string input = File.ReadAllText("Input/12.txt");

        internal long Part1()
        {
            return Solve(1);
        }

        internal long Part2()
        {
            return Solve(5);
        }

        private long Solve(int copies)
        {
            long answer = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                var sections = line.Split(" ");
                var record = Unfold(sections[0], '?', copies);
                var groups = Unfold(sections[1], ',', copies).Split(',').Select(int.Parse);
                answer += GetArrangements(record, ImmutableStack.CreateRange(groups.Reverse()), new MemoCache());
            }
            return answer;
        }

        string Unfold(string s, char joiningChar, int copies)
        {
            return string.Join(joiningChar, Enumerable.Repeat(s, copies));
        }

        long GetArrangements(string record, ImmutableStack<int> groups, MemoCache cache)
        {
            if (!cache.ContainsKey((record, groups)))
            {
                cache[(record, groups)] = KickOffRecursion(record, groups, cache);
            }
            return cache[(record, groups)];
        }

        long KickOffRecursion(string record, ImmutableStack<int> groups, MemoCache cache)
        {
            switch (record.FirstOrDefault())
            {
                case '.': return Operational(record, groups, cache);
                case '#': return Damaged(record, groups, cache);
                case '?': return Unknown(record, groups, cache);
                default: return EndOfRecord(groups);
            }
        }

        long Operational(string record, ImmutableStack<int> groups, MemoCache cache)
        {
            return GetArrangements(record[1..], groups, cache);
        }

        long Unknown(string record, ImmutableStack<int> groups, MemoCache cache)
        {
            return GetArrangements("." + record[1..], groups, cache) + GetArrangements("#" + record[1..], groups, cache);
        }

        long Damaged(string record, ImmutableStack<int> groups, MemoCache cache)
        {
            if (!groups.Any()) return 0;
            var nextGroupLength = groups.Peek();
            groups = groups.Pop();
            var possiblyDamaged = record.TakeWhile(s => s is '#' or '?').Count();
            if (possiblyDamaged < nextGroupLength) return 0;
            if (record.Length == nextGroupLength) return GetArrangements("", groups, cache);
            if (record[nextGroupLength] == '#') return 0;
            return GetArrangements(record[(nextGroupLength + 1)..], groups, cache);
        }

        long EndOfRecord(ImmutableStack<int> groups)
        {
            return groups.Any() ? 0 : 1;
        }
    }
}
