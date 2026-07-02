using System;

class programm
{
    static void Main(string[] args)
    {
        int n1 = int.Parse(args[0]);
        int m1 = int.Parse(args[1]);
        int n2 = int.Parse(args[2]);
        int m2 = int.Parse(args[3]);

        finalarray(n1, m1);
        Console.Write(" ");
        finalarray(n2, m2);
    }
    static void finalarray(int n, int m)
    {
        int [] array = new int [n];
        for (int i = 0; i < n; i++)
        {
            array [i] = i + 1;
        }
        int current = 0;
        Console.Write(array[0]);
        
        while(true)
        {
            current = (current + m - 1) % n;
            if (current == 0) break;
            Console.Write (" " + array[current]);
}
}
}
