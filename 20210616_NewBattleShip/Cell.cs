using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    /// <summary>
    ///                        Deck
    ///    Имя          Назначение                  Тип       Диапазон значений 
    ///    topPosition  Коодинаты по вертикале      int
    ///    leftPosition Коодинаты по горизонтале    int
    ///    stateCell    Состояние клетки            StateCell
    /// </summary>

    struct Cell
    {
        public int topPosition;
        public int leftPosition;
        public StateCell stateCell;
    }
}
