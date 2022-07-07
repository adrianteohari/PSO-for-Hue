using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particles
{
   public  class Problema
    {
        int rezolutieX, rezolutieY;

        public double searchedHue;

        public Problema(int rezolutiex, int rezolutiey, double searchedhue)
        {
            rezolutieX = rezolutiex;
            rezolutieY = rezolutiey;
            searchedHue = searchedhue;
        }
        public double FunctieObiectiv(double hue, double saturation, double value)
        {
            if (hue > searchedHue + 10 || hue < searchedHue - 10)
                return 0;

            return (0.2*saturation)+(0.8*value);
        }

        public int Xmin()
        {
            return 0;
        }

        public int Xmax()
        {
            return rezolutieX;
        }

        public int Ymax()
        {
            return rezolutieY;
        }

        public int Ymin()
        {
            return 0;
        }

    }
}
