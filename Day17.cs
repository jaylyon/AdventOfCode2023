using System.Diagnostics;
using System.Numerics;

namespace AOC2023
{
    internal class Day17
    {
        private static Dictionary<Complex, int>? map;
        private record Cursor(Complex Position, Complex Direction, int StraightMemory);
        private record Rules(Func<Cursor, bool> CanGoStraight, Func<Cursor, bool> CanTurn);

        public Day17()
        {
            var input = File.ReadAllText(@"Input\17.txt");
            var lines = input.Split(Environment.NewLine);
            map = new();
            foreach (var y in Enumerable.Range(0, lines.Length))
            foreach (var x in Enumerable.Range(0, lines[0].Length))
            {
                var value = int.Parse(lines[y].Substring(x, 1));
                var position = new Complex(x, y);
                map.Add(position, value);
            }
        }

        public int Part1() =>
            HeatLoss(new Rules(c => c.StraightMemory < 3, _ => true));

        public int Part2() =>
            HeatLoss(new Rules(c => c.StraightMemory < 10, c => c.StraightMemory >= 4));


        private static int HeatLoss(Rules rules)
        {
            var start = Complex.Zero;
            var end = map?.Keys.MaxBy(p => p.Imaginary + p.Real);
            var q = new PriorityQueue<Cursor, int>(); // priority carries the accumulated heat loss
            var visited = new HashSet<Cursor>();

            q.Enqueue(new Cursor(start, Complex.One, 0), 0);
            q.Enqueue(new Cursor(start, Complex.ImaginaryOne, 0), 0);

            while (q.TryDequeue(out var cursor, out var heatLoss))
            {
                if (cursor.Position == end && rules.CanTurn(cursor)) return heatLoss;

                foreach (var possibleCursor in PossibleCursors(cursor, rules))
                {
                    if (!map!.ContainsKey(possibleCursor.Position) || visited.Contains(possibleCursor)) continue;
                    visited.Add(possibleCursor);
                    q.Enqueue(possibleCursor, heatLoss + map[possibleCursor.Position]);
                }
            }
            throw new UnreachableException("You shall not pass");
        }

        private static IEnumerable<Cursor> PossibleCursors(Cursor cursor, Rules rules)
        {
            if (rules.CanGoStraight(cursor))
                yield return cursor with
                {
                    Position = cursor.Position + cursor.Direction,
                    StraightMemory = cursor.StraightMemory + 1
                };

            if (!rules.CanTurn(cursor)) yield break;
            var d = cursor.Direction * Complex.ImaginaryOne;
            yield return new Cursor(cursor.Position + d, d, 1);
            yield return new Cursor(cursor.Position - d, -d, 1);
        }
    }
}