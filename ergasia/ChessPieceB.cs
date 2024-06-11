using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//University Project for lesson
//"Object-Oriented development"
//2023-2024
//project and assets from Ioanna Andrianou
// AKA Ιωάννα Ανδριανού
//(Tarmac on GitHub)

namespace ergasia
{
    internal class ChessPieceB
    {
        //Chess Piece types: 
        //1) Pawn (υπηρετης)
        //2) Rook (πυργος)
        //3) Knight (αλογο)
        //4) Bishop (αξιωματικος)
        //5) Queen
        //6) King

        //Chess Piece Colours:
        //1)Black
        //2)White

        public PictureBox chessP = new PictureBox();
        //a variable that shows if the piece is clicked on or not
        public bool selected = false;

        //initializes the piece with a location type and colour
        public ChessPieceB(int startX, int startY, int type,int colour)
        {
            chessP.Location = new Point(startX, startY);
            chessP.Size = new Size(50, 50);
            chessP.BackColor = Color.Transparent;
            chessP.Visible = true;
            chessP.BackgroundImageLayout = ImageLayout.Stretch;
            //function that finds the correct background for the piece
            findImage(type, colour);
            //custom event handlers for the piece's picturebox (chessP)
            chessP.Click += new System.EventHandler(chessP_Click);
            chessP.DoubleClick += new System.EventHandler(chessP_DoubleClick);
            chessP.Refresh();
        }

        //if a piece is clicked on it's chosen to move
        private void chessP_Click(object sender, EventArgs e)
        {
            selected = true;
        }

        //if it's doublclicked it get "deleted"
        //(moved out of chessboards bounds)
        private void chessP_DoubleClick(object sender, EventArgs e)
        {
            chessP.Location=new Point(-50,-50);
            selected = false;
        }

        //used to change the picturebox's parent to the chessboard
        public void ChangeParent(PictureBox parent)
        {
            chessP.Parent = parent;
        }

        //returns piece's location
        public Point GetLocation()
        {
            return chessP.Location;
        }

        //changes piece's location
        public void ChangeLocation(Point newLocation)
        {
            chessP.Location = newLocation;
        }

        //finds the correct background for the piece's picturebox 
        //based on its type and colour
        private void findImage(int type, int colour)
        {
            if (colour == 1) 
            { 
                if (type == 1) 
                {
                    chessP.BackgroundImage = Properties.Resources.B_Kn;
                }
                else if(type == 2)
                {
                    chessP.BackgroundImage = Properties.Resources.B_T;
                }
                else if (type == 3)
                {
                    chessP.BackgroundImage = Properties.Resources.B_H;
                }
                else if (type == 4)
                {
                    chessP.BackgroundImage = Properties.Resources.B_S;
                }
                else if (type == 5)
                {
                    chessP.BackgroundImage = Properties.Resources.B_Q;
                }
                else
                {
                    chessP.BackgroundImage = Properties.Resources.B_K;
                }
            } 
            else
            {
                if (type == 1)
                {
                    chessP.BackgroundImage = Properties.Resources.W_Kn;
                }
                else if (type == 2)
                {
                    chessP.BackgroundImage = Properties.Resources.W_T;
                }
                else if (type == 3)
                {
                    chessP.BackgroundImage = Properties.Resources.W_H;
                }
                else if (type == 4)
                {
                    chessP.BackgroundImage = Properties.Resources.W_S;
                }
                else if (type == 5)
                {
                    chessP.BackgroundImage = Properties.Resources.W_Q;
                }
                else
                {
                    chessP.BackgroundImage = Properties.Resources.W_K;
                }
            }
        }
    }
}
