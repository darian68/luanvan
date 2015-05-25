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
        private string fileName;
        private string[] boneNames;
        public LoadAmcFile(string fileName, string[] boneNames)
        {
            this.fileName = fileName;
            this.boneNames = boneNames;
        }
        /*
         * This method return an array for all dof values
         */
        public double[] readDataAsVetor()
        {
            List<double> data = new List<double>();
            int numBones = boneNames.Length;
            string[] lines;
            String[] temp;
            lines = File.ReadAllLines(fileName);
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
                            data.Add(double.Parse(temp[j]));
                        }
                        // if dof has less then 3 value, set 0 for the rest
                        for (int k = j; k < 4; k++)
                        {
                            //data.Add(1);
                        }
                        break;
                    }
                } // end for bones
            }
            return data.ToArray();
        }

        /*
         * This method return 2 dimention array
         * Each elemnt contain data of a frame
         */
        public double[][] readDataAs2DVetor()
        {
            List<double[]> data = new List<double[]>();
            List<double> frame;
            int numBones = boneNames.Length;
            string[] lines;
            String[] temp;
            lines = File.ReadAllLines(fileName);
            frame = new List<double>();
            int lineIndex = 0;
            int frameIndex = 0;
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
                            //frame.Add(1);
                        }
                        break;
                    }
                } // end for bones
                // Check frame number in the file
                if (temp.Length == 1)
                {
                    if (frame.Count > 0)
                    {
                        data.Add(frame.ToArray());
                    }
                    frame = new List<double>();
                    frameIndex++;
                }
                // Add last frame
                if (lineIndex == lines.Length - 1)
                {
                    if (frame.Count > 0)
                    {
                        data.Add(frame.ToArray());
                    }
                    frame = new List<double>();
                }
                lineIndex++;
            }// end for lines
            return data.ToArray();
        }
    }
}
