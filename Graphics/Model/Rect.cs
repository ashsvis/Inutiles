using System;
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
        public override void DrawRibbon(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null)
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
                // левый верхний угол
                case 0:
                    marker.Kind = MarkerKind.SizeNW;
                    break;
                // правый нижний угол
                case 1:
                    marker.Kind = MarkerKind.SizeSE;
                    break;
                // правый верхний угол
                case 2:
                    marker.Kind = MarkerKind.SizeNE;
                    break;
                // левый нижний угол
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
        /// Положение прямоугольника при нажатии на кнопку
        /// </summary>
        protected RectangleF DownRectangle = RectangleF.Empty;

        /// <summary>
        /// Обработка нажатия кнопки для указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public override void MouseDown(Point location, Keys modifierKeys)
        {
            base.MouseDown(location, modifierKeys);
            DownRectangle = GetRectangle();
        }

        /// <summary>
        /// Обработка перемещения указателя
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public override void MouseMove(Point location, Keys modifierKeys)
        {
            base.MouseMove(location, modifierKeys);
            if (!mouseDowned) return;
            // нет выделенного маркера, значит, тянут за фигуру
            if (currentMarker == null)
            {
                if (OffsetLocation.IsEmpty) return;
                // корректируем положение всех маркеров на величину смещения
                markers.ForEach(x => x.Location = PointF.Add(x.Location, OffsetLocation));
                return; 
            }

            // пересчёт маркеров
            var rect = GetRectangle();
            var scaleHeight = rect.Height / DownRectangle.Height;
            var scaleWidth = rect.Width / DownRectangle.Width;
            float scale = Math.Min(scaleHeight, scaleWidth);
            //Console.WriteLine($"scaleHeight: {scaleHeight}, scaleWidth: {scaleWidth}, scale: {scale}");

            //currentMarker.Location = new PointF(currentMarker.Location.X * scale, currentMarker.Location.Y * scale);

            // Первые два опорных маркера (0 и 1) обозначают левый верхний и правый нижний углы прямоугольника
            // и по ним строится структура прямоугольника.
            // Два следующих маркера (2 и 3) обозначают углы побочной диагонали
            // прямоугольника и при их перемещении необходимо корректировать положение опорных маркеров.
            // Далее идут маркеры для изменения размеров сторон: (4 и 5) для ширины, (6 и 7) для высоты фигуры
            switch (currentMarker.Kind)
            {
                // левый верхний угол 
                case MarkerKind.SizeNW:
                    markers[0].Location = new PointF(currentMarker.Location.X, currentMarker.Location.Y);
                    break;
                // правый нижний угол 
                case MarkerKind.SizeSE:
                    markers[1].Location = new PointF(currentMarker.Location.X, currentMarker.Location.Y);
                    break;
                // правый верхний угол 
                case MarkerKind.SizeNE:
                    markers[0].Location = new PointF(markers[0].Location.X, currentMarker.Location.Y);
                    markers[1].Location = new PointF(currentMarker.Location.X, markers[1].Location.Y);
                    break;
                // левый нижний угол
                case MarkerKind.SizeSW:
                    markers[0].Location = new PointF(currentMarker.Location.X, markers[0].Location.Y);
                    markers[1].Location = new PointF(markers[1].Location.X, currentMarker.Location.Y);
                    break;
                // тянем за правый средний маркер 
                case MarkerKind.SizeE:
                    markers[1].Location = new PointF(currentMarker.Location.X, markers[1].Location.Y);
                    break;
                // тянем за левый средний маркер 
                case MarkerKind.SizeW:
                    markers[0].Location = new PointF(currentMarker.Location.X, markers[0].Location.Y);
                    break;
                // тянем за нижний средний маркер 
                case MarkerKind.SizeS:
                    markers[1].Location = new PointF(markers[1].Location.X, currentMarker.Location.Y);
                    break;
                // тянем за верхний средний маркер 
                case MarkerKind.SizeN:
                    markers[0].Location = new PointF(markers[0].Location.X, currentMarker.Location.Y);
                    break;
            }
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
            // угловые маркеры главной диагонали
            markers.Add(new Marker { Location = rect.Location, Kind = MarkerKind.SizeNW });
            var ptSE = new PointF(rect.Location.X + rect.Width, rect.Location.Y + rect.Height);
            markers.Add(new Marker { Location = ptSE, Kind = MarkerKind.SizeSE });
            // угловые маркеры побочной диагонали
            var ptNE = new PointF(rect.Location.X + rect.Width, rect.Location.Y);
            markers.Add(new Marker { Location = ptNE,Kind = MarkerKind.SizeNE });
            var ptSW = new PointF(rect.Location.X, rect.Location.Y + rect.Height);
            markers.Add(new Marker { Location = ptSW, Kind = MarkerKind.SizeSW });
            // маркеры ширины
            var ptW = new PointF(rect.Location.X, rect.Location.Y + rect.Height / 2);
            markers.Add(new Marker { Location = ptW, Kind = MarkerKind.SizeW });
            var ptE = new PointF(rect.Location.X + rect.Width, rect.Location.Y + rect.Height / 2);
            markers.Add(new Marker { Location = ptE, Kind = MarkerKind.SizeE });
            // маркеры высоты
            var ptN = new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y);
            markers.Add(new Marker { Location = ptN, Kind = MarkerKind.SizeN });
            var ptS = new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height);
            markers.Add(new Marker { Location = ptS, Kind = MarkerKind.SizeS });
        }
    }
}