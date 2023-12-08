﻿using AOC2023;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        var d1 = new Day01();
        Stopwatch sw = new Stopwatch();
        Console.WriteLine("Day 01");
        sw.Start();
        Console.WriteLine($"    Part 1 = {d1.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d1.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        
        var d2 = new Day02();
        Console.WriteLine("Day 02");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d2.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d2.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");
        
        var d3 = new Day03();
        Console.WriteLine("Day 03");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d3.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d3.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        var d4 = new Day04();
        Console.WriteLine("Day 04");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d4.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d4.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        var d5 = new Day05p2();
        var d5bf = new Day05p1();
        Console.WriteLine("Day 05");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d5bf.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d5.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        var d6 = new Day06();
        Console.WriteLine("Day 06");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d6.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d6.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        var d7 = new Day07();
        Console.WriteLine("Day 07");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d7.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d7.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        var d8 = new Day08();
        Console.WriteLine("Day 08");
        sw.Restart();
        Console.WriteLine($"    Part 1 = {d8.Part1()} ({sw.Elapsed.TotalMilliseconds} ms)");
        sw.Restart();
        Console.WriteLine($"    Part 2 = {d8.Part2()} ({sw.Elapsed.TotalMilliseconds} ms)\n");

        sw.Stop();
    }
}