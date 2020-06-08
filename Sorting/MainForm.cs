using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Sorting
{
    public partial class MainForm : Form
    {
        // список рисуемых объектов
        private Elements elements = new Elements();

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
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // запускаем поток для модификации модели
            pkgPainter.RunWorkerAsync();
            // добавим единственный элемент
            elements.Add(new Element());
            // добавим ещё элемент
            //elements.Add(new Element());
        }

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
            while (!worker.CancellationPending)
            {
                elements.Update();
                // требование перерисовки формы
                this.Invalidate();
                // отдыхаем
                Thread.Sleep(1000/60);
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
    }
}
