namespace Graphics
{
    partial class ToolFormSelectFigures
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
            this.SuspendLayout();
            // 
            // ToolFormSelectFigures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(26, 285);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolFormSelectFigures";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Построить";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectFiguresToolForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectFiguresToolForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SelectFiguresToolForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectFiguresToolForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}