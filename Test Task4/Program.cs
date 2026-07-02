using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("nums.txt");

        int[] nums = new int[lines.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = int.Parse(lines[i]);
        }
        Array.Sort(nums);

        int median = nums[nums.Length / 2];

        int movies = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            movies += Math.Abs(nums[i] - median);
        }

        if (movies <= 20) Console.WriteLine(movies);
        else Console.WriteLine("20 moves are not enough");
}
}
