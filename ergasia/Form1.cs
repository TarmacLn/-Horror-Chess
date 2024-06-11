using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

//University Project for lesson
//"Object-Oriented development"
//2023-2024
//project and assets from Ioanna Andrianou
// AKA Ιωάννα Ανδριανού
//(Tarmac on GitHub)

namespace ergasia
{
    public partial class Form1 : Form
    {
        //the names the players choose for themselves
        private string player1_name;
        private string player2_name;
        //The class database help us use and give commands to the database "matches.db"
        private DataBase database=new DataBase();
        public Form1()
        {

            InitializeComponent();
        }

        //the database object creates the table Match in "matches.db"
        //(if it doesn't exist)
        //to be able to store future data.
        private void Form1_Load(object sender, EventArgs e)
        {
            database.start();
        }

        //start button,goes to player screen where the players choose their names 
        private void button1_Click(object sender, EventArgs e)
        {
            playerNames();
        }

        //leaderboard button, goes to match history screen
        private void button2_Click(object sender, EventArgs e)
        {
            leaderboard();
        }

        //exit button, exits from the application
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //begin button, appears in player screen,clicking it takes us to form2 where the game begins 
        //for the two players.
        private void button4_Click(object sender, EventArgs e)
        {
            player1_name = (string)textBox1.Text;
            player2_name = (string)textBox2.Text;
            hidePlayerNames();
            showMenu();
            Form2 form2 = new Form2(player1_name, player2_name);
            form2.Show();
            this.Hide();
        }

        //Back button that appears on leaderboard/match history screen
        //returns us to main menu
        private void button5_Click(object sender, EventArgs e)
        {
            hideLeaderboard();
            showMenu();
        }


        //------------------------------------------
        //From here on the functions are used to hiding and appearing things 
        //for the menu and for every screen

        //1)Player screen
        private void playerNames()
        {
            hideMenu();
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button4.Visible = true;
        }

        private void hidePlayerNames()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button4.Visible = false;
        }

        //2)Leaderboard/Match history screen
        private void leaderboard()
        {
            hideMenu();
            matches.Visible = true;
            button5.Visible = true;
            label5.Visible = true;

            database.previousMatches(matches);
        }

        private void hideLeaderboard()
        {
            matches.Visible = false;
            button5.Visible = false;
            label5.Visible = false;
        }

        //3)Main Menu screen
        private void hideMenu()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void showMenu()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }
    }
}
