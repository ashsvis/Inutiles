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
        ToolFormSelectFigures SelectFigures;

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
            SelectFigures = new ToolFormSelectFigures(this);
            SelectFigures.Owner = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SelectFigures.Show();
            LocationToolSelectFigures();
        }

        private void LocationToolSelectFigures()
        {
            SelectFigures.Location = new Point(this.Location.X - SelectFigures.Size.Width + 8, this.Location.Y + 29);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            LocationToolSelectFigures();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            LocationToolSelectFigures();
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
            tsmiBeginLine.Visible = Selected == null;
            tsmiBeginRectangle.Visible = Selected == null;
            // управление видимостью пунктов связи между фигурами
            tsmiLinkToFigure.Visible = Selected != null && Selected.Parent == null;
            tsmiUnlinkFromFigure.Visible = Selected != null && Selected.Parent != null;
        }

        /// <summary>
        /// Добавление фигуры к списку построенных
        /// </summary>
        private void AddFigure()
        {
            // добавляем новую фигуру в список
            figures.Add(Builded);
            Builded.Name = $"Fig{figures.Count}";
            // построенная фигура делается текущей
            Selected = Builded;
            Mode = EditorMode.None;
            Cursor = Cursors.Default;
            Builded = null;
            SelectFigures.Cursor = Cursors.Default;
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
            Markers fig;
            switch (Mode)
            {
                case EditorMode.Link:
                    if (e.Button == MouseButtons.Left)
                    {
                        CancelBegin();
                        // ищем фигуру "родителя" под курсором
                        fig = figures.LastOrDefault(x => x.Contained(e.Location));
                        // фигура "родителя" должна быть
                        if (fig != null)
                        {
                            // она не должна быть "сама собой" и она не должна быть в дальних "потомках"
                            if (fig != Selected && !CycleExists(Selected, fig))
                            {
                                // убираем связь с прежним "родителем"
                                Selected.Parent?.Childs.Remove(Selected);
                                // добавляем связь у "родителя"
                                fig.Childs.Add(Selected);
                                // указываем "родителя"
                                Selected.Parent = fig;
                                // теперь выбран "родитель"
                                Selected = fig;
                            }
                            else
                                Cursor = Cursors.No;    // показываем, что привязку нельзя сделать                            
                        }
                    }
                    break;
                case EditorMode.Build:
                    if (e.Button == MouseButtons.Left)
                    {
                        // добавлем следующий узел
                        Builded.Add(e.Location);
                        // для прямоугольника нужно всего два узла,
                        // поэтому после добавления второго узла фигуру добавляем и заканчиваем с построением
                        if (Builded is Rect && Builded.Count == 2)
                            AddFigure();
                    }
                    break;
                default:
                    // ищем попадание указателя на фигуру
                    Selected = figures.LastOrDefault(x => x.Contained(e.Location));
                    Invalidate();
                    break;
            }

            // передаём информацию о нажатии кнопки указателя
            Selected?.MouseDown(e.Location, ModifierKeys);

            // обновление содержимого формы
            Invalidate();
        }

        /// <summary>
        /// Проверка наличия цикла в цепочки связей фигур
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="fig"></param>
        /// <returns></returns>
        private bool CycleExists(Markers selected, Markers fig)
        {
            // если в первом круге есть ссылка на будущего "родителя", то сразу выходим
            if (selected.Childs.Contains(fig))
                return true;
            // иначе вызываем эту функцию для всей "потомков"
            foreach (var child in selected.Childs)
            {
                if (CycleExists(child, fig))
                    return true;
            }
            // цикла нет
            return false;
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
                case EditorMode.Link:
                    Cursor = Cursors.UpArrow;
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

        /// <summary>
        /// Переход в режим рисования прямой линии
        /// </summary>
        public void BeginLine()
        {
            Selected = null;
            Builded = new Polyline();
            Mode = EditorMode.Build;
            Invalidate();
            SelectFigures.Cursor = Cursors.Cross;
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

        /// <summary>
        /// Переход в режим рисования прямоугольника
        /// </summary>
        public void BeginRectangle()
        {
            Selected = null;
            Builded = new Rect();
            Mode = EditorMode.Build;
            Invalidate();
            SelectFigures.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Отмена режимов редактирования
        /// </summary>
        public void CancelBegin()
        {
            Builded = null;
            Mode = EditorMode.None;
            Invalidate();
            SelectFigures.Cursor = Cursors.Default;
        }

        private void tsmiLinkToFigure_Click(object sender, EventArgs e)
        {
            LinkBegin();
        }

        /// <summary>
        /// Начать "связывание" фигур
        /// </summary>
        public void LinkBegin()
        {
            Builded = null;
            Mode = EditorMode.Link;
            Invalidate();
            SelectFigures.Cursor = Cursors.UpArrow;
        }

        private void tsmiUnlinkFromFigure_Click(object sender, EventArgs e)
        {
            UnlinkFugure();
        }

        /// <summary>
        /// Разрыв связи между фигурами
        /// </summary>
        public void UnlinkFugure()
        {
            // убираем связь с прежним "родителем"
            Selected?.Parent?.Childs.Remove(Selected);
            // убираем ссылку на "родителя"
            if (Selected != null) Selected.Parent = null;
        }
    }
}
