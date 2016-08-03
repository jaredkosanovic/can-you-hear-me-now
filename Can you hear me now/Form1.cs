using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Can_you_hear_me_now
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void draw_Click(object sender, EventArgs e)
        {
            //declaring local variables for input validation
            int tran1x = 0;
            int tran1y = 0;
            int tran2x = 0;
            int tran2y = 0;

            double wavelength = 0;
            
            //input validation for transmitter 1 x coordinate
            do
            {
                try
                {
                    tran1x = System.Convert.ToInt16(trans1x.Text);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Not a valid X coordinate for transmitter 1!");
                    return;
                }

                if ((tran1x < 1) || (tran1x > 400))
                {
                    MessageBox.Show("Not a valid X coordinate for transmitter 1!");
                    return;        
                }

            }
            while ((tran1x < 1) || (tran1x > 400));

            //input validation for transmitter 1 y coordinate
            do
            {
                try
                {
                    tran1y = System.Convert.ToInt16(trans1y.Text);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Not a valid Y coordinate for transmitter 1!");
                    return;
                }

                if ((tran1y < 1) || (tran1y > 400))
                {
                    MessageBox.Show("Not a valid Y coordinate for transmitter 1!");
                    return;
                }

            }
            while ((tran1y < 1) || (tran1y > 400));

            //input validation for transmitter 2 x coordinate
            do
            {
                try
                {
                    tran2x = System.Convert.ToInt16(trans2x.Text);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Not a valid X coordinate for transmitter 2!");
                    return;
                }

                if ((tran2x < 1) || (tran2x > 400))
                {
                    MessageBox.Show("Not a valid X coordinate for transmitter 2!");
                    return;
                }

            }
            while ((tran2x < 1) || (tran2x > 400));

            //input validation for transmitter 2 y coordinate
            do
            {
                try
                {
                    tran2y = System.Convert.ToInt16(trans2y.Text);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Not a valid Y coordinate for transmitter 2!");
                    return;
                }

                if ((tran2y < 1) || (tran2y > 400))
                {
                    MessageBox.Show("Not a valid Y coordinate for transmitter 2!");
                    return;
                }

            }
            while ((tran2y < 1) || (tran2y > 400));

            //input validation for wavelength value
            do
            {
                try
                {
                    wavelength = System.Convert.ToDouble(wavelengthtext.Text);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Not a valid wavelength value! Must be greater than 0.");
                    return;
                }

                if (wavelength <= 0)
                {
                    MessageBox.Show("Not a valid wavelength value! Must be greater than 0.");
                    return;
                }
            }
            while (wavelength <= 0);

            //declaring the transmitters
            transmitter Trans1 = new transmitter();
            transmitter Trans2 = new transmitter();

            //and the antenna
            antenna MyPhone = new antenna();

            //settings the x and y coordinates of the transmitters in their classes
            Trans1.Set_X(tran1x);
            Trans1.Set_Y(tran1y);

            Trans2.Set_X(tran2x);
            Trans2.Set_Y(tran2y);
            
            //new drawing pen
            System.Drawing.Pen RectanglePen = new System.Drawing.Pen(Color.Black, 1);

            int PBwidth = pictureBox1.Width;
            int PBheight = pictureBox1.Height;

            for (int Xpos = 0; Xpos < PBwidth; Xpos++) //Loops through all X coordinates
            {
                for (int Ypos = 0; Ypos < PBheight; Ypos++) //Loops through all Y coordinates at each X
                {
                    MyPhone.Set_X(Xpos);
                    MyPhone.Set_Y(Ypos);

                    //Get the signal strength from the antenna object
                    double SignalStrength = MyPhone.signal_strength(Trans1.Get_X(), Trans1.Get_Y(), Trans2.Get_X(), Trans2.Get_Y(), wavelength);

                    //Convert the signal strength into an RGB color with 100% strength equal to RGB 255,255,255
                    int ColorStrength = System.Convert.ToInt16(255.0 * (SignalStrength / 2.0));

                    //Set the pen color based on the signal strength
                    RectanglePen.Color = System.Drawing.Color.FromArgb(ColorStrength, ColorStrength, ColorStrength);

                    //Draws the rectangle at the given coordinates
                    pictureBox1.CreateGraphics().DrawRectangle(RectanglePen, Xpos, PBheight - Ypos, 1, 1);
                }
            }
            
            //draws a red square around where transmitter 1 is located
            for (int Xpos = (Trans1.Get_X() - 4); Xpos <= (Trans1.Get_X() + 4); Xpos++)
            {
                for (int Ypos = (Trans1.Get_Y() - 4); Ypos <= (Trans1.Get_Y() + 4); Ypos++)
                {
                    RectanglePen.Color = System.Drawing.Color.FromName("Red");

                    pictureBox1.CreateGraphics().DrawRectangle(RectanglePen, Xpos, (400 - Ypos), 1, 1);
                }
            }

            //draws a blue square around where transmitter 2 is located
            for (int Xpos = (Trans2.Get_X() - 4); Xpos <= (Trans2.Get_X() + 4); Xpos++)
            {
                for (int Ypos = (Trans2.Get_Y() - 4); Ypos <= (Trans2.Get_Y() + 4); Ypos++)
                {
                    RectanglePen.Color = System.Drawing.Color.FromName("Blue");

                    pictureBox1.CreateGraphics().DrawRectangle(RectanglePen, Xpos, (400 - Ypos), 1, 1);
                }
            }
        }

        //the exit button closes the program
        private void Exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    //simple transmitter class...stores x and y values
    class transmitter
    {
        private int x_coordinate;
        private int y_coordinate;

        public int Get_X() { return(x_coordinate); }
        public int Get_Y() { return(y_coordinate); }
        public void Set_X(int X) { x_coordinate = X; }
        public void Set_Y(int Y) { y_coordinate = Y; }
    }
    //antenna class, similar to transmitter except it can calculate signal strength
    class antenna
    {
        private int ant_x_coord;
        private int ant_y_coord;

        public int Get_X() { return (ant_x_coord); }
        public int Get_Y() { return (ant_y_coord); }
        public void Set_X(int X) { ant_x_coord = X; }
        public void Set_Y(int Y) { ant_y_coord = Y; }
 
        public double signal_strength(int x1, int y1, int x2, int y2, double wave)
        {
            double pi = Math.PI;
            double d1, d2, sig_strength;

            //calculating the distance between the antenna and the transmitter using the Pythagorean theorm
            d1 = (Math.Sqrt((Math.Pow((ant_x_coord - x1), 2)) + (Math.Pow((ant_y_coord - y1), 2))));
            d2 = (Math.Sqrt((Math.Pow((ant_x_coord - x2), 2)) + (Math.Pow((ant_y_coord - y2), 2))));

            //calcuting the signal strength using the supplied formula
            sig_strength = (1 + Math.Sin((pi / 2) + (Math.Abs(d1 - d2) / wave * (2 * pi))));

            return sig_strength;
        }
    } 
} //scroll up, why are you down here?
