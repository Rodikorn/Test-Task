using System;
using System.IO;

class Programm
{
    static void Main (string[] args)
    {
        String Ellipse = "ellipse.txt";
        String Points = "points.txt";

        string[] coordinates = File.ReadAllLines (Ellipse);
        string[] center = coordinates[0].Split(' ');
        string[] radius = coordinates[1].Split(' ');

        float x0 = float.Parse(center[0]);
        float y0 = float.Parse(center[1]);
        float r1 = float.Parse(radius[0]);
        float r2 = float.Parse(radius[1]);

        string[] pointcoordinates = File.ReadAllLines (Points);

        foreach (string line in pointcoordinates)
        {
            string[] coords = line.Split(' ');
            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);

            float value = ((x - x0)*(x - x0)) / (r1*r1) + ((y - y0)*(y - y0)) / (r2*r2);

            if (Math.Abs(value - 1F) < 1e-7F) Console.WriteLine(0);
            else if (value < 1f) Console.WriteLine(1);
            else Console.WriteLine(2);
        }

}
}