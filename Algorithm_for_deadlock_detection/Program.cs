using System;

class Program
{
    static int[,] max = new int[100, 100];
    static int[,] alloc = new int[100, 100];
    static int[,] need = new int[100, 100];
    static int[] avail = new int[100];
    static int n, r;

    static void Main()
    {
        Console.WriteLine("********** Deadlock Detection Algo ************");
        Input();
        Show();
        Cal();
        Console.ReadKey();
    }

    static void Input()
    {
        Console.Write("Enter the number of Processes: ");
        n = int.Parse(Console.ReadLine());
        Console.Write("Enter the number of resources instances: ");
        r = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the Max Matrix:");
        for (int i = 0; i < n; i++)
        {
            string[] temp = Console.ReadLine().Split(' ');
            for (int j = 0; j < r; j++)
            {
                max[i, j] = int.Parse(temp[j]);
            }
        }
        Console.WriteLine("Enter the Allocation Matrix:");
        // Nhập ma trận phân bổ (Nhập vào từng phần tử của ma trận, mỗi lần nhập vào là một hàng trông ma trận, ví dụ 0 1 0)
        for (int i = 0; i < n; i++)
        {
            string[] temp = Console.ReadLine().Split(' ');
            for (int j = 0; j < r; j++)
            {
                alloc[i, j] = int.Parse(temp[j]);
            }
        }
        Console.WriteLine("Enter the available Resources:");
        string[] strings = Console.ReadLine().Split(' ');
        for (int j = 0; j < r; j++)
        {
            avail[j] = int.Parse(strings[j]);
        }
    }

    static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("Process\t\tAllocation\t\tMax\t\t\tAvailable");
        for (int i = 0; i < n; i++)
        {
            Console.Write("\nP" + (i + 1) + "\t");
            for (int j = 0; j < r; j++)
            {
                Console.Write("\t" + alloc[i, j]);
            }
            Console.Write("\t");
            for (int j = 0; j < r; j++)
            {
                Console.Write(max[i, j] + "\t");
            }
            if (i == 0)
            {
                for (int j = 0; j < r; j++)
                {
                    Console.Write(avail[j] + "\t");
                }
            }
        }
        Console.WriteLine();
    }

    static void Cal()
    {
        int[] finish = new int[100];
        int[,] temp;
        int flag = 1;
        int k, c1 = 0;
        int[] dead = new int[100];
        int[] safe = new int[100];
        int i, j;

        for (i = 0; i < n; i++)
        {
            finish[i] = 0;
        }

        // Tìm ma trận nhu cầu
        for (i = 0; i < n; i++)
        {
            for (j = 0; j < r; j++)
            {
                need[i, j] = max[i, j] - alloc[i, j];
            }
        }

        while (flag != 0)
        {
            flag = 0;
            for (i = 0; i < n; i++)
            {
                int c = 0;
                for (j = 0; j < r; j++)
                {
                    if (finish[i] == 0 && need[i, j] <= avail[j])
                    {
                        c++;
                        if (c == r)
                        {
                            for (k = 0; k < r; k++)
                            {
                                avail[k] += alloc[i, j];
                                finish[i] = 1;
                                flag = 1;
                            }
                            if (finish[i] == 1)
                            {
                                i = n;
                            }
                        }
                    }
                }
            }
        }

        j = 0;
        flag = 0;
        for (i = 0; i < n; i++)
        {
            if (finish[i] == 0)
            {
                dead[j] = i;
                j++;
                flag = 1;
            }
        }

        if (flag == 1)
        {
            Console.WriteLine();
            Console.WriteLine("\n\nSystem is in Deadlock and the Deadlock process are");
            for (i = 0; i < j; i++)
            {
                Console.Write("P" + (dead[i] + 1) + "\t");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("\nNo Deadlock Occur");
        }
    }
}
