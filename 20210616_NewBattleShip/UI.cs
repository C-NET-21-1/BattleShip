using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    class UI
    {
        public static void ShowField(Field field, int left, int top)
        {
            Console.SetCursorPosition(left, top);

            for (int i = 0; i < field.playingField.GetLength(0); i++)
            {
                for (int j = 0; j < field.playingField.GetLength(1); j++)
                {
                    Console.Write("{0}  ", field.playingField[i, j]);
                }
                top++;
                Console.SetCursorPosition(left, top);
            }
        }

        public static void ShowPositionDeck(Cell deck)
        {
            Console.SetCursorPosition(0, 15);
            Console.Write(deck.topPosition);
            Console.SetCursorPosition(0, 16);
            Console.Write(deck.leftPosition);
        }

        public static void ShowOrientationShip(Ship ship)
        {
            Console.SetCursorPosition(0, 17);
            Console.Write(ship.route);
        }

        public static void ShowAllowed(bool allowed)
        {
            Console.SetCursorPosition(0, 18);
            Console.Write(allowed);
        }

        public static void Score(Field hero, Field enemy)
        {
            Console.SetCursorPosition(0, 20);
            Console.Write("   ");
            Console.SetCursorPosition(0, 21);
            Console.Write("   ");

            Console.SetCursorPosition(0, 20);
            Console.Write(hero.counterDeck);
            Console.SetCursorPosition(0, 21);
            Console.Write(enemy.counterDeck);
        }

        public static void SetPostitionDeck(ref Cell newDeck, ConsoleKey key, Orientation route, int sizeField, int counterDeck)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newDeck.topPosition--;

                    if (newDeck.topPosition < 0)
                    {
                        newDeck.topPosition = 0;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    newDeck.topPosition++;

                    if(route == Orientation.Horizontal)
                    {
                        if (newDeck.topPosition > sizeField - 1)
                        {
                            newDeck.topPosition = sizeField - 1;
                        }
                    }
                    else
                    {
                        if (newDeck.topPosition > sizeField - counterDeck)
                        {
                            newDeck.topPosition = sizeField - counterDeck;
                        }
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    newDeck.leftPosition--;

                    if (newDeck.leftPosition < 0)
                    {
                        newDeck.leftPosition = 0;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    newDeck.leftPosition++;

                    if (route == Orientation.Horizontal)
                    {
                        if (newDeck.leftPosition > sizeField - counterDeck)
                        {
                            newDeck.leftPosition = sizeField - counterDeck;
                        }
                    }
                    else
                    {
                        if (newDeck.leftPosition > sizeField - 1)
                        {
                            newDeck.leftPosition = sizeField - 1;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        public static void SetRouteShip(ref Ship ship, ConsoleKey key)
        {
            if(key == ConsoleKey.Spacebar)
            {
                if (ship.route == Orientation.Vertikal)
                {
                    ship.route = Orientation.Horizontal;
                }
                else
                {
                    ship.route = Orientation.Vertikal;
                }
            }
        }

        public static void MoveCursor(ConsoleKey key, ref Cell cursor)
        {
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    cursor.topPosition--;
                    if(cursor.topPosition < 0)
                    {
                        cursor.topPosition = 0;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    cursor.topPosition++;
                    if (cursor.topPosition > 9)
                    {
                        cursor.topPosition = 9;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    cursor.leftPosition--;
                    if (cursor.leftPosition < 0)
                    {
                        cursor.leftPosition = 0;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    cursor.leftPosition++;
                    if (cursor.leftPosition > 9)
                    {
                        cursor.leftPosition = 9;
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
