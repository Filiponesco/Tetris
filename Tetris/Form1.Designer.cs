namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbEkran = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblTxtScore = new System.Windows.Forms.Label();
            this.HMoveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbEkran)).BeginInit();
            this.SuspendLayout();
            // 
            // pbEkran
            // 
            this.pbEkran.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pbEkran.Location = new System.Drawing.Point(7, 8);
            this.pbEkran.Name = "pbEkran";
            this.pbEkran.Size = new System.Drawing.Size(254, 445);
            this.pbEkran.TabIndex = 0;
            this.pbEkran.TabStop = false;
            this.pbEkran.Paint += new System.Windows.Forms.PaintEventHandler(this.PbEkran_Paint);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblScore.Location = new System.Drawing.Point(263, 34);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(70, 25);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "label1";
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Location = new System.Drawing.Point(120, 81);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(35, 13);
            this.lblGameOver.TabIndex = 2;
            this.lblGameOver.Text = "label1";
            // 
            // lblTxtScore
            // 
            this.lblTxtScore.AutoSize = true;
            this.lblTxtScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTxtScore.Location = new System.Drawing.Point(263, 9);
            this.lblTxtScore.Name = "lblTxtScore";
            this.lblTxtScore.Size = new System.Drawing.Size(68, 25);
            this.lblTxtScore.TabIndex = 3;
            this.lblTxtScore.Text = "Score";
            //            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 497);
            this.Controls.Add(this.lblTxtScore);
            this.Controls.Add(this.lblGameOver);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbEkran);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbEkran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEkran;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblTxtScore;
        private System.Windows.Forms.Timer HMoveTimer;
    }
}

