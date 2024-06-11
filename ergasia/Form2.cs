using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ergasia
{
    public partial class Form2 : Form
    {
        //database object to send the data to the database
        DataBase database=new DataBase();
        //Player's names
        string player1;
        string player2;

        //tracks if a piece is supposed to move in the next click
        bool move = false;
        //List used in printing the chessPieces.
        //the chessPieces are objects from the call ChessPieceB
        List<ChessPieceB> chessPieces= new List<ChessPieceB>();
        //shows which piece is gonna move in the next click
        int movePiece;
        //tracks seconds left for players turn
        int turn = 30;
        //shows whose turn it is (it starts with player2 bc they're the white team)
        int playerTurn=2;
        //total time playing
        int playtime;

        //------------------------------------------------

        //the form2 initializes with the player names given by form1
        public Form2(string player1,string player2)
        {
            InitializeComponent();
            printChessPieces();
            this.player1 = player1;
            this.player2 = player2;
            label1.Text = player1+ " VS "+player2;
            label4.Text = player2 + "'s turn!";

            //label2 shows the current time and get updated every second by timer 2
            label2.Text = DateTime.Now.ToString();
            timer2.Start();

        }

        //this function prints all the chesspieces.
        private void printChessPieces()
        {
            //first "for" prints the black pieces at the top of the board
            for (int i = 0; i < 16; i++)
            {
                int y = (i / 8) * 50;
                int x;
                //the function findType finds based on the starting location of the piece
                //what kind of appearance it must have(what's its role)
                int type=findType(i);

                //the chessboard is 400x400 pixels so each tile is 50x50
                //(the first 8 tiles are treated differently for correct calculations)
                if (i < 8) { x = i * 50; }
                else { x = (i % 8) * 50; }
                //makes the chesspiece based on its location ,type and colour
                //(1 for black, 2 for white)
                ChessPieceB piece = new ChessPieceB(x, y, type,1);
                //the parent of the pieces must be the chessboard(pictureBox1)
                //for the transparency of the bg of the pieces to work
                //also all the locations are based of the chessboard this way.
                piece.ChangeParent(pictureBox1);
                //adds new piece on the list
                chessPieces.Add(piece);
            }
            //same for the white pieces, the only difference is the math to calculate
            //their locations since they're on the bottom side of the chessboard
            for (int i = 0; i < 16; i++)
            {
                int x = (i % 8) * 50;
                int y = ((i / 8) * 50) + 6 * 50;
                //we do -8 because in the board the pawns are on the top line for the white
                //and on the bottom for the black
                int type=findType(i-8);
                ChessPieceB piece = new ChessPieceB(x, y, type,2);
                piece.ChangeParent(pictureBox1);
                chessPieces.Add(piece);

            }
        }

        //end match button, after clicking it you choose which one of the players won
        //(by clicking button3 or 4)
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            label7.Visible = true;
            button3.Text = player1;
            button4.Text = player2;
            button3.Visible = true;
            button4.Visible = true;
           
        }

        //When form 2 closes we automatically go to form1 (the menu)
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   
        }

        //when one of the players moves their mouse outside of the pieces
        //the form checks if a piece is selected
        //and if yes, changes move to true so that the next click
        //moves the piece.
        //which piece is moved is based on the function isPieceSelected().
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPieceSelected() > 0)
            {
                move = true;
                movePiece = IsPieceSelected();
            }
        }


        //if one of the players clicks on the chessboard it checks whether move is true
        //if not nothing happens.
        //otherwise it moves the piece found in the "movePiece" place of the list
        //(which is calculated in mousemove function)
        //to the tile clicked and changes the turn.
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (move)
            {
                chessPieces[movePiece].ChangeLocation(newLocation(e.X,e.Y));
                resetPieceSelection();
                move = false;
                turn = 30;
                if (playerTurn == 1)
                {
                    label4.Text = player2+"'s turn!";
                    playerTurn = 2;
                    label4.BackColor = Color.White;
                    label4.ForeColor = Color.Black;
                }
                else
                {
                    label4.Text = player1+"'s turn!";
                    label4.BackColor = Color.Black;
                    label4.ForeColor = Color.White;
                    playerTurn = 1;
                }
            }
        }


        //this function is used so there can not be multiple chesspieces selected at the same time
        private void resetPieceSelection()
        {
            foreach (ChessPieceB c  in chessPieces)
            {
                c.selected = false;
            }
        }


        //this function finds to which tile the piece will be moved based 
        //on the click coordinates
        private Point newLocation(int x, int y)
        {
            int line = x / 50;
            int col = y / 50;
            return new Point(line*50, col*50);
        }


        //this function finds if a piece is selected
        //(clicked) and returns the place of the piece
        //in the list.
        //otherwise returns -1
        private int IsPieceSelected()
        {
            for (int i = 0;i < 32; i++)
            {
                if (chessPieces[i].selected==true)
                {
                    return i;
                }
            }
            return -1;
        }


        //this function finds the type of the piece based on its location at the
        //start of the match and it's used at creating the pieces.
        private int findType(int i)
        {
            int type;
            switch (i)
            {
                case 0:
                case 7:
                    type = 2;
                    break;
                case 1:
                case 6:
                    type = 3;
                    break;
                case 2:
                case 5:
                    type = 4;
                    break;
                case 3:
                    type = 5;
                    break;
                case 4:
                    type = 6;
                    break;
                default:
                    type = 1;
                    break;
            }
            return type;
        }


        //this button starts the turn timer for the players
        //which gives each player 30 sec to make their move
        //(also it hides instructions)
        private void start_timer_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label8.Visible=false;
            start_timer.Visible = false;
        }


        //every second the timer updates the label which show how
        //many seconds the player has left to make their move.
        private void timer1_Tick(object sender, EventArgs e)
        {
            turn--;
            if (turn == 0)
            {
                label6.Visible = true;
                timer1.Stop();
            }
            else
            {
                label5.Text = turn +"sec";
            }
        }


        //Return to menu button, returns to form1 without saving this match
        private void button2_Click(object sender, EventArgs e)

        {
            timer2.Stop();
            timer1.Stop();
            this.Close();
        }


        //Player1 won button,uses the database object to return the match data
        //to the database and returns to form1
        private void button3_Click(object sender, EventArgs e)
        {
            database.returnStats(player1, player2, label2.Text, playtime, button3.Text);
            timer2.Stop();
            timer1.Stop();
            this.Close();
        }


        //Player2 won button, same with the other but with different winner
        private void button4_Click(object sender, EventArgs e)
        {
            database.returnStats(player1, player2, label2.Text, playtime, button4.Text);
            timer2.Stop();
            timer1.Stop();
            this.Close();
        }


        //Updates the local time and date every second
        //and the playtime variable which stores total time played
        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            playtime++;
        }



    }
}
