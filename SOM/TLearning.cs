using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM
{
    class TLearning
    {
        public int N;
        TSOM SOM;
        TX[] XX;

        Random rnd;

        public TLearning(TSOM SOM, TX[] XX)
        {
            this.SOM = SOM;
            N = SOM.N;
            this.XX = XX;

            rnd = new Random();

            DoLearning();
        }

        void DoLearning()
        {
            int t = 0;
            int T = 20000;

            while (t < T)
            {
                TX X = GetX();

                double min = double.MaxValue;
                int ind = -1;

                for (int n = 0; n < SOM.Count; n++)
                {
                    double d = SOM[n].R(X.x);

                    if (d < min)
                    {
                        min = d;
                        ind = n;
                    }
                }

                for (int i = 0; i < SOM.Count; i++)
                {
                    for (int n = 0; n < N; n++)
                    {
                        SOM[i].w[n] += eta(t) * h(i, ind, t) * (X.x[n] - SOM[i].w[n]);
                    }
                }

                t++;
            }

        }

        TX GetX()
        {
            return XX[rnd.Next(XX.Count())];
        }

        public double sigma(int t)
        {
            double sigma0 = 20;
            double tau1 = 1000.0 / Math.Log(sigma0);

            return sigma0 * Math.Exp(-t / tau1);
        }

        public double eta(int t)
        {
            double eta0 = 0.1;
            double tau2 = 2000;

            return eta0 * Math.Exp(- t / tau2);
        }

        public double h(int ind1, int ind2, int t)
        {
            double d = SOM.d(ind1, ind2);

            return Math.Exp(-(d * d) / (2 * sigma(t)));
        }
    }
}
