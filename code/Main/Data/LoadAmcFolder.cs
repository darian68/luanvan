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
            int numFiles = fileList.Length;
            int numBones = boneNames.Length;
            string fileName;
            LoadAmcFile amcFile;
            for (int f = 0; f < numFiles; f++)
            {
                fileName = fileList[f];
                amcFile = new LoadAmcFile(fileName, this.boneNames);
                data.Add(amcFile.readDataAsVetor());
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
            int numFiles = fileList.Length;
            int numBones = boneNames.Length;
            string fileName;
            LoadAmcFile amcFile;
            for (int f = 0; f < numFiles; f++)
            {
                fileName = fileList[f];
                amcFile = new LoadAmcFile(fileName, this.boneNames);
                data.Add(amcFile.readDataAs2DVetor());
            }
            return data.ToArray();
        }
    }
}
