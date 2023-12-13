using System.Collections;

namespace AOC2023
{
    class Day10
    {
        private static readonly string input = File.ReadAllText(@"Input\10.txt");
        private Location start;
        private List<Location> locations;
        private Dictionary<Location, int> vectorDictionary = new();

        public Day10()
        {
            start = Location.FindChar(input, 'S');
            locations = Location.Headings.Where(v => GetNext(start + v, v) != Location.Zero).ToList();
        }

        internal int Part1()
        {
            return locations.Max(v => MaxDistance(v, vectorDictionary));
        }

        internal int Part2()
        {
            HashSet<Location> pp = new(vectorDictionary.Keys) { start };
            return locations.Zip(new[] { Matrix.RotateRight, Matrix.RotateLeft })
                .Sum(t => CountFill(t.First, t.Second, pp));
        }

        private int MaxDistance(Location current, Dictionary<Location, int> vectorDictionary)
        {
            int d = 0, max = 0;
            for (Location q = start + current; q != start; q += current = GetNext(q, current))
                if (!vectorDictionary.TryAdd(q, ++d))
                    max = Math.Max(max, vectorDictionary[q] = Math.Min(vectorDictionary[q], d));
            return max;
        }

        private int CountFill(Location v, Matrix m, HashSet<Location> pp)
        {
            int count = 0;
            for (Location q = start + v; q != start; q += v = GetNext(q, v))
                count += Location.FloodFill(q + v * m, pp);
            return count;
        }

        private static Location GetNext(Location p, Location v)
        {
            return Location.GetChar(p, input) switch
            {
                '|' when v.x == 0 => v,
                '-' when v.y == 0 => v,
                'L' => v.y == 0 ? Location.North : Location.East,
                'J' => v.y == 0 ? Location.North : Location.West,
                '7' => v.y == 0 ? Location.South : Location.West,
                'F' => v.y == 0 ? Location.South : Location.East,
                _ => Location.Zero,
            };
        }
    }

    public struct Location : IEquatable<Location>
    {
        public static readonly Location Zero = default;

        public static readonly Location North = (0, -1);
        public static readonly Location East = (1, 0);
        public static readonly Location South = (0, 1);
        public static readonly Location West = (-1, 0);

        public static readonly Location[] Headings = { North, East, South, West };

        public readonly int x;
        public readonly int y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly bool Equals(Location other) =>
            x == other.x &&
            y == other.y;

        public static Location[] GetNeighbors(Location p) => new Location[]
        {
            new(p.x, p.y - 1),
            new(p.x + 1, p.y),
            new(p.x, p.y + 1),
            new(p.x - 1, p.y)
        };

        public static int FloodFill(Location from, HashSet<Location> pp) =>
             pp.Add(from)
                ? 1 + GetNeighbors(from).Sum(q => FloodFill(q, pp))
                : 0;

        public static Location FindChar(string s, char c, Range range) =>
            new(s.IndexOf(c) % (range.Width + 1), s.IndexOf(c) / (range.Width + 1));

        public static Location FindChar(string s, char c) =>
            FindChar(s, c, Range.FromField(s));

        public static char GetChar(Location p, string s, Range range) =>
            s[p.y * (range.Width + 1) + p.x];

        public static char GetChar(Location p, string s) =>
            GetChar(p, s, Range.FromField(s));

        public readonly Location Add(Location other) =>
            new(x + other.x, y + other.y);

        public static Location operator +(Location left, Location right) =>
            left.Add(right);

        public readonly Location Mul(Matrix m) =>
            new(x * m.m11 + y * m.m21 + m.m31, x * m.m12 + y * m.m22 + m.m32);

        public static Location operator *(Location vector, Matrix matrix) =>
            vector.Mul(matrix);

        public static Location Min(Location left, Location right) =>
            new(Math.Min(left.x, right.x), Math.Min(left.y, right.y));

