using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
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