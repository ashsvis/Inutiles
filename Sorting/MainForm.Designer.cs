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
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnReverseRange = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pkgPainter
            // 
            this.pkgPainter.WorkerSupportsCancellation = true;
            this.pkgPainter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.pkgPainter_DoWork);
            // 
            // btnReorder
            // 
            this.btnReorder.Location = new System.Drawing.Point(121, 8);
            this.btnReorder.Name = "btnReorder";
            this.btnReorder.Size = new System.Drawing.Size(103, 23);
            this.btnReorder.TabIndex = 0;
            this.btnReorder.Text = "Перемешать";
            this.btnReorder.UseVisualStyleBackColor = true;
            this.btnReorder.Click += new System.EventHandler(this.btnReorder_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 8);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(48, 23);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Пуск";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnReverseRange
            // 
            this.btnReverseRange.Location = new System.Drawing.Point(230, 8);
            this.btnReverseRange.Name = "btnReverseRange";
            this.btnReverseRange.Size = new System.Drawing.Size(103, 23);
            this.btnReverseRange.TabIndex = 0;
            this.btnReverseRange.Text = "Обратный ряд";
            this.btnReverseRange.UseVisualStyleBackColor = true;
            this.btnReverseRange.Click += new System.EventHandler(this.btnReverseRange_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(67, 8);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(48, 23);
            this.btnStep.TabIndex = 0;
            this.btnStep.Text = "Шаг";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 803);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnReverseRange);
            this.Controls.Add(this.btnReorder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сортировка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker pkgPainter;
        private System.Windows.Forms.Button btnReorder;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnReverseRange;
        private System.Windows.Forms.Button btnStep;
    }
}

