namespace Sorting
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
            this.pkgPainter = new System.ComponentModel.BackgroundWorker();
            this.btnReorder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pkgPainter
            // 
            this.pkgPainter.WorkerSupportsCancellation = true;
            this.pkgPainter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.pkgPainter_DoWork);
            // 
            // btnReorder
            // 
            this.btnReorder.Location = new System.Drawing.Point(103, 12);
            this.btnReorder.Name = "btnReorder";
            this.btnReorder.Size = new System.Drawing.Size(94, 23);
            this.btnReorder.TabIndex = 0;
            this.btnReorder.Text = "Перемешать";
            this.btnReorder.UseVisualStyleBackColor = true;
            this.btnReorder.Click += new System.EventHandler(this.btnReorder_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 616);
            this.Controls.Add(this.btnReorder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.Text = "Сортировка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker pkgPainter;
        private System.Windows.Forms.Button btnReorder;
    }
}

