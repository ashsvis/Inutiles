using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
        ///private Markers markers = new Markers();
        public Markers Selected;

        private List<Markers> figures = new List<Markers>();

        private Polyline polyline;

        private Rect rectangle;

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
            // рисуем все фигуры
            foreach (var fig in figures)
                fig.Draw(e.Graphics, Pens.Black);
            // рисуем все запомненные маркеры
            Selected?.DrawMarkers(e.Graphics);

            if (Mode == EditorMode.BuildLine)
            {
                polyline.Draw(e.Graphics);
                polyline.DrawRibbonLine(e.Graphics, PointToClient(MousePosition));
            }
            if (Mode == EditorMode.BuildRect)
            {
                rectangle.Draw(e.Graphics);
                rectangle.DrawRibbonRect(e.Graphics, PointToClient(MousePosition));
            }
        }

        /// <summary>
        /// Обработчик добавления маркера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddMarker_Click(object sender, EventArgs e)
        {            
            // добавляем маркер в позиции курсора, запомненной при открытии контекстного меню
            ///markers.Add(mousePosition);
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
            switch (Mode)
            {
                case EditorMode.BuildLine:
                    figures.Add(polyline);
                    Selected = polyline;
                    Mode = EditorMode.None;
                    Cursor = Cursors.Default;
                    e.Cancel = true;
                    return;
            }
            // запоминаем позицию курсора в координатах поверхности щелчка
            mousePosition = PointToClient(MousePosition);
            // делаем видимым пункт контекстного меню, если в точке вызова меню есть маркер
            tsmiRemoveMarker.Visible = Selected != null ? Selected.MarkerExists(mousePosition) : false;
        }

        /// <summary>
        /// Обработчик удаления маркера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRemoveMarker_Click(object sender, EventArgs e)
        {
            // удаляем маркер в позиции курсора, запомненной при открытии контекстного меню
            Selected?.Remove(mousePosition);
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
            switch (Mode)
            {
                case EditorMode.BuildLine:
                    if (e.Button == MouseButtons.Left)
                        polyline.Add(e.Location);
                    break;
                case EditorMode.BuildRect:
                    if (e.Button == MouseButtons.Left)
                    {
                        rectangle.Add(e.Location);
                        if (rectangle.Count == 2)
                        {
                            figures.Add(rectangle);
                            Selected = rectangle;
                            Mode = EditorMode.None;
                            Cursor = Cursors.Default;
                        }
                    }
                    break;
                default:
                    // ищем попадание указателя на фигуру
                    var fig = figures.LastOrDefault(x => x.Contained(e.Location));
                    Selected = fig;
                    Invalidate();
                    break;
            }

            // передаём информацию о нажатии кнопки указателя
            Selected?.MouseDown(e.Location, ModifierKeys);
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
                case EditorMode.BuildRect:
                    Cursor = Cursors.Cross;
                    break;
                default:
                    // ищем "верхнюю" фигуру под курсором
                    var fig = figures.LastOrDefault(x => x.Contained(e.Location));
                    // если фигура есть, показываем её курсор, иначе - курсор по умолчанию
                    Cursor = fig != null ? fig.MarkerCursor(e.Location, ModifierKeys) : Cursors.Default;
                    break;
            }
            // передаём информацию о перемещении указателя
            Selected?.MouseMove(e.Location, ModifierKeys);
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
            Selected?.MouseUp(e.Location, ModifierKeys);
            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Дотупные режимы редактора
        /// </summary>
        enum EditorMode
        {
            None,
            BuildLine,
            BuildRect
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
            polyline = new Polyline();
            Mode = EditorMode.BuildLine;
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
                        figures.Add(polyline);
                        Selected = polyline;
                    }
                    if (Mode == EditorMode.BuildRect)
                    {
                        figures.Add(rectangle);
                        Selected = rectangle;
                    }
                    Mode = EditorMode.None;
                    break;
            }
            Cursor = Cursors.Default;
            Invalidate();
        }

        /// <summary>
        /// Переход в режим рисования прямоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBeginRectangle_Click(object sender, EventArgs e)
        {
            rectangle = new Rect();
            Mode = EditorMode.BuildRect;
            Invalidate();
        }
    }
}
