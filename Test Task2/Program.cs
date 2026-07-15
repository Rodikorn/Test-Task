using System;
using System.IO;

class Program
{
    static void Main (string[] args)
    {
        if (args.Length != 2)
        {
            return;
        }
        try
        {
            string[] Ellipse = File.ReadAllLines(args[0]);
            string[] center = Ellipse[0].Split(' ');
            string[] radius = Ellipse[1].Split(' ');

            double x0 = double.Parse(center[0]);
            double y0 = double.Parse(center[1]);
            double r1 = double.Parse(radius[0]);
            double r2 = double.Parse(radius[1]);

            string[] pointcoordinates = File.ReadAllLines(args[1]);

            foreach (string line in pointcoordinates)
            {
                string[] coords = line.Split(' ');
                double x = double.Parse(coords[0]);
                double y = double.Parse(coords[1]);

                double value = ((x - x0) * (x - x0)) / (r1 * r1) + ((y - y0) * (y - y0)) / (r2 * r2);

                if (Math.Abs(value - 1F) < 1e-9F) Console.WriteLine(0);
                else if (value < 1f) Console.WriteLine(1);
                else Console.WriteLine(2);
            }
        }
        catch
        {
            Console.WriteLine("Ошибка");
            return;
        }
}
}