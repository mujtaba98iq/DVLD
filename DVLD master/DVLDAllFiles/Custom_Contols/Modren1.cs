using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Success_or_fail_Custom
{
    public partial class Modren1 : Control
    {
        public event Action<bool> OnDrawingClosed;
        protected virtual void DrawingClose()
        {
            Action<bool> handler  = OnDrawingClosed;
            if (handler != null)
            {
                handler(true);
            }
        }

        System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        int AnmiationCorner = 10;

        public enum Mode { None , Num , TrueSign , FalseSign}
        Mode enMode = Mode.TrueSign , ActiveMode = Mode.None;
        public Mode Shape
        {
            get { return enMode; }
            set { enMode = value; Invalidate(); }
        }

        public int signWidth = 8;
        public int SignWidth 
        { 
            get{ return signWidth; }
            set { signWidth = value; Invalidate(); }
        }

        private Color signColor = Color.White ;
        public Color SignColor
        {
            get { return signColor; }
            set { signColor = value; Invalidate(); }
        }

        private Color backColor = Color.MediumSeaGreen;
        public Color BakColor
        {
            get { return backColor; }
            set { backColor = value; Invalidate(); }
        }
        string Value = "1";
        public string value
        {
            get { return Value; }
            set { Value = value; Invalidate(); }
        }

        Pen penRound = new Pen(Color.MediumSeaGreen, 8);
        Pen pen = new Pen(Color.White, 10);
        Brush brush = new SolidBrush(Color.MediumSeaGreen);

        Rectangle rec;

        public Modren1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            animationTimer.Interval = 1;
            AnmiationCorner = 10;
            animationTimer.Tick += Timer_Tick;

            this.Size = new Size(130 ,130);
            rec = new Rectangle(0, 0,Width , Height);
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {        
            if (AnmiationCorner < 360)
            {
                AnmiationCorner +=10;             
                if (AnmiationCorner >= 360)
                {
                    AnmiationCorner = 360;
                    animationTimer.Stop();
                    ActiveMode = enMode;
                    if(OnDrawingClosed != null)
                    {
                        DrawingClose();
                    }
                }
                Invalidate();
            }
            
        }

        private GraphicsPath GetTrueGraphicsPath()
        {
            GraphicsPath gp = new GraphicsPath();
            PointF pointF1 = new PointF(Width/5, Height/2.1f);
            PointF pointF2 = new PointF(Width / 2.9f, Height / 1.5f);
            PointF pointF3 = new PointF(Width / 1.3f, Height / 3.1f);

            PointF[] pointFs = { pointF1, pointF2, pointF3 };//

            gp.AddLines(pointFs);
            return gp;
        }

        private void DrawFalseGraphicsPath(Pen pen,PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            PointF pointF1 = new PointF(Width / 3f, Height / 3f);
            PointF pointF2 = new PointF(Width / 1.4f, Height / 1.4f);


            e.Graphics.DrawLine(pen , pointF1,pointF2);

            pointF1 = new PointF(Width / 1.4f, Height / 3f);
            pointF2 = new PointF(Width / 3, Height / 1.41f);
 
            e.Graphics.DrawLine(pen, pointF1, pointF2);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            animationTimer.Start();

            pen = new Pen(signColor, SignWidth);
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            penRound = new Pen(backColor, 8);
            brush = new SolidBrush(backColor);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
          
            e.Graphics.FillPie(brush, rec,  AnmiationCorner, AnmiationCorner);


            switch (ActiveMode)
            {
                case Mode.None:
                    break;
                case Mode.Num:
                {
                     StringFormat sf = new StringFormat();
                     sf.Alignment = StringAlignment.Center;
                     sf.LineAlignment = StringAlignment.Center;
                     e.Graphics.DrawString(Value, Font, new SolidBrush(signColor), new RectangleF(0, 0, Width, Height), sf);
                        break;
                }

                case Mode.TrueSign:
                {
                     e.Graphics.DrawPath(pen, GetTrueGraphicsPath());
                     break;
                }

                case Mode.FalseSign:
                {
                        DrawFalseGraphicsPath(pen , e);
                     break;
                }

            }


            base.OnPaint(e);
            
        }
        private void SuccessMessage_Load(object sender, EventArgs e)
        {

        }

        protected override void OnResize(EventArgs e)
        {
            this.Height = this.Width;
            rec.Size = this.Size;
            base.OnResize(e);
        }
        
        
          
        
    }
}
