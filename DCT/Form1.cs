﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCT
{
    public partial class Form1 : Form
    {
        private List<List<Tuple<int, int>>> y;
        private List<List<Tuple<int, int>>> u;
        private List<List<Tuple<int, int>>> v;

        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp;
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bmp = (Bitmap)Bitmap.FromFile(dlg.FileName);
                pictureBox1.Image = bmp;

                var yuvBitmap = BitmapYUV.FromBitmap(bmp);

<<<<<<< HEAD
                this.y = this.Process(yuvBitmap.Component(Component.Y));
                this.u = this.Process(yuvBitmap.Component(Component.U));
                this.v = this.Process(yuvBitmap.Component(Component.V));
=======
                var Y = this.Process(yuvBitmap.Component(Component.Y));
                var U = this.Process(yuvBitmap.Component(Component.Y));
                var V = this.Process(yuvBitmap.Component(Component.Y));

                var temp = new List<List<List<Tuple<int, int>>>>();

                temp.Add(Y);
                temp.Add(U);
                temp.Add(V);

                Writer.Write(20,temp);
>>>>>>> a96d4229c2c7c25fdf3cc5dc1286e9c59ef0337c
            }
        }

        private List<List<Tuple<int, int>>> Process(Matrix matrix)
        {
            var res = new List<List<Tuple<int, int>>>();

            var dct = Dct.Generate();

            for (int i = 0; i < matrix.Rows; i = i + 8)
                for (int j = 0; j < matrix.Cols; j = j + 8) 
                {
                    var subMtrx = this.GetSubmatrix(matrix, i, j);

                    var pDct = dct*subMtrx*dct.Transpose;

                    var quant = new Quantizer(20);

                    //var quantMtrx = quant.MakeQuantization(pDct.Sasai());

                    //var zsc = new ZigzagScan<int>(quantMtrx);

                    //var wtf = zsc.Scan();

                    //var compressed = Compressor.MakeCompression(wtf);

                    //res.Add(compressed);
                }

            return res;
        }

        private Matrix GetSubmatrix(Matrix matrix, int x, int y)
        {
            var ret = new Matrix(8, 8);

            for (int i = x; i < x + 8; i++)
                for (int j = y; j < y + 8; j++)
                    ret[i - x, j - y] = matrix[i, j];

            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
