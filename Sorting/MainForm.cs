using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sorting
{
    public partial class MainForm : Form
    {
        // список рисуемых объектов
        private List<ArrayElements> elements = new List<ArrayElements>();

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

        private void CreateAndReorder(int @case = 0)
        {
            elements.Clear();
            var rand = new Random();
            var a = new List<int[]>();
            var count = 25;
            a.Add(new int[count]);
            a.Add(new int[count]);
            a.Add(new int[count]);
            a.Add(new int[count]);
            var cash = new List<int>();
            var initCash = false;
            foreach (var arr in a)
            {
                elements.Add(new ArrayElements());
                var length = a.First().Length;
                for (var i = 0; i < length; i++)
                {
                    var value = @case == 0 ? rand.Next(1, 100) : length - i;
                    arr[i] = initCash ? cash[i] : value;
                    if (!initCash)
                        cash.Add(value);
                }
                initCash = true;
            }
            // добавим элементы
            var n = 0;
            var x = 40;
            foreach (var arr in a)
            {
                var location = new System.Drawing.PointF(x, 40);
                var length = a.First().Length;
                for (var i = 0; i < length; i++)
                {
                    var element = CreateArrayElement();
                    element.Location = location;
                    element.Value = arr[i];
                    elements[n].Add(element);
                    location.Y += element.Size.Height + 5;
                }
                n++;
                x += 80;
            }
            var logs = new List<Tuple<int, int>>[a.Count];
            for (var i = 0; i < a.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        logs[i] = MethodsHolder.BubbleSort(a[i]);
                        break;
                    case 1:
                        logs[i] = MethodsHolder.ShakerSort(a[i]);
                        break;
                    case 2:
                        logs[i] = MethodsHolder.InsertionSort(a[i]);
                        break;
                    case 3:
                        logs[i] = MethodsHolder.QuickSort(a[i]);
                        break;
                }
            }
            // запускаем поток для модификации модели
            pkgPainter.RunWorkerAsync(logs);
        }

        private static ArrayElement CreateArrayElement() => 
            new ArrayElement() { Size = new System.Drawing.SizeF(25, 25) };

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
                if (runned || step)
                {
                    for (var i = 0; i < logs.Length; i++)
                    {
                        if (elements[i].Stabilized)
                        {
                            elements[i].DoIteration(logs[i]);
                            if (i == logs.Length - 1)
                                step = false;
                        }
                        else
                            elements[i].Update();
                    }
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
            foreach (var elem in elements)
                elem.DrawAt(e.Graphics);
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            step = true;
            runned = false;
            btnStartStop.Text = "Пуск";

            btnReorder.Enabled = false;
            pkgPainter.CancelAsync();
            while (pkgPainter.IsBusy)
                Application.DoEvents();
            btnReorder.Enabled = true;
            CreateAndReorder();
        }

        private void btnReverseRange_Click(object sender, EventArgs e)
        {
            step = true;
            runned = false;
            btnStartStop.Text = "Пуск";

            btnReorder.Enabled = false;
            pkgPainter.CancelAsync();
            while (pkgPainter.IsBusy)
                Application.DoEvents();
            btnReorder.Enabled = true;
            CreateAndReorder(1);
        }

        private bool runned = false;

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            runned = !runned;
            btnStartStop.Text = runned ? "Стоп" : "Пуск";
        }

        private bool step = true;

        private void btnStep_Click(object sender, EventArgs e)
        {
            step = true;
        }
    }
}
