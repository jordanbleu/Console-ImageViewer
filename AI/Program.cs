using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Select image to make look bad";
                fd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
                fd.Multiselect = false;
                fd.ShowDialog();

                Image img = Image.FromFile(fd.FileName);
                img.RotateFlip(RotateFlipType.Rotate90FlipX);
                Bitmap bmp = (Bitmap)img;



                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;
                Console.Title = "Console Image Output thing";
                Console.WindowTop = 0;
                Console.WindowLeft = 0;


                int w = bmp.Width;
                int h = bmp.Height;

                // we need to scale down high resolution images...
                int complexity = (int)Math.Floor(Convert.ToDecimal(((w / 100) + (h / 100)) / 2));

                if (complexity < 1) { complexity = 1; }



                for (var x = 0; x < w; x += complexity)
                {
                    for (var y = 0; y < h; y += complexity)
                    {
                        Color clr = bmp.GetPixel(x, y);
                        Console.ForegroundColor = getNearestConsoleColor(clr);
                        Console.Write("█");
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("Image Size: " + w + " x " + h);
                Console.WriteLine("Detail Level: " + complexity);
                Console.WriteLine("(C) 2017 by Jordan Bleu");
                Console.WriteLine("Press a key to try again");

                Console.ReadKey();
            }
        }


        static ConsoleColor getNearestConsoleColor(Color color)
        {
            // this is very likely to be awful and hilarious
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int total = r + g + b;
            decimal darkThreshold = 0.35m; // how dark a color has to be overall to be the dark version of a color

            ConsoleColor cons = ConsoleColor.White;


            if (total >= 39 && total < 100 && areClose(r, g) && areClose(g, b) && areClose(r, b))
            {
                cons = ConsoleColor.DarkGray;
            }

            if (total >= 100 && total < 180 && areClose(r, g) && areClose(g, b) && areClose(r, b))
            {
                cons = ConsoleColor.Gray;
            }


            // if green is the highest value
            if (g > b && g > r)
            {
                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total / 765m) < darkThreshold)
                {
                    cons = ConsoleColor.DarkGreen;
                }
                else
                {
                    cons = ConsoleColor.Green;
                }
            }

            // if red is the highest value
            if (r > g && r > b)
            {

                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total/765m)  < darkThreshold)
                {
                    cons = ConsoleColor.DarkRed;
                }
                else
                {
                    cons = ConsoleColor.Red;
                }
            }

            // if blue is the highest value
            if (b > g && b > r)
            {
                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total/765m)  < darkThreshold)
                {
                    cons = ConsoleColor.DarkBlue; 
                }
                else {
                    cons = ConsoleColor.Blue;
                }
            }


            if (r > b && g > b && areClose(r, g))
            {
                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total/765m)  < darkThreshold)
                {
                    cons = ConsoleColor.DarkYellow;
                }
                else
                {
                    cons = ConsoleColor.Yellow;
                }
            }



            if (b > r && g > r && areClose(b, g))
            {
                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total/765m)  < darkThreshold)
                {
                    cons = ConsoleColor.DarkCyan;
                }
                else
                {
                    cons = ConsoleColor.Cyan;
                }
            }





            if (r > g && b > g && areClose(r, b))
            {
                // ..and color is less that 25% of color
                if (Convert.ToDecimal(total/765m)  < darkThreshold)
                {
                    cons = ConsoleColor.DarkMagenta;
                }
                else
                {
                    cons = ConsoleColor.Magenta;
                }
            }

            if (total >= 180 && areClose(r, g) && areClose(g, b) && areClose(r, b))
            {
                cons = ConsoleColor.White;
            }


            // BLACK
            if (total < 39)
            {
                cons = ConsoleColor.Black;
            }

            



            return cons;
        }
        /// <summary>
        /// Returns true if the numbers are pretty close
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool areClose(int a, int b)
        {
            int diff = Math.Abs(a - b);

            if (diff < 30)
            {
                return true;
            }
            else return false;

        }
    }


}
