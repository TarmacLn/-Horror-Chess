using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Reflection.Emit;

//University Project for lesson
//"Object-Oriented development"
//2023-2024
//project and assets from Ioanna Andrianou
// AKA Ιωάννα Ανδριανού
//(Tarmac on GitHub)

namespace ergasia
{
    internal class DataBase
    {
        String connectionString = "Data source=matches.db;Version=3";
        SQLiteConnection connection;
        public DataBase()
        {

        }


        //creates table in database "matches.db" for info needed in leaderboard/match history
        public void start()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createTableSQL = "Create table if not exists Match(" +
                "Match_ID integer primary key autoincrement," +
                "Player1 Text," +
                "Player2 Text," +
                "Date Text," +
                "PlayTime integer," +
                "Winner Text)";
            SQLiteCommand command = new SQLiteCommand(createTableSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //Shows info in leaderboard/match history screen's textbox
        public void previousMatches(System.Windows.Forms.TextBox textBox)
        {
            textBox.Clear();
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            string selectSQL = "Select * from Match";
            SQLiteCommand command = new SQLiteCommand(selectSQL, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox.AppendText(reader.GetString(1));
                textBox.AppendText(",");
                textBox.AppendText(reader.GetString(2));
                textBox.AppendText(",");
                textBox.AppendText(reader.GetString(3));
                textBox.AppendText(",");
                textBox.AppendText(reader.GetInt16(4).ToString());
                textBox.AppendText(",");
                textBox.AppendText(reader.GetString(5));
                textBox.AppendText("\n \n");
            }
            connection.Close();
        }

        //takes info from form2 after a match ends and stores it in the database
        public void returnStats(string p1,string p2,string date,int playtime,string won)
        {
            playtime = playtime / 60;
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            string insertSQL = "Insert into Match(Player1,Player2," +
                "Date,PlayTime,Winner) values(" +
                "@player1,@player2,@date,@playtime,@winner)";
            SQLiteCommand command = new SQLiteCommand(insertSQL, connection);
            command.Parameters.AddWithValue("player1", p1);
            command.Parameters.AddWithValue("player2", p2);
            command.Parameters.AddWithValue("date", date);
            command.Parameters.AddWithValue("playtime", playtime);
            command.Parameters.AddWithValue("winner", won);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
