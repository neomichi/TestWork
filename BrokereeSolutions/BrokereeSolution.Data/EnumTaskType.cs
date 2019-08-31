using System;
using System.Collections.Generic;
using System.Text;

namespace BrokereeSolution.Data
{
    public enum ActionType
    {
        /// <summary>
        /// Перезаписать ресурс
        /// </summary>
        DefaultUpdate = 10,
        /// <summary>
        /// Вставка подстроки - в начало
        /// </summary>
        InsertSubBegin = 20,
        /// <summary>
        /// Вставка подстроки - в конец
        /// </summary>
        InsertSubEnd = 30,
        /// <summary>
        /// Вставка подстроки - в любое место по индексу
        /// </summary>
        InsertSubIndex = 40,

        /// <summary>
        /// Удаление подстроки по индексу и длине
        /// </summary>
        DeleteSub = 50,

        /// <summary>
        /// Замена подстроки на другую
        /// </summary>
        ReplaceSub = 60,
    }
}
