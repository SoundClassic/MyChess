namespace ChessForm
{
    partial class ChessForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessForm));
            this.Board = new System.Windows.Forms.Panel();
            this.URL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Board
            // 
            this.Board.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Board.Location = new System.Drawing.Point(12, 12);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(720, 670);
            this.Board.TabIndex = 0;
            this.Board.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Board_MouseClick);
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(84, 700);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(459, 22);
            this.URL.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 700);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // Connect
            // 
            this.Connect.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Connect.Location = new System.Drawing.Point(550, 689);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(182, 48);
            this.Connect.TabIndex = 3;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // WindowsChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 749);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.Board);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WindowsChess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowsChess";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WindowsChess_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Board;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Connect;
    }
}

