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

        private Markers polyline = new Markers();

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

            polyline.DrawFigure(e.Graphics);
            if (Mode == EditorMode.BuildLine)
                polyline.DrawRibbonLine(e.Graphics, PointToClient(MousePosition));
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
            if (Mode == EditorMode.BuildLine)
            {
                polyline.Add(e.Location);
            }

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
            switch (Mode)
            {
                case EditorMode.BuildLine:
                    Cursor = Cursors.Cross;
                    break;
                default:
                    Cursor = markers.MarkerCursor(e.Location, ModifierKeys);
                    break;
            }
            // передаём информацию о перемещении указателя
            markers.MouseMove(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Обработчик отпускания кнопки указателя 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // передаём информацию об отпускании кнопки указателя
            markers.MouseUp(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Дотупные режимы редактора
        /// </summary>
        enum EditorMode
        {
            None,
            BuildLine
        }

        // теущий режим редактора
        private EditorMode Mode = EditorMode.None;

        /// <summary>
        /// Переход в режим рисования прямой линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBeginLine_Click(object sender, EventArgs e)
        {
            Mode = EditorMode.BuildLine;
            polyline.Clear();
            Invalidate();
        }

        /// <summary>
        /// Обработчик нажатия клавиш на клавиатуре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (Mode == EditorMode.BuildLine)
                    {

                    }
                    Mode = EditorMode.None;
                    break;
            }
            Cursor = Cursors.Default;
            Invalidate();
        }
    }
}
