﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphics
{
    public abstract class Markers
    {
        protected List<Marker> markers = new List<Marker>();

        public string Name { get; set; }

        /// <summary>
        /// Список привязанных, "дочерних" фигур
        /// </summary>
        public List<Markers> Childs = new List<Markers>();

        /// <summary>
        /// Объект привязки, к которому относится эта фигура
        /// </summary>
        public Markers Parent { get; set; }


        public override string ToString()
        {
            return Name;
        }

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
            Add(new Marker() { Location = point });
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
        /// Рисуем "резиновую" фигуру
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="mousePosition"></param>
        public abstract void DrawRibbon(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null);

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
            // ищем "верхний" маркер
            var marker = markers.LastOrDefault(x => x.Bounds.Contains(location.X, location.Y));
            // если не маркер, то тянут за тело фигуры
            if (marker == null) return Cursors.Hand;
            // иначе возвращаем курсор маркера
            return marker.Cursor;
        }

        protected Marker currentMarker = null;
        protected bool mouseDowned = false;
        
        /// <summary>
        /// Положение указателя при нажатии на кнопку
        /// </summary>
        protected Point DownLocation = Point.Empty;

        /// <summary>
        /// Смещение указателя после нажатия на кнопку
        /// </summary>
        protected Size OffsetLocation = Size.Empty;

        /// <summary>
        /// Обработка нажатия кнопки для указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public virtual void MouseDown(Point location, Keys modifierKeys)
        {
            currentMarker = markers.LastOrDefault(x => x.Bounds.Contains(location.X, location.Y));
            if (currentMarker != null)
            {
                // заводим новый объект для перемещаемого маркера.
                currentMarker = new Marker()
                {
                    Location = location,
                    Kind = currentMarker.Kind,
                    Index = currentMarker.Index
                };
            }
            mouseDowned = true;
            DownLocation = location;
            // передача события нажатия указателя всем "дочерним" фигурам
            Childs.ForEach(x => x.MouseDown(location, modifierKeys));
        }

        /// <summary>
        /// Обработка перемещения указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public virtual void MouseMove(Point location, Keys modifierKeys)
        {
            // вычисление смещения относительно точки первоначального нажатия, используется в перегруженных методах потомков
            OffsetLocation = new Size(location.X - DownLocation.X, location.Y - DownLocation.Y);
            // корректируем точку нажатия указателя (это важно для правильности вычисления offsetLocation)
            DownLocation = location; // работает только при перетаскивании за "тело" фигуры
            // передача события перемещения нажатого указателя всем "дочерним" фигурам
            if (mouseDowned && currentMarker == null && Childs.Count > 0)
                Childs.ForEach(x => x.MouseMove(location, modifierKeys));
            // перемещение выбранного маркера
            if (mouseDowned && currentMarker != null)
                currentMarker.Location = location;
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