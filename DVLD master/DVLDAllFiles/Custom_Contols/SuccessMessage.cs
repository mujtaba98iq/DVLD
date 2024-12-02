using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Success_or_fail_Custom.SuccessOfUnsuccess
{
    public partial class SuccessMessage : UserControl
    {    
   
        Pen pen = new Pen(Color.White, 8);
        Pen penRound = new Pen(Color.MediumSeaGreen, 8);
        Brush brush = new SolidBrush(Color.MediumSeaGreen);//or  LimeSeaGreen
        Rectangle rec = new Rectangle(285, 50, 130, 130);
        
        public SuccessMessage()
        {
            InitializeComponent();
        }
       
        private GraphicsPath GetGraphicsPath()
        {
            GraphicsPath gp = new GraphicsPath();
            PointF pointF1 = new PointF(310, 110);
            PointF pointF2 = new PointF(335, 135);
            PointF pointF3 = new PointF(390, 90);

            PointF[] pointFs = { pointF1, pointF2, pointF3 };//

            gp.AddLines(pointFs);
            return gp;
        }

        private void DrawingPie(PaintEventArgs e)
        {
            short EndAngle = 0;
            while (EndAngle <= 360)
            {
                e.Graphics.DrawArc(penRound, rec, 0, EndAngle);
                EndAngle++;
            }



        }
        protected override void OnPaint(PaintEventArgs e)
        {
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            DrawingPie(e);
            e.Graphics.FillPie(brush, rec, 0, 360);
            e.Graphics.DrawPath(pen, GetGraphicsPath());

        }
        private void SuccessMessage_Load(object sender, EventArgs e)
        {

        }

      

    }
}
