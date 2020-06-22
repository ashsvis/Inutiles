using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // просим обносить содержимое формы
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
            // просим обносить содержимое формы
            Invalidate();
        }
    }
}
