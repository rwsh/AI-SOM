using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM
{
    class TSOM
    {
        public TMap Map;
        public int M;
        public int N;

        public TNeuron[,] Net;

        public TSOM(TMap Map, int M, int N)
        {
            this.Map = Map;
            this.M = M;
            this.N = N;

            Net = new TNeuron[M, M];

            Random rnd = new Random();

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Net[i, j] = new TNeuron(N, null, rnd);
                }
            }
        }

        public int Count
        {
            get
            {
                return M * M;
            }
        }

        public int[] Get_ij(int ind)
        {
            int[] ij = new int[2];

            ij[0] = ind / M;
            ij[1] = ind % M;

            return ij;
        }

        public double d(int[] ij1, int[] ij2)
        {
            return Math.Abs((ij1[0] - ij2[0]) * (ij1[0] - ij2[0]) +
                (ij1[1] - ij2[1]) * (ij1[1] - ij2[1]));
        }

        public double d(int ind1, int ind2)
        {
            int[] ij1 = Get_ij(ind1);
            int[] ij2 = Get_ij(ind2);

            return Math.Abs((ij1[0] - ij2[0]) * (ij1[0] - ij2[0]) +
                (ij1[1] - ij2[1]) * (ij1[1] - ij2[1]));
        }


        public TNeuron this[int ind]
        {
            get
            {
                int[] ij = Get_ij(ind);

                return Net[ij[0], ij[1]];
            }

            set
            {
                int[] ij = Get_ij(ind);

                Net[ij[0], ij[1]] = value;
            }
        }
    }


}
