using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unfair.Modules;

namespace Unfair.Overlay.Controls
{
    public class MenuControl : Control
    {
        public List<Module> modules;

        public MenuControl() : base()
        {
            modules = new List<Module>();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            Brush borderBrush = Brushes.Red;
            Brush backgroundBrush = Brushes.Black;

            Pen pen = new Pen(borderBrush);

            StringFormat titleFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            Rectangle rectangle = new Rectangle(Location, new Size(150, (modules.Count * Font.Height) + Font.Height + 6));
            Rectangle titleRectangle = new Rectangle(Location, new Size(150, Font.Height));

            g.FillRectangle(backgroundBrush, rectangle);
            g.DrawRectangle(pen, rectangle);

            g.DrawRectangle(pen, titleRectangle);
            g.DrawString(Text, Font, Brushes.White, titleRectangle, titleFormat);

            int index = 0;
            foreach (var module in modules)
            {
                var brush = module.IsToggled ? Brushes.LimeGreen : Brushes.Red;
                g.DrawString(module.Name, Font, brush, new PointF(rectangle.Left + 2.5f, titleRectangle.Bottom + 3f + (Font.Height * index)));
                index++;
            }
        }
    }
}