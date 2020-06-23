﻿using System;
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
                Prev = location;
                location = value;
            }
        }

        /// <summary>
        /// Предыдущая позиция маркера (его центральная точка)
        /// </summary>
        public PointF Prev { get; internal set; }

        /// <summary>
        /// Маркер двигался
        /// </summary>
        /// <returns></returns>
        internal bool IsMoved()
        {
            return Math.Abs(Prev.X - location.X) >= 0.0001f ||
                Math.Abs(Prev.Y - location.Y) >= 0.0001f;
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
        public virtual Cursor Cursor { get => Cursors.Hand; }

        public MarkerKind Kind { get; set; } = MarkerKind.Node;
    }

    public enum MarkerKind
    {
        Node,
        Location,
        SizeAll,
        SizeNE,
        SizeSW,
        SizeNW,
        SizeSE
    }
}