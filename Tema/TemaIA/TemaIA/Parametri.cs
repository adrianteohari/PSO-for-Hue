using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particles
{
   public class Parametri
    {
        int clusterSize;
        int parameters;
        double maxVelocity;
        double inertia;
        double c1, c2;
        int iteratii;

        public Parametri(int cluster,int param,double maxvel,int iter)
        {
            c1 = 1;
            c2 = 2;
            inertia =0.4;
            clusterSize = cluster;
            parameters = param;
            maxVelocity = maxvel;
            iteratii = iter;
        }

        public int ClusterSize()
        {
            return clusterSize;
        }

        public int Parameters()
        {
            return parameters;
        }

        public double MaxVelocity()
        {
            return maxVelocity;
        }
        public double Inertia()
        {
            return inertia;
        }

        public double C1()
        {
            return c1;
        }

        public double C2()
        {
            return c2;
        }

        public int Iteratii()
        {
            return iteratii;
        }

    }
}
