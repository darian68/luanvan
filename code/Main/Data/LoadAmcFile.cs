using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main.Data
{
    class LoadAmcFile
    {
        public string[] pointnames;
        public List<double[]> vectorlist;
        public LoadAmcFile(string path, string[] pointnames)
        {
            this.pointnames = pointnames;
            this.vectorlist = new List<double[]>();
            var lines = File.ReadAllLines(path);
            List<double> vector = new List<double>();

            foreach (var line in lines)
            {
                String[] temp = line.Split(' ');
                for (int n = 0; n < this.pointnames.Length; n++)
                //Lặp trong mảng pointnames[]
                {
                    if (line.StartsWith(this.pointnames[n]))
                    {
                        int length = temp.Length;
                        for (int j = 1; j < length; j++)
                            vector.Add(double.Parse(temp[j]));
                        if (n == this.pointnames.Length - 1)
                        {
                            vectorlist.Add(vector.ToArray());
                            vector = new List<double>();
                        }
                        break;
                    }
                }
            }
        }
    }
}
