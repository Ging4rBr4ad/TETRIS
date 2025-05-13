using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace тетрис
{
    internal class тетрис
    {
        // 0 - ПУСТАЯ КЛЕТКА
        // 1 - ЛЕТЯЩАЯ ФИГУРА
        // 2 - СТОЯЩАЯ ФИГУРА

            // координаты "нижнего края" фигуры
            static int X = 0; // высота
            static int Y = 0; // широта

        class Field
        {
            public int width { get; set; }
            public int height { get; set; }
            int[,] field;
            public int score { get; set; }

            public Field(int heighT, int widtH)
            {
                height = heighT;
                width = widtH;
                field = new int[height, width];

                for(int i = 0; i < height - 1; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        field[i, j] = 0;
                    }
                }
            }

            public int this[int i, int y]
            {
                get
                {
                    return field[i, y];
                }
                set
                {
                    field[i, y] = value;
                }
            }

            public void Clear()
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        field[i, j] = 0;
                    }
                }
            }


            // переписанный MoveDown
            public bool MoveDown(ref Shape shape)
            {
                bool move = true;

                //можно ли подвинуть фигуру вниз
                int k = 0;
                for (; k < shape.size; k++)
                {
                    if (shape[k].Item1 + 1 != height) // если фигура не на дне
                    {
                        if (field[shape[k].Item1 + 1, shape[k].Item2] == 2) //если место занято,
                        { move = false; break; }
                    }
                    else
                    { move = false; break; }
                }
 
                // то движение вниз,
                    if (move)
                {
                    for (int i = 0; i < shape.size; i++)
                    {
                        field[shape[i].Item1, shape[i].Item2] -= 1;
                        shape[i].Item1++;
                        field[shape[i].Item1, shape[i].Item2] += 1;
                    }
                    X++;
                }
                else
                // иначе - ЗАВЕРШИТЬ ДВИЖЕНИЕ
                {
                    IsFilled(shape);
                    // фигура остановилась
                    for (int i = 0; i < shape.size; i++)
                    {
                        field[shape[i].Item1, shape[i].Item2] = 2;
                    }
                    shape.SetShape(this);
                    return true;
                }
                return false;
            }

            public void MoveRight(Shape shape)
            {
                bool move = true;

                // можно ли подвинуть фигуру вправо
                for (int i = 0; i < shape.size; i++)
                {
                    if (shape[i].Item2 + 1 != width) // если фигура не на правом краю
                    {
                        if (field[shape[i].Item1, shape[i].Item2 + 1] == 2) //то проверить, занято ли там
                        {
                            move = false; break;
                        }
                    }
                    else
                    { move = false; break; }
                }
                //движение вправо
                    if (move)
                {
                    Y++;
                    for (int i = 0; i < shape.size; i++)
                    {
                        field[shape[i].Item1, shape[i].Item2] -= 1;
                        shape[i].Item2++;
                        field[shape[i].Item1, shape[i].Item2] += 1;
                    }
                }
            }

            public void MoveLeft(Shape shape)
            {
                bool move = true;

                // можно ли подвинуть фигуру вправо
                for (int i = 0; i < shape.size; i++)
                {
                    if (shape[i].Item2 - 1 != -1) // если фигура не на правом краю
                    {
                        if (field[shape[i].Item1, shape[i].Item2 - 1] == 2) //то проверить, занято ли там
                        {
                            move = false; break;
                        }
                    }
                    else
                    { move = false; break; }
                }
                //движение влево
                if (move)
                {
                    Y--;
                    for (int i = 0; i < shape.size; i++)
                    {
                        field[shape[i].Item1, shape[i].Item2] -= 1;
                        shape[i].Item2--;
                        field[shape[i].Item1, shape[i].Item2] += 1;
                    }
                }
            }

            public void Turn(Shape shape)
            {

            }

            /*// углы
                if (field[X - 1, Y - 1] == 1)
                { field[X - 1, Y - 1] = 0; field[X - 1, Y + 1] = 1; }

                if (field[X - 1, Y + 1] == 1)
                { field[X - 1, Y + 1] = 0; field[X + 1, Y + 1] = 1; }

                if (field[X + 1, Y + 1] == 1)
                { field[X + 1, Y + 1] = 0; field[X + 1, Y - 1] = 1; }

                if (field[X + 1, Y - 1] == 1)
                { field[X + 1, Y - 1] = 0; field[X - 1, Y - 1] = 1; }

                // стороны
                if (field[X, Y - 1] == 1)
                { field[X, Y - 1] = 0; field[X - 1, Y] = 1; }

                if (field[X - 1, Y] == 1)
                { field[X - 1, Y] = 0; field[X, Y + 1] = 1; }

                if (field[X, Y + 1] == 1)
                { field[X, Y + 1] = 0; field[X + 1, Y] = 1; }

                if (field[X + 1, Y] == 1)
                { field[X + 1, Y] = 0; field[X, Y - 1] = 1; }*/

            public void Show()
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("---TETRIS---\n");
                for (int i = 0; i < height; i++)
                {
                    Console.Write("| ");
                    for (int k = 0; k < width; k++)
                    {
                        if (field[i, k] == 0)
                            Console.Write(".");
                        else if (field[i, k] == 1)
                            Console.Write("1");
                        else if (field[i, k] == 2)
                            Console.Write("2");

                        Console.Write(" ");
                    }
                    Console.WriteLine("|");
                }
                    Console.WriteLine("-------------------");
                Console.WriteLine("Score: " + score);
            }

            public bool IsLost()
            {
                bool Lost = false;

                for(int i = 0; i < width; i++)
                {
                    if (field[1, i] == 2)
                    { Lost = true; break; }
                }

                return Lost;
            }

            public void IsFilled(Shape shape)
            {
                for (int i = 0; i < height; i++)
                {
                    bool cleanLine = true;

                    for (int k = 0; k < width; k++)
                    {
                        if (field[i, k] == 0)
                        { cleanLine = false; break; }
                    }

                    if(cleanLine)
                    {
                        for (int k = i; k != 0; k--)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                field[k, j] = field[k - 1, j];
                            }
                        }
                        for (int j = 0; j < width; j++)
                            field[0, j] = 0;
                        score += 50;
                    }
                }
            }

        }

        class Shape
        {
            Pair[] shape; // одномерный массив с координатами фигуры
            public int size { get; set; } //количество координат фигуры
            public Shape()
            {
            }

            public Pair this[int idx]
            {
                get
                {
                    return shape[idx];
                }
                set
                {
                    shape[idx] = value;
                }
            }

            public void SetShape(Field field)
            {
                Random rand = new Random();
                switch (rand.Next(6))
                {
                    case 0:
                        size = 4;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 0);
                        shape[1] = new Pair(0, 1);
                        shape[2] = new Pair(1, 0);
                        shape[3] = new Pair(1, 1);
                        break;
                    case 1:
                        size = 3;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 0);
                        shape[1] = new Pair(1, 0);
                        shape[2] = new Pair(2, 0);
                        break;
                    case 2:
                        size = 4;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 1);
                        shape[1] = new Pair(1, 0);
                        shape[2] = new Pair(1, 1);
                        shape[3] = new Pair(1, 2);
                        break;
                    case 3:
                        size = 4;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 0);
                        shape[1] = new Pair(0, 1);
                        shape[2] = new Pair(0, 2);
                        shape[3] = new Pair(1, 2);
                        break;
                    case 4:
                        size = 4;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 0);
                        shape[1] = new Pair(0, 1);
                        shape[2] = new Pair(1, 1);
                        shape[3] = new Pair(1, 2);
                        break;
                    case 5:
                        size = 5;
                        shape = new Pair[size];
                        shape[0] = new Pair(0, 0);
                        shape[1] = new Pair(1, 0);
                        shape[2] = new Pair(1, 1);
                        shape[3] = new Pair(1, 2);
                        shape[4] = new Pair(0, 2);
                        break;
                }

                X = 1; Y = 1;

                for (int h = 0; h < size; h++)
                    field[shape[h].Item1, shape[h].Item2] = 1;
            }

            public class Pair
            {
                public int Item1 { get; set; }
                public int Item2 { get; set; }
                public Pair(int x, int y)
                {
                    Item1 = x;
                    Item2 = y;
                }
            }

            static void Main(string[] args)
            {
                const int height = 15, width = 8;
                Field field = new Field(height, width);
                Shape shape = new Shape();
                shape.SetShape(field);

                while (true)
                {
                    field.Show();
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        switch (key)
                        {
                            case ConsoleKey.D:
                                field.MoveRight(shape);
                                break;
                            case ConsoleKey.A:
                                field.MoveLeft(shape);
                                break;
                            case ConsoleKey.S:
                                field.MoveDown(ref shape);
                                break;
                            case ConsoleKey.W:
                                field.Turn(shape);
                                break;
                        }
                    }
                    Thread.Sleep(500);
                    if (field.MoveDown(ref shape))
                    {
                        if (field.IsLost())
                        {
                            Console.WriteLine("Game Over!\nFinal score: " + field.score);
                            break;
                        }
                    }

                }
            }
        }
    }
}