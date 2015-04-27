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
        string outPutDirectory = new Uri (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSVM_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            string pathRun = Path.Combine(outPutDirectory, "Acclaim\\training\\run");
            string pathWalk = Path.Combine(outPutDirectory, "Acclaim\\training\\walk");
            string pathJump = Path.Combine(outPutDirectory, "Acclaim\\training\\jump");
            //string pathJump = "D:\\acclaim\\training\\jump";
            //string pathDance = "D:\\acclaim\\training\\dance";
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
            //string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head" };
            //string[] boneNames = new string[] { "thorax", "head", "rhand", "lhand", "rfemur", "lfemur", "rfoot", "lfoot" };
            //string[] boneNames = new string[] {"root"};
            //
            //string[] boneNames = new string[] { "head" };

            LoadAmcFolder amcRunFolder = new LoadAmcFolder(pathRun, boneNames);
            LoadAmcFolder amcWalkFolder = new LoadAmcFolder(pathWalk, boneNames);
            LoadAmcFolder amcJumpFolder = new LoadAmcFolder(pathJump, boneNames);
            //LoadAmcFolder amcJump = new LoadAmcFolder(pathJump, boneNames);
            //LoadAmcFolder amcDance = new LoadAmcFolder(pathDance, boneNames);
            int dimension = 3 * (boneNames.Length + 1);
            double[][][] runInput = amcRunFolder.readDataAs3DVetor();
            double[][][] walkInput = amcWalkFolder.readDataAs3DVetor();
            double[][][] jumpInput = amcJumpFolder.readDataAs3DVetor();
            //double[][] danceInput = amcDance.data.ToArray();
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            int size = runInput.Length;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(new PCA().transform(runInput[i], dimension)));
                outputs.Add(Activity.RUN);
            }
            size = walkInput.Length;
            for (int i = 0; i < size; i++)
            {
                inputs.Add(Matrix.Concatenate(new PCA().transform(walkInput[i], dimension)));
                outputs.Add(Activity.WALK);
            }
            size = jumpInput.Length;
            //for (int i = 0; i < size; i++)
            {
                //inputs.Add(Matrix.Concatenate(jumpInput[i]));
                //outputs.Add(Activity.JUMP);
            }
            // Create a new Linear kernel with length = (3 * (boneNames.Length + 1(if have root)) )
            IKernel kernel = new DynamicTimeWarping(dimension);
            // Create a new Multi-class Support Vector Machine with 1 input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(0, kernel, 2);
            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, inputs.ToArray(), outputs.ToArray());
            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs)
                {
                    Complexity = 450
                };
            // Run the learning algorithm
            double error = teacher.Run();
            /*
            DynamicTimeWarping kernel = new DynamicTimeWarping(length: 3 * (boneNames.Length + 1));

            // Now we can create the machine. When using variable-length
            // kernels, we will need to pass zero as the input length:
            var machine = new KernelSupportVectorMachine(kernel, inputs: 0);


            // Create the Sequential Minimal Optimization learning algorithm
            var smo = new SequentialMinimalOptimization(machine, inputs.ToArray(), outputs.ToArray())
            {
                Complexity = 1.5
            };

            // And start learning it!
            double error = smo.Run(true);
            */
            txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = inputs.Count;
            for (int i = 0; i < size; i++)
            {
                double result = machine.Compute(inputs[i]);
                txtOutput.Text += "\n"+ (i+1).ToString()+"-Expected: " + outputs[i] + " - Actual: " + result.ToString();
            }
            /*
            string pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\16_55.amc");
            LoadAmcFile runFile = new LoadAmcFile(pathPatternRun, boneNames);
            double result = machine.Compute(runFile.readDataAsVetor());
            //txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\09_01.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + (result);
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\09_02.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + (result);
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\16_35.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            //txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\08_06.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\08_03.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + result;
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\07_01.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + (result);
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\07_03.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = machine.Compute(runFile.readDataAsVetor());
            txtOutput.Text += "\nResult Class: " + (result);
             */
        }

        private void btnHMM_Click(object sender, EventArgs e)
        {
            
            txtOutput.Text = "";
            string pathRun = Path.Combine(outPutDirectory, "Acclaim\\training\\run");
            string pathWalk = Path.Combine(outPutDirectory, "Acclaim\\training\\walk");
            // 23 bones
            string[] boneNames = new string[] { "root", "lowerback", "upperback", "thorax", "lowerneck", "upperneck", "head", "rclavicle", "lclavicle", "rhumerus", "lhumerus", "rfemur", "lfemur", "rradius", "lradius", "rtibia", "ltibia", "lwrist", "rwrist", "lhand", "rhand", "lfoot", "rfoot" };
            LoadAmcFolder amcRunFolder = new LoadAmcFolder(pathRun, boneNames);
            LoadAmcFolder amcWalkFolder = new LoadAmcFolder(pathWalk, boneNames);
            //LoadAmcFolder amcJump = new LoadAmcFolder(pathJump, boneNames);
            //LoadAmcFolder amcDance = new LoadAmcFolder(pathDance, boneNames);
            double[][][] runInput = amcRunFolder.readDataAs3DVetor();
            double[][][] walkInput = amcWalkFolder.readDataAs3DVetor();

            // Kmeans
            // Create a new K-Means algorithm with 3 clusters 
            KMeans kmeans = new KMeans(10);

            // Compute the algorithm, retrieving an integer array
            //  containing the labels for each of the observations
            int[][] inputs = new int[runInput.Length + walkInput.Length][];
            int[] outputs = new int[runInput.Length + walkInput.Length];
            int size = runInput.Length;
            for (int i = 0; i < size; i++)
            {
                inputs[i] = kmeans.Compute(runInput[i]);
                outputs[i] = Activity.RUN;
            }
            int size2 = walkInput.Length;
            for (int i = 0; i < size2; i++)
            {
                inputs[i + size] = kmeans.Compute(walkInput[i]);
                outputs[i + size] = Activity.WALK;
            }
            // We are trying to predict two different classes
            int classes = 2;

            // Each sequence may have up to two symbols (0 or 1)
            int symbols = 10;

            // Nested models will have two states each
            int[] states = new int[] { 10, 10 };

            // Creates a new Hidden Markov Model Sequence Classifier with the given parameters
            HiddenMarkovClassifier classifier = new HiddenMarkovClassifier(classes, states, symbols);

            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning(classifier,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
                {
                    Tolerance = 0.0001,
                    Iterations = 0
                }
            );
            
            // Train the sequence classifier using the algorithm
            double likelihood = teacher.Run(inputs, outputs);
            double error = teacher.ComputeError(inputs, outputs);
            txtOutput.Text = "Finished training! Error ratio: " + error.ToString();
            size = inputs.Length;
            for (int i = 0; i < size; i++)
            {
                double result = classifier.Compute(inputs[i]);
                txtOutput.Text += "\n" + (i + 1).ToString() + "-Expected: " + outputs[i] + " - Actual: " + result.ToString();
            }
            /*
            txtOutput.Text += "\nError: " + error.ToString();
            string pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\09_01.amc");
            LoadAmcFile runFile = new LoadAmcFile(pathPatternRun, boneNames);
            int result = classifier.Compute(kmeans.Compute(runFile.readDataAs2DVetor()));
            txtOutput.Text += "\nResult Class: " + (result);

            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\09_02.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = result = classifier.Compute(kmeans.Compute(runFile.readDataAs2DVetor()));
            txtOutput.Text += "\nResult Class: " + (result);
            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\07_01.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = classifier.Compute(kmeans.Compute(runFile.readDataAs2DVetor()));
            txtOutput.Text += "\nResult Class: " + (result);

            pathPatternRun = Path.Combine(outPutDirectory, "Acclaim\\pattern\\07_03.amc");
            runFile = new LoadAmcFile(pathPatternRun, boneNames);
            result = result = classifier.Compute(kmeans.Compute(runFile.readDataAs2DVetor()));
            txtOutput.Text += "\nResult Class: " + (result);
             */
        }
    }
}
