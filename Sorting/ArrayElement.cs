using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sorting
{
    internal class ArrayElement
    {
        public PointF Location { get; set; }

        public SizeF Size { get; set; }

        public int Value { get; set; }

        private List<PointF> frames = new List<PointF>();

        public bool Stabilized
        {
            get
            {
                return frames.Count == 0;
            }
        }

        public override string ToString()
        {
            return $"{Value} {Stabilized}";
        }

        private int selfIndex;
        private int goalIndex;

        public void SetGoalLocation(PointF goalLocation, int selfIndex, int goalIndex)
        {
            if (Math.Abs(goalLocation.X - Location.X) < 0.0001 &&
                Math.Abs(goalLocation.Y - Location.Y) < 0.0001) return;
            this.selfIndex = selfIndex;
            this.goalIndex = goalIndex;
            var pt2 = GetMiddlePoint(Location, goalLocation);
            if (goalLocation.Y > Location.Y)
                pt2.X += 32;
            else
                pt2.X -= 32;
            var pt1 = GetMiddlePoint(Location, pt2);
            var pt3 = GetMiddlePoint(pt2, goalLocation);
            var ptL_1 = GetMiddlePoint(Location, pt1);
            var pt1_2 = GetMiddlePoint(pt1, pt2);
            var pt2_3 = GetMiddlePoint(pt2, pt3);
            var pt3_g = GetMiddlePoint(pt3, goalLocation);
            frames.Clear();
            frames.AddRange(new[] { ptL_1, pt1, pt1_2, pt2, pt2_3, pt3, pt3_g, goalLocation, goalLocation });

        }

        private PointF GetMiddlePoint(PointF location, PointF goal)
        {
            var midX = (goal.X + location.X) / 2f;
            var midY = (goal.Y + location.Y) / 2f;
            return new PointF(midX, midY);
        }

        public void DrawAt(Graphics graphics)
        {
            var rect = new RectangleF(Location, Size);
            graphics.FillRectangle(Brushes.White, rect);
            graphics.DrawRectangles(Pens.Black, new[] { rect });
            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                graphics.DrawString($"{Value}", SystemFonts.DefaultFont, Brushes.Black, rect, format);
            }
        }

        public void Update(ArrayElements elements)
        {
            if (frames.Count > 0)
            {
                Location = new PointF(frames[0].X, frames[0].Y);
                frames.RemoveAt(0);
            }
        }
    }

}