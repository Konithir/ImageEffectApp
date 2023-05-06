using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AnalizaObrazu
{
    public partial class Form1 : Form
    {
        Bitmap _bitmapa; 
        public Form1()
        {
            InitializeComponent();
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {

                _bitmapa = new Bitmap(this.openFileDialog.FileName);
                this.mainPictureBox.Image = _bitmapa;
            }
        }

        private void negatywToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.Negatyw(_bitmapa);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas CPU Negatywu - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void gPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.CNegatyw(_bitmapa);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas GPU Negatywu - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void cPUToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.Jasnosc(_bitmapa);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas CPU Jasnosc - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void gPUToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.CJasnosc(_bitmapa);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas GPU Jasnosc - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.Rozjasnianie(_bitmapa,25);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas CPU Rozjasnianie - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.Rozjasnianie(_bitmapa, -25);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas CPU Rozjasnianie - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.CRozjasnianie(_bitmapa, 25);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas GPU Rozjasnianie - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bitmapaWynikowa = Efekty.CRozjasnianie(_bitmapa, -25);
            this.mainPictureBox.Image = bitmapaWynikowa;
            watch.Stop();
            Console.WriteLine("Gotowe!Czas GPU Rozjasnianie - " + watch.Elapsed.TotalMilliseconds + " millisekund.");
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z
        }
    }
}
