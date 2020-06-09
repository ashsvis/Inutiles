using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sorting
{
    internal class ArrayElements : List<ArrayElement>
    {
        /// <summary>
        /// Метод вызывает аналогичный метод рисования у всех элементов списка
        /// </summary>
        /// <param name="graphics">Канва для рисования</param>
        public void DrawAt(Graphics graphics)
        {
            // для каждого элемнта в списке вызовем метод рисования на канве
            try
            {
                this.ToList().ForEach(x => x.DrawAt(graphics));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private int i;

        /// <summary>
        /// Проверка стабильности списка (нет текущих перемещений)
        /// </summary>
        public bool Stabilized
        {
            get
            {
                return this.ToList().All(x => x.Stabilized);
            }
        }

        /// <summary>
        /// Метод вызывает аналогичный метод коррекции состояния у всех элементов списка
        /// </summary>
        public void Update()
        {
            // для каждого элемента в списке вызовем метод коррекции состояния
            try
            {
                this.ToList().ForEach(x => x.Update(this));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal void DoIteration(List<Tuple<int, int>> log)
        {
            if (log.Count == 0) return;
            var i1 = log[0].Item1;
            var i2 = log[0].Item2;
            this[i1].SetGoalLocation(this[i2].Location, i1, i2);
            this[i2].SetGoalLocation(this[i1].Location, i2, i1);
            log.RemoveAt(0);
        }
    }
}