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

        String filePath = @"C:\Users\jakri\OneDrive\Documents\Pixel Box Images\Images";
        String textPath = @"C:\Users\jakri\OneDrive\Documents\Pixel Box Images\Colors";
        List<string> colors = new List<string>();
        Color btnColor = new Color();
        int startIndex, endIndex;

        public void SaveDesign(Button[,] btn, Panel[,] panel)
        {

            PrepBoard(btn, panel);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JPEG|*.jpg";
            saveFile.Title = "Save Design";
            saveFile.InitialDirectory = filePath;
            saveFile.RestoreDirectory = true;
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {

                int width = panel.GetLength(0);
                int height = panel.GetLength(1);

                using (Bitmap bmp = new Bitmap(width, height))
                {

                    for (int i = 0; i < btn.GetLength(0); i++)
                    {

                        for (int j = 0; j < btn.GetLength(1); j++)
                        {

                            Rectangle rect = new Rectangle(panel[i, j].Width * i,
                                panel[i, j].Height * j, panel[i, j].Width, panel[i, j].Height);
                            panel[i,j].DrawToBitmap(bmp, rect);

                        }

                    }

                    bmp.Save(saveFile.FileName);

                }

                var savedFile = System.IO.Path.GetFileNameWithoutExtension(saveFile.FileName);
                string path = textPath + "\\" + savedFile + ".txt";

                if (!File.Exists(path))
                {

                    using (StreamWriter writer = new StreamWriter(path, true))
                    {

                        for (int i = 0; i < btn.GetLength(0); i++)
                        {

                            for (int j = 0; j < btn.GetLength(1); j++)
                            {

                                startIndex = btn[i, j].BackColor.ToString().IndexOf("[") + 1;
                                endIndex = btn[i, j].BackColor.ToString().IndexOf("]");
                                writer.WriteLine(btn[i, j].BackColor.ToString().Substring(startIndex, endIndex - startIndex));

                            }

                        }

                        writer.Close();

                    }

                }

            }

        }

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
            openFile.ShowDialog();

            if (openFile.OpenFile() != null)
            {

                LoadData(openFile, btn);

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
                        Console.WriteLine(colors[counter]);
                        counter += 1;

                    }

                }

                counter = 0;

            }

        }

    }
}
