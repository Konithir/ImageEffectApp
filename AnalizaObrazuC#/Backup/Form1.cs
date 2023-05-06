using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
