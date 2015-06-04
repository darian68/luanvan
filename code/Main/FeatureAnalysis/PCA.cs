using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Analysis;
using Accord.Math;
using Accord.Statistics.Kernels;
namespace Main.FeatureExtraction
{
    class KPCA
    {
        KernelPrincipalComponentAnalysis kpca;
        public KPCA(double[][] sourceMatrix, double threshold)
        {
            // Create a new linear kernel
            IKernel kernel = new Linear();
            // Creates the Kernel Principal Component Analysis of the given data
            kpca = new KernelPrincipalComponentAnalysis(sourceMatrix.ToMatrix(), kernel);
            kpca.Threshold = threshold; // 0.0001;
            // Compute the Kernel Principal Component Analysis
            kpca.Compute();
        }
        public double[][] transform(double[][] sourceMatrix, int dimension) {
            // KPCA
            return kpca.Transform(sourceMatrix.ToMatrix(), dimension).ToArray();
        }
    }
    class KLDA
    {
        private KernelDiscriminantAnalysis kda;
        public KLDA(double[][] inputs, int[] outputs, double regular)
        {
             // use, such as a linear kernel function.
            IKernel kernel = new Linear();
            // Then, we will create a KDA using this linear kernel.
            kda = new KernelDiscriminantAnalysis(inputs.ToMatrix(), outputs, kernel);
            kda.Regularization = 0.1;//regular; //0.01
            kda.Threshold = regular;
            kda.Compute(); // Compute the analysis
        }
        public double[][] transform(double[][] inputs, int dimension)
        {
            return kda.Transform(inputs.ToMatrix(), dimension).ToArray();
        }
        public int classify(double[] inputs)
        {
            return kda.Classify(inputs);
        }
        public int[] classify(double[][] inputs)
        {
            return kda.Classify(inputs);
        }
    }
}
