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

            List<double> data = new List<double>();
            List<double> data2 = new List<double>();
            char[] delimiters = { '\t' };


            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string WholePath = openFileDialog1.FileName;
                string FileName = openFileDialog1.SafeFileName;
                string DirectoryPath = Path.GetDirectoryName(WholePath);

                MessageBox.Show(WholePath + " " + FileName + " " + DirectoryPath);

                Stream stream = File.Open(WholePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader FileStream = new StreamReader(stream);
                string HeaderLine = FileStream.ReadLine();
           
                var columns = HeaderLine.Split(delimiters);

                string Line;
                while ((Line = FileStream.ReadLine()) != null)
                {
                    string[] DataColumns = FileStream.ReadLine().Split(delimiters);
                    data.Add(Convert.ToDouble(DataColumns[0]));
                    data2.Add(Convert.ToDouble(DataColumns[4]));
                }

            }


            var plt = formsPlot1.Plot;

            plt.AddSignal(data.ToArray(), sampleRate: 48_000);
            plt.AddSignal(data2.ToArray(), sampleRate: 48_000);
            plt.Title("One Million Points");
            plt.AxisAuto();


            plt.SaveFig("quickstart_signal.png");
        }
    }
}
