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

    public partial class Determinant : Form
    {
        double[,] a;
        public Determinant()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        int N;

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1_ValueChanged(sender, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = initial_DGV.RowCount = initial_DGV.ColumnCount = (int)numericUpDown1.Value;
         

            label2.Text = string.Empty;
        }

        private void random_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            
            if (manual_radioButton.Checked)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        initial_DGV[j, i].Value = null;
                    }
                }

                initial_DGV.ReadOnly = false;
            }

            label2.Text = string.Empty;
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
                        if (initial_DGV[j, i].Value == null) initial_DGV[j, i].Value = 0;
                        mx[i, j] = Convert.ToDouble(initial_DGV[j, i].Value);
                    }
                }

                label2.Text = $"Определитель = {mx.CalculateDeterminant()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void initial_DGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(initial_DGV_KeyPress);
        }

        private void initial_DGV_KeyPress(object sender, KeyPressEventArgs e)
        {
            string t = ((TextBox)sender).Text;
            char k = e.KeyChar;

            e.Handled = !(char.IsDigit(k) || k == ',' && !t.Contains(",") || k == '-' && !t.Contains("-") || k == (char)Keys.Back);
        }

        private void initial_DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = string.Empty;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // сохраняем значения ячеек DataGridView в массив
            var data = new object[initial_DGV.Rows.Count, initial_DGV.Columns.Count];
            for (int i = 0; i < initial_DGV.Rows.Count; i++)
            {
                for (int j = 0; j < initial_DGV.Columns.Count; j++)
                {
                    data[i, j] = initial_DGV.Rows[i].Cells[j].Value;
                }
            }

            // открываем вторую форму и передаем значения через конструктор
            var form2 = new InversMatrix(data);
            form2.Show();
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void manual_radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        

        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            initial_DGV.Rows.Clear();
            initial_DGV.Columns.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu MainMenu = new MainMenu();
            MainMenu.ShowDialog();
        }

       

        private void button7_Click(object sender, EventArgs e)
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
                    foreach (DataGridViewRow row in initial_DGV.Rows)
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