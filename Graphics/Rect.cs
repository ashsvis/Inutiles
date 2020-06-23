using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Graphics
{
    public class Rect : Markers
    {
        /// <summary>
        /// Рисуем линию через все точки
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(System.Drawing.Graphics graphics, Pen pen = null, Brush brush = null)
        {
            if (markers.Count < 2) return;
            graphics.FillRectangle(brush ?? Brushes.White, GetRectangle());
            graphics.DrawRectangles(pen ?? Pens.DarkMagenta, new[] { GetRectangle() });
        }

        /// <summary>
        /// Резиновый прямоугольник тянется от последней точки списка до точки позиции курсора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="mousePosition"></param>
        /// <param name="pen"></param>
        public void DrawRibbonRect(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null)
        {
            if (markers.Count < 1) return;
            graphics.DrawRectangles(pen ?? Pens.Magenta, new[] { GetRectangle(mousePosition) });
        }

        private RectangleF GetRectangle(Point mousePosition)
        {
            var location = new PointF(Math.Min(mousePosition.X, markers[0].Location.X), Math.Min(mousePosition.Y, markers[0].Location.Y));
            var size = new SizeF(Math.Abs(mousePosition.X - markers[0].Location.X), Math.Abs(mousePosition.Y - markers[0].Location.Y));
            return new RectangleF(location, size);
        }

        private RectangleF GetRectangle()
        {
            var location = new PointF(Math.Min(markers[1].Location.X, markers[0].Location.X), Math.Min(markers[1].Location.Y, markers[0].Location.Y));
            var size = new SizeF(Math.Abs(markers[1].Location.X - markers[0].Location.X), Math.Abs(markers[1].Location.Y - markers[0].Location.Y));
            return new RectangleF(location, size);
        }

        /// <summary>
        /// Определяем попадание точки на контур фигуры
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public override bool Contained(Point mousePosition)
        {
            using (var path = new GraphicsPath())
            {
                // добавляем в путь контур полилинии
                path.AddRectangle(GetRectangle());
                path.AddRectangles(markers.Select(x => x.Bounds).ToArray());
                // ширина линии для поиска попадания равна размеру маркера
                using (var pen = new Pen(Color.Black, 5f))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.StartCap = LineCap.RoundAnchor;
                    pen.EndCap = LineCap.RoundAnchor;
                    return path.IsVisible(mousePosition) || path.IsOutlineVisible(mousePosition, pen);
                }
            }
        }

        /// <summary>
        /// Добавляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public override void Add(Marker marker)
        {
            switch (markers.Count)
            {
                case 0:
                    marker.Kind = MarkerKind.SizeNW;
                    break;
                case 1:
                    marker.Kind = MarkerKind.SizeSE;
                    break;
                case 2:
                    marker.Kind = MarkerKind.SizeNE;
                    break;
                case 3:
                    marker.Kind = MarkerKind.SizeSW;
                    break;
                default:
                    marker.Kind = MarkerKind.Node;
                    break;
            }
            markers.Add(marker);
        }

        /// <summary>
        /// Удаляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public override void Remove(Marker marker)
        {
            // удаление маркеров не используется для прямоугольника
        }

        /// <summary>
        /// Обработка перемещения указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public override void MouseMove(Point location, Keys modifierKeys)
        {
            // пересчёт маркеров
            if (currentMarker == null) return;
            if (!currentMarker.IsMoved()) return;
            switch (currentMarker.Kind)
            {
                case MarkerKind.SizeNE:
                    markers[0].Location = new PointF(markers[0].Location.X, currentMarker.Location.Y);
                    markers[1].Location = new PointF(currentMarker.Location.X, markers[1].Location.Y);
                    break;
                case MarkerKind.SizeSW:
                    markers[0].Location = new PointF(currentMarker.Location.X, markers[0].Location.Y);
                    markers[1].Location = new PointF(markers[1].Location.X, currentMarker.Location.Y);
                    break;
            }
            base.MouseMove(location, modifierKeys);
        }

        /// <summary>
        /// Обработка отпускания кнопки указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public override void MouseUp(Point location, Keys modifierKeys)
        {
            base.MouseUp(location, modifierKeys);
            // пересчёт маркеров
            var rect = GetRectangle();
            markers.Clear();
            markers.Add(new Marker { Location = rect.Location, Prev = rect.Location, Kind = MarkerKind.SizeNW });
            var ptSE = new PointF(rect.Location.X + rect.Width, rect.Location.Y + rect.Height);
            markers.Add(new Marker { Location = ptSE, Prev = ptSE, Kind = MarkerKind.SizeSE });
            var ptNE = new PointF(rect.Location.X + rect.Width, rect.Location.Y);
            markers.Add(new Marker { Location = ptNE, Prev = ptNE, Kind = MarkerKind.SizeNE });
            var ptSW = new PointF(rect.Location.X, rect.Location.Y + rect.Height);
            markers.Add(new Marker { Location = ptSW, Prev = ptSW, Kind = MarkerKind.SizeSW });
        }
    }
}