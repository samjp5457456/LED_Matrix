using System.Diagnostics.Metrics;

namespace Challenge3JaceAkridge
{
    public partial class ColorCommunicator : Form
    {

        // Initalize button grid, userColor and boolean clear
        Button[,] theGrid = new Button[16, 16];
        Panel[,] panel = new Panel[16, 16];
        Color userColor = new Color();
        Logic logic = new Logic();
        Boolean clear = true;
        Boolean erase = false;

        /// <summary>
        /// This is a constructor that will be called every time the form loads
        /// </summary>
        public ColorCommunicator()
        {

            InitializeComponent();
            InstantiateButtons();

        }

        /// <summary>
        /// This method creates a button grid
        /// </summary>
        public void InstantiateButtons()
        {

            Size btnSize = new Size(85, 85);
            int counter = -8;

            for (int row = 0; row < theGrid.GetLength(0); row++)
            {

                for (int col = 0; col < theGrid.GetLength(1); col++)
                {

                    theGrid[row, col] = new Button();
                    panel[row, col] = new Panel();
                    theGrid[row, col].Size = btnSize;
                    theGrid[row, col].Location = new Point(((this.ClientSize.Width / 2) + counter*btnSize.Width) + (col * btnSize.Width), (row * btnSize.Height) + menuStrip.Height);
                    this.Controls.Add(theGrid[row, col]);
                    theGrid[row, col].FlatStyle = FlatStyle.Flat;
                    theGrid[row, col].Click += GridButtonClickHandler!; // Subscribe button to click handler
                    theGrid[row, col].BackColor = Color.Transparent;
                    panel[row, col].BackColor = Color.Transparent;
        
                }

            }

            ColorAndClear(btnSize);
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click!;
            openToolStripMenuItem.Click += openToolStripMenuItem_Click!;

        }

        /// <summary>
        /// This method places the color and clear buttons and subscribes the
        /// color buttons to ColorButton_ClickHandler
        /// </summary>
        /// <param name="btnSize"></param>
        private void ColorAndClear(Size btnSize)
        {

            colorbtn1.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 2 * btnSize.Height);
            colorbtn2.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 3 * btnSize.Height);
            colorbtn3.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 4 * btnSize.Height);
            colorbtn4.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 5 * btnSize.Height);
            colorbtn5.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 6 * btnSize.Height);
            colorbtn6.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 7 * btnSize.Height);
            colorbtn7.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 8 * btnSize.Height);
            colorbtn8.Location = new Point(24 * btnSize.Width + 10, menuStrip.Height + 9 * btnSize.Height);
            clearbtn.Location = new Point(23 * btnSize.Width + 10, menuStrip.Height + 11 * btnSize.Height);
            erasebtn.Location = new Point(23 * btnSize.Width + 10, menuStrip.Height + 14 * btnSize.Height);

            colorbtn1.Click += ColorButton_ClickHandler!;
            colorbtn2.Click += ColorButton_ClickHandler!;
            colorbtn3.Click += ColorButton_ClickHandler!;
            colorbtn4.Click += ColorButton_ClickHandler!;
            colorbtn5.Click += ColorButton_ClickHandler!;
            colorbtn6.Click += ColorButton_ClickHandler!;
            colorbtn7.Click += ColorButton_ClickHandler!;
            colorbtn8.Click += ColorButton_ClickHandler!;

        }

        /// <summary>
        /// This function handled when a button on the grid is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GridButtonClickHandler(object sender, EventArgs e)
        {

            // Mouse enters event?
            // Unity?

            if (userColor.IsEmpty && erase == false)
            {

                MessageBox.Show("Please select a color", "No Color Selected", MessageBoxButtons.OK);

            }
            else
            {

                if (erase)
                {

                    ((Button)sender).BackColor = Color.Transparent;

                }
                else
                {

                    ((Button)sender).BackColor = userColor;

                }

            }

        }

        /// <summary>
        /// This method handles what happens when clear is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearbtn_ClickHandler(object sender, EventArgs e)
        {

            CheckBoard();

            if (clear)
            {

                MessageBox.Show("Nothing to clear", "Grid Empty", MessageBoxButtons.OK);

            }
            else
            {

                // If user clicks yes, clear the grid. Else, keep the contents of the grid.
                if (MessageBox.Show("Are you sure you want to clear?", "Confirm Clear", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {

                    ClearBoard();
                    ResetColorButtons();

                }
                else
                {

                    KeepBoard();

                }

            }

        }

        /// <summary>
        /// This method checks if the board is clear
        /// </summary>
        private bool CheckBoard()
        {
            for (int row = 0; row < theGrid.GetLength(0); row++)
            {

                for (int col = 0; col < theGrid.GetLength(1); col++)
                {

                    if (theGrid[row, col].BackColor != Color.Transparent)
                    {

                        clear = false;
                        break;

                    }

                }

            }

            return clear;

        }

        /// <summary>
        /// This method clears the board
        /// </summary>
        private void ClearBoard()
        {
            for (int row = 0; row < theGrid.GetLength(0); row++)
            {

                for (int col = 0; col < theGrid.GetLength(1); col++)
                {

                    theGrid[row, col].BackColor = Color.Transparent;

                }

            }

            userColor = Color.Empty;
            clear = true;

        }

        /// <summary>
        /// This method keeps the content of the board currently
        /// </summary>
        private void KeepBoard()
        {
            for (int row = 0; row < theGrid.GetLength(0); row++)
            {

                for (int col = 0; col < theGrid.GetLength(1); col++)
                {

                    theGrid[row, col].BackColor = theGrid[row, col].BackColor;

                }

            }
        }

        /// <summary>
        /// This method sets the user color to the color of the button clicked
        /// on the bottom of the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorButton_ClickHandler(object sender, EventArgs e)
        {

            ResetColorButtons();

            ((Button)sender).ForeColor = Color.Orange;

            userColor = Color.FromName(((Button)sender).Text);

            if (erase == true)
            {

                erase = false;
                MessageBox.Show("Erase deactivated.\nThe active color is " + 
                    ((Button)sender).Text.ToLower(), "Color Changed", MessageBoxButtons.OK);

            }
            else
            {

                MessageBox.Show("The active color is " + ((Button)sender).Text.ToLower(), "Color Changed", MessageBoxButtons.OK);

            }

        }

        /// <summary>
        /// This method resets the color buttons
        /// </summary>
        private void ResetColorButtons()
        {

            colorbtn1.ForeColor = Color.Black;
            colorbtn2.ForeColor = Color.White;
            colorbtn3.ForeColor = Color.Black;
            colorbtn4.ForeColor = Color.Black;
            colorbtn5.ForeColor = Color.Black;
            colorbtn6.ForeColor = Color.Black;
            colorbtn7.ForeColor = Color.Black;
            colorbtn8.ForeColor = Color.Black;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            logic.PrepBoard(theGrid, panel);
            logic.SaveDesign(theGrid, panel);

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            logic.ImportDesign(theGrid, panel);

        }

        /// <summary>
        /// This method handles what the application does when the erase
        /// button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void erasebtn_Click(object sender, EventArgs e)
        {
            
            CheckBoard();

            if (clear)
            {

                MessageBox.Show("Nothing to erase", "Grid Empty", MessageBoxButtons.OK);

            }
            else
            {

                if (erase == false)
                {

                    erase = true;

                }

            }

        }

        /// <summary>
        /// This method shows a textbox upon first opening the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorCommunicator_Load(object sender, EventArgs e)
        {

            MessageBox.Show("1. Click a color before clicking anywhere " +
                "on the box.\n" + "2. Click the erase button to start " +
                "erasing, then click a color to stop.", "Some Instructions",
                MessageBoxButtons.OK);

        }
    }

}