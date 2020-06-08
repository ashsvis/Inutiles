using System;
using System.Diagnostics;
using System.Drawing;

namespace Sorting
{
    /// <summary>
    /// Базовый элемент для рисования
    /// </summary>
    internal class Element
    {
        /// <summary>
        /// Таймер для отсчёта времени между событиями
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// поле для хранения временного промежутка
        /// </summary>
        private TimeSpan timeSpan = new TimeSpan();

        public Element()
        {
            // запускаем таймер
            stopwatch.Start();
        }

        /// <summary>
        /// Запоминаем время, прошедшее с предыдущего вызова этого метода
        /// </summary>
        public void Update()
        {
            // получаем промежуток
            timeSpan = stopwatch.Elapsed;
            // перезапускаем таймер
            stopwatch.Restart();
        }

        /// <summary>
        /// Рисование на канве некоторого содержимого
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawAt(Graphics graphics)
        {
            // выводим текстовую строку
            graphics.DrawString(this.ToString(), SystemFonts.DefaultFont, Brushes.Black, PointF.Empty);
        }

        /// <summary>
        /// Перегрузка метода преобразования в строку позволяет выводить особенные значения
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // в даннм случае выводится резница во времени между двумя соседними вызовами Update()
            return $"{timeSpan}";
        }
    }
}