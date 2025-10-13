using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        string pathFileWeights;
        protected int numofneurons;
        protected int numofprevneurons;
        protected const double learningrate = 0.060;
        protected const double momentum = 0.050d;
        protected double[,] lastdeltaweights;
        protected Neuron[] neurons;

        public Neuron[] Neurons { get => neurons; set => neurons = value; }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < numofneurons; i++)
                {
                    Neurons[i].Activator(value);
                }
                   
            }
        }

        protected Layer(int non, int nopn, NeuronType nt, string nm_Layers)
        {
            numofneurons = non;
            numofprevneurons = nopn;
            Neurons = new Neuron[non];
            name_Layer = nm_Layers;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory//";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";
            lastdeltaweights = new double[non, nopn + 1];
            double[,] Weights;

            if (File.Exists(pathFileWeights))
            {
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights);
            }
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }

            for(int i = 0; i < non; i++)
            {
                double[] tmp_weights = new double[nopn + 1];
                for(int j = 0; j < nopn + 1; j++)
                {
                    tmp_weights[j] = Weights[i, j];

                }
                Neurons[i] = new Neuron(tmp_weights, nt);
            }

    }
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurons + 1];


            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path);
                    string[] memory_elemnt;
                    for (int i = 0; i < numofneurons; i++)
                    {
                        memory_elemnt = tmpStrWeights[i].Split(delim);
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_elemnt[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;

                case MemoryMode.SET:
                    tmpStrWeights = new string[numofneurons];
                    for (int i = 0; i < numofneurons; i++)
                    {
                        string[] memory_elemnt_set = new string[numofprevneurons + 1];
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            memory_elemnt_set[j] = Neurons[i].Weights[j].ToString(System.Globalization.CultureInfo.InvariantCulture);
                        }
                        tmpStrWeights[i] = string.Join(";", memory_elemnt_set);
                    }
                    File.WriteAllLines(path, tmpStrWeights);
                    break;

                case MemoryMode.INIT:
                    Random rand = new Random();
                    for (int i = 0; i < numofneurons; i++)
                    {
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            //weights[i, j] = rand.NextDouble();
                            weights[i, j] = GenerateNormalRandom(rand, 0, 1);
                        }
                    }
                    break;
            }
            return weights;
        }

        private double GenerateNormalRandom(Random rand, double mean, double stdDev)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                 Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * randStdNormal;
        }
    }
}
