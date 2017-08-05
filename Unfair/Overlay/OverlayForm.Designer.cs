namespace Unfair.Overlay
{
    partial class OverlayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayForm));
            this.moduleMenu = new Unfair.Overlay.Controls.MenuControl();
            this.SuspendLayout();
            // 
            // moduleMenu
            // 
            this.moduleMenu.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moduleMenu.ForeColor = System.Drawing.Color.Black;
            this.moduleMenu.Location = new System.Drawing.Point(10, 10);
            this.moduleMenu.Name = "moduleMenu";
            this.moduleMenu.Size = new System.Drawing.Size(172, 83);
            this.moduleMenu.TabIndex = 0;
            this.moduleMenu.Text = "Unfair";
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.moduleMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OverlayForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OverlayForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MenuControl moduleMenu;
    }
}