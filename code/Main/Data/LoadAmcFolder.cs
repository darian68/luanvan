using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Main.Data
{
    class LoadAmcFolder
    {
        private string path;
        private string[] boneNames;
        public LoadAmcFolder(string path, string[] boneNames)
        {
            this.path = path;
            this.boneNames = boneNames;
        }
        /*
         * This method return 2 dimention array 
         * Each element is data of an amc file
         * Each file is a vector of all dof values
         */
        public double[][] readDataAs2DVetor() {
            string[] fileList = Directory.GetFiles(this.path, "*.amc");
            List<double[]> data = new List<double[]>();
            List<double> dof;
            int numFiles = fileList.Length;
            int numBones = boneNames.Length;
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
                        if (line.StartsWith(boneNames[n]))
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
                data.Add(dof.ToArray());
            }
            return data.ToArray();
        }

        /*
         * This method return 3 dimention array
         * Each elemnt contain data of an amc file
         * Each file is a 2D vector of all dof value
         */
        public double[][][] readDataAs3DVetor()
        {
            List<double[][]> data = new List<double[][]>();
            string[] fileList = Directory.GetFiles(this.path, "*.amc");
            List<double[]> dof;
            List<double> frame;
            int numFiles = fileList.Length;
            int numBones = boneNames.Length;
            string fileName;
            string[] lines;
            String[] temp;
            for (int f = 0; f < numFiles; f++)
            {
                fileName = fileList[f];
                lines = File.ReadAllLines(fileName);
                dof = new List<double[]>();
                frame = new List<double>();
                int lineIndex = 0;
                foreach (string line in lines)
                {
                    temp = line.Split(' ');
                    for (int n = 0; n < numBones; n++)
                    {
                        if (line.StartsWith(boneNames[n]))
                        {
                            int length = temp.Length;
                            int j;
                            for (j = 1; j < length; j++)
                            {
                                frame.Add(double.Parse(temp[j]));
                            }
                            // if dof has less then 3 value, set 0 for the rest
                            for (int k = j; k < 4; k++)
                            {
                                frame.Add(0.00);
                            }
                            break;
                        }
                    } // end for bones
                    // Check frame number in the file
                    if (temp.Length == 1)
                    {
                        if (frame.Count > 0)
                        {
                            dof.Add(frame.ToArray());
                        }
                        frame = new List<double>();
                    }
                    // Add last frame
                    if (lineIndex == lines.Length - 1)
                    {
                        if (frame.Count > 0)
                        {
                            dof.Add(frame.ToArray());
                        }
                        frame = new List<double>();
                    }
                    lineIndex++;
                }// end for lines
                // Add data of a file to the list
                data.Add(dof.ToArray());
            }
            return data.ToArray();
        }
    }
}
