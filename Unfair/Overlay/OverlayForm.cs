using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unfair.Core;
using Unfair.Overlay.Controls;

namespace Unfair.Overlay
{
    public partial class OverlayForm : Form
    {
        public readonly Cheat Cheat;

        private Task cheatTask;
        private Font debugFont;
        private Brush debugBrush;

        public OverlayForm()
        {
            InitializeComponent();

            Cheat = new Cheat();
            Cheat.Closed += Cheat_Closed;

            cheatTask = new Task(() => { Cheat.Start(); });

            debugFont = new Font("Consolas", 10, FontStyle.Bold);
            debugBrush = Brushes.Yellow;
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            if (!Cheat.Hook("csgo"))
            {
                MessageBox.Show("Could not find CS:GO. Check if it's running and re-open the cheat.",
                                "Unfair",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            RECT gameWindowRect = GetWindowRect(Cheat.Memory.Process.MainWindowHandle);

            Left = gameWindowRect.left;
            Top = gameWindowRect.top;
            Size = new Size(gameWindowRect.right - gameWindowRect.left, gameWindowRect.bottom - gameWindowRect.top);

            DoubleBuffered = true;

            cheatTask.Start();
            moduleMenu.modules = Cheat.Modules;
        }

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            // DrawStringWithShadow(g, $"X:{Cheat.LocalPlayer.Origin.x} Y:{Cheat.LocalPlayer.Origin.y} Z:{Cheat.LocalPlayer.Origin.z}", debugFont, debugBrush, new PointF(1f, 1f));
        }

        private void Cheat_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void DrawStringWithShadow(Graphics g, string text, Font font, Brush brush, PointF pos)
        {
            g.DrawString(text, font, Brushes.Black, new PointF(pos.X + 1, pos.Y + 1));
            g.DrawString(text, font, brush, pos);
        }

        private RECT GetWindowRect(IntPtr hwnd)
        {
            GetWindowRect(hwnd, out RECT rect);
            return rect;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
    }
}