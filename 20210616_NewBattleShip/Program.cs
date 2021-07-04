using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Field fieldHero;
            Field fieldEnemy;
            Ship ship;
            Cell deck;
            Cell cursor = BL.CreateDeck();
            int size = 10;
            int counterShip = 10;
            int counterDeck = 4;
            ConsoleKey key;

            fieldHero = BL.CreateField(size, counterShip);
            fieldEnemy = BL.CreateField(size, counterShip);

            BL.GenerateGameField(ref fieldEnemy);
            BL.GenerateGameField(ref fieldHero);
            /*while (counterShip > 0)
            {
                deck = BL.CreateDeck();
                ship = BL.CreateShip(counterDeck);

                do
                {
                    key = Console.ReadKey(true).Key;
                    UI.SetPostitionDeck(ref deck, key, ship.route, size, counterDeck);
                    UI.SetRouteShip(ref ship, key);
                    UI.ShowPositionDeck(deck);
                    UI.ShowOrientationShip(ship);

                } while (key != ConsoleKey.Enter);

                BL.AddAllDeck(ref ship, deck);

                bool flag = BL.IsAllowed(fieldHero, ship);

                if (flag)
                {
                    BL.AddShip(ref fieldHero, ship);
                    BL.DecrementDeck(counterShip, ref counterDeck);
                    counterShip--;
                }

                UI.ShowAllowed(flag);

                BL.FillField(ref fieldHero);
                UI.ShowField(fieldHero, 0, 0);
            }

            BL.FillField(ref fieldHero);

            UI.ShowField(fieldHero, 0, 0);*/
            UI.ShowField(fieldEnemy, 50, 0);
            
            while (fieldEnemy.counterDeck > 0)
            {
                do
                {
                    key = Console.ReadKey(true).Key;
                    UI.MoveCursor(key, ref cursor);
                    UI.ShowPositionDeck(cursor);
                } while (key != ConsoleKey.Enter);

                bool flag = BL.IsFreeCell(cursor, fieldEnemy);

                if (flag)
                {
                    BL.Shot(cursor, ref fieldEnemy);
                    BL.SetMissAroundDeck(ref fieldEnemy);
                    BL.GenerationShot(ref fieldHero);
                }
                if(fieldHero.counterDeck == 0)
                {
                    break;
                }

                UI.ShowAllowed(flag);
                UI.ShowField(fieldEnemy, 50, 0);
                UI.ShowField(fieldHero, 0, 0);
                UI.Score(fieldHero, fieldEnemy);
            }

            if(fieldEnemy.counterDeck == 0)
            {
                Console.SetCursorPosition(0, 19);
                Console.WriteLine("Победа");
            }
            if (fieldHero.counterDeck == 0)
            {
                Console.SetCursorPosition(0, 19);
                Console.WriteLine("Поражение");
            }

            Console.ReadKey();
        }
    }
}
