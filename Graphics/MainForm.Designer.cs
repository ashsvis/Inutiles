namespace Graphics
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemoveMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBeginLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBeginRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLinkToFigure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnlinkFromFigure = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPopup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsPopup
            // 
            this.cmsPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveMarker,
            this.tsmiAddMarker,
            this.tsmiBeginLine,
            this.tsmiBeginRectangle,
            this.tsmiLinkToFigure,
            this.tsmiUnlinkFromFigure});
            this.cmsPopup.Name = "cmsPopup";
            this.cmsPopup.Size = new System.Drawing.Size(218, 158);
            this.cmsPopup.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPopup_Opening);
            // 
            // tsmiRemoveMarker
            // 
            this.tsmiRemoveMarker.Name = "tsmiRemoveMarker";
            this.tsmiRemoveMarker.Size = new System.Drawing.Size(217, 22);
            this.tsmiRemoveMarker.Text = "Удалить маркер";
            this.tsmiRemoveMarker.Visible = false;
            this.tsmiRemoveMarker.Click += new System.EventHandler(this.tsmiRemoveMarker_Click);
            // 
            // tsmiAddMarker
            // 
            this.tsmiAddMarker.Name = "tsmiAddMarker";
            this.tsmiAddMarker.Size = new System.Drawing.Size(217, 22);
            this.tsmiAddMarker.Text = "Добавить маркер";
            this.tsmiAddMarker.Visible = false;
            this.tsmiAddMarker.Click += new System.EventHandler(this.tsmiAddMarker_Click);
            // 
            // tsmiBeginLine
            // 
            this.tsmiBeginLine.Image = global::Graphics.Properties.Resources.poliline;
            this.tsmiBeginLine.Name = "tsmiBeginLine";
            this.tsmiBeginLine.Size = new System.Drawing.Size(217, 22);
            this.tsmiBeginLine.Text = "Начать линию";
            this.tsmiBeginLine.Click += new System.EventHandler(this.tsmiBeginLine_Click);
            // 
            // tsmiBeginRectangle
            // 
            this.tsmiBeginRectangle.Image = global::Graphics.Properties.Resources.rect;
            this.tsmiBeginRectangle.Name = "tsmiBeginRectangle";
            this.tsmiBeginRectangle.Size = new System.Drawing.Size(217, 22);
            this.tsmiBeginRectangle.Text = "Начать прямоугольник";
            this.tsmiBeginRectangle.Click += new System.EventHandler(this.tsmiBeginRectangle_Click);
            // 
            // tsmiLinkToFigure
            // 
            this.tsmiLinkToFigure.Name = "tsmiLinkToFigure";
            this.tsmiLinkToFigure.Size = new System.Drawing.Size(217, 22);
            this.tsmiLinkToFigure.Text = "Привязать к фигуре...";
            this.tsmiLinkToFigure.Click += new System.EventHandler(this.tsmiLinkToFigure_Click);
            // 
            // tsmiUnlinkFromFigure
            // 
            this.tsmiUnlinkFromFigure.Name = "tsmiUnlinkFromFigure";
            this.tsmiUnlinkFromFigure.Size = new System.Drawing.Size(217, 22);
            this.tsmiUnlinkFromFigure.Text = "Убрать привязку к фигуре";
            this.tsmiUnlinkFromFigure.Click += new System.EventHandler(this.tsmiUnlinkFromFigure_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.cmsPopup;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графика";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsPopup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsPopup;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiBeginLine;
        private System.Windows.Forms.ToolStripMenuItem tsmiBeginRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiLinkToFigure;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnlinkFromFigure;
    }
}

