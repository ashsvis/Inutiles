using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sorting
{
    internal class ArrayElement : Element
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

        private int goalValue;

        public void SetGoalLocation(PointF goalLocation, int goalValue)
        {
            if (Math.Abs(goalLocation.X - Location.X) < 0.0001 &&
                Math.Abs(goalLocation.Y - Location.Y) < 0.0001) return;
            this.goalValue = goalValue;
            var pt2 = GetMiddlePoint(Location, goalLocation);
            var pt1 = GetMiddlePoint(Location, pt2);
            var pt3 = GetMiddlePoint(pt2, goalLocation);
            frames.Clear();
            frames.AddRange(new[] { Location, pt1, pt2, pt3, goalLocation });
        }

        private PointF GetMiddlePoint(PointF location, PointF goal)
        {
            var midX = (goal.X + location.X) / 2f;
            var midY = (goal.Y + location.Y) / 2f;
            return new PointF(midX, midY);
        }

        public override void DrawAt(Graphics graphics)
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

        public override void Update()
        {
            if (frames.Count > 0)
            {
                Location = new PointF(frames[0].X, frames[0].Y);
                frames.RemoveAt(0);
                if (frames.Count == 0)
                    Value = goalValue;
            }
        }
    }

}