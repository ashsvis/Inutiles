using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
    public partial class ToolFormSelectFigures : Form
    {
        private MainForm mainForm;

        public ToolFormSelectFigures(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void SelectFiguresToolForm_Load(object sender, EventArgs e)
        {
            Size = new Size(22, 24 * 3);
        }

        private void SelectFiguresToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void SelectFiguresToolForm_Paint(object sender, PaintEventArgs e)
        {
            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, rect);
            rect.Location = new Point(3, 3);
            rect.Size = new Size(22, 22);
            e.Graphics.DrawImageUnscaled(Properties.Resources.arrow, rect);
            rect.Offset(0, 24);
            e.Graphics.DrawImageUnscaled(Properties.Resources.poliline, rect);
            rect.Offset(0, 24);
            e.Graphics.DrawImageUnscaled(Properties.Resources.rect, rect);
        }

        private void SelectFiguresToolForm_MouseDown(object sender, MouseEventArgs e)
        {
            var point = e.Location;
            switch (point.Y / 24)
            {
                case 0:
                    mainForm.CancelBegin();
                    break;
                case 1:
                    Cursor = Cursors.Cross;
                    mainForm.BeginLine();
                    break;
                case 2:
                    Cursor = Cursors.Cross;
                    mainForm.BeginRectangle();
                    break;
            }

        }
    }
}
