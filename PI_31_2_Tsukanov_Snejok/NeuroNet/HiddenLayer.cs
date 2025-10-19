using System;
using System.Collections.Generic;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) :
            base(non, nopn, nt, type)
        { }
        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[numofneurons];
            for (int i = 0; i < numofneurons; i++)
                hidden_out[i] = neurons[i].Output;
            nextLayer.Data = hidden_out;// передача выходного сигнала на вход след слоя
        }
        public override double[] BackwardPass(double[] gr_nums)
        {
            double[] gr_sum = new double[numofneurons];
            //Здесь надо будет дописать код метода

            return gr_sum;
        }
    }
}
