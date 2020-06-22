using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// точка для хранения запомненной позиции курсора
        /// </summary>
        public Point mousePosition;

        /// <summary>
        /// Перечень маркеров
        /// </summary>
        private Markers markers = new Markers();

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Обработчик вызывается при перерисовке поверхности формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // рисуем все запомненные маркеры
            markers.DrawMarkers(e.Graphics);
        }

        /// <summary>
        /// Обработчик добавления маркера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddMarker_Click(object sender, EventArgs e)
        {            
            // добавляем маркер в позиции курсора, запомненной при открытии контекстного меню
            markers.Add(mousePosition);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Обработчик события открытия контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPopup_Opening(object sender, CancelEventArgs e)
        {
            // запоминаем позицию курсора в координатах поверхности щелчка
            mousePosition = PointToClient(MousePosition);
            // делаем видимым пункт контекстного меню, если в точке вызова меню есть маркер
            tsmiRemoveMarker.Visible = markers.MarkerExists(mousePosition);
        }

        /// <summary>
        /// Обработчик удаления маркера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRemoveMarker_Click(object sender, EventArgs e)
        {
            // удаляем маркер в позиции курсора, запомненной при открытии контекстного меню
            markers.Remove(mousePosition);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Обработчик нажатия кнопки указателя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // передаём информацию о нажатии кнопки указателя
            markers.MouseDown(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Обработчик перемещения указателя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            // показываем курсор маркера
            Cursor = markers.MarkerCursor(e.Location, ModifierKeys);
            // передаём информацию о перемещении указателя
            markers.MouseMove(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // передаём информацию об отпускании кнопки указателя
            markers.MouseUp(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }
    }
}
