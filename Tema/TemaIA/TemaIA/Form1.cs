using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Particles;


namespace TemaIA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct HSVColor
        {
            public double Hue;
            public double Saturation;
            public double Value;

            public int hue;
            public int saturation;
            public int value;
        }
        public static HSVColor GetHSV(Color color)
        {
            HSVColor toReturn = new HSVColor();

            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            toReturn.Hue = Math.Round(color.GetHue(), 2);
            toReturn.Saturation = ((max == 0) ? 0 : 1d - (1d * min / max)) * 100;
            toReturn.Saturation = Math.Round(toReturn.Saturation, 2);
            toReturn.Value = Math.Round(((max / 255d) * 100), 2);

            toReturn.hue = (int)toReturn.Hue;
            toReturn.saturation = (int)toReturn.Saturation;
            toReturn.value = (int)toReturn.Value;


            return toReturn;
        }

        
        public static Bitmap mask,bmp;
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd=new OpenFileDialog() { Filter="JPG|*.jpg|PNG|*.png|Bitmap|*.bmp",ValidateNames=true,Multiselect=false})
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                        bmp = (Bitmap)pictureBox1.Image;
                        mask=new Bitmap(bmp);


                        richTextBox1.Text = bmp.Width.ToString();
                        richTextBox1.Text += " "+bmp.Height.ToString()+'\n';


                        Particles.Particle_Swarm_Optimisation.pixels = new HSVColor[bmp.Height* bmp.Width];
                        double maxV=0, maxS=0;
                        //calculam matricea(imaginea cu valorile HSV) ce trebuie trimisa la PSO
                        for (int i = 0; i < bmp.Height; i++)
                        {
                            for (int j = 0; j < bmp.Width; j++)
                            {
                                double R = bmp.GetPixel(i, j).R;
                                
                                Color original = Color.FromArgb(bmp.GetPixel(i, j).R, bmp.GetPixel(i, j).G, bmp.GetPixel(i, j).B);

                                //calculam HSV
                                HSVColor hsv = GetHSV(original);
                                //richTextBox1.Text +="Pixel: "+i+" "+j+" "+ hsv.hue + " " + hsv.saturation + " " + hsv.value + '\n';
                                if (hsv.Saturation > maxS)
                                    maxS = hsv.Saturation;
                                if (hsv.Value > maxV)
                                    maxV = hsv.Value;
                                //updatam matricea
                                Particles.Particle_Swarm_Optimisation.pixels[(i*bmp.Width)+j] = hsv;
                            }
                            
                        }
                        richTextBox1.Text += "MAX SAT: " + maxS.ToString() + " MAX VALUE: " + maxV.ToString() + "\n";
                        richTextBox1.Text += "\n\n";

                        }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            mask = new Bitmap(bmp);
            pictureBox1.Image = mask;
            //PSO
            int hue = 255;
            Problema problema = new Problema(bmp.Width, bmp.Height, Double.Parse(textBox1.Text));
            Parametri parametri = new Parametri(10, 2, 6 ,100);

            //Rezultat in textbox
            try
            {
                Particle result = Particle_Swarm_Optimisation.PSO(problema, parametri);
                mask.SetPixel(result.positionX, result.positionY, Color.PaleVioletRed);
                mask.SetPixel(result.positionX + 1, result.positionY, Color.PaleVioletRed);
                mask.SetPixel(result.positionX - 1, result.positionY, Color.PaleVioletRed);
                mask.SetPixel(result.positionX, result.positionY + 1, Color.PaleVioletRed);
                mask.SetPixel(result.positionX, result.positionY - 1, Color.PaleVioletRed);

                richTextBox1.Text = " ";
                int x, y;
                x = result.positionX;
                y = result.positionY;
                HSVColor pixel = GetHSV(bmp.GetPixel(x, y));
                richTextBox1.Text += "PIXEL: x=" + x + " y=" + y + " " + "HUE: " + pixel.hue + " SATURATION: " + pixel.saturation + " VALUE: " + pixel.value + '\n';
            }
            catch(Exception ex)
            {
                MessageBox.Show("Inafara imaginii!");
            }
               
        }
    }
}
