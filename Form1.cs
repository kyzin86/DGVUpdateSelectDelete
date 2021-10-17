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
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;   //Подключаем класс SqlConnection
        private SqlDataAdapter adapter = null;       //Подключаем класс SqlDataAdapter
        private DataTable table;                     //Подключаем класс DataTable
        public Form1()
        {
            InitializeComponent();
            //RegistrationForm f3 = new RegistrationForm();  // вызов в конструкторе для закрытия формы входа
            //f3.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "facultetDataSet.Students". При необходимости она может быть перемещена или удалена.
            this.studentsTableAdapter.Fill(this.facultetDataSet.Students);

            //Подключение к БД
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-0RM14IT\SQLEXPRESS;Initial Catalog=facultet;Integrated Security=True");
            sqlConnection.Open();

            //Вывод ДатаГридВью
            adapter = new SqlDataAdapter("SELECT Id, FirstName, LastName, Adress FROM Students", sqlConnection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowHeadersVisible = false;   //убрать первую колонку
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            MessageBox.Show("Записи обновлены!", "Уведомление");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните пустые поля", "Ошибка!");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                 $"INSERT INTO [Students] (FirstName, LastName, Adress) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}')",
                  sqlConnection);
                MessageBox.Show("Добавлено записей: " + command.ExecuteNonQuery().ToString());
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                table.Clear();         // обновление таблицы после добавления записи
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                //command.ExecuteNonQuery().ToString();
            }

        }
        // Сбрасываем фильтр Поиска
        private void button3_Click(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
            textBox4.Clear();
        }
        //**************************************************************************


        //фильтрация по столбцу Имя сразу при наборе текста в поле с помощью св-ва TextChanged у textBox4
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"FirstName LIKE '%{textBox4.Text}%'";

        }
        //**************************************************************************


        //фильтрация по столбцу Имя в ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"FirstName LIKE '%{comboBox1.Text}%'";
        }

        //удаление записи
        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Введите значение в поле ID.", "Ошибка");
            }
            else
            {
                SqlCommand mycommand = new SqlCommand(
              $"DELETE FROM Students WHERE ID = N'{textBox5.Text}'",
                sqlConnection);
                MessageBox.Show("Удалено записей: " + mycommand.ExecuteNonQuery().ToString());
                textBox5.Clear();
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            //try
            //{

            //    int Nomer = int.Parse(this.textBox5.Text);
            //    string connectionString = @"Data Source=DESKTOP-0RM14IT\SQLEXPRESS;Initial Catalog=facultet;Integrated Security=True";
            //    SqlConnection conn = new SqlConnection(connectionString);
            //    conn.Open();
            //    SqlCommand myCommand = conn.CreateCommand();
            //    myCommand.CommandText = "DELETE FROM Students WHERE id = @id";
            //    myCommand.Parameters.Add("@id", SqlDbType.Int);
            //    myCommand.Parameters["@id"].Value = Nomer;
            //    int UspeshnoeIzmenenie = myCommand.ExecuteNonQuery();
            //    if (UspeshnoeIzmenenie != 0)
            //    {
            //        MessageBox.Show("Запись удалена", "Изменение записи");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Не удалось внести изменения", "Изменение записи");
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Введите значение в поле id.", "Изменение записи");
            //}
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //sqlConnection.Close();
            this.Close();
        }

        private void добавитьРаботникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните пустые поля", "Ошибка!");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                 $"INSERT INTO [Students] (FirstName, LastName, Adress) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}')",
                  sqlConnection);
                MessageBox.Show("Добавлено записей: " + command.ExecuteNonQuery().ToString());
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                //command.ExecuteNonQuery().ToString();
            }

        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void перейтиНаГлавноеОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
            this.Close();
        }
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            // создаём запрос
            var sql = @"UPDATE Students
                 SET {dataGridView1.Columns[e.ColumnIndex].HeaderText} = @param
                 WHERE {dataGridView.Columns[0].HeaderText} = @id";

            // добавляем параметры
            sqlCommand.Parameters.AddWithValue("param", dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            sqlCommand.Parameters.AddWithValue("id", dataGridView1[0, e.RowIndex].Value);
            //**************************************************************************
        }

    
    }

}
