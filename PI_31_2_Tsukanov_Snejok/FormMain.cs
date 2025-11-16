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
        private Network network;
        HiddenLayer test; //для тестирования генерации весов
        double[] inputPixels;
        public FormMain()
        {
            InitializeComponent();
            inputPixels = new double[15];
            for (int i = 0; i< 15; i++)
            {
                inputPixels[i] = 0;
            }
            network = new Network();
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
        
        
        private void TestButton_Click(object sender, EventArgs e) //для тестирования генерации весов
        {
            test = new HiddenLayer(9, 7, NeuronType.Hidden, nameof(test));
            test.WeightInitialize(MemoryMode.SET, AppDomain.CurrentDomain.BaseDirectory + "memory\\test_memory.csv");
        }
        

        private void button_Recognize_Click(object sender, EventArgs e)
        {
            network.ForwardPass(network, inputPixels);
            label_Output.Text = network.Fact.ToList().IndexOf(network.Fact.Max()).ToString();
            label_Probability.Text = (100 * network.Fact.Max()).ToString("0.00") + "%";
        }

        private void ButtonTrain_Click(object sender, EventArgs e)
        {
            network.Train(network);
            for (int i = 0; i < network.E_error_avr.Length; i++)
            {
                chart_Eavr.Series[0].Points.AddY(network.E_error_avr[i]);
            }
            MessageBox.Show("обучение успешно завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
