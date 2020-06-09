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
            var rand = new Random();
            var a = new int[15];
            for (var i = 0; i < a.Length; i++)
            {
                a[i] = a.Length - i; //rand.Next(1, 100);
            }
            // добавим элементы
            var location = new System.Drawing.PointF(100, 50);
            for (var i = 0; i < a.Length; i++)
            {
                var element = CreateArrayElement();
                element.Location = location;
                element.Value = a[i];
                elements.Add(element);
                location.Y += element.Size.Height + 10;
            }
            var log = MethodsHolder.BubbleSort(a);
            // запускаем поток для модификации модели
            pkgPainter.RunWorkerAsync(log);
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
            var log = (List<Tuple<int, int>>)e.Argument;
            while (!worker.CancellationPending)
            {
                if (elements.Stabilized)
                {
                    try
                    {
                        elements.DoIteration(log);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    try
                    {
                        elements.Update();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                // требование перерисовки формы
                this.Invalidate();
                // отдыхаем
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Обновление визуального содержимого формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            elements.DrawAt(e.Graphics);
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {

        }
    }
}
