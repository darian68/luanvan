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
        public List<double> data;
        public LoadAmcFile(string fileName, string[] pointnames)
        {
            int numBones = pointnames.Length;
            string[] lines = File.ReadAllLines(fileName);
            data = new List<double>();
            String[] temp;
            foreach (var line in lines)
            {
                temp = line.Split(' ');
                for (int n = 0; n < numBones; n++)
                {
                    if (line.StartsWith(pointnames[n]))
                    {
                        int length = temp.Length;
                        int j;
                        for (j = 1; j < length; j++)
                        {
                            data.Add(double.Parse(temp[j]));
                        }
                        // if dof has less then 3 value, set 0 for the rest
                        for (int k = j; k < 4; k++)
                        {
                            data.Add(0.00);
                        }
                        break;
                    }
                } // end for bones
            }// end for lines
        }
    }
}
