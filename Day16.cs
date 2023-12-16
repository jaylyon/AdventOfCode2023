namespace AOC2023
{
    internal class Day16
    {

        private static Tile[,]? contraption;

        public Day16()
        {
            string input = File.ReadAllText(@"Input\16.txt");
            var lines = input.Split(Environment.NewLine).ToList();
            contraption = new Tile[lines[0].Length, lines.Count];
            for (var y = 0; y < contraption.GetLength(1); y++)
                for (var x = 0; x < contraption.GetLength(0); x++)
                    contraption[x, y] = new Tile() { Value = lines[y][x] };
        }

        internal int Part1()
        {
            var beam = new Beam() { Heading = Direction.E, X = -1, Y = 0 };
            beam.Advance();
            return GetEnergizedTiles();
        }

        internal int Part2()
        {
            resetContraption();
            var max = 0;
            for (var x = 0; x < contraption.GetLength(1); x++)
            {
                var beam = new Beam() { Heading = Direction.S, X = x, Y = -1 };
                beam.Advance();
                max = Math.Max(GetEnergizedTiles(), max);
                resetContraption();

                beam = new Beam() { Heading = Direction.N, X = x, Y = contraption.GetLength(0) };
                beam.Advance();
                max = Math.Max(GetEnergizedTiles(), max);
                resetContraption();
            }
            for (var y = 0; y < contraption.GetLength(0); y++)
            {
                var beam = new Beam() { Heading = Direction.E, X = -1, Y = y };
                beam.Advance();
                max = Math.Max(GetEnergizedTiles(), max);
                resetContraption();

                beam = new Beam() { Heading = Direction.W, X = contraption.GetLength(0), Y = y };
                beam.Advance();
                max = Math.Max(GetEnergizedTiles(), max);
                resetContraption();
            }

            return max;
        }

        internal class Beam
        {
            public Direction Heading { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public void Advance()
            {
                switch (Heading)
                {
                    case Direction.N: Y--; break;
                    case Direction.E: X++; break;
                    case Direction.S: Y++; break;
                    case Direction.W: X--; break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (X < 0 || Y < 0 || X >= contraption.GetLength(0) || Y >= contraption.GetLength(1)) return;
                var tile = contraption[X, Y];
                if (tile.VisitedHeadings.Contains(Heading)) return;
                tile.VisitedHeadings.Add(Heading);
                tile.IsEnergized = true;
                switch (tile.Value)
                {
                    case '.':
                        Advance(); break;
                    case '/':
                        switch (Heading)
                        {
                            case Direction.N: Heading = Direction.E; break;
                            case Direction.E: Heading = Direction.N; break;
                            case Direction.S: Heading = Direction.W; break;
                            case Direction.W: Heading = Direction.S; break;
                        }
                        Advance();
                        break;
                    case '\\':
                        switch (Heading)
                        {
                            case Direction.N: Heading = Direction.W; break;
                            case Direction.E: Heading = Direction.S; break;
                            case Direction.S: Heading = Direction.E; break;
                            case Direction.W: Heading = Direction.N; break;
                        }
                        Advance();
                        break;
                    case '-':
                        if (Heading is Direction.N or Direction.S)
                        {
                            Heading = Direction.W;
                            var beam = new Beam() { Heading = Direction.E, X = this.X, Y = this.Y };
                            beam.Advance();
                        }
                        Advance();
                        break;
                    case '|':
                        if (Heading is Direction.E or Direction.W)
                        {
                            Heading = Direction.N;
                            var beam = new Beam() { Heading = Direction.S, X = this.X, Y = this.Y };
                            beam.Advance();
                        }
                        Advance();
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int GetEnergizedTiles()
        {
            var answer = 0;
            for (var y = 0; y < contraption.GetLength(0); y++)
                for (var x = 0; x < contraption.GetLength(1); x++)
                    answer += contraption[x, y].IsEnergized ? 1 : 0;

            return answer;
        }

        private void resetContraption()
        {
            for (var y = 0; y < contraption.GetLength(0); y++)
                for (var x = 0; x < contraption.GetLength(1); x++)
                {
                    contraption[x, y].IsEnergized = false;
                    contraption[x, y].VisitedHeadings.Clear();
                }
        }
    }


    internal class Tile
    {
        public bool IsEnergized { get; set; }
        public HashSet<Direction> VisitedHeadings { get; set; } = new();
        public char Value { get; set; }
    }

    internal enum Direction
    {
        N, S, E, W
    }

}
