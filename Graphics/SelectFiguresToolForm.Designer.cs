namespace Graphics
{
    partial class SelectFiguresToolForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbArrow = new System.Windows.Forms.ToolStripButton();
            this.tsbBuildLine = new System.Windows.Forms.ToolStripButton();
            this.tsbBuildRect = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrow,
            this.tsbBuildLine,
            this.tsbBuildRect});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(24, 283);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbArrow
            // 
            this.tsbArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrow.Image = global::Graphics.Properties.Resources.arrow;
            this.tsbArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrow.Name = "tsbArrow";
            this.tsbArrow.Size = new System.Drawing.Size(29, 20);
            this.tsbArrow.Text = "Отменить выбор";
            this.tsbArrow.Click += new System.EventHandler(this.tsbArrow_Click);
            // 
            // tsbBuildLine
            // 
            this.tsbBuildLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuildLine.Image = global::Graphics.Properties.Resources.poliline;
            this.tsbBuildLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildLine.Name = "tsbBuildLine";
            this.tsbBuildLine.Size = new System.Drawing.Size(29, 20);
            this.tsbBuildLine.Text = "Начать линию";
            this.tsbBuildLine.Click += new System.EventHandler(this.tsbBuildLine_Click);
            // 
            // tsbBuildRect
            // 
            this.tsbBuildRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuildRect.Image = global::Graphics.Properties.Resources.rect;
            this.tsbBuildRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildRect.Name = "tsbBuildRect";
            this.tsbBuildRect.Size = new System.Drawing.Size(29, 20);
            this.tsbBuildRect.Text = "Начать прямоугольник";
            this.tsbBuildRect.Click += new System.EventHandler(this.tsbBuildRect_Click);
            // 
            // SelectFiguresToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(41, 285);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectFiguresToolForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Построить";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectFiguresToolForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectFiguresToolForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBuildLine;
        private System.Windows.Forms.ToolStripButton tsbBuildRect;
        private System.Windows.Forms.ToolStripButton tsbArrow;
    }
}