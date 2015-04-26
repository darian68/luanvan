using Accord.Statistics.Distributions.DensityKernels;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Data
{
    class DTWG : DynamicTimeWarping
    {
       
        public DTWG(int t) : base(t)
        {
            
        }
        public override double Distance(double[] x, double[] y)
        {
            double BC = base.Distance(x, y);
            //double BC1 = Distance(x, y);
            return BC;
        }
        public override double Function(double[] x, double[] y)
        {
            //double BC = base.Function(x, y);
            double BC = Distance(x, y);
            return Math.Exp(-BC / 100);
        }
    }
}
