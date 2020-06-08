using System.Collections.Generic;
using System.Drawing;

namespace Sorting
{
    /// <summary>
    /// Контейнер для визульных элементов
    /// </summary>
    internal class Elements : List<Element>
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
        
        /// <summary>
        /// Метод вызывает аналогичный метод коррекции состояния у всех элементов списка
        /// </summary>
        public void Update()
        {
            // для каждого элемнта в списке вызовем метод коррекции состояния
            this.ForEach(x => x.Update());
        }
    }
}