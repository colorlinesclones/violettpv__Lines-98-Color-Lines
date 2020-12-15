using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linii
{
    class Program
    {
        public static char[,] pole = new char[9, 9];     // масив 9 на 9
        public static Random r = new Random();           // для рандому кульок
        public static char[] color = { 'B', 'G', 'Y', 'M', 'C', 'R', 'H', 'D', 'F' };    // змінна для кольорів
        public static int points = 0;       // для подальшого нарахунку балів
        public static bool findPath = false;
        public static bool noGenerate = false;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;   // змінюємо кодування консолі для коректного відображення символів 
            Console.SetWindowSize(53, 30);
            ClearP.ClearPole();
            while (true)
            {
                ShowPole.Show();   // виводить поле

                if (Movement.Move() == false)         // неправильно виконано переміщення кульки
                {
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                    Console.WriteLine("Try again");
                    Console.ReadKey();
                    ClearP.ClearPole();
                }

                if (Generation.Generate() == false)    // поле заповнене кульками/неправильна генерація = програш 
                {
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                    Console.WriteLine("Try again");
                    Console.ReadKey();
                    ClearP.ClearPole();
                }
            }
        }
    }

    class Check
    {
       public static void Chk(int x, int y)
        {
            if (Program.pole[x, y] == ' ')     // перевірка, що клітинка пуста 
            {
                return;
            }

            // Horizontal  
            int count = 0;
            int k = 1;
            int m = 0;
            int[] tmp = new int[9];       // створюємо масив для горизонтальної лінії поля 
            for (int i = 0; i < 9; i++)  { tmp[i] = -1; }  // привоюємо "і" значення -1
            bool left = true;
            bool right = true;

            for (int i = 0; i < 9; i++)  
            {
                if (y + k < 9 && right == true)   // перевірка по праву сторону горизонтально
                {
                    if (Program.pole[x, y] == Program.pole[x, y + k])
                    {
                        tmp[m] = y + k;
                        m++;
                        count++;
                    }
                    else
                    {
                        right = false;
                    }
                }
                if (y - k >= 0 && left == true)   // перевірка по ліву сторону горизонтально
                {
                    if (Program.pole[x, y] == Program.pole[x, y - k])
                    {
                        tmp[m] = y - k;
                        m++;
                        count++;
                    }
                    else
                    {

                        left = false;
                    }
                }
                k++;
            }

            if (count >= 4)   // якщо 4 клітинки зліва || справа, залишаємо клітинку пустою
            {
                Program.pole[x, y] = ' ';
                for (int i = 0; i < 9; i++)
                {
                    if (tmp[i] != -1)
                    {
                        Program.pole[x, tmp[i]] = ' ';
                    }
                }

                Program.points += (count + 1) * 2;  // нараховуємо бали
                Program.noGenerate = true;          // вдала генерація не треба генерувати нові три
                return;
            }

            // Vertical 
            count = 0;
            k = 1;
            m = 0;
            for (int i = 0; i < 9; i++) { tmp[i] = -1; }
            left = true;
            right = true;

            for (int i = 0; i < 9; i++)
            {
                if (x + k < 9 && right == true)
                {
                    if (Program.pole[x, y] == Program.pole[x + k, y])
                    {
                        tmp[m] = x + k;
                        m++;
                        count++;
                    }
                    else
                    {
                        right = false;
                    }
                }
                if (x - k >= 0 && left == true)
                {
                    if (Program.pole[x, y] == Program.pole[x - k, y])
                    {
                        tmp[m] = x - k;
                        m++;
                        count++;
                    }
                    else
                    {

                        left = false;
                    }
                }
                k++;
            }

            if (count >= 4)
            {
                Program.pole[x, y] = ' ';
                for (int i = 0; i < 9; i++)
                {
                    if (tmp[i] != -1)
                    {
                        Program.pole[tmp[i], y] = ' ';
                    }
                }

                Program.points += (count + 1) * 2;
                Program.noGenerate = true;
                return;
            }

            // Diagonal -- /    
            count = 0;
            k = 1;
            m = 0;
            for (int i = 0; i < 9; i++) { tmp[i] = -1; }
            left = true;
            right = true;

            for (int i = 0; i < 9; i++)
            {
                if (x - k >= 0 && y + k < 9 && right == true)
                {
                    if (Program.pole[x, y] == Program.pole[x - k, y + k])
                    {
                        tmp[m] = x - k;
                        m++;
                        count++;
                    }
                    else
                    {
                        right = false;
                    }
                }
                if (x + k < 9 && y - k >= 0 && left == true)
                {
                    if (Program.pole[x, y] == Program.pole[x + k, y - k])
                    {
                        tmp[m] = x + k;
                        m++;
                        count++;
                    }
                    else
                    {
                        left = false;
                    }
                }
                k++;
            }

            if (count >= 4)
            {
                Program.pole[x, y] = ' ';
                for (int i = 0; i < 9; i++)
                {
                    if (tmp[i] != -1)
                    {
                        Program.pole[tmp[i], y - (tmp[i] - x)] = ' ';
                    }
                }
                Program.points += (count + 1) * 2;
                Program.noGenerate = true;
                return;
            }

            // Diagonal -- \     
            count = 0;
            k = 1;
            m = 0;
            for (int i = 0; i < 9; i++) { tmp[i] = -1; }
            left = true;
            right = true;

            for (int i = 0; i < 9; i++)
            {
                if (x + k < 9 && y + k < 9 && right == true)
                {
                    if (Program.pole[x, y] == Program.pole[x + k, y + k])
                    {
                        tmp[m] = x + k;
                        m++;
                        count++;
                    }
                    else
                    {
                        right = false;
                    }
                }
                if (x - k >= 0 && y - k >= 0 && left == true)
                {
                    if (Program.pole[x, y] == Program.pole[x - k, y - k])
                    {
                        tmp[m] = x - k;
                        m++;
                        count++;
                    }
                    else
                    {
                        left = false;
                    }
                }
                k++;
            }

            if (count >= 4)  //
            {
                Program.pole[x, y] = ' ';
                for (int i = 0; i < 9; i++)
                {
                    if (tmp[i] != -1)
                    {
                        Program.pole[tmp[i], y + (tmp[i] - x)] = ' ';
                    }
                }
                Program.points += (count + 1) * 2;
                Program.noGenerate = true;
                return;
            }
        }
    } 
 
    class Movement
    {
        public static bool Move()
        {
            Console.WriteLine("From: ");
            string h1 = Console.ReadLine();
            h1 = h1.ToLower();                // конвертуємо на маленькі літери
            Console.WriteLine("To: ");
            string h2 = Console.ReadLine();
            h2 = h2.ToLower();

            if (h1.Length != 2 || h2.Length != 2)   // якщо менше двох символів == помилка
            {
                Console.WriteLine("Error input!");
                Console.ReadKey();
            }
            else
            {
                int x1, y1, x2, y2;
                char x1a = h1[0];
                char x2a = h2[0];
                if (char.IsDigit(x1a) && char.IsDigit(x2a)) // перевірка char - Digit, число
                {
                    x1 = Convert.ToInt32(Convert.ToString(x1a));  // спочатку конвертуємо у стрінг, а потім у int
                    x2 = Convert.ToInt32(Convert.ToString(x2a));
                    x1--;     // значення -1
                    x2--;
                    y1 = Convertss.Convertikus(h1[1]);  // користуємось класом "Convertss" для конвертування літери у int для вводу координат
                    y2 = Convertss.Convertikus(h2[1]);
                    if (y1 == -1 || y2 == -1)           // якщо значення дорівнює -1 == помилка
                    {
                        Console.WriteLine("Error input!");
                        Console.ReadKey();
                    }
                    else if (Program.pole[x1, y1] == ' ')   // якщо нічого не ввели == помилка
                    {
                        Console.WriteLine("Error input!");
                        Console.ReadKey();
                    }
                    else if (Program.pole[x2, y2] != ' ')   // якщо при вводі щось інше == помилка
                    {
                        Console.WriteLine("Error input!");
                        Console.ReadKey();
                    }
                    else
                    {
                        // Find path (+)
                        Path.isPath(x1, y1, x2, y2);   // викликаємо клас "Path", щоб перевірити чи вільний шлях

                        // Clear (+)
                        for (int i = 0; i < 9; i++)   //для тесту перевірки вільного шляху
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (Program.pole[i, j] == '+')
                                {
                                    Program.pole[i, j] = ' ';
                                }
                            }
                        }

                        if (Program.findPath == true)  // якщо шлях вільний, перевіряємо за класом "Check"
                        {
                            Program.findPath = false;
                            Program.pole[x2, y2] = Program.pole[x1, y1];
                            Program.pole[x1, y1] = ' ';
                            Check.Chk(x2, y2);
                        }
                        else  // помилка, немає вільного шляху
                        {
                            Program.noGenerate = true;
                            Console.WriteLine("No path!");
                            Console.ReadKey();
                            return true;     // return false; починало гру спочатку *виправлено*
                        }


                    }
                }
                else
                {
                    Console.WriteLine("Error input!");
                    Console.ReadKey();
                }
            }
            return true;
           
        }
    }

    class Path
    {
       public static void isPath(int x, int y, int x2, int y2)  // метод рекурсії  
        {
            if (x < 8)    // чи є ще 8 клітинок
            {
                if (x + 1 == x2 && y == y2)  
                {
                    Program.findPath = true;
                }
                if (Program.pole[x + 1, y] == ' ')
                {
                    Program.pole[x + 1, y] = '+';
                    isPath(x + 1, y, x2, y2); // викликає сама себе
                }

            }
            if (y < 8)
            {
                if (x == x2 && y + 1 == y2)
                {
                    Program.findPath = true;
                }
                if (Program.pole[x, y + 1] == ' ')
                {
                    Program.pole[x, y + 1] = '+';
                    isPath(x, y + 1, x2, y2);
                }
            }
            if (x > 0)
            {
                if (x - 1 == x2 && y == y2)
                {
                    Program.findPath = true;
                }
                if (Program.pole[x - 1, y] == ' ')
                {
                    Program.pole[x - 1, y] = '+';
                    isPath(x - 1, y, x2, y2);
                }
            }
            if (y > 0)
            {
                if (x == x2 && y - 1 == y2)
                {
                    Program.findPath = true;
                }
                if (Program.pole[x, y - 1] == ' ')
                {
                    Program.pole[x, y - 1] = '+';
                    isPath(x, y - 1, x2, y2);
                }
            }
        }
    }

    class Convertss
    {
        public static int Convertikus(char c)
        {
            int rez = -1;
            switch (c)
            {
                case 'a':
                    rez = 0;
                    break;
                case 'b':
                    rez = 1;
                    break;
                case 'c':
                    rez = 2;
                    break;
                case 'd':
                    rez = 3;
                    break;
                case 'e':
                    rez = 4;
                    break;
                case 'f':
                    rez = 5;
                    break;
                case 'g':
                    rez = 6;
                    break;
                case 'h':
                    rez = 7;
                    break;
                case 'i':
                    rez = 8;
                    break;
                case 'j':
                    rez = 9;
                    break;
            }
            return rez;
        }
    }

    class Generation
    {
        public static bool Generate()  // генерування кульок
        {
            if (Program.noGenerate == true)
            {
                Program.noGenerate = false;
                return true;
            }

            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Program.pole[i, j] == ' ')  // перевірка наявності пустої клітинки
                    {
                        count++;
                    };
                }
            }

            if (count >= 3)
            {
                count = 0;
                int x, y, c;
                while (count < 3)
                {
                    x = Program.r.Next(0, 9); 
                    y = Program.r.Next(0, 9);
                    c = Program.r.Next(0, 9);
                    if (Program.pole[x, y] == ' ')
                    {
                        Program.pole[x, y] = Program.color[c];  // рандом кольору
                        Check.Chk(x, y);  // перевірка
                        count++;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class ClearP
    {
        public static void ClearPole()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Program.pole[i, j] = ' ';  // заповнює масив пустими клітинками
                }
            }

            //----------------    T E S T   ----------------
            //for (int i = 2; i < 7; i++)
            //{
            //    pole[i, 1] = 'Y';
            //    pole[i, 2] = 'Y';
            //    pole[i, 3] = 'Y';
            //    pole[i, 4] = 'Y';
            //}
            //----------------    /T E S T   ----------------


            int count = 0;
            int x, y, c;
            while (count < 3)   // перевірка щоб з'явилися лише три кульки
            {
                x = Program.r.Next(0, 9);  // рядки
                y = Program.r.Next(0, 9);  // стопчик
                c = Program.r.Next(0, 9);  // колір
                if (Program.pole[x, y] == ' ')
                {
                    Program.pole[x, y] = Program.color[c]; 
                    count++;
                }
            }
        }
    }

    class ShowPole
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Points: " + Program.points);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" +----A----B----C----D----E----F----G----H----I----+ ");
            Console.WriteLine(" |                                                 | ");
            for (int i = 0; i < 9; i++)
            {
                Console.Write(" " + (i + 1) + "  ");     // після перевірки масиву, передаємо символам кольори
                for (int j = 0; j < 9; j++)
                {
                    if (Program.pole[i, j] == 'R')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'B')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'G')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'Y')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'D')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == 'F')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("  ●  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Program.pole[i, j] == ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("     ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //if (Program.pole[i, j] == '+')
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write("  +  ");
                    //    Console.ForegroundColor = ConsoleColor.White;
                    //}

                }
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  | ");
                Console.WriteLine(" |                                                 | ");
            }
            Console.WriteLine(" +-------------------------------------------------+ ");
            Console.BackgroundColor = ConsoleColor.Black;

        }
    }
}

