using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PI_31_2_Tsukanov_Snejok.NeuroNet;

namespace PI_31_2_Tsukanov_Snejok
{
    public partial class FormMain : Form
    {
        HiddenLayer test; //для тестирования 

        double[] inputPixels;
        public FormMain()
        {
            inputPixels = new double[15];
            for (int i = 0; i< 15; i++)
            {
                inputPixels[i] = 0;
            }
            InitializeComponent();
        }
        private void Changing_State_Pixel_Button_Click(object sender, EventArgs e) // Обработчик данных для кнопки
        {
            if (((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = Color.Black;
                inputPixels[((Button)sender).TabIndex] = 1d;
            }
            else
            {
                ((Button)sender).BackColor = Color.White;
                inputPixels[((Button)sender).TabIndex] = 0d;
            }
        }

        private void button_SaveSample_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name == "SaveTrainSample" ? "train.txt" : "test.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + name;
            string tmpStr = numericUpDown_NecessaryOutput.Value.ToString();

            for (int i=0; i<inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            test = new HiddenLayer(9, 7, NeuronType.Hidden, nameof(test));
            test.WeightInitialize(MemoryMode.SET, "test.csv");
        }
    }
}
