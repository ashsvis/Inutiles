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
            this.tsmiAddMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPopup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsPopup
            // 
            this.cmsPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveMarker,
            this.tsmiAddMarker});
            this.cmsPopup.Name = "cmsPopup";
            this.cmsPopup.Size = new System.Drawing.Size(181, 70);
            this.cmsPopup.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPopup_Opening);
            // 
            // tsmiAddMarker
            // 
            this.tsmiAddMarker.Name = "tsmiAddMarker";
            this.tsmiAddMarker.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddMarker.Text = "Добавить маркер";
            this.tsmiAddMarker.Click += new System.EventHandler(this.tsmiAddMarker_Click);
            // 
            // tsmiRemoveMarker
            // 
            this.tsmiRemoveMarker.Name = "tsmiRemoveMarker";
            this.tsmiRemoveMarker.Size = new System.Drawing.Size(180, 22);
            this.tsmiRemoveMarker.Text = "Удалить маркер";
            this.tsmiRemoveMarker.Visible = false;
            this.tsmiRemoveMarker.Click += new System.EventHandler(this.tsmiRemoveMarker_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.cmsPopup;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графика";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.cmsPopup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsPopup;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveMarker;
    }
}

