﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace SOM
{
    class TFiler
    {
        ArrayList XX;

        public int N = -1;

        public double[] Maxs;

        public TFiler(string Name)
        {
            try
            {
                XX = new ArrayList();
                StreamReader f = new StreamReader(Name);

                string s;

                while ((s = f.ReadLine()) != null)
                {
                    Add(s);
                }

                f.Close();
            }
            catch
            {
                XX = null;
            }
        }


        public void Add(string s)
        {
            if (s.Length < 3)
            {
                return;
            }

            string[] ss = s.Split('\t');
            N = ss.Count();

            TX X = new TX(N, s, null);

            for (int i = 0; i < N; i++)
            {
                try
                {
                    X.x[i] = Convert.ToDouble(ss[i]);
                }
                catch
                {
                    return;
                }
            }

            XX.Add(X);
        }

        public void Normalize()
        {
            //for (int i = 0; i < Count; i++)
            //{
            //    this[i].Normalize();
            //}

            Maxs = new double[N];

            for (int i = 0; i < N; i++)
            {
                double M = Double.MinValue;

                for (int j = 0; j < Count; j++)
                {
                    if (Math.Abs(this[j][i]) > M)
                    {
                        M = Math.Abs(this[j][i]);
                    }
                }

                if (M < 1E-17)
                {
                    M = 1;
                }

                for (int j = 0; j < Count; j++)
                {
                    this[j][i] /= M;
                }

                Maxs[i] = M;
            }

        }

        public TX this[int i]
        {
            get
            {
                return (TX)XX[i];
            }
            set
            {
                XX.Add(value);
            }
        }


        public int Count
        {
            get
            {
                return XX.Count;
            }
        }
    }
}
