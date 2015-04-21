using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main.Data;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSVM_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            string pathRun = "D:\\acclaim\\training\\run";
            string pathWalk = "D:\\acclaim\\training\\walk";
            string pathJump = "D:\\acclaim\\training\\jump";
            string pathDance = "D:\\acclaim\\training\\dance";
            // All bones
            string[] boneNames = new string[] { "root", "lhipjoint", "rhipjoint", "lowerback", "lhipjoint", "lfemur", "lfemur", "ltibia", "ltibia", "lfoot", "lfoot", "ltoes", "rhipjoint", "rfemur", "rfemur", "rtibia", "rtibia", "rfoot", "rfoot", "rtoes", "lowerback", "upperback", "upperback", "thorax", "thorax", "lowerneck", "lclavicle", "rclavicle", "lowerneck", "upperneck", "upperneck", "head", "lclavicle", "lhumerus", "lhumerus", "lradius", "lradius", "lwrist", "lwrist", "lhand", "lthumb", "lhand", "lfingers", "rclavicle", "rhumerus", "rhumerus", "rradius", "rradius", "rwrist", "rwrist", "rhand", "rthumb", "rhand", "rfingers" };
            //
            LoadAmcFolder amcRun = new LoadAmcFolder(pathRun, boneNames);
            LoadAmcFolder amcWalk = new LoadAmcFolder(pathWalk, boneNames);
            LoadAmcFolder amcJump = new LoadAmcFolder(pathJump, boneNames);
            LoadAmcFolder amcDance = new LoadAmcFolder(pathDance, boneNames);
            double[][] runInput = amcRun.vectorlist.ToArray();
            double[][] walkInput = amcWalk.vectorlist.ToArray();
            double[][] jumpInput = amcJump.vectorlist.ToArray();
            double[][] danceInput = amcDance.vectorlist.ToArray();
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            // Get input from run
            int size = runInput.Count();
            for (int s = 0; s < size; s++)
            {
                inputs.Add(runInput[s]);
                outputs.Add(Activity.RUN);
            }
            // Get input from walk
            size = walkInput.Count();
            for (int s = 0; s < size; s++)
            {
                inputs.Add(walkInput[s]);
                outputs.Add(Activity.WALK);
            }
            /*
            // Get input from jump
            size = jumpInput.Count();
            for (int s = 0; s < size; s++)
            {
                inputs.Add(jumpInput[s]);
                outputs.Add(Activity.JUMP);
            }
            // Get input from dance
            size = danceInput.Count();
            for (int s = 0; s < size; s++)
            {
                inputs.Add(danceInput[s]);
                outputs.Add(Activity.DANCE);
            }
             */
            // Create a new Linear kernel
            IKernel kernel = new DynamicTimeWarping(1);
            // Create a new Multi-class Support Vector Machine with 1 input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(0, kernel, 2);
            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, inputs.ToArray(), outputs.ToArray());
            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);
            // Run the learning algorithm
            double error = teacher.Run();
            txtOutput.Text = "Finish training!";
        }
    }
}
