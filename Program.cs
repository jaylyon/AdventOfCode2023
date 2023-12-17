using AOC2023;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        int today = DateTime.Today.Day;

        Stopwatch sw = new Stopwatch();

        if (today == 1)
        {
            var d1 = new Day01();
            Console.WriteLine("Day 01");
            sw.Start();
            Console.WriteLine($"    Part 1 = {d1.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d1.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 2)
        {
            var d2 = new Day02();
            Console.WriteLine("Day 02");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d2.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d2.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 3)
        {
            var d3 = new Day03();
            Console.WriteLine("Day 03");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d3.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d3.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 4)
        {
            var d4 = new Day04();
            Console.WriteLine("Day 04");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d4.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d4.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 5)
        {
            var d5 = new Day05p2();
            var d5bf = new Day05p1();
            Console.WriteLine("Day 05");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d5bf.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d5.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 6)
        {
            var d6 = new Day06();
            Console.WriteLine("Day 06");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d6.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d6.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 7)
        {
            var d7 = new Day07();
            Console.WriteLine("Day 07");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d7.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d7.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 1)
        {
            var d8 = new Day08();
            Console.WriteLine("Day 08");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d8.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d8.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 9)
        {
            var d9 = new Day09();
            Console.WriteLine("Day 09");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d9.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d9.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 10)
        {
            var d10 = new Day10();
            Console.WriteLine("Day 10");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d10.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d10.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 11)
        {
            var d11 = new Day11();
            Console.WriteLine("Day 11");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d11.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d11.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 12)
        {
            var d12 = new Day12();
            Console.WriteLine("Day 12");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d12.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d12.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 13)
        {
            var d13 = new Day13();
            Console.WriteLine("Day 13");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d13.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d13.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 14)
        {
            var d14 = new Day14();
            Console.WriteLine("Day 14");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d14.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d14.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 15)
        {
            var d15 = new Day15();
            Console.WriteLine("Day 15");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d15.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d15.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 16)
        {
            var d16 = new Day16();
            Console.WriteLine("Day 16");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d16.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d16.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        if (today == 17)
        {
            var d17 = new Day17();
            Console.WriteLine("Day 17");
            sw.Restart();
            Console.WriteLine($"    Part 1 = {d17.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
            sw.Restart();
            Console.WriteLine($"    Part 2 = {d17.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        }

        sw.Stop();
    }
}