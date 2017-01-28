using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace SOM
{
    class TMap
    {
        Canvas g;
        public int M;

        public TMap(Canvas g, int M)
        {
            this.g = g;
            this.M = M;

            DrawGrid();
        }

        public void DrawSOM(TSOM SOM)
        {
            double max = 0;

            for (int i = 0; i < SOM.Count; i++)
            {
                double v = SOM[i].Val();
                if (v > max)
                {
                    max = v;
                }
            }

            Brush br;

            double L = g.Width;

            double dl = L / M;

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    br = GetColor(SOM.Net[i, j].Val() / max);

                    Ellipse O = new Ellipse();
                    O.Stroke = br;
                    O.Fill = br;
                    O.Width = dl;
                    O.Height = dl;
                    O.Margin = new Thickness(i * dl, j * dl, 0, 0);
                    g.Children.Add(O);
                }
            }
        }

        public void DrawData(TSOM SOM, TX[] XX)
        {
            double[,] Data = new double[M, M];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Data[i, j] = 0;
                }
            }

            double max = 0;

            for (int k = 0; k < XX.Count(); k++)
            {
                double min = double.MaxValue;
                int ind = -1;

                for (int n = 0; n < SOM.Count; n++)
                {
                    double d = SOM[n].R(XX[k].x);

                    if (d < min)
                    {
                        min = d;
                        ind = n;
                    }
                }

                int[] ij = SOM.Get_ij(ind);
                Data[ij[0], ij[1]] += 1;

                if (Data[ij[0], ij[1]] > max)
                {
                    max = Data[ij[0], ij[1]];
                }
            }

            Brush br;

            double L = g.Width;

            double dl = L / M;

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    br = GetColor(Data[i, j] / max);

                    Ellipse O = new Ellipse();
                    O.Stroke = br;
                    O.Fill = br;
                    O.Width = dl;
                    O.Height = dl;
                    O.Margin = new Thickness(i * dl, j * dl, 0, 0);
                    g.Children.Add(O);
                }
            }

        }

        public void DrawCheck(int i, int j)
        {
            double L = g.Width;

            double dl = L / M;

            Ellipse O = new Ellipse();
            O.Stroke = Brushes.Black;
            O.Fill = Brushes.Black;
            O.Width = dl;
            O.Height = dl;
            O.Margin = new Thickness(i * dl, j * dl, 0, 0);
            g.Children.Add(O);
        }

        Brush GetColor(double v)
        {
            if (v > 0.85)
            {
                return Brushes.Red;
            }

            if (v > 0.71)
            {
                return Brushes.Orange;
            }

            if (v > 0.57)
            {
                return Brushes.Yellow;
            }

            if (v > 0.43)
            {
                return Brushes.Green;
            }

            if (v > 0.29)
            {
                return Brushes.Blue;
            }

            if (v > 0.14)
            {
                return Brushes.Pink;
            }

            return Brushes.White;
        }


        public void DrawGrid()
        {
            double L = g.Width;

            double dl = L / M;

            Line l;

            for (int n = 0; n <= M; n++)
            {
                l = new Line();
                l.X1 = 0;
                l.X2 = L;
                l.Y1 = dl * n;
                l.Y2 = l.Y1;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                g.Children.Add(l);

                l = new Line();
                l.X1 = dl * n;
                l.X2 = l.X1;
                l.Y1 = 0;
                l.Y2 = L;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                g.Children.Add(l);

            }
        }
    }

}
