using System.Drawing;

Color[,] pixelBox = new Color[16, 16];

while (true)
{

    for (int i = 0; i < pixelBox.GetLength(0); i++)
    {

        for (int j = 0; j < pixelBox.GetLength(1); j++)
        {

            pixelBox[i, j] = Color.White;

        }

    }

}
