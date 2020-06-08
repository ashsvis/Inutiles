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
            this.ForEach(x => x.DrawAt(graphics));
        }

        private int i;

        /// <summary>
        /// Проверка стабильности списка (нет текущих перемещений)
        /// </summary>
        public bool Stabilized
        {
            get
            {
                return this.All(x => x.Stabilized);
            }
        }

        /// <summary>
        /// Метод вызывает аналогичный метод коррекции состояния у всех элементов списка
        /// </summary>
        public void Update()
        {
            // для каждого элемнта в списке вызовем метод коррекции состояния
            this.ForEach(x => x.Update());
        }

        public void DoBubbleIteration(ref int i)
        {
            i = i == 0 ? 1 : i;
            if (i < this.Count)
            {
                if (this[i].Value < this[i - 1].Value)
                {
                    this[i - 1].SetGoalLocation(this[i].Location, this[i].Value);
                    this[i].SetGoalLocation(this[i - 1].Location, this[i - 1].Value);
                    //
                    //var mem = this[i].Value;
                    //this[i].Value = this[i - 1].Value;
                    //this[i - 1].Value = mem;
                }
                i++;
            }
            else
                i = 0;
        }

        /// <summary>
        /// Перемешать значения
        /// </summary>
        public void Reorder()
        {
            var rand = new Random();
            foreach (var item in this)
            {
                item.Value = rand.Next(0, 1000);
            }
        }
    }
}