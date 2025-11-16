using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    enum MemoryMode
    {
        GET,
        SET,
        INIT
    }

    enum NeuronType
    {
        Hidden,
        Output
    }

    enum NetworkMode
    {
        Train,
        Test,
        Demo
    }
}
