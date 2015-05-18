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
    class PCA
    {
        public PCA()
        {

        }
        public double[][] transform(double[][] sourceMatrix, int dimension) {
            //return sourceMatrix;
            // PCA
            /*
            // Creates the Principal Component Analysis of the given source
            var pca = new PrincipalComponentAnalysis(sourceMatrix, AnalysisMethod.Center);
            // Compute the Principal Component Analysis
            pca.Compute();
            // Creates a projection considering 80% of the information
            return pca.Transform(sourceMatrix, dimension);
            */

            // KPCA
            // Create a new linear kernel
            IKernel kernel = new Linear();

            // Creates the Kernel Principal Component Analysis of the given data
            var kpca = new KernelPrincipalComponentAnalysis(sourceMatrix.ToMatrix(), kernel);
            kpca.Threshold = 0.0001;
            // Compute the Kernel Principal Component Analysis
            kpca.Compute();

            // Creates a projection considering 80% of the information
            return kpca.Transform(sourceMatrix.ToMatrix(), dimension).ToArray();
        }
    }
}
