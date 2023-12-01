using AOC2023;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        var d1 = new Day01();
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine($"Day 01 Part 1 = {d1.Part1()} ({sw.Elapsed.Microseconds} µs)");
        sw.Restart();
        Console.WriteLine($"Day 01 Part 2 = {d1.Part2()} ({sw.Elapsed.Microseconds} µs)");
        sw.Stop();
    }
}