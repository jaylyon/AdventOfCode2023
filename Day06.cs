using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC2023
{
    internal class Day06
    {
        private const string input = @"Time:        41     96     88     94
Distance:   214   1789   1127   1055";
        private int[]? Times;
        private int[]? Distances;

        internal long Part1()
        {
            var lines = input.Split(Environment.NewLine);
            Times = Regex.Matches(lines[0], @"\d+").Select(m => int.Parse(m.Value)).ToArray();
            Distances = Regex.Matches(lines[1], @"\d+").Select(m => int.Parse(m.Value)).ToArray();
            long answer = 1;
            for (int i = 0; i < Times.Length; i++) { answer *= GetWinners(Times[i], Distances[i]); }
            return answer;
        }

        internal long Part2()
        {
            return GetWinners(41968894, 214178911271055);
        }

        long GetWinners(long time, long distance)
        {
            double t = Convert.ToDouble(time);
            double d = Convert.ToDouble(distance + 1);
            double root1 = (-t + Math.Sqrt(Math.Pow(t, 2d) - 4d * d)) / -2d; 
            double root2 = (-t - Math.Sqrt(Math.Pow(t, 2d) - 4d * d)) / -2d;
            return (long)(Math.Floor(root2) - Math.Ceiling(root1) + 1);
        }
    }
}