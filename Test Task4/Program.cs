using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
            return;

        try
        {
            string[] lines = File.ReadAllLines(args[0]);

            int[] nums = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                nums[i] = int.Parse(lines[i]);
            }

            Array.Sort(nums);
            int median = nums[nums.Length / 2];
            int moves = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                moves += Math.Abs(nums[i] - median);
            }

            if (moves <= 20)
                Console.WriteLine(moves);
            else
                Console.WriteLine("20 ходов недостаточно для приведения всех элементов массива к одному числу");
        }
        catch
        {
            Console.WriteLine("Ошибка");
            return;
        }
    }
}