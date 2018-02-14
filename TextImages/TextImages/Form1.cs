using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG; *.png; *.jpg | All files(*.*) | *.* ";
            
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

        

        }

        public string ConvertImageToASCII()
        {

            
            string result="";

            Bitmap inputImage = new Bitmap(pictureBox1.Image);
            int blocksHorizLength =  Constants.HorizontalDetalization;
            int blocksVertLength =  Constants.VerticalDetalization;
            int HorizSizeOfBlock = inputImage.Width / Constants.HorizontalDetalization;
            int VertSizeOfBlock = inputImage.Height / Constants.VerticalDetalization;


            progressBar1.Maximum = blocksHorizLength * blocksVertLength;
            float[] RedArray = new float[HorizSizeOfBlock * VertSizeOfBlock];
            float[] GreenArray = new float[HorizSizeOfBlock * VertSizeOfBlock];
            float[] BlueArray = new float[HorizSizeOfBlock * VertSizeOfBlock];

            UInt32 pixel;

            float AvgRed, AvgGreen, AvgBlue, AvgTotal;
            int num;

            for (int indexBlockVert=0; indexBlockVert< blocksVertLength; indexBlockVert++ )
            {
                for (int indexBlockHoriz=0; indexBlockHoriz< blocksHorizLength; indexBlockHoriz++)
                {

                    for (int i=0; i<  VertSizeOfBlock;i++)
                        for (int j=0; j< HorizSizeOfBlock; j++)
                        {
                            try {
                                pixel = (UInt32)(inputImage.GetPixel(indexBlockHoriz * HorizSizeOfBlock + j, indexBlockVert * VertSizeOfBlock + i).ToArgb());

                                RedArray[i * HorizSizeOfBlock + j] = (float)((pixel & 0x00FF0000) >> 16); // красный
                                GreenArray[i * HorizSizeOfBlock + j] = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                                BlueArray[i * HorizSizeOfBlock + j] = (float)(pixel & 0x000000FF); // синий
                            }
                            catch
                            {
                                MessageBox.Show("Error occures!");
                                return "Error";
                            }
                        }
                    AvgRed = RedArray.Average();
                    AvgGreen = GreenArray.Average();
                    AvgBlue = BlueArray.Average();
                    AvgTotal = (AvgRed + AvgGreen + AvgBlue) / 3.0f;

                    num = (int)AvgTotal / (int)Constants.FrameForEachSymbol;
                    result += Constants.ArrayOfDrawingSymbols[num];
                    progressBar1.PerformStep();
                }
                result += "\n";
            }
            
            return result;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ASCIIOutput.Text = ConvertImageToASCII();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Constants.HorizontalDetalization =(int) numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Constants.VerticalDetalization = (int)numericUpDown2.Value;
        }
    }
}
