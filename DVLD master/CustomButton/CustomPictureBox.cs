using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCustomControll
{
    internal  class CustomPictureBox : PictureBox  
    {
        private short BorderSize = 1 ;
        private short BorderRadius = 300;
        private Color BorderColor = Color.Black;
        private DashStyle BorderDashStyle = DashStyle.Solid;

        [Category("Custom Property")]
        public DashStyle PenBorderDashStyle
        {
            set
            {
                BorderDashStyle = value;
                this.Invalidate();
            }
            get
            {
                return BorderDashStyle;
            }
        }


        [Category("Custom Property")]
        public short BorderSizeProperty
        {
            get
            {
                return BorderSize;
            }
            set
            {
                BorderSize = value;
                this.Invalidate();
            }
        }

        [Category("Custom Property")]
        public short BorderRadiusProperty
        {
            get
            {
                return BorderRadius;
            }
            set
            {
                BorderRadius = value;
                this.Invalidate();
            }
        }

        [Category("Custom Property")]
        public Color BorderColorProperty
        {
            get
            {
                return BorderColor;
            }
            set
            {
                BorderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Property")]
        public Color BackGroundColor
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;

            }
        }

        [Category("Custom Property")]
        public Color FontColor
        {
            get
            {
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;

            }
        }



        public CustomPictureBox()
        {
            this.Size = new Size(300, 300);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Resize += new EventHandler(this.onResize);
        }

        private void onResize(object sender, EventArgs e)
        {
            if (BorderRadius > this.Height)
            {
                BorderRadius = (short)this.Height;
            }
        }

        private GraphicsPath GetGraphicsPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (BorderRadius > 2)
            {
                
                using (GraphicsPath pathSurface = GetGraphicsPath(rectSurface, BorderRadius))
                using (GraphicsPath pathBorder = GetGraphicsPath(rectBorder, BorderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(BorderColor, BorderSize))
                {
                    penBorder.DashStyle = BorderDashStyle ;
                    penBorder.Alignment = PenAlignment.Inset;

                    this.Region = new Region(pathSurface);
                    e.Graphics.DrawPath(penSurface, pathSurface);

                    if (BorderSize >= 1)
                    {
                        e.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }

            else
            {
                this.Region = new Region(rectSurface);

                if (BorderSize >= 1)
                {
                    using (Pen penBorder = new Pen(BorderColor, BorderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Invalidate();

            }
        }

    }
}
