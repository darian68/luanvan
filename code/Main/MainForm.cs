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
            string pathRun = "C:\\acclaim\\training\\run";
            string pathWalk = "C:\\acclaim\\training\\walk";
            string pathJump = "C:\\acclaim\\training\\jump";
            string pathDance = "C:\\acclaim\\training\\dance";
            // All bones
            //string[] boneNames = new string[] {"lhipjoint", "rhipjoint", "lowerback", "lhipjoint", "lfemur", "lfemur", "ltibia", "ltibia", "lfoot", "lfoot", "ltoes", "rhipjoint", "rfemur", "rfemur", "rtibia", "rtibia", "rfoot", "rfoot", "rtoes", "lowerback", "upperback", "upperback", "thorax", "thorax", "lowerneck", "lclavicle", "rclavicle", "lowerneck", "upperneck", "upperneck", "head", "lclavicle", "lhumerus", "lhumerus", "lradius", "lradius", "lwrist", "lwrist", "lhand", "lthumb", "lhand", "lfingers", "rclavicle", "rhumerus", "rhumerus", "rradius", "rradius", "rwrist", "rwrist", "rhand", "rthumb", "rhand", "rfingers" };
            string[] boneNames = new string[] { "lhipjoint", "rhipjoint", "rfemur", "lfemur", "rtibia", "ltibia", "rfoot", "head", "rhumerus", "lhumerus", "rhand", "lhand", "lfoot"};
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
            inputs.Add(Matrix.Concatenate(runInput));
            outputs.Add(Activity.RUN);
            inputs.Add(Matrix.Concatenate(walkInput));
            outputs.Add(Activity.WALK);
            /*
            inputs.Add(Matrix.Concatenate(jumpInput));
            outputs.Add(Activity.JUMP);
            inputs.Add(Matrix.Concatenate(danceInput));
            outputs.Add(Activity.DANCE);
             */
            // Create a new Linear kernel
            IKernel kernel = new DynamicTimeWarping(62);
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
            string pathPatternRun = "C:\\acclaim\\pattern\\09_01.amc"; //run 0
            LoadAmcFile runFile = new LoadAmcFile(pathPatternRun, boneNames);
            int result = machine.Compute(Matrix.Concatenate(runFile.vectorlist.ToArray()));
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "C:\\acclaim\\pattern\\07_01.amc";// walk 1
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(Matrix.Concatenate(runFile.vectorlist.ToArray()));
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "C:\\acclaim\\pattern\\16_55.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(Matrix.Concatenate(runFile.vectorlist.ToArray()));
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "C:\\acclaim\\pattern\\07_03.amc"; // walk 1
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(Matrix.Concatenate(runFile.vectorlist.ToArray()));
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = "C:\\acclaim\\pattern\\16_35.amc"; // run 0
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(Matrix.Concatenate(runFile.vectorlist.ToArray()));
            txtOutput.Text += "\nResult Class: " + result;
        }
    }
}
