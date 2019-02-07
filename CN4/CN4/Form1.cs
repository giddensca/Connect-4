using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN4
{
    public partial class Form1 : Form
    {
        bool FlipColor = true;
        string[,] theGrid = new string[7, 8];
        int row;
        int col;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Creates in InputBox from VB Library that prompts players to enter their names
            lblP1.Text = Interaction.InputBox("Player 1 Enter a Name", "Player 1", "", -1, -1);
            lblP2.Text = Interaction.InputBox("Player 2 Enter a Name", "Player 2", "", -1, -1);
            MessageBox.Show("This is a turn based game. Each player will take a turn to drop their coin. The first person to get four of their coins" +
                " in a row via any direction. THEY WIN!!!!!");


            //Populate Array
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    theGrid[row, col] = "*";
                }
            }
        }

        public void BoxHasBeenClicked(object sender, EventArgs e)
        {
            //Declarationsc
            PictureBox picBox = (PictureBox)sender;
            col = int.Parse(picBox.Name.Substring(4, 1));
            row = int.Parse(picBox.Name.Substring(3, 1));




            //If FlipColor TRUE, then picImage changes to Red #### If FlipColor FALSE, then picImage changes to Black
            if (FlipColor)
            {
                //Calls The Drop Method which helps solve which row the coin can fall to
                theGrid[DroppingCoin(col), col] = "R";
                //Set PictureBox
                picBox.Enabled = false;
                FlipColor = !FlipColor;
                DecreaseCoinCountPlayer1();
                CheckForWinner();
            }
            else
            {
                //set image to black coin
                //Calls The Drop Method which helps solve which row the coin can fall to
                theGrid[DroppingCoin(col), col] = "B";
                picBox.Enabled = false;
                FlipColor = !FlipColor;
                DecreaseCoinCountPlayer2();
                CheckForWinner();
            }
        }
        public int DroppingCoin(int whichCol)
        {
            PictureBox temp;
            int whichRow = 0;
            //Enters the loop going through the specified column, row by row
            for (int row = 0; row < 6; row++)
            {
                if (theGrid[row, whichCol] == "*")
                {
                    //Since condition is met, need to return the row number the condition was met on and end the loop.
                    whichRow = row;
                    //Code that allows the picture to appear where in should in grid #### "Falling coin"
                    temp = (PictureBox)this.Controls["pic" + whichRow + col];
                    if (FlipColor == true)
                    {
                        temp.Image = CN4.Properties.Resources.RedCoin;
                    }
                    else
                    {
                        temp.Image = CN4.Properties.Resources.BlackCoin;
                    }
                    
                    row = 0;
                    break;
                }
            }
            //Returns the correct row number to the array so it can be populated correctly
            return whichRow;
        }
        private void DecreaseCoinCountPlayer1()
        {
            var value = this.lblChips1.Text;
            var intValue = 14;
            Int32.TryParse(value, out intValue);
            this.lblChips1.Text = (--intValue).ToString();
            LimitToCoinsOne(intValue);
        }
        private void DecreaseCoinCountPlayer2()
        {
            var value = this.lblChips2.Text;
            var intValue = 14;
            Int32.TryParse(value, out intValue);
            this.lblChips2.Text = (--intValue).ToString();
            LimitToCoinsTwo(intValue);
        }
        private void CheckForWinner()
        {
            /* This section of code looks at the array, scanning through each cell of the array.
             * After itdoes this it usesthe info it gathers while scanning to see if four cells that are connected
             * share the same color. Either by looking row by row, column by column, and by scanning through every single
             * diagonal
             */
            //HorizontalCheck
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (theGrid[y, x] == "R" && theGrid[y, x + 1] == "R" && theGrid[y, x + 2] == "R" && theGrid[y, x + 3] == "R")
                    {
                        MessageBox.Show("PLAYER ONE WINS!!!!");
                        DisableBoard();
                    }
                    if (theGrid[y, x] == "B" && theGrid[y, x + 1] == "B" && theGrid[y, x + 2] == "B" && theGrid[y, x + 3] == "B")
                    {
                        MessageBox.Show("PLAYER TWO WINS!!!!");
                        DisableBoard();
                    }
                }
            }

            //VerticalCheck
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (theGrid[y, x] == "R" && theGrid[y + 1, x] == "R" && theGrid[y + 2, x] == "R" && theGrid[y + 3, x] == "R")
                    {
                        MessageBox.Show("PLAYER ONE WINS!!!!");
                        DisableBoard();
                    }
                    if (theGrid[y, x] == "B" && theGrid[y + 1, x] == "B" && theGrid[y + 2, x] == "B" && theGrid[y + 3, x] == "B")
                    {
                        MessageBox.Show("PLAYER TWO WINS!!!!");
                        DisableBoard();
                    }
                }
            }

            //Diagonal Left Check
            //Try Catch to stop it from breaking when reaching the ends of the array
            try
            {
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        if (theGrid[y, x] == "R" && theGrid[y + 1, x + 1] == "R" && theGrid[y + 2, x + 2] == "R" && theGrid[y + 3, x + 3] == "R")
                        {
                            MessageBox.Show("PLAYER ONE WINS!!!!");
                            DisableBoard();
                        }
                        if (theGrid[y, x] == "B" && theGrid[y + 1, x + 1] == "B" && theGrid[y + 2, x + 2] == "B" && theGrid[y + 3, x + 3] == "B")
                        {
                            MessageBox.Show("PLAYER TWO WINS!!!!");
                            DisableBoard();
                        }
                    }
                }

                //Diagonal Right Check
                //Try Catch to stop it from breaking when reaching the ends of the array
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 7; x > 0; x--)
                    {
                        if (theGrid[y, x] == "R" && theGrid[y + 1, x - 1] == "R" && theGrid[y + 2, x - 2] == "R" && theGrid[y + 3, x - 3] == "R")
                        {
                            MessageBox.Show("PLAYER ONE WINS!!!!");
                            DisableBoard();
                        }
                        if (theGrid[y, x] == "B" && theGrid[y + 1, x - 1] == "B" && theGrid[y + 2, x - 2] == "B" && theGrid[y + 3, x - 3] == "B")
                        {
                            MessageBox.Show("PLAYER TWO WINS!!!!");
                            DisableBoard();
                        }
                    }
                }
            }    
        
            catch (IndexOutOfRangeException) { }

        }
        private void DisableBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    // This method drops the cellName string variable and simply builds the name on the fly.

                    // The "this" keyword states you are working with the currently active form.
                    this.Controls["pic" + row + col].Enabled = false;
                }
            }
        }
        private int LimitToCoinsOne(int PlayerNumberOne)
        {
            //Looks at the value in the player one coin count
            if (PlayerNumberOne <= 0)
            {
                MessageBox.Show("Player one has no coins left!", MessageBoxIcon.Stop.ToString());
                FlipColor = !FlipColor
            }
            return PlayerNumberOne;
        }
        private int LimitToCoinsTwo(int PlayerNumberTwo)
        {
            //Looks at the value in the player one coin count
            if (PlayerNumberTwo <= 0)
            {
                MessageBox.Show("Neither Player has any coins left. Start a new game!", MessageBoxIcon.Stop.ToString());
                FlipColor = !FlipColor;
                DisableBoard();
            }
            return PlayerNumberTwo;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            PictureBox temp;

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    // This method drops the cellName string variable and simply builds the name on the fly.

                    // The "this" keyword states you are working with the currently active form.
                    //   Once found, recast the object in the collection into a PictureBox object.
                    temp = (PictureBox)this.Controls["pic" + row + col];

                    // Clear the Image property of the object.
                    temp.Image = CN4.Properties.Resources.Empty;
                }
            }
            //Re-Populate Array
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    theGrid[row, col] = "*";
                }
            }
            //Set Labels to 14 again
            this.lblChips1.Text = "14";
            this.lblChips2.Text = "14";
            //Enable Board
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    // This method drops the cellName string variable and simply builds the name on the fly.

                    // The "this" keyword states you are working with the currently active form.
                    this.Controls["pic" + row + col].Enabled = true;
                }
            }
        }
    }
}
