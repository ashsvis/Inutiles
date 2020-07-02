using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
    /// <summary>
    /// Класс маркера
    /// </summary>
    public class Marker
    {
        private PointF location;

        /// <summary>
        /// Позиция маркера (его центральная точка)
        /// </summary>
        public PointF Location
        {
            get => location;
            internal set
            {
                location = value;
            }
        }

        /// <summary>
        /// Размер маркера в пискелях
        /// </summary>
        public SizeF Size { get => new SizeF(6, 6); }

        /// <summary>
        /// Прямоугольник с положением и размерами маркера
        /// </summary>
        public RectangleF Bounds => new RectangleF(PointF.Subtract(Location, new SizeF(Size.Width / 2, Size.Height / 2)), Size);

        /// <summary>
        /// Курсор маркера перегружаемый
        /// </summary>
        public virtual Cursor Cursor { get => GetMarkerCursor(); }

        /// <summary>
        /// Тип маркера, определяет его назначение в фигуре
        /// </summary>
        public MarkerKind Kind { get; set; } = MarkerKind.Node;

        /// <summary>
        /// Порядковый номер индекса в массиве индексов
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Метод возвращает вид курсора для этого маркера
        /// </summary>
        /// <returns></returns>
        private Cursor GetMarkerCursor()
        {
            switch (Kind)
            {
                case MarkerKind.Node:
                    return Cursors.Cross;
                case MarkerKind.SizeNE:
                case MarkerKind.SizeSW:
                    return Cursors.SizeNESW;
                case MarkerKind.SizeNW:
                case MarkerKind.SizeSE:
                    return Cursors.SizeNWSE;
                case MarkerKind.SizeAll:
                    return Cursors.SizeAll;
                case MarkerKind.SizeW:
                case MarkerKind.SizeE:
                    return Cursors.SizeWE;
                case MarkerKind.SizeN:
                case MarkerKind.SizeS:
                    return Cursors.SizeNS;
                default:
                    return Cursors.Default;
            }
        }
    }

    public enum MarkerKind
    {
        Node,
        Location,
        SizeAll,
        SizeNE,
        SizeSW,
        SizeNW,
        SizeSE,
        SizeW,
        SizeE,
        SizeN,
        SizeS
    }
}