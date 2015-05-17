using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Analysis;
using Accord.Math;

namespace Main.FeatureExtraction
{
    class PCA
    {
        public PCA()
        {

        }
        public double[][] transform(double[][] sourceMatrix, int dimension) {
            return sourceMatrix;
            /*
            // Creates the Principal Component Analysis of the given source
            var pca = new PrincipalComponentAnalysis(sourceMatrix, AnalysisMethod.Center);
            // Compute the Principal Component Analysis
            pca.Compute();
            // Creates a projection considering 80% of the information
            return pca.Transform(sourceMatrix, dimension);
             * */
        }
    }
}
