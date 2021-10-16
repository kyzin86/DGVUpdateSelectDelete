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

namespace DGV
{
    public partial class UsersForm : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter adapter = null;
        private DataTable table;
        public UsersForm()
        {
            InitializeComponent();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "facultetDataSet1.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.facultetDataSet1.users);
            
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-0RM14IT\SQLEXPRESS;Initial Catalog=facultet;Integrated Security=True");
            sqlConnection.Open();
            adapter = new SqlDataAdapter("SELECT id, FirstName, LastName, login, password FROM users", sqlConnection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowHeadersVisible = false;   //убрать первую колонку
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) 
                || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Заполните пустые поля", "Ошибка!");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                 $"INSERT INTO [users] (FirstName, LastName, login, password) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{textBox4.Text}')",
                  sqlConnection);
                MessageBox.Show("Добавлено записей: " + command.ExecuteNonQuery().ToString());
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                table.Clear();         // обновление таблицы после добавления записи
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                //command.ExecuteNonQuery().ToString();
            }
        }

        private void сменитьПользователяToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
            this.Close();
        }
    }
}
