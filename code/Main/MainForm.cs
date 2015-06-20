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
using System.Threading;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;

namespace Main
{
    public partial class MainForm : Form
    {
        private string outPutDirectory = new Uri (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        private List<double[][]> trainInputs = null;
        private List<double[][]> testInputs = null;
        private List<int> trainOutputs = null;
        private List<int> testOutputs = null;
        private int numberClasses = 4;
        private int numberFrame = 120;
        private int dimensions = 5;
        private double threshold = 0.0;
        private double c = 0.4;
        public MainForm()
        {
            InitializeComponent();
        }
        private void readParams()
        {
            dimensions = int.Parse(txtDim.Text);
        }
        private void readData()
        {
            if (trainInputs == null 
                || testInputs == null 
                || trainOutputs == null 
                || testOutputs == null)
            {
                trainInputs = new List<double[][]>();
                testInputs = new List<double[][]>();
                trainOutputs = new List<int>();
                testOutputs = new List<int>();
                // all bones (29)
                string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "rhumerus", "rradius", "rwrist", "rhand", "rfingers", "rthumb", "lclavicle", "lhumerus", "lradius", "lwrist", "lhand", "lfingers", "lthumb", "rfemur", "rtibia", "rfoot", "rtoes", "lfemur", "ltibia", "lfoot", "ltoes" };
                // 23 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "lclavicle", "rhumerus", "lhumerus", "rfemur", "lfemur", "rradius", "lradius", "rtibia", "ltibia", "lwrist", "rwrist", "lhand", "rhand", "lfoot", "rfoot" };
                // 13 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "lclavicle", "rhumerus", "lhumerus", "rfemur", "lfemur"};
                // 7 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head"};
                // 4 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax"};
                // 3 bones
                //string[] boneNames = new string[] { "root", "lowerback", "upperback"};
                // 11 bones
                //string[] boneNames = new string[] { "root", "rradius", "lradius", "rtibia", "ltibia", "lwrist", "rwrist", "lhand", "rhand", "lfoot", "rfoot" };
                string pathRun = Path.Combine(outPutDirectory, "Acclaim\\training\\run");
                string pathWalk = Path.Combine(outPutDirectory, "Acclaim\\training\\walk");
                string pathJump = Path.Combine(outPutDirectory, "Acclaim\\training\\jump");
                string pathDance = Path.Combine(outPutDirectory, "Acclaim\\training\\dance");
                string testRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\run");
                string testWalk = Path.Combine(outPutDirectory, "Acclaim\\pattern\\walk");
                string testJump = Path.Combine(outPutDirectory, "Acclaim\\pattern\\jump");
                string testDance = Path.Combine(outPutDirectory, "Acclaim\\pattern\\dance");
                LoadAmcFolder amcRunFolder = new LoadAmcFolder(pathRun, boneNames);
                LoadAmcFolder amcWalkFolder = new LoadAmcFolder(pathWalk, boneNames);
                LoadAmcFolder amcJumpFolder = new LoadAmcFolder(pathJump, boneNames);
                LoadAmcFolder amcDanceFolder = new LoadAmcFolder(pathDance, boneNames);
                LoadAmcFolder amcTestRunFolder = new LoadAmcFolder(testRun, boneNames);
                LoadAmcFolder amcTestWalkFolder = new LoadAmcFolder(testWalk, boneNames);
                LoadAmcFolder amcTestJumpFolder = new LoadAmcFolder(testJump, boneNames);
                LoadAmcFolder amcTestDanceFolder = new LoadAmcFolder(testDance, boneNames);
                double[][][] runInput = amcRunFolder.readDataAs3DVetor(numberFrame);
                double[][][] walkInput = amcWalkFolder.readDataAs3DVetor(numberFrame);
                double[][][] jumpInput = amcJumpFolder.readDataAs3DVetor(numberFrame);
                double[][][] danceInput = amcDanceFolder.readDataAs3DVetor(numberFrame);
                double[][][] testRunInput = amcTestRunFolder.readDataAs3DVetor(numberFrame);
                double[][][] testWalkInput = amcTestWalkFolder.readDataAs3DVetor(numberFrame);
                double[][][] testJumpInput = amcTestJumpFolder.readDataAs3DVetor(numberFrame);
                double[][][] testDanceInput = amcTestDanceFolder.readDataAs3DVetor(numberFrame);
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
                size = danceInput.Length;
                for (int i = 0; i < size; i++)
                {
                    trainInputs.Add(danceInput[i]);
                    trainOutputs.Add(Activity.DANCE);
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
                size = testDanceInput.Length;
                for (int i = 0; i < size; i++)
                {
                    testInputs.Add(testDanceInput[i]);
                    testOutputs.Add(Activity.DANCE);
                }
            }
        }
        private void showResult(int[] result, int[] expected)
        {
            int size = result.Length;
            double accurancy = 0.0;
            int[] activitySize = new int[numberClasses];// 0: run, 1: walk, 2: jump, 3: dance
            double[] activityAccuracy = new double[numberClasses];
            double[,] scale = new double[numberClasses, numberClasses];
            for (int i = 0; i < size; i++)
            {
                activitySize[expected[i]]++;
                if (expected[i] == result[i])
                {
                    accurancy += 1;
                    activityAccuracy[expected[i]] += 1;
                }
                scale[expected[i], result[i]]++;
                txtOutput.Text += "\n" + (i + 1).ToString() + "-Expected: " + expected[i] + " - Actual: " + result[i].ToString();
            }
            txtOutput.Text += "\n Detail";
            txtOutput.Text += "\n =============================================";
            for (int i = 0; i < numberClasses; i++)
            {
                for (int j = 0; j < numberClasses; j++)
                {
                    txtOutput.Text += "\n Matrix[" + i + "," + j + "] = " + scale[i, j] + "/" + activitySize[i]+ "("+(scale[i, j] / activitySize[i]).ToString()+")";
                }
                txtOutput.Text += "\n -------------------------------";
            }
            txtOutput.Text += "\n Summary";
            txtOutput.Text += "\n =============================================";
            for (int i = 0; i < numberClasses; i++)
            {
                txtOutput.Text += "\n Accurate rate: " + activityAccuracy[i].ToString() + "/" + activitySize[i] + "(" + (activityAccuracy[i] / activitySize[i]).ToString() + ")";
            }
            txtOutput.Text += "\n Total: " + accurancy.ToString() + "/" + size + "(" + (accurancy / size).ToString() + ")";
        }
        private int[] doPCA(int dimension)
        {
            List<double[]> inputs = new List<double[]>();
            List<double[]> testInputs2D = new List<double[]>();
            int size = trainInputs.Count;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(trainInputs[i]));
            }
            size = testInputs.Count;
            for (int i = 0; i < size; i++)
            {
                testInputs2D.Add(Matrix.Concatenate(testInputs[i]));
            }
            KPCA pca = new KPCA(inputs.ToArray(), threshold);
            double[][] trainresult = pca.transform(inputs.ToArray(), dimension);
            double[][] testresult = pca.transform(testInputs2D.ToArray(), dimension);
            // Create a new Linear kernel
            IKernel kernel = new Linear();
            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(trainresult[0].Length, kernel, numberClasses);
            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, trainresult, trainOutputs.ToArray());
            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);
            // Run the learning algorithm
            double error = teacher.Run();
            //txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = testInputs.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(testresult[i]);
            }
            return result;
        }
        private void btnKPCA_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "PCA...";
            readParams();
            readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            /*for (int i = 1; i < 164; i++)
            {
                int[] result = doPCA(i, threshold);
                txtOutput.Text += "\n------------------------ " + i;
                showResult(result, testOutputs.ToArray());
            }*/
            int[] result = doPCA(dimensions);
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }
        private int[] doLDA(int dimensions)
        {
            List<double[]> inputs = new List<double[]>();
            List<double[]> testInputs2D = new List<double[]>();
            int size = trainInputs.Count;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(trainInputs[i]));
            }
            size = testInputs.Count;
            for (int i = 0; i < size; i++)
            {
                testInputs2D.Add(Matrix.Concatenate(testInputs[i]));
            }
            KLDA kda = new KLDA(inputs.ToArray(), trainOutputs.ToArray(), threshold);
            double[][] trainresult = kda.transform(inputs.ToArray(), dimensions);
            double[][] testresult = kda.transform(testInputs2D.ToArray(), dimensions);
            // Create a new Linear kernel
            IKernel kernel = new Linear();
            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(trainresult[0].Length, kernel, numberClasses);
            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, trainresult, trainOutputs.ToArray());
            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);
            // Run the learning algorithm
            double error = teacher.Run();
            //txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = testresult.Length;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(testresult[i]);
            }
            return result;
        }
        private void btnKLDA_Click(object sender, EventArgs e)        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "LDA...";
            readParams();           readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            /*for (int i = 1; i < 164; i++)
            {
                int[] result = doLDA(i, threshold);
                txtOutput.Text += "\n------------------------ " + i;
                showResult(result, testOutputs.ToArray());
            }*/
            int[] result = doLDA(dimensions);
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private int[] doSVM()
        {
            List<double[]> inputs = new List<double[]>();
            List<double[]> testInputs2D = new List<double[]>();
            int size = trainInputs.Count;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(trainInputs[i]));
            }
            size = testInputs.Count;
            for (int i = 0; i < size; i++)
            {
                testInputs2D.Add(Matrix.Concatenate(testInputs[i]));
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
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);
            // Run the learning algorithm
            double error = teacher.Run();
            //txtOutput.Text += "Finished training! Error ratio: " + error.ToString();
            size = testInputs2D.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(testInputs2D[i]);
            }
            return result;
        }
        private void btnSVM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "SVM Only...";
            readParams();
            readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int[] result = doSVM();
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private int doCombine(int[] f, double[] w)
        {
            double[] p = new double[numberClasses];
            int size = f.Length;
            for (int i = 0; i < size; i++)
            {
                p[f[i]] += w[i];
            }
            double maxp = p.Max();
            for (int i = 0; i < numberClasses; i++)
            {
                if (p[i] == maxp) return i;
            }
            return -1;
        }
        private void btnCombine_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            //0: SVM, 1: PCA, 2: LDA
            double[] w = new double[] {0.83, 0.9, 0.86 };
            int size = testOutputs.Count;
            Thread t1 = new Thread(new ThreadStart(PCAThread));
            Thread t2 = new Thread(new ThreadStart(LDAThread));
            Thread t3 = new Thread(new ThreadStart(AnotherThread));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = doCombine(new int[] { anotherResult[i], PCAResult[i], LDAResult[i]}, w);
            }
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }
        int[] PCAResult, LDAResult, anotherResult;
        private void PCAThread()
        {
            PCAResult = doPCA(49);
        }
        private void LDAThread()
        {
            LDAResult = doLDA(138);
        }
        private void AnotherThread()
        {
            anotherResult = doSVM();
        }
    }
}
