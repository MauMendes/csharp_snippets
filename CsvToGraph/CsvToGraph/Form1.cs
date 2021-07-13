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

using ScottPlot.Renderable;
using ScottPlot.Plottable;

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


            List<string> Header = new List<string>();
            List<List<double>> data = new List<List<double>>();

            char[] delimiters = { '\t' };


            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string WholePath = openFileDialog1.FileName;
                string FileName = openFileDialog1.SafeFileName;
                string DirectoryPath = Path.GetDirectoryName(WholePath);

                //MessageBox.Show(WholePath + " " + FileName + " " + DirectoryPath);

                Stream stream = File.Open(WholePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader FileStream = new StreamReader(stream);

                string []Lines = File.ReadAllLines(WholePath);
                string []HeaderLine = Lines[0].Split(delimiters);


                
                foreach (var item in HeaderLine)
                {
                    if (item != "")
                    {
                        Header.Add(item);
                        data.Add(new List<double>());
                    }
                }


                int skip = 0;
                foreach (var line in Lines)
                {
                    if (skip == 0)//skipHeader
                    {
                        skip = 1;
                        continue;
                    }
                    else 
                    { 
                        var items = line.Split(delimiters);
                        int index = 0;
                        foreach (var item in items)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if(index < data.Count)
                                {
                                    data[index].Add(Convert.ToDouble(item));
                                    index++;
                                }
                            }
                        }
                    }
                }
            }


            var plt = formsPlot1.Plot;

            List<SignalPlot> lstAxis = new List<SignalPlot>();
            

            for (int i = 0; i < 4; i++)
            {
                var axis = plt.AddSignal(data[i].ToArray(), sampleRate: 48_000);
                lstAxis.Add(axis);
            }

            lstAxis[0].YAxisIndex = 0;
            lstAxis[1].YAxisIndex = 1;


            plt.AddAxis(Edge.Left, 2);
            plt.AddAxis(Edge.Left, 3);


            //plt.AddSignal(data2.ToArray(), sampleRate: 48_000);
            plt.Title("One Million Points");
            plt.AxisAuto();


            plt.SaveFig("quickstart_signal.png");
        }
    }
}
