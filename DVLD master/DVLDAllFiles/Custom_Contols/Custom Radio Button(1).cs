using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Test_Custom_Radio_Button.CustomRadioButton
{
    public class CustomRadioButton : RadioButton
    {

        //Fields
        private Color checkedColor = Color.MediumSlateBlue;
        private Color unCheckedColor = Color.Gray;

        private float rbBorderSize = 18;
        private float rbCheckSize = 12;
        private float RadWidth = 1.6f;

        [Category("Custom Property")]
        public float BorderSize
        {
            get
            {
                return rbBorderSize;
            }
            set
            {
                rbBorderSize = value;
                this.Invalidate();
            }
        }

        [Category("Custom Property")]
        public float CheckSize
        {
            get
            {
                return rbCheckSize;
            }
            set
            {
                rbCheckSize = value;
                this.Invalidate();
            }
        }
        [Category("Custom Property")]
        public float RadioWidth
        {
            get
            {
                return RadWidth;
            }
            set
            {
                RadWidth = value;
                this.Invalidate();
            }
        }

        //Constructor
        public CustomRadioButton()
        {
            this.MinimumSize = new Size(0, 21);
            this.Padding = new Padding(10, 0, 0, 0);
        }


        //Properties
        public Color CheckedColor
        {
            get { return checkedColor; }
            set
            {
                checkedColor = value;
                this.Invalidate();
            }
        }
        public Color UnCheckedColor
        {
            get { return unCheckedColor; }
            set
            {
                unCheckedColor = value;
                this.Invalidate();
            }
        }


        //Overridden methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //Fields
            Graphics graphics = pevent.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // rbBorderSize = 28F;
             //rbCheckSize = 12F;

            RectangleF rectRbBorder = new RectangleF()
            {
                X = 1.5F,
                Y = (this.Height - rbBorderSize) / 2, //Center
                Width = rbBorderSize,
                Height = rbBorderSize
            };

            RectangleF rectRbCheck = new RectangleF()
            {
                X = rectRbBorder.X + ((rectRbBorder.Width - rbCheckSize) / 2), //Center
                Y = (this.Height - rbCheckSize) / 2, //Center
                Width = rbCheckSize,
                Height = rbCheckSize
            };

            //Drawing

            using (Pen penBorder = new Pen(checkedColor, RadWidth))
            using (SolidBrush brushRbCheck = new SolidBrush(checkedColor))
            using (SolidBrush brushText = new SolidBrush(this.ForeColor))
            {
                //Draw surface
                graphics.Clear(this.BackColor);
                //Draw Radio Button
                if (this.Checked)
                {
                    graphics.DrawEllipse(penBorder, rectRbBorder);//Circle border
                    graphics.FillEllipse(brushRbCheck, rectRbCheck); //Circle Radio Check
                }
                else
                {
                    penBorder.Color = unCheckedColor;
                    graphics.DrawEllipse(penBorder, rectRbBorder); //Circle border
                }
                //Draw text
                graphics.DrawString(this.Text, this.Font, brushText,
                    rbBorderSize + 8, (this.Height - TextRenderer.MeasureText(this.Text, this.Font).Height) / 2);//Y=Center
            }
        }


        //X-> Obsolete code, this was replaced by the Padding property in the constructor
        //(this.Padding = new Padding(10,0,0,0);)


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = TextRenderer.MeasureText(this.Text, this.Font).Width + 30;
        }



    }
}
