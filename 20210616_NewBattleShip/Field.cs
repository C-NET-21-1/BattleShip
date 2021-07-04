using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    /// <summary>
    ///                        Field
    ///    Имя              Назначение                              Тип             Диапазон значений 
    ///    playingField     Массив игрового поля                    StateCell[,]
    ///    ships            Массив кораблей на игровом поле         Ship[]
    ///    counterShip      Колличество кораблей на игровом поле    int
    ///    counterDeck      Колличество палуб на игровом поле       int
    /// </summary>

    struct Field
    {
        public StateCell[,] playingField;
        public Ship[] ships;
        public int counterShip;
        public int counterDeck;
    }
}
