
namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class Network
    {
        // Архитектура: 15-71-35-10
        private InputLayer inputLayer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(71, 15, NeuronType.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(35, 71, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 35, NeuronType.Output, nameof(output_layer));
        
        private double[] fact = new double[10];
        private double[] e_error_avr;

        public double[] Fact { get => fact; }

        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; }

        public Network() { }

        public void ForwardPass(Network net, double[] netInput)
        {
            net.hidden_layer1.Data = netInput;
            net.hidden_layer1.Recognize(null, net.hidden_layer2);
            net.hidden_layer2.Recognize(null, net.output_layer);
            net.output_layer.Recognize(net, null);
        }


        public void Train(Network net)
        {
            net.inputLayer = new InputLayer(NetworkMode.Train);
            int epoches = 10;
            double tmpSumError;
            double[] errors;
            double[] temp_gsums1;
            double[] temp_gsums2;
            e_error_avr = new double[epoches];
            for (int k = 0; k < epoches; k++)
            {
                e_error_avr[k] = 0;
                net.inputLayer.Shuffling_Array_Rows(net.inputLayer.Trainset);
                for (int i = 0; i < net.inputLayer.Trainset.GetLength(0); i++)
                {
                    double[] tmpTrain = new double[15];
                    for (int j = 0; j < tmpTrain.Length; j++)
                        tmpTrain[j] = net.inputLayer.Trainset[i, j + 1];
                    ForwardPass(net, tmpTrain);

                    tmpSumError = 0;
                    errors = new double[net.fact.Length];
                    for (int x = 0; x < errors.Length; x++)
                    {
                        if (x == net.inputLayer.Trainset[i, 0])
                            errors[x] = 1.0 - net.fact[x];
                        else
                            errors[x] = -net.fact[x];

                        tmpSumError += errors[x] * errors[x] / 2;
                    }
                    e_error_avr[k] += tmpSumError / errors.Length;

                    temp_gsums2 = net.output_layer.BackwardPass(errors);
                    temp_gsums1 = net.hidden_layer2.BackwardPass(temp_gsums2);
                    net.hidden_layer1.BackwardPass(temp_gsums1);
                }
                e_error_avr[k] /= net.inputLayer.Trainset.GetLength(0);
            }
            net.inputLayer = null;
            net.hidden_layer1.WeightInitialize(MemoryMode.SET, nameof(hidden_layer1) + "memory.csv");
            net.hidden_layer2.WeightInitialize(MemoryMode.SET, nameof(hidden_layer2) + "memory.csv");
            net.output_layer.WeightInitialize(MemoryMode.SET, nameof(output_layer) + "memory.csv");
        }

    }
}