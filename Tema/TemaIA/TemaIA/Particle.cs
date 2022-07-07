using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaIA;
using static TemaIA.Form1;

namespace Particles
{
    
    public class Particle
    {
        public int positionX,positionY;
        public double velocityX,velocityY;
        public double cost;
        public Particle best;
    }

    public class Particle_Swarm_Optimisation {
        public static Random rand = new Random();
        public static HSVColor[] pixels;
        
        public static Particle PSO(Problema problema,Parametri parametri)
    {
            //Initializare
            List<Particle> roi = new List<Particle>(parametri.ClusterSize());
            

            int xmin, xmax, ymin, ymax;
            xmin = problema.Xmin();
            xmax = problema.Xmax();
            ymin = problema.Ymin();
            ymax = problema.Ymax();

            for (int i = 0; i < parametri.ClusterSize(); ++i)
            {
                Particle P = new Particle();
                
                P.positionX = rand.Next(xmin, xmax);////
                P.positionY = rand.Next(ymin, ymax);////

                Form1.mask.SetPixel(P.positionX, P.positionY, Color.GreenYellow);

                HSVColor pixel = pixels[(P.positionX * problema.Xmax()) + P.positionY];

                

                P.cost = problema.FunctieObiectiv(pixel.Hue, pixel.Saturation, pixel.Value);
                P.velocityX = 1;
                P.velocityY = 1;
                P.best = P;

                roi.Add(P);

            }

            //Program
        Particle optimSocial = roi[0];
        foreach (Particle p in roi)
        {
                if (p.cost > optimSocial.cost)
                {
                    optimSocial = p;
                    Console.WriteLine("X: " + optimSocial.positionX + " Y:" + optimSocial.positionY + " values: " + pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].saturation+" "+ pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].value + "\n");
                }
        }
        for (int i = 0; i < parametri.Iteratii(); ++i)
        {
            foreach (Particle p in roi)
            {
                double r1 = rand.NextDouble();
                double r2 = rand.NextDouble();
                p.velocityX = parametri.Inertia() * p.velocityX + parametri.C1() * r1 * (p.best.positionX - p.positionX) + parametri.C2() * r2 * (optimSocial.positionX - p.positionX);
                p.velocityY = parametri.Inertia() * p.velocityY + parametri.C1() * r1 * (p.best.positionY - p.positionY) + parametri.C2() * r2 * (optimSocial.positionY - p.positionY);

                


                if (p.velocityX > parametri.MaxVelocity())
                    p.velocityX = parametri.MaxVelocity();
                if (p.velocityX < -parametri.MaxVelocity())
                    p.velocityX = -parametri.MaxVelocity();

                if (p.velocityY > parametri.MaxVelocity())
                    p.velocityY = parametri.MaxVelocity();
                if (p.velocityY < -parametri.MaxVelocity())
                    p.velocityY = -parametri.MaxVelocity();

                p.positionX = (int)(p.positionX + p.velocityX);
                p.positionY = (int)(p.positionY + p.velocityY);
            
                if (p.positionX > problema.Xmax())
                    p.positionX = problema.Xmax()-1;
                if (p.positionX < problema.Xmin())
                    p.positionX = problema.Xmin();

                if (p.positionY > problema.Ymax())
                    p.positionY = problema.Ymax()-1;
                if (p.positionY < problema.Ymin())
                    p.positionY = problema.Ymin();
                   
                Form1.mask.SetPixel(p.positionX, p.positionY, Color.White);

                HSVColor pixel= pixels[(p.positionX * problema.Xmax()) + p.positionY]; ;
                p.cost = problema.FunctieObiectiv(pixel.Hue, pixel.Saturation, pixel.Value);

                if (p.cost > p.best.cost)
                {
                    p.best = p;
                        if (p.cost > optimSocial.cost)
                        {
                            optimSocial = p;
                            Console.WriteLine("X: " + optimSocial.positionX + " Y:" + optimSocial.positionY + " values: " + pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].saturation + " " + pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].value + "\n");
                        }
                    }

            }

        }

            Console.WriteLine("END ::");
            Console.WriteLine("X: " + optimSocial.positionX + " Y:" + optimSocial.positionY + " values: " + pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].saturation + " " + pixels[optimSocial.positionX * problema.Xmax() + optimSocial.positionY].value + "\n");
            Console.WriteLine("\n\n\n\n" + problema.Xmax());
            return optimSocial;//rezultatul
    }
   
    }


}
