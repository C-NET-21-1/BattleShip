using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20210616_NewBattleShip
{
    /// <summary>
    ///                        StateCell
    ///    Имя              Назначение                              Тип             Диапазон значений 
    ///    Empty            Пустая клетка                
    ///    Miss             Промах на клетке       
    ///    Deck             Палуба на клетке          
    ///    Hit              Попадание в палубу
    ///    Kill             Убит корабль на клетке
    /// </summary>

    enum StateCell
    {
        E,
        M,
        D,
        H,
        K
    }
}
