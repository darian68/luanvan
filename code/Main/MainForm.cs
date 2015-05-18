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
using System.IO;
using System.Reflection;
using Main.FeatureExtraction;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.MachineLearning;

namespace Main
{
    public partial class MainForm : Form
    {
        private string outPutDirectory = new Uri (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        private List<double[][]> trainInputs = null;
        private List<double[][]> testInputs = null;
        private List<int> trainOutputs = null;
        private List<int> testOutputs = null;
        private int numberClasses = 3;
        private int numberFrame = 120;
        private int dimension = 5;
        private double threshold = 0.0001;
        private double c = 0.4;
        public MainForm()
        {
            InitializeComponent();
        }
        private void readData()
        {
            // Read params
            if (numberFrame != int.Parse(txtFrames.Text))
            {
                numberFrame = int.Parse(txtFrames.Text);
                // re-read data
                trainInputs = null;
            }
            dimension = int.Parse(txtDim.Text);
            if (txtCom.Text != string.Empty)
            {
                c = double.Parse(txtCom.Text);
            }
            threshold = double.Parse(txtThreshold.Text);
            if (trainInputs == null 
                || testInputs == null 
                || trainOutputs == null 
                || testOutputs == null)
            {
                trainInputs = new List<double[][]>();
                testInputs = new List<double[][]>();
                trainOutputs = new List<int>();
                testOutputs = new List<int>();
                // 23 bones
                string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "lclavicle", "rhumerus", "lhumerus", "rfemur", "lfemur", "rradius", "lradius", "rtibia", "ltibia", "lwrist", "rwrist", "lhand", "rhand", "lfoot", "rfoot" };
                // 13 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "lclavicle", "rhumerus", "lhumerus", "rfemur", "lfemur"};
                // 7 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head"};
                // 4 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax"};
                // 3 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback"};
                string pathRun = Path.Combine(outPutDirectory, "Acclaim\\training\\run");
                string pathWalk = Path.Combine(outPutDirectory, "Acclaim\\training\\walk");
                string pathJump = Path.Combine(outPutDirectory, "Acclaim\\training\\jump");
                string testRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\run");
                string testWalk = Path.Combine(outPutDirectory, "Acclaim\\pattern\\walk");
                string testJump = Path.Combine(outPutDirectory, "Acclaim\\pattern\\jump");
                LoadAmcFolder amcRunFolder = new LoadAmcFolder(pathRun, boneNames);
                LoadAmcFolder amcWalkFolder = new LoadAmcFolder(pathWalk, boneNames);
                LoadAmcFolder amcJumpFolder = new LoadAmcFolder(pathJump, boneNames);
                LoadAmcFolder amcTestRunFolder = new LoadAmcFolder(testRun, boneNames);
                LoadAmcFolder amcTestWalkFolder = new LoadAmcFolder(testWalk, boneNames);
                LoadAmcFolder amcTestJumpFolder = new LoadAmcFolder(testJump, boneNames);
                double[][][] runInput = amcRunFolder.readDataAs3DVetor(numberFrame);
                double[][][] walkInput = amcWalkFolder.readDataAs3DVetor(numberFrame);
                double[][][] jumpInput = amcJumpFolder.readDataAs3DVetor(numberFrame);
                double[][][] testRunInput = amcTestRunFolder.readDataAs3DVetor(numberFrame);
                double[][][] testWalkInput = amcTestWalkFolder.readDataAs3DVetor(numberFrame);
                double[][][] testJumpInput = amcTestJumpFolder.readDataAs3DVetor(numberFrame);
                int size = runInput.Length;
                for (int i = 0; i < size; i++)
                {
                    trainInputs.Add(runInput[i]);
                    trainOutputs.Add(Activity.RUN);
                }
                size = walkInput.Length;
                for (int i = 0; i < size; i++)
                {
                    trainInputs.Add(walkInput[i]);
                    trainOutputs.Add(Activity.WALK);
                }
                size = jumpInput.Length;
                for (int i = 0; i < size; i++)
                {
                    trainInputs.Add(jumpInput[i]);
                    trainOutputs.Add(Activity.JUMP);
                }
                size = testRunInput.Length;
                for (int i = 0; i < size; i++)
                {
                    testInputs.Add(testRunInput[i]);
                    testOutputs.Add(Activity.RUN);
                }
                size = testWalkInput.Length;
                for (int i = 0; i < size; i++)
                {
                    testInputs.Add(testWalkInput[i]);
                    testOutputs.Add(Activity.WALK);
                }
                size = testJumpInput.Length;
                for (int i = 0; i < size; i++)
                {
                    testInputs.Add(testJumpInput[i]);
                    testOutputs.Add(Activity.JUMP);
                }
            }
        }
        private void btnKPCA_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readData();
            List<double[]> inputs = new List<double[]>();
            int size = trainInputs.Count;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(new KPCA().transform(trainInputs[i], dimension, threshold)));
            }
            // Create a new Linear kernel
            IKernel kernel = new Linear();
            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(inputs[0].Length, kernel, numberClasses);
            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, inputs.ToArray(), trainOutputs.ToArray());
            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            if (txtCom.Text != string.Empty)
            {
                teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                    new SequentialMinimalOptimization(svm, classInputs, classOutputs)
                    {
                        Complexity = c
                    };
            }
            else
            {
                teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                    new SequentialMinimalOptimization(svm, classInputs, classOutputs);
            }
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            // Run the learning algorithm
            double error = teacher.Run();
            txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = testInputs.Count;
            double acc = 0.0;
            for (int i = 0; i < size; i++)
            {
                int result = machine.Compute(Matrix.Concatenate(new KPCA().transform(testInputs[i], dimension, threshold)));
                if (testOutputs[i] == result)
                {
                    acc += 1;
                }
                txtOutput.Text += "\n" + (i + 1).ToString() + "-Expected: " + testOutputs[i] + " - Actual: " + result.ToString();
            }
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            double accRate = acc / size;
            txtOutput.Text += "\n Accurate rate: " + acc.ToString() + "/" + size + "(" + accRate.ToString() + ")";
            txtOutput.Text += "\n Training time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private void btnKLDA_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "NOT IMPLEMENT YET";
            Cursor.Current = Cursors.Default;
        }

        private void btnDTW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "NOT IMPLEMENT YET";
            Cursor.Current = Cursors.Default;
        }
    }
}
