using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Sorting
{
    public partial class MainForm : Form
    {
        // список рисуемых объектов
        private ArrayElements elements = new ArrayElements();

        public MainForm()
        {
            InitializeComponent();
            // против мерцания
            DoubleBuffered = true;
        }

        /// <summary>
        /// Первоначальная загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // запускаем поток для модификации модели
            pkgPainter.RunWorkerAsync();
            // добавим элементы
            var location = new System.Drawing.PointF(10, 10);
            for (var i = 0; i < 15; i++)
            {
                var element = CreateArrayElement();
                element.Location = location;
                elements.Add(element);
                location.Y += element.Size.Height + 10;
            }
            lock (elements)
            {
                elements.Reorder();
            }
        }

        private static ArrayElement CreateArrayElement() => 
            new ArrayElement() { Size = new System.Drawing.SizeF(35, 35) };

        /// <summary>
        /// При закрытии главной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // останавливаем поток модификации модели
            pkgPainter.CancelAsync();
        }

        /// <summary>
        /// Фоновый процесс для изменения свойств списка рисуемых объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pkgPainter_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var i = 0;
            elements.DoBubbleIteration(ref i);
            while (!worker.CancellationPending)
            {
                lock (elements)
                {
                    if (elements.Stabilized)
                        elements.DoBubbleIteration(ref i);
                    else
                        elements.Update();
                }
                // требование перерисовки формы
                this.Invalidate();
                // отдыхаем
                Thread.Sleep(33);
            }
        }

        /// <summary>
        /// Обновление визуального содержимого формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            lock (elements)
            {
                elements.DrawAt(e.Graphics);
            }
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            lock (elements)
            {
                elements.Reorder();
            }
        }
    }
}
