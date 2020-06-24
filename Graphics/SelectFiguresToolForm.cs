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
        public SelectFiguresToolForm()
        {
            InitializeComponent();
        }

        private void SelectFiguresToolForm_Load(object sender, EventArgs e)
        {
            Size = new Size(toolStrip1.Width, 100);
        }

        private void SelectFiguresToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
