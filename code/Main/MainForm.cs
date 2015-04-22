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
using Accord.Math;

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
            //string pathJump = "D:\\acclaim\\training\\jump";
            //string pathDance = "D:\\acclaim\\training\\dance";
            string[] boneNames = new string[] { "thorax", "head", "rhand", "lhand", "rfemur", "lfemur", "rfoot", "lfoot" };
            //
            //string[] boneNames = new string[] { "root" };
            LoadAmcFolder amcRun = new LoadAmcFolder(pathRun, boneNames);
            LoadAmcFolder amcWalk = new LoadAmcFolder(pathWalk, boneNames);
            //LoadAmcFolder amcJump = new LoadAmcFolder(pathJump, boneNames);
            //LoadAmcFolder amcDance = new LoadAmcFolder(pathDance, boneNames);
            double[][] runInput = amcRun.data.ToArray();
            double[][] walkInput = amcWalk.data.ToArray();
            //double[][] jumpInput = amcJump.data.ToArray();
            //double[][] danceInput = amcDance.data.ToArray();
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            int size = runInput.Length;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(runInput[i]);
                outputs.Add(Activity.RUN);
            }
            size = walkInput.Length;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(walkInput[i]);
                outputs.Add(Activity.WALK);
            }
            // Create a new Linear kernel with length = 3 = dof
            IKernel kernel = new DynamicTimeWarping(length: 6);
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
            string pathPatternRun = "D:\\acclaim\\pattern\\run\\16_55.amc"; // run 0
            LoadAmcFile runFile = new LoadAmcFile(pathPatternRun, boneNames);
            int result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\walk\\08_01.amc";// walk 1
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\walk\\08_02.amc"; // walk 1
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\walk\\08_01.amc"; // walk 1
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\run\\09_03.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\run\\35_17.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\run\\35_18.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "D:\\acclaim\\pattern\\run\\35_19.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.data.ToArray());
            txtOutput.Text += "\nResult Class: " + result;
        }
    }
}
