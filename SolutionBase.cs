namespace AOC2023
{
    internal abstract class SolutionBase
    {
        public SolutionBase(int day)
        {
            Day = day;
            PuzzleInput = File.ReadAllText($"{day}.txt");
        }

        public int Day { get; set; }

        public string PuzzleInput { get; set; }

    }
}
