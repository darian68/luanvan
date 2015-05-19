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
        public KPCA()
        {

        }
        public double[][] transform(double[][] sourceMatrix, int dimension, double threshold) {
            // KPCA
            // Create a new linear kernel
            IKernel kernel = new Linear();
            // Creates the Kernel Principal Component Analysis of the given data
            var kpca = new KernelPrincipalComponentAnalysis(sourceMatrix.ToMatrix(), kernel);
            kpca.Threshold = threshold; // 0.0001;
            // Compute the Kernel Principal Component Analysis
            kpca.Compute();
            return kpca.Transform(sourceMatrix.ToMatrix(), dimension).ToArray();
        }
    }
    class KLDA
    {
        private KernelDiscriminantAnalysis kda;
        public KLDA(double[][] inputs, int[] outputs)
        {
             // use, such as a linear kernel function.
            IKernel kernel = new Linear();
            // Then, we will create a KDA using this linear kernel.
            kda = new KernelDiscriminantAnalysis(inputs.ToMatrix(), outputs, kernel);
            kda.Regularization = 0.001;
            kda.Compute(); // Compute the analysis
        }
        public double[][] transform(double[][] inputs)
        {
            return kda.Transform(inputs.ToMatrix()).ToArray();
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
