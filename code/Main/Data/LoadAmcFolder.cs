using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Main.Data
{
    class LoadAmcFolder
    {
        public List<double[]> data;
        public LoadAmcFolder(string path, string[] pointnames)
        {
            string[] fileList = Directory.GetFiles(path, "*.amc");
            this.data = new List<double[]>();
            List<double> dof;
            int numFiles = fileList.Length;
            int numBones = pointnames.Length;
            string fileName;
            string[] lines;
            String[] temp;
            for (int f = 0; f < numFiles; f++)
            {
                fileName = fileList[f];
                lines = File.ReadAllLines(fileName);
                dof = new List<double>();
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
                                dof.Add(double.Parse(temp[j]));
                            }
                            // if dof has less then 3 value, set 0 for the rest
                            for (int k = j; k < 4; k++)
                            {
                                dof.Add(0.00);
                            }
                            break;
                        }
                    } // end for bones
                }// end for lines
                // Add data of a file to the list
                this.data.Add(dof.ToArray());
            }
        }
    }
}
