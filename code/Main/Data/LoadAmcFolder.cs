using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Main.Data
{
    class LoadAmcFolder
    {
        public string[] pointnames;
        public List<int> fileindexlist;
        public List<double[]> vectorlist;
        public LoadAmcFolder(string path, string[] pointnames)
        {
            string[] fileList = Directory.GetFiles(path, "*.amc");
            this.pointnames = pointnames;
            this.vectorlist = new List<double[]>();
            this.fileindexlist =new List<int>();

            for (int f = 0; f < fileList.Length; f++)
            {
                this.fileindexlist.Add(this.vectorlist.Count);
                //fileindexlist này dùng để nhớ vị trí vector của các file

                string fileName = fileList[f];
                var lines = File.ReadAllLines(fileName);
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
}
