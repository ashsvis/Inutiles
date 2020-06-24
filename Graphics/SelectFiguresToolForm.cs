using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics
{
    public partial class SelectFiguresToolForm : Form
    {
        private MainForm mainForm;

        public SelectFiguresToolForm(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void SelectFiguresToolForm_Load(object sender, EventArgs e)
        {
            Size = new Size(toolStrip1.Width, 100);
        }

        private void SelectFiguresToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void tsbArrow_Click(object sender, EventArgs e)
        {
            mainForm.CancelBegin();
        }

        private void tsbBuildLine_Click(object sender, EventArgs e)
        {
            mainForm.BeginLine();
        }

        private void tsbBuildRect_Click(object sender, EventArgs e)
        {
            mainForm.BeginRectangle();
        }
    }
}
