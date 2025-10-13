using System;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type): 
            base(non, nopn, nt, type)
        { }
    }
}
