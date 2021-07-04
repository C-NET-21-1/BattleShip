using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    class BL
    {
        static Random random = new Random();

        public static Field CreateField(int size, int counterShip)
        {
            Field field;

            field.playingField = new StateCell[size, size];
            field.ships = new Ship[counterShip];
            field.counterShip = 0;
            field.counterDeck = 0;

            return field;
        }

        public static Ship CreateShip(int counterDecks)
        {
            Ship ship;

            ship.decks = new Cell[counterDecks];
            ship.route = Orientation.Horizontal;
            ship.counterDecks = counterDecks;

            return ship;
        }

        public static Cell CreateDeck()
        {
            Cell deck;

            deck.topPosition = 0;
            deck.leftPosition = 0;
            deck.stateCell = StateCell.D;

            return deck;
        }

        public static void AddAllDeck(ref Ship ship, Cell deck)
        {
            switch(ship.route)
            {
                case Orientation.Horizontal:
                    for (int i = 0; i < ship.decks.Length; i++)
                    {
                        ship.decks[i].topPosition = deck.topPosition;
                        ship.decks[i].leftPosition = deck.leftPosition;
                        ship.decks[i].stateCell = StateCell.D;
                        deck.leftPosition++;

                    }
                    break;
                case Orientation.Vertikal:
                    for (int i = 0; i < ship.decks.Length; i++)
                    {
                        ship.decks[i].topPosition = deck.topPosition;
                        ship.decks[i].leftPosition = deck.leftPosition;
                        ship.decks[i].stateCell = StateCell.D;
                        deck.topPosition++;
                    }
                    break;
                default:
                    break;
            }
        }

        public static void AddShip(ref Field field, Ship ship)
        {
            if(field.counterShip < field.ships.Length)
            {
                field.ships[field.counterShip] = ship;
                field.counterShip++;
            }
        }

        public static void DecrementDeck(int counterShip, ref int counterDeck)
        {
            if (counterShip == 10)
            {
                counterDeck = 3;
            }
            if (counterShip == 8)
            {
                counterDeck = 2;
            }
            if (counterShip == 5)
            {
                counterDeck = 1;
            }
            if (counterShip == 1)
            {
                counterDeck = 0;
            }

        }

        public static void FillField(ref Field field)
        {
            for (int i = 0; i < field.counterShip; i++)
            {
                for (int j = 0; j < field.ships[i].decks.Length; j++)
                {
                    field.playingField[field.ships[i].decks[j].topPosition, 
                        field.ships[i].decks[j].leftPosition] = StateCell.D;
                    field.counterDeck++;
                }             
            }
        }

        public static bool IsAllowed(Field field, Ship ship)
        {
            bool allowed = true;

            for (int i = 0; i < field.counterShip; i++)
            {
                for (int j = 0; j < field.ships[i].decks.Length; j++)
                {
                    for (int y = 0; y < ship.decks.Length; y++)
                    {
                        allowed = FlagAllowedPosition(ship.decks[y].leftPosition, ship.decks[y].topPosition, 
                            field.ships[i].decks[j].leftPosition, field.ships[i].decks[j].topPosition);

                        if(!allowed)
                        {
                            return allowed;
                        }
                       
                    }
                }                
            }

            return allowed;
        }

        static bool FlagAllowedPosition(int newDeckLeft, int newDeckTop, 
                int currentDeckLeft, int currentDeckTop)
        {
            bool result = true;

            for (int shiftDeckTop = -1; shiftDeckTop <= 1; shiftDeckTop++)
            {
                for (int shiftDeckLeft = -1; shiftDeckLeft <= 1; shiftDeckLeft++)
                {
                    if (newDeckLeft + shiftDeckLeft != currentDeckLeft 
                            || newDeckTop + shiftDeckTop != currentDeckTop)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return result;
        }

        public static void GenerateGameField(ref Field field)
        {
            int counterDeck = 4;
            int counterShip = 10;
            Cell deck; 
            Ship ship;

            while(counterShip > 0)
            {
                deck = CreateDeck();
                ship = CreateShip(counterDeck);
                ship.route = (Orientation)random.Next(0, 2);

                if(ship.route == Orientation.Horizontal)
                {
                    deck.leftPosition = random.Next(0, 11 - counterDeck);
                    deck.topPosition = random.Next(0, 10);
                }
                else
                {
                    deck.leftPosition = random.Next(0, 10);
                    deck.topPosition = random.Next(0, 11 - counterDeck);
                }

                AddAllDeck(ref ship, deck);
                bool flag = IsAllowed(field, ship);

                if (flag)
                {
                    AddShip(ref field, ship);
                    DecrementDeck(counterShip, ref counterDeck);
                    counterShip--;
                }
            }

            FillField(ref field);
        }
        
        public static void GenerationShot(ref Field field)
        {
            Cell cursor = CreateDeck();
            bool flag = false;

            do
            {
                cursor.topPosition = random.Next(0,10);
                cursor.leftPosition = random.Next(0, 10);
                flag = IsFreeCell(cursor, field);
            
            } while (flag == false);

            Shot(cursor, ref field);
            SetMissAroundDeck(ref field);
        }

        public static void Shot(Cell position, ref Field updateField)
        {
            int numberShip = 0;

            if(updateField.playingField[position.topPosition, 
                    position.leftPosition] == StateCell.D)
            {
                updateField.playingField[position.topPosition, 
                        position.leftPosition] = StateCell.H;
                updateField.counterDeck--;

                for (int i = 0; i < updateField.ships.Length; i++)
                {
                    for (int j = 0; j < updateField.ships[i].decks.Length; j++)
                    {
                        if(updateField.ships[i].decks[j].topPosition == position.topPosition 
                                && updateField.ships[i].decks[j].leftPosition == position.leftPosition)
                        {
                            updateField.ships[i].decks[j].stateCell = StateCell.H; 
                        }
                    }
                }

                if(IsAllHitDeck(updateField, ref numberShip))
                {
                    UpdateField(ref updateField, numberShip);
                }
            }
            else
            {
                updateField.playingField[position.topPosition, 
                    position.leftPosition] = StateCell.M;
            }
        }


        public static bool IsFreeCell(Cell position, Field updateField)
        {
            bool flag = false;

            if(updateField.playingField[position.topPosition, position.leftPosition] == StateCell.E ||
                    updateField.playingField[position.topPosition, position.leftPosition] == StateCell.D)
            {
                flag = true;
            }

            return flag;
        }

        static bool IsAllHitDeck(Field updateField, ref int numberShip)
        {
            bool allHit = true;
            int counter = 0;

            for (int i = 0; i < updateField.ships.Length; i++)
            {
                for (int j = 0; j < updateField.ships[i].decks.Length; j++)
                {
                    if (updateField.ships[i].decks[j].stateCell == StateCell.H)
                    {
                        counter++;
                    }
                    if(counter == updateField.ships[i].decks.Length)
                    {
                        numberShip = i;
                        return true;
                    }
                    else
                    {
                        allHit = false;
                    }
                }
                counter = 0;
            }

            return allHit;

        }

        static void UpdateField(ref Field field, int numberShip)
        {
           for (int j = 0; j < field.ships[numberShip].decks.Length; j++)
           {
               if (field.ships[numberShip].decks[j].stateCell == StateCell.H)
               {
                   field.playingField[field.ships[numberShip].decks[j].topPosition,
                        field.ships[numberShip].decks[j].leftPosition] = StateCell.K;
                   field.ships[numberShip].decks[j].stateCell = StateCell.K;
               }
           }
        }
  
        public static void SetMissAroundDeck(ref Field field)
        {
            Cell cursor;
            cursor.stateCell = StateCell.E;

            for (int i = 0; i < field.playingField.GetLength(0); i++)
            {
                for (int j = 0; j < field.playingField.GetLength(1); j++)
                {
                    if(field.playingField[i, j] == StateCell.K)
                    {
                        cursor.topPosition = i;
                        cursor.leftPosition = j;
                        MissAroundDeck(ref field, cursor);
                    }
                }
            }
        }

        static void MissAroundDeck(ref Field field, Cell cursor)
        {
            int shiftTop = 1;
            int shiftBottom = 1;
            int shiftRight = 1;
            int shiftLeft = 1;

            if (cursor.topPosition == 0)
            {
                shiftTop = 0;
            }
            if(cursor.topPosition == 9)
            {
                shiftBottom = 0;
            }
            if (cursor.leftPosition == 0)
            {
                shiftRight = 0;
            }
            if (cursor.leftPosition == 9)
            {
                shiftLeft = 0;
            }

            for (int i = cursor.topPosition - shiftTop; i <= cursor.topPosition + shiftBottom; i++)
            {
                for (int j = cursor.leftPosition - shiftRight; j <= cursor.leftPosition + shiftLeft; j++)
                {
                    if(field.playingField[i, j] == StateCell.E)
                    {
                        field.playingField[i, j] = StateCell.M;
                    }
                }
            }
        }
   
    }
}
