﻿using System;
using System.Collections.Generic;
using System.IO;

namespace PI_31_2_Tsukanov_Snejok.NeuroNet
{
    class InputLayer
    {
        private double[,] trainset;
        private double[,] testset;
        
        public double[,] Trainset { get => trainset; }
        public double[,] Tesetset { get => testset;  }

        public InputLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpArrStr;
            string[] tmpStr;

            switch (nm)
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    trainset = new double[tmpArrStr.Length, 16];

                    for (int i =0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' '); 

                        for (int j =0; j < 16; j++)
                        {
                            trainset[i, j] = double.Parse(tmpStr[j]);
                        }
                    }
                    Shuffling_Array_Rows(trainset);
                    break;

                case NetworkMode.Test:
                    tmpArrStr = File.ReadAllLines(path + "test.txt");
                    testset = new double[tmpArrStr.Length, 16];

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' ');

                        for (int j = 0; j < 16; j++)
                        {
                            testset[i, j] = double.Parse(tmpStr[j]);
                        }
                    }
                    Shuffling_Array_Rows(testset);
                    break;
            }
        }
        public void Shuffling_Array_Rows(double[,] arr)
        {
            Random random = new Random();
            int rowCount = arr.GetLength(0);
            int colCount = arr.GetLength(1);

            for (int i = rowCount - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                for (int col = 0; col < colCount; col++)
                {
                    double temp = arr[i, col];
                    arr[i, col] = arr[j, col];
                    arr[j, col] = temp;
                }
            }
        }
    }
}
