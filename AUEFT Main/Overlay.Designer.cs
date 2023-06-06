
namespace AuftEftMain
{
    partial class Overlay
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DrawTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DrawTimer
            // 
            this.DrawTimer.Enabled = true;
            this.DrawTimer.Interval = 1;
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Overlay";
            this.Text = "Overlay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Overlay_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Overlay_FormClosed);
            this.Load += new System.EventHandler(this.Overlay_Load);
            this.VisibleChanged += new System.EventHandler(this.Overlay_VisibleChanged);
            this.Click += new System.EventHandler(this.Overlay_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Overlay_Paint);

            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer DrawTimer;
    }
}