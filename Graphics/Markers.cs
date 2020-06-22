using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphics
{
    public class Markers
    {
        protected List<Marker> markers = new List<Marker>();

        /// <summary>
        /// Добавляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public void Add(Marker marker)
        {
            markers.Add(marker);
        }

        /// <summary>
        /// Создаем и добавляем маркер в позиции, переданной через аргумент
        /// </summary>
        /// <param name="point"></param>
        public void Add(PointF point)
        {
            Add(new Marker() { Location = point });
        }

        /// <summary>
        /// Удаляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public void Remove(Marker marker)
        {
            markers.Remove(marker);
        }

        /// <summary>
        /// Удаляем первый найденный маркер в позиции, переданной через аргумент
        /// </summary>
        /// <param name="point"></param>
        public void Remove(PointF point)
        {
            var marker = markers.LastOrDefault(x => x.Bounds.Contains(point.X, point.Y));
            if (marker == null) return;
            Remove(marker);
        }

        /// <summary>
        /// Рисуем все маркеры из коллекции
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Draw(System.Drawing.Graphics graphics, Pen pen = null)
        {
            foreach (var marker in markers)
                graphics.DrawRectangles(pen ?? Pens.Magenta, new[] { marker.Bounds });
        }

        /// <summary>
        /// Метод подключения перегрузок для проверки попадания на фигуру
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public virtual bool Contained(Point mousePosition)
        {
            return false;
        }

        /// <summary>
        /// Выдаем признак существования маркера в указанной точке
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public bool MarkerExists(Point mousePosition)
        {
            return markers.Exists(x => x.Bounds.Contains(mousePosition.X, mousePosition.Y));
        }

        /// <summary>
        /// Выдаем вид курсора маркера, если есть
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        /// <returns></returns>
        public Cursor MarkerCursor(Point location, Keys modifierKeys)
        {
            var marker = markers.LastOrDefault(x => x.Bounds.Contains(location.X, location.Y));
            if (marker == null) return Cursors.Default;
            return marker.Cursor;
        }

        private Marker marker = null;
        private bool downed = false;

        /// <summary>
        /// Обработка нажатия кнопки для указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public void MouseDown(Point location, Keys modifierKeys)
        {
            marker = markers.LastOrDefault(x => x.Bounds.Contains(location.X, location.Y));
            if (marker != null)
                downed = true;
        }

        /// <summary>
        /// Обработка перемещения указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public void MouseMove(Point location, Keys modifierKeys)
        {
            if (downed && marker != null)
            {
                marker.Location = location;
            }
        }

        /// <summary>
        /// Заполнение внешнего списка данными о внутренних маркерах
        /// </summary>
        /// <param name="markers"></param>
        public void Fill(Markers markers)
        {
            markers.Clear();
            foreach (var marker in this.markers)
                markers.Add(marker);
        }

        /// <summary>
        /// Обработка отпускания кнопки указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public void MouseUp(Point location, Keys modifierKeys)
        {
            if (downed)
                downed = false;
        }

        /// <summary>
        /// Очистка содержимого внутреннего списка
        /// </summary>
        public void Clear()
        {
            markers.Clear();
        }
    }
}