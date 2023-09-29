using System.Diagnostics.Metrics;
using System.Text;

namespace Challenge3JaceAkridge
{
    public partial class ColorCommunicator : Form
    {

        /* Initalize the visible button grid, the invisible panel grid,
           the user color, an instantiation of the logic class,
           and booleans for clear, erase, drawing and awaiting click statuses. */
        Button[,] theGrid = new Button[16, 16];
        Panel[,] panel = new Panel[16, 16];
        Color userColor = new Color();
        Size standardSize = new Size(80, 80);
        Size selectSize = new Size(100, 100);
        Logic logic = new Logic();
        Boolean clear = true;
        Boolean erase = false;
        Boolean drawing = false;

        /// <summary>
        /// This is a constructor that will be called every time the form loads
        /// </summary>
        public ColorCommunicator()
        {

            InitializeComponent();
            InstantiateButtons();

        }

        /// <summary>
        /// This method shows a textbox upon first opening the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorCommunicator_Load(object sender, EventArgs e)
        {

            MessageBox.Show("1. Select a color to start drawing.\n"
                + "2. Click the erase button to start erasing, then click " + 
                "a color to stop.\n" + "3. Please use descriptive names " + 
                "when saving your images.", "Some Instructions",
                MessageBoxButtons.OK);

        }

        /// <summary>
        /// This method creates the button grid and does the necessary subscriptions
        /// for the events in the application
        /// </summary>
        public void InstantiateButtons()
        {

            int counter = -8;

            for (int row = 0; row < theGrid.GetLength(0); row++)
            {

                for (int col = 0; col < theGrid.GetLength(1); col++)
                {

                    theGrid[row, col] = new Button();
                    panel[row, col] = new Panel();
                    theGrid[row, col].Size = standardSize;
                    panel[row, col].Size = standardSize;
                    theGrid[row, col].Location = new Point(((this.ClientSize.Width / 2) + counter*standardSize.Width) + (col * standardSize.Width), (row * standardSize.Height) + menuStrip.Height);
                    this.Controls.Add(theGrid[row, col]);
                    theGrid[row, col].FlatStyle = FlatStyle.Flat;
                    theGrid[row, col].MouseEnter += OnMouseEnter!;
                    theGrid[row, col].MouseLeave += OnMouseLeave!;
                    theGrid[row, col].BackColor = Color.Transparent;
                    panel[row, col].BackColor = Color.Transparent;
        
                }

            }

            // Set up the color and clear buttons
            ColorAndClear(standardSize);

            // Subscribe the clicks of the menu strip buttons to the event handlers
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
        /// The Mouse Enter event is activated when a mouse hovers over the button.
        /// This method changes the back color of the button depending on the logic
        /// Set by the booleans.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, System.EventArgs e)
        {

            ((Button)sender).Size = selectSize;
            ChangeButton(sender);

        }

        /// <summary>
        /// Change the button size back to its original size when
        /// the mouse leaves the area of a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, System.EventArgs e)
        {

            ((Button)sender).Size = standardSize;

        }

        /// <summary>
        /// This method changes the back color of the current button based
        /// on the logic set by the booleans after ChangeStatus().
        /// </summary>
        /// <param name="sender"></param>
        private void ChangeButton(object sender)
        {

            if (erase)
            {

                ((Button)sender).BackColor = Color.Transparent;

            }
            else if (drawing)
            {

                ((Button)sender).BackColor = userColor;

            }
            else
            {

                ((Button)sender).BackColor = ((Button)sender).BackColor;

            }

        }

        /// <summary>
        /// This method handles what happens when clear is clicked.
        /// Either tell the user there is nothing to clear or read the result
        /// from the pop-up dialog box and respond accordingly.
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

                    // Clear the board and reset the buttons
                    ClearBoard();
                    ResetColorButtons();

                }
                else
                {

                    // Keep the colors on the board as is if the user clicks "No"
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
        /// This method resets the board, userColor and clear boolean
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
        /// This method keeps the content of the board
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
        /// to the right of the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorButton_ClickHandler(object sender, EventArgs e)
        {

            /* Reset the color buttons, set awatingClick to true, 
               set the font of the button of the active color to orange, 
               store the activeColor */
            ResetColorButtons();

            ((Button)sender).ForeColor = Color.Orange;

            userColor = Color.FromName(((Button)sender).Text);

            /* If the user was erasing, say erasing is deactivated and their active color.
               Otherwise, notify the user what their active color is in a pop-up box. */
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

            drawing = true;

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

        /// <summary>
        /// This method calls the necessary methods in the logic class to save a design
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            drawing = false;
            erase = false;

            logic.PrepBoard(theGrid, panel);
            logic.SaveDesign(theGrid, panel);

        }

        /// <summary>
        /// This method calls the necessary methods from the logic class to import a design
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            drawing = false;
            erase = false;

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

            // Notify the user if the board is clear. Otherwise, do nothing.
            if (clear)
            {

                MessageBox.Show("Nothing to erase", "Grid Empty", MessageBoxButtons.OK);

            }
            else 
            {

                erase = true;
            
            }

        }

    }

}