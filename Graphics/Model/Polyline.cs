using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Graphics
{
    public class Polyline : Markers
    {
        /// <summary>
        /// Рисуем линию через все точки
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(System.Drawing.Graphics graphics, Pen pen = null, Brush brush = null)
        {
            if (markers.Count < 2) return;
            graphics.DrawLines(pen ?? Pens.DarkMagenta, markers.Select(x => x.Location).ToArray());
        }

        /// <summary>
        /// Резиновая линия тянется от последней точки списка до точки позиции курсора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="mousePosition">точка позиции курсора</param>
        public override void DrawRibbon(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null)
        {
            if (markers.Count < 1) return;
            graphics.DrawLine(pen ?? Pens.Magenta, markers.Last().Location, mousePosition);
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
                path.AddLines(markers.Select(x => x.Location).ToArray());
                path.AddRectangles(markers.Select(x => x.Bounds).ToArray());
                // ширина линии для поиска попадания равна размеру маркера
                using (var pen = new Pen(Color.Black, 5f))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.StartCap = LineCap.RoundAnchor;
                    pen.EndCap = LineCap.RoundAnchor;
                    return path.IsOutlineVisible(mousePosition, pen);
                }
            }
        }

        /// <summary>
        /// Добавляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public override void Add(Marker marker)
        {
            marker.Kind = MarkerKind.Node;
            marker.Index = markers.Count;
            markers.Add(marker);
        }

        /// <summary>
        /// Удаляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public override void Remove(Marker marker)
        {
            // если точек меньше трёх, то удаления не будет, так как линия уже не будет существовать
            if (markers.Count < 3) return;
            markers.Remove(marker);
            // перенумерация маркеров
            var n = 0;
            foreach (var m in markers)
                m.Index = n++;
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
            if (currentMarker != null)
            {
                // передаём положение перемещаемого маркера маркеру с исходномым номером
                markers[currentMarker.Index].Location = new PointF(currentMarker.Location.X, currentMarker.Location.Y);
                return;
            }
            if (OffsetLocation.IsEmpty) return;
            // корректируем положение всех маркеров на величину смещения
            markers.ForEach(x => x.Location = PointF.Add(x.Location, OffsetLocation));
        }
    }
}