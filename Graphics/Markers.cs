using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphics
{
    public abstract class Markers
    {
        protected List<Marker> markers = new List<Marker>();

        /// <summary>
        /// Добавляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public virtual void Add(Marker marker)
        {
            markers.Add(marker);
        }

        /// <summary>
        /// Создаем и добавляем маркер в позиции, переданной через аргумент
        /// </summary>
        /// <param name="point"></param>
        public void Add(PointF point)
        {
            Add(new Marker() { Location = point, Prev = point });
        }

        /// <summary>
        /// Удаляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public virtual void Remove(Marker marker)
        {
            // при удалении маркера из списка - пытаемся удалить маркер выбранной фигуры
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
        public virtual void DrawMarkers(System.Drawing.Graphics graphics, Pen pen = null, Brush brush = null)
        {
            // если кнопка указателя нажата, ничего не рисуем
            if (mouseDowned) return;
            foreach (var marker in markers)
                graphics.DrawRectangles(pen ?? Pens.Magenta, new[] { marker.Bounds });
        }

        /// <summary>
        /// Рисуем все маркеры из коллекции
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(System.Drawing.Graphics graphics, Pen pen = null, Brush brush = null);

        /// <summary>
        /// Метод подключения перегрузок для проверки попадания на фигуру
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public abstract bool Contained(Point mousePosition);

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

        protected Marker currentMarker = null;
        protected bool mouseDowned = false;
        
        /// <summary>
        /// Положение указателя при нажатии на кнопку
        /// </summary>
        protected Point downLocation = Point.Empty;

        /// <summary>
        /// Смещение указателя после нажатия на кнопку
        /// </summary>
        protected Size offsetLocation = Size.Empty;

        /// <summary>
        /// Обработка нажатия кнопки для указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public void MouseDown(Point location, Keys modifierKeys)
        {
            currentMarker = markers.LastOrDefault(x => x.Bounds.Contains(location.X, location.Y));
            mouseDowned = true;
            downLocation = location;
        }

        /// <summary>
        /// Обработка перемещения указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public virtual void MouseMove(Point location, Keys modifierKeys)
        {
            offsetLocation = new Size(location.X - downLocation.X, location.Y - downLocation.Y);
            if (mouseDowned && currentMarker != null)
            {
                currentMarker.Location = location;
            }
        }

        /// <summary>
        /// Обработка отпускания кнопки указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public virtual void MouseUp(Point location, Keys modifierKeys)
        {
            if (mouseDowned)
            {
                mouseDowned = false;
            }
        }

        /// <summary>
        /// Возврат количества накопленных маркеров
        /// </summary>
        public int Count { get => markers.Count; }
    }
}