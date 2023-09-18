using Accessibility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Challenge3JaceAkridge
{
    internal class Logic
    {

        /* Initialize the file paths, an empty colors string list, a btnColor,
           and a start and end index.*/
        String filePath = @"C:\Users\jakri\OneDrive\Documents\Pixel Box Images\Images";
        String textPath = @"C:\Users\jakri\OneDrive\Documents\Pixel Box Images\Colors";
        List<string> colors = new List<string>();
        Color btnColor = new Color();
        int startIndex, endIndex;
        
        /// <summary>
        /// This method saves the design on the board
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="panel"></param>
        public void SaveDesign(Button[,] btn, Panel[,] panel)
        {

            PrepBoard(btn, panel);

            // Open the save file dialog box
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JPEG|*.jpg";
            saveFile.Title = "Save Design";
            saveFile.InitialDirectory = filePath;
            saveFile.RestoreDirectory = true;
            var dialog = saveFile.ShowDialog();

            if (dialog == System.Windows.Forms.DialogResult.OK)
            {

                if (saveFile.FileName != null)
                {

                    SaveData(btn, panel, saveFile);

                }
                else
                {

                    MessageBox.Show("Please enter a file name", "Empty File Name", MessageBoxButtons.OK);

                }

            }
            else
            {

                saveFile.Dispose();

            }

        }

        /// <summary>
        /// This method saves the data from the grid in a bitmap and text file
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="panel"></param>
        /// <param name="saveFile"></param>
        private void SaveData(Button[,] btn, Panel[,] panel, SaveFileDialog saveFile)
        {

            // Initialize the width and height for the bitmap file
            int width = panel.GetLength(0);
            int height = panel.GetLength(1);

            using (Bitmap bmp = new Bitmap(width, height))
            {

                for (int i = 0; i < btn.GetLength(0); i++)
                {

                    for (int j = 0; j < btn.GetLength(1); j++)
                    {

                        // Draw the rectangle to the bitmap
                        Rectangle rect = new Rectangle(panel[i, j].Width * i,
                            panel[i, j].Height * j, panel[i, j].Width, panel[i, j].Height);
                        panel[i, j].DrawToBitmap(bmp, rect);

                    }

                }

                // Save the file
                bmp.Save(saveFile.FileName);

            }

            // Get the file path for the text file that will hold the colors on the grid
            var savedFile = System.IO.Path.GetFileNameWithoutExtension(saveFile.FileName);
            string path = textPath + "\\" + savedFile + ".txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {

                for (int i = 0; i < btn.GetLength(0); i++)
                {

                    for (int j = 0; j < btn.GetLength(1); j++)
                    {

                        // Write the name of the color for each line in the file
                        startIndex = btn[i, j].BackColor.ToString().IndexOf("[") + 1;
                        endIndex = btn[i, j].BackColor.ToString().IndexOf("]");
                        writer.WriteLine(btn[i, j].BackColor.ToString().Substring(startIndex, endIndex - startIndex));

                    }

                }

                // Close the writer
                writer.Close();

            }

        }

        /// <summary>
        /// This method translates the board data to the panel
        /// </summary>
        /// <param name="theGrid"></param>
        /// <param name="panel"></param>
        public void PrepBoard(Button[,] theGrid, Panel[,] panel)
        {

            for (int i = 0; i < theGrid.GetLength(0); i++)
            {

                for (int j = 0; j < theGrid.GetLength(1); j++)
                {

                    panel[i, j].BackColor = theGrid[i, j].BackColor;

                }

            }

        }

        public void ImportDesign(Button[,] btn, Panel[,] panel)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPEG|*.jpg";
            openFile.Title = "Open Design";
            openFile.InitialDirectory = filePath;
            openFile.RestoreDirectory = true;
            var dialog = openFile.ShowDialog();

            if (dialog == System.Windows.Forms.DialogResult.OK)
            {

                if (openFile.OpenFile() != null)
                {

                    LoadData(openFile, btn);

                }

            }
            else
            {

                openFile.Dispose();

            }

        }

        public void LoadData(OpenFileDialog openFile, Button[,] btn)
        {

            var fileToOpen = System.IO.Path.GetFileNameWithoutExtension(openFile.FileName);
            string path = textPath + "\\" + fileToOpen + ".txt";
            int counter = 0;

            using (StreamReader reader = new StreamReader(path, true))
            {

                colors = File.ReadAllLines(path).ToList<String>();

                for (int i = 0; i < btn.GetLength(0); i++)
                {

                    for (int j = 0; j < btn.GetLength(1); j++)
                    {

                        btnColor = Color.FromName(colors[counter]);
                        btn[i, j].BackColor = btnColor;
                        counter += 1;

                    }

                }

                counter = 0;

            }

        }

    }

}
