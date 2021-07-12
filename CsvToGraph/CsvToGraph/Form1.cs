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

namespace CsvToGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory =
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string WholePath = openFileDialog1.FileName;
                string FileName = openFileDialog1.SafeFileName;
                string DirectoryPath = Path.GetDirectoryName(WholePath);

                MessageBox.Show(WholePath + " " + FileName + " " + DirectoryPath);
            }


            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };

            var plt = formsPlot1.Plot;


            double[] values = ScottPlot.DataGen.RandomWalk(1_000_000);
            plt.AddSignal(values, sampleRate: 48_000);
            plt.Title("One Million Points");

            plt.SaveFig("quickstart_signal.png");
        }
    }
}
