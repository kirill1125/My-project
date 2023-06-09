using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Determinant
{
    public partial class InversMatrix : Form
    {
        double[,] a;
        public InversMatrix()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        int N;

       
        public InversMatrix(object[,] data)
        {
            InitializeComponent();

            numericUpDown1.Value = data.GetLength(0);
           
            for (int i = 0; i < data.GetLength(0); i++)
            {
              
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = data[i, j];
                }
            }                  
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            numericUpDown1_ValueChanged(sender, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = dataGridView1.RowCount = dataGridView1.ColumnCount = (int)numericUpDown1.Value;
           

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           

            if (radioButton2.Checked)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        dataGridView1[j, i].Value = null;
                    }
                }

                dataGridView1.ReadOnly = false;
            }



            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Matrix mx = new Matrix(N);

            try
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (dataGridView1[j, i].Value == null) dataGridView1[j, i].Value = 0;
                        mx[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                    }
                }

                Matrix inv = mx.Invert(out double det);

                if (inv != null)
                {
                    dataGridView2.RowCount = dataGridView2.ColumnCount = N;

                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            dataGridView2[j, i].Value = inv[i, j];
                        }
                    }
                }
                else MessageBox.Show("Для выроженной матрицы обратная не существует.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string t = ((TextBox)sender).Text;
            char k = e.KeyChar;

            e.Handled = !(char.IsDigit(k) || k == ',' && !t.Contains(",") || k == '-' && !t.Contains("-") || k == (char)Keys.Back);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Создаем диалоговое окно для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Устанавливаем начальную директорию и фильтр файлов
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Открываем диалоговое окно и проверяем, что пользователь нажал "ОК"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Открываем файл для записи
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Перебираем строки таблицы
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Перебираем ячейки в строке
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            // Записываем значение в файл без пробела в конце строки
                            writer.Write(cell.Value);

                            // Если это не последняя ячейка в строке, добавляем разделитель
                            if (cell.ColumnIndex < row.Cells.Count - 1)
                            {
                                writer.Write(" ");
                            }
                        }

                        // Переходим на следующую строку
                        writer.WriteLine();
                    }
                }
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu MainMenu = new MainMenu();
            MainMenu.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Создаем диалоговое окно для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Устанавливаем начальную директорию и фильтр файлов
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Открываем диалоговое окно и проверяем, что пользователь нажал "ОК"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Открываем файл для записи
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Перебираем строки таблицы
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Перебираем ячейки в строке
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            // Записываем значение в файл без пробела в конце строки
                            writer.Write(cell.Value);

                            // Если это не последняя ячейка в строке, добавляем разделитель
                            if (cell.ColumnIndex < row.Cells.Count - 1)
                            {
                                writer.Write(" ");
                            }
                        }

                        // Переходим на следующую строку
                        writer.WriteLine();
                    }
                }
            }
            Application.Exit();
        }
    }
}

