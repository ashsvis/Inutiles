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
        SelectFiguresToolForm SelectFigures;

        /// <summary>
        /// точка для хранения запомненной позиции курсора
        /// </summary>
        public Point mousePosition;

        /// <summary>
        /// выбранная фигура
        /// </summary>
        public Markers Selected;

        /// <summary>
        /// конструируемая фигура
        /// </summary>
        public Markers Builded;

        /// <summary>
        /// Список добавленных фигур
        /// </summary>
        private List<Markers> figures = new List<Markers>();

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SelectFigures = new SelectFiguresToolForm(this);
            SelectFigures.Owner = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SelectFigures.Show();
            LocationSelectFigures();
        }

        private void LocationSelectFigures()
        {
            SelectFigures.Location = new Point(this.Location.X - SelectFigures.Size.Width + 7, this.Location.Y + 31);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            LocationSelectFigures();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            LocationSelectFigures();
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

            if (Mode == EditorMode.Build)
            {
                Builded.Draw(e.Graphics);
                Builded.DrawRibbon(e.Graphics, PointToClient(MousePosition));
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
            if (Mode == EditorMode.Build && Builded is Polyline)
            {
                AddFigure();
                e.Cancel = true;
                return;
            }
            // запоминаем позицию курсора в координатах поверхности щелчка
            mousePosition = PointToClient(MousePosition);
            // делаем видимым пункт контекстного меню, если в точке вызова меню есть маркер
            tsmiRemoveMarker.Visible = Selected != null && Selected.MarkerExists(mousePosition);
        }

        /// <summary>
        /// Добавление фигуры к списку построенных
        /// </summary>
        private void AddFigure()
        {
            figures.Add(Builded);
            Selected = Builded;
            Mode = EditorMode.None;
            Cursor = Cursors.Default;
            Builded = null;
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
                case EditorMode.Build:
                    if (e.Button == MouseButtons.Left)
                    {
                        Builded.Add(e.Location);
                        if (Builded is Rect && Builded.Count == 2)
                            AddFigure();
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
                case EditorMode.Build:
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

        // текущий режим редактора
        private EditorMode Mode { get; set; } = EditorMode.None;

        /// <summary>
        /// Переход в режим рисования прямой линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBeginLine_Click(object sender, EventArgs e)
        {
            BeginLine();
        }

        public void BeginLine()
        {
            Selected = null;
            Builded = new Polyline();
            Mode = EditorMode.Build;
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
                    if (Mode == EditorMode.Build && Builded is Polyline)
                        AddFigure();
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
            BeginRectangle();
        }

        public void BeginRectangle()
        {
            Selected = null;
            Builded = new Rect();
            Mode = EditorMode.Build;
            Invalidate();
        }

        public void CancelBegin()
        {
            Builded = null;
            Mode = EditorMode.None;
            Invalidate();
        }
    }
}
