namespace AOC2023
{
    internal class Day15
    {
        private readonly string input = File.ReadAllText(@"Input\15.txt");

        private static int Hash(string s)
        {
            var hash = 0;
            foreach (var c in s.Where(c => c != '\n'))
            {
                hash += (int)c;
                hash *= 17;
                hash %= 256;
            }
            return hash;
        }

        internal int Part1()
        {
            return input.Split(',').Sum(Hash);
        }

        internal int Part2()
        {
            var boxes = Enumerable.Range(0, 256).Select(_ => new Box()).ToArray();
            foreach (var s in input.Split(","))
            {
                var values = s.Split('-', '=');
                var label = values[0];
                var box = boxes[Hash(label)];

                if (s.Contains('-'))
                    box.Lenses.RemoveAll(l => l.Label == label);
                else
                {
                    var f = int.Parse(values[1]);
                    var lens = box.Lenses.FirstOrDefault(b => b.Label == label);
                    if (lens != null)
                        lens.FocalLength = f;
                    else
                        box.Lenses.Add(new Lens() { Label = label, FocalLength = f });
                }
            }

            return boxes.Select((t, i) => t.Lenses.Sum(lens => (i + 1) * (t.Lenses.IndexOf(lens) + 1) * lens.FocalLength)).Sum();
        }

        private class Box
        {
            public readonly List<Lens> Lenses = new();
        }

        private class Lens
        {
            public string? Label { get; init; }
            public int FocalLength { get; set; }
        }
    }
}