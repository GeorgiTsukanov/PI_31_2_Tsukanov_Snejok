using System;
using static System.Math;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class Neuron
    {
        private NeuronType type;
        private double[] weights;
        private double[] inputs;
        private double output;
        private double derivative;

        private double a = 0.01d;

        public double[] Weights { get => weights; set => weights = value; }
        public double[] Inputs { get => inputs; set => inputs = value; }
        public double Output { get => output; }
        public double Derivative { get => derivative; }

        public Neuron(double[] memoryWeigths, NeuronType typeNeuron)
        {
            type = typeNeuron;
            weights = memoryWeigths;
        }
        public void Activator(double[] i)
        {
            inputs = i;

            double sum = weights[0];

            for (int j = 0; j < inputs.Length; j++)
            {
                sum += inputs[j] * weights[j + 1];
            }

            switch (type)
            {
                case NeuronType.Hidden:
                    output = LeakyReLU(sum);
                    derivative = LeakyRely_Derivativator(sum);
                    break;

                case NeuronType.Output:
                    output = Exp(sum);
                    break;
            }
        }

        public double LeakyReLU(double z)
        {
            return Max(a*z, z);
        }

        public double LeakyRely_Derivativator(double z)
        {
            if (z <= 0) return 0.1d;
            else return 1;

        }
    }
}
