using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    /// <summary>
    ///                        Ship
    ///    Имя          Назначение          Тип       Диапазон значений 
    ///    decks        Палубы корабля      Deck[]
    ///    route        Ориентация корабля  Orintation
    ///    counterDecks Колличество палуб   int
    /// </summary>
    struct Ship
    {
        public Cell[] decks;
        public Orientation route;
        public int counterDecks;
    }
}
