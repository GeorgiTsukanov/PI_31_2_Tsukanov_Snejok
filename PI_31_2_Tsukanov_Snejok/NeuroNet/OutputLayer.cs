namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class OutputLayer : Layer
    {
        public OutputLayer(int non, int nonp, NeuronType nt, string type) :
            base(non, nonp, nt, type)
        { }

        public override void Recognize(Network net, Layer nextLayer)
        {
            double e_sum = 0;
            for (int i = 0; i < neurons.Length; i++)
                e_sum += neurons[i].Output;

            for (int i = 0; i < neurons.Length; i++)
                net.Fact[i] = neurons[i].Output / e_sum;
        }

        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numofneurons + 1];
            //Здесь надо будет дописать код
            return gr_sum;
        }
    }
}
