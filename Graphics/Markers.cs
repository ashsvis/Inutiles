using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Graphics
{
    public class Markers
    {
        private List<Marker> markers = new List<Marker>();

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
        public void DrawMarkers(System.Drawing.Graphics graphics)
        {
            foreach (var marker in markers)
                graphics.DrawRectangles(Pens.Magenta, new[] { marker.Bounds });
        }

        /// <summary>
        /// Рисуем линию через все точки
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawFigure(System.Drawing.Graphics graphics)
        {
            if (markers.Count < 2) return;
            graphics.DrawLines(Pens.DarkMagenta, markers.Select(x => x.Location).ToArray());
        }

        public void DrawRibbonLine(System.Drawing.Graphics graphics, Point mousePosition)
        {
            if (markers.Count < 1) return;
            graphics.DrawLine(Pens.Magenta, markers.Last().Location, mousePosition);
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

    /// <summary>
    /// Класс маркера
    /// </summary>
    public class Marker
    {
        /// <summary>
        /// Позиция маркера (его центральная точка)
        /// </summary>
        public PointF Location { get; internal set; }

        /// <summary>
        /// Размер маркера в пискелях
        /// </summary>
        public SizeF Size { get => new SizeF(4, 4); }

        /// <summary>
        /// Прямоугольник с положением и размерами маркера
        /// </summary>
        public RectangleF Bounds => new RectangleF(PointF.Subtract(Location, new SizeF(Size.Width / 2, Size.Height / 2)), Size);

        /// <summary>
        /// Курсор маркера перегружаемый
        /// </summary>
        public virtual Cursor Cursor { get => Cursors.Hand; }
    }
}