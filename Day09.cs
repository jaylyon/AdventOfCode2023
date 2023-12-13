namespace AOC2023
{
    internal class Day09

    {
        private static readonly string input = File.ReadAllText(@"Input\09.txt");

        private List<List<int>> Report = new();

        public Day09()
        {
            foreach (var line in input.Split(Environment.NewLine))
            {
                Report.Add(line.Split(' ').Select(int.Parse).ToList());
            }
        }

        internal int Part1()
        {
            foreach (var valueList in Report) Extrapolate(valueList);
            return Report.Select(v => v.Last()).Sum();
        }

        internal int Part2()
        {
            return Report.Select(v => v.First()).Sum();
        }

        private static void Extrapolate(IList<int> valueList)
        {
            if (valueList.All(v => v == 0)) return;
            List<int> newValueList = new();
            for (var i = 0; i < valueList.Count - 1; i++)
            {
                newValueList.Add(valueList[i + 1] - valueList[i]);
            }
            Extrapolate(newValueList);
            valueList.Add(valueList.Last() + newValueList.Last());
            valueList.Insert(0, valueList[0] - newValueList[0]);
        }
    }
}