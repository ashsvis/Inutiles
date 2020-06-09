using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Sorting
{
    public partial class MainForm : Form
    {
        // список рисуемых объектов
        private ArrayElements elements1 = new ArrayElements();
        private ArrayElements elements2 = new ArrayElements();

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
            CreateAndReorder();
        }

        private void CreateAndReorder()
        {
            var rand = new Random();
            var a1 = new int[15];
            var a2 = new int[15];
            for (var i = 0; i < a1.Length; i++)
            {
                var value = rand.Next(1, 100);
                a1[i] = value;
                a2[i] = value;
            }
            elements1.Clear();
            // добавим элементы
            var location = new System.Drawing.PointF(170, 20);
            for (var i = 0; i < a1.Length; i++)
            {
                var element = CreateArrayElement();
                element.Location = location;
                element.Value = a1[i];
                elements1.Add(element);
                location.Y += element.Size.Height + 10;
            }
            elements2.Clear();
            location = new System.Drawing.PointF(270, 20);
            for (var i = 0; i < a2.Length; i++)
            {
                var element = CreateArrayElement();
                element.Location = location;
                element.Value = a2[i];
                elements2.Add(element);
                location.Y += element.Size.Height + 10;
            }
            var log1 = MethodsHolder.BubbleSort(a1);
            var log2 = MethodsHolder.BubbleSort(a2);
            // запускаем поток для модификации модели
            pkgPainter.RunWorkerAsync(new[] { log1, log2 });
        }

        private static ArrayElement CreateArrayElement() => 
            new ArrayElement() { Size = new System.Drawing.SizeF(32, 32) };

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
            var logs = (List<Tuple<int, int>>[])e.Argument;
            while (!worker.CancellationPending)
            {
                if (runned)
                {
                    if (elements1.Stabilized)
                        elements1.DoIteration(logs[0]);
                    else
                        elements1.Update();
                    if (elements2.Stabilized)
                        elements2.DoIteration(logs[1]);
                    else
                        elements2.Update();
                }
                // требование перерисовки формы
                this.Invalidate();
                // отдыхаем
                Thread.Sleep(25);
            }
        }

        /// <summary>
        /// Обновление визуального содержимого формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            elements1.DrawAt(e.Graphics);
            elements2.DrawAt(e.Graphics);
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            btnReorder.Enabled = false;
            pkgPainter.CancelAsync();
            while (pkgPainter.IsBusy)
                Application.DoEvents();
            btnReorder.Enabled = true;
            CreateAndReorder();
        }

        private bool runned = false;

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            runned = !runned;
            btnStartStop.Text = runned ? "Стоп" : "Пуск";
        }
    }
}
