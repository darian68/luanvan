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
        private int dimension = 5;
        private double threshold = 0.0001;
        private double c = 0.4;
        public MainForm()
        {
            InitializeComponent();
        }
        private void readParams()
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
            for (int i = 0; i < size; i++)
            {
                activitySize[expected[i]]++;
                if (expected[i] == result[i])
                {
                    accurancy += 1;
                    activityAccuracy[expected[i]] += 1;
                }
                txtOutput.Text += "\n" + (i + 1).ToString() + "-Expected: " + expected[i] + " - Actual: " + result[i].ToString();
            }
            for (int i = 0; i < numberClasses; i++)
            {
                txtOutput.Text += "\n Accurate rate: " + activityAccuracy[i].ToString() + "/" + activitySize[i] + "(" + (activityAccuracy[i] / activitySize[i]).ToString() + ")";
            }
            txtOutput.Text += "\n Total: " + accurancy.ToString() + "/" + size + "(" + (accurancy / size).ToString() + ")";
        }
        private int[] doPCA(int dimension, double threshold)
        {
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
            // Run the learning algorithm
            double error = teacher.Run();
            //txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = testInputs.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(Matrix.Concatenate(new KPCA().transform(testInputs[i], dimension, threshold)));
            }
            return result;
        }
        private void btnKPCA_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int[] result = doPCA(dimension, threshold);
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }
        private int[] doLDA(int dimention, double threshold)
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
            double[][] trainresult = kda.transform(inputs.ToArray(), dimention);
            double[][] testresult = kda.transform(testInputs2D.ToArray(), dimention);
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
        private void btnKLDA_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            readData();
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int[] result = doLDA(dimension, threshold);
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private void btnDTW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            // Set numberFrame = 0 to read all frames.
            numberFrame = 0;
            readData();
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
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var kernel = new Gaussian<DynamicTimeWarping>(new DynamicTimeWarping(dimension));
            kernel.Sigma = 0.1;
            var machine = new MulticlassSupportVectorMachine(0, kernel, numberClasses);
            var teacher = new MulticlassSupportVectorLearning(machine, inputs.ToArray(), trainOutputs.ToArray());
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs){
                    CacheSize = 0
                };
            double error = teacher.Run();
            txtOutput.Text += "Finished training! Error ratio: " + error.ToString();
            size = testInputs2D.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(testInputs2D[i]);
            }
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private void btnSVM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            readData();
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
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
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
            txtOutput.Text += "Finished training! Error ratio: " + error.ToString();
            size = testInputs2D.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = machine.Compute(testInputs2D[i]);
            }
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Total time: " + seconds.ToString() + " seconds";
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtOutput.Text = "RUNNING...";
            readParams();
            // Set numberFrame = 0 to read all frames.
            //numberFrame = 0;
            int symbols = 5;
            int state = 5;
            double tolerance = 0.0001;
            symbols = int.Parse(txtSymbol.Text);
            state = int.Parse(txtState.Text);
            tolerance = double.Parse(txtTolerance.Text);
            List<int> states = new List<int>();
            for (int i = 0; i < numberClasses; i++)
            {
                states.Add(state);
            }
            // K-means
            readData();
            List<int> temp = new List<int>();
            List<int[]> trainData = new List<int[]>();
            List<int[]> testData = new List<int[]>();
            List<double[]> kmeanData = new List<double[]>();
            int size = trainInputs.Count;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < trainInputs[i].Length; j++)
                {
                    kmeanData.Add(trainInputs[i][j]);
                }
            }
            KMeans kmeans = new KMeans(symbols, Distance.SquareEuclidean);
            kmeans.Tolerance = 0.0001;
            int[] labels = kmeans.Compute(kmeanData.ToArray());
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < trainInputs[i].Length; j++)
                {
                    temp.Add(kmeans.Clusters.Nearest(trainInputs[i][j]));
                }
                trainData.Add(temp.ToArray());
                temp = new List<int>();
            }
            size = testInputs.Count;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < testInputs[i].Length; j++)
                {
                    temp.Add(kmeans.Clusters.Nearest(testInputs[i][j]));
                }
                testData.Add(temp.ToArray());
                temp = new List<int>();
            }
            long begin = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            // Nested models will have two states each
            // Creates a new Hidden Markov Model Sequence Classifier with the given parameters
            HiddenMarkovClassifier classifier = new HiddenMarkovClassifier(numberClasses, states.ToArray(), symbols);
            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning(classifier,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
                {
                    Tolerance = tolerance,
                    //Iterations = 0
                }
            );

            // Train the sequence classifier using the algorithm
            double likelihood = teacher.Run(trainData.ToArray(), trainOutputs.ToArray());
            txtOutput.Text += "Finished training! Likelihood: " + likelihood.ToString();
            size = testData.Count;
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = classifier.Compute(testData[i]);
            }
            showResult(result, testOutputs.ToArray());
            long end = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long seconds = (end - begin) / 1000;
            txtOutput.Text += "\n Tottal time: " + seconds.ToString() + " seconds";
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
            //0: PCA, 1: LDA, 
            double[] w = new double[] {0.735, 0.794, 0.765 };
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
                result[i] = doCombine(new int[] { PCAResult[i], LDAResult[i], anotherResult[i] }, w);
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
            PCAResult = doPCA(4, 0.0001);
        }
        private void LDAThread()
        {
            LDAResult = doLDA(3, 0.1);
        }
        private void AnotherThread()
        {
            anotherResult = doLDA(2, 0.1);
        }
    }
}