        public static Location Max(Location left, Location right) =>
            new(Math.Max(left.x, right.x), Math.Max(left.y, right.y));

        public static implicit operator (int x, int y)(Location value) =>
            (value.x, value.y);

        public static implicit operator Location((int x, int y) value) =>
            new(value.x, value.y);

        public static bool operator ==(Location left, Location right) =>
            left.Equals(right);

        public static bool operator !=(Location left, Location right) =>
            !left.Equals(right);

    }

    public struct Range : IEquatable<Range>, IEnumerable<Location>
    {
        public Range(Location min, Location max)
        {
            Min = min;
            Max = max;
        }

        public Range(Location max)
            : this(Location.Zero, max)
        {
        }

        public Range(int x, int y)
            : this(new(x, y))
        {
        }

        public Location Min { get; }
        public Location Max { get; }

        public int Width => Max.x - Min.x + 1;
        public int Height => Max.y - Min.y + 1;

        public readonly override bool Equals(object obj) =>
            obj is Range other && Equals(other);

        public readonly bool Equals(Range other) =>
            Min.Equals(other.Min) &&
            Max.Equals(other.Max);

        public readonly override int GetHashCode() =>
            HashCode.Combine(Min, Max);

        public readonly IEnumerator<Location> GetEnumerator()
        {
            for (var y = Min.y; y <= Max.y; y++)
                for (var x = Min.x; x <= Max.x; x++)
                    yield return new(x, y);
        }

        readonly IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public readonly bool IsMatch(Location vector) =>
            vector.x >= Min.x && vector.x <= Max.x &&
            vector.y >= Min.y && vector.y <= Max.y;


        public readonly Range Intersect(Range other) =>
            new(min: Location.Max(Min, other.Min), max: Location.Min(Max, other.Max));


        public static Range FromField(string s) =>
            new(GetWidth(s) - 1, GetHeight(s) - 1);

        private static int GetWidth(string s) =>
            s.IndexOf('\n');

        private static int GetHeight(string s) =>
            (s.Length + 1) / GetWidth(s);

        public static implicit operator (Location min, Location max)(Range value) =>
            (value.Min, value.Max);

        public static implicit operator Range((Location min, Location max) value) =>
            new(value.min, value.max);

        public static bool operator ==(Range left, Range right) =>
            left.Equals(right);

        public static bool operator !=(Range left, Range right) =>
            !left.Equals(right);
    }

    public struct Matrix
    {
        public static readonly Matrix Zero = default;
        public static readonly Matrix Identity = new(1, 0, 0, 1);
        public static readonly Matrix RotateRight = new(0, 1, -1, 0);
        public static readonly Matrix RotateLeft = new(0, -1, 1, 0);
        public static readonly Matrix MirrorHorizontal = new(1, 0, 0, -1);
        public static readonly Matrix MirrorVertical = new(-1, 0, 0, 1);
        public static readonly Matrix Flip = new(-1, 0, 0, -1);

        public readonly int m11;
        public readonly int m12;
        public readonly int m21;
        public readonly int m22;
        public readonly int m31;
        public readonly int m32;

        public Matrix(int m11, int m12, int m21, int m22, int m31 = 0, int m32 = 0)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        public Matrix(int m)
        {
            m11 = (m & 3) - 1;
            m12 = ((m >> 2) & 3) - 1;
            m21 = ((m >> 4) & 3) - 1;
            m22 = ((m >> 6) & 3) - 1;
            m31 = m32 = 0;
        }

        public static implicit operator Matrix((int m11, int m12, int m21, int m22) m) =>
            new(m.m11, m.m12, m.m21, m.m22);

        public static implicit operator Matrix((int m11, int m12, int m21, int m22, int m31, int m32) m) =>
            new(m.m11, m.m12, m.m21, m.m22, m.m31, m.m32);

        public static explicit operator Matrix(int m) =>
            new(m);

    }
}