using System;
using System.Collections.Generic;
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
using System.IO;

namespace SOM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TMap Map;
        TSOM SOM;
        TFiler Filer;
        TLearning Learning;
        TX[] XX;

        int M = 20;
        int N;

        public MainWindow()
        {
            InitializeComponent();

            Map = null;
            SOM = null;
            
        }

        private void cmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmBuild(object sender, RoutedEventArgs e)
        {
            g.Children.Clear();

            Filer = new TFiler("gen.txt");

            N = Filer[0].N;

            Filer.Normalize();

            Map = new TMap(g, M);

            SOM = new TSOM(Map, M, N);

            XX = new TX[Filer.Count];

            for (int i = 0; i < Filer.Count; i++)
            {
                XX[i] = Filer[i];
            }

            Learning = new TLearning(SOM, XX);

            Map.DrawSOM(SOM);

        }

        private void cmShowData(object sender, RoutedEventArgs e)
        {
            if (SOM == null)
            {
                return;
            }

            g.Children.Clear();

            Map = new TMap(g, M);

            Map.DrawData(SOM, XX);
        }

        private void cmGenerate(object sender, RoutedEventArgs e)
        {
            StreamWriter f = new StreamWriter("gen.txt");

            Random rnd = new Random();

            int N = 2;

            for (int i = 0; i < 100; i++)
            {
                string s = "";

                int a = rnd.Next(0, 100);
                int d = 0;

                if ((i % 2) == 0 )
                {
                    d = rnd.Next(20, 50);

                }
                else
                {
                    d = rnd.Next(100, 150);
                }

                f.WriteLine("{0}\t{1}", a, a + d);
            }

            f.Close();
        }

        private void cmCheck(object sender, RoutedEventArgs e)
        {
            string s = textBox.Text;

            string[] ss = s.Split('\t');
            int Ns = ss.Count();

            if (Ns != N)
            {
                return;
            }

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

            X.Normalize();

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

            int[] ij = SOM.Get_ij(ind);

            Map.DrawCheck(ij[0], ij[1]);
        }
    }
}
