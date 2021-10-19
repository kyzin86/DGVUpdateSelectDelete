using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;


namespace DGV
{
    public partial class RegistrationForm : Form
    {
        private SqlConnection sqlConnection = null;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "1234")
            {
                this.Hide();
                UsersForm usersform = new UsersForm();
                usersform.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Логин/Пароль введены неверно", "Ошибка!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String loginUser = textBox1.Text;
            String passUser = textBox2.Text;

            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-UIE3VBD;Initial Catalog=facultet;Integrated Security=True");
            sqlConnection.Open();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = @login AND password = @password", sqlConnection);
            command.Parameters.Add("@login", SqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Логин/Пароль введены неверно", "Ошибка");
            }

        }
    }
    
}
