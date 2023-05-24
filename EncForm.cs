using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoppaCodes
{
    public partial class EncForm : Form
    {

        private PrimitivePolynomial[] polynomials;
        private int polynomial = 285;
        private int[] coef;
        private int[] prims;
        private string EncMsg;
        public EncForm()
        {
            InitializeComponent();
            UpdateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = " Текстовые документы (*.txt)| *.txt| Изображения (*.png) | *.png| Аудио (*.mp3) | *.mp3";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            textBox1.Text = Encoding.UTF8.GetString(File.ReadAllBytes(filename));
            MessageBox.Show("Файл открыт");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "") throw new Exception("Отсутствует текст для кодирования");
                GF field = new GF(polynomial, 256, 1);
                (EncMsg, coef, prims) = LinearGoppaCode.Encode(textBox1.Text, field);
                var data = GlobalFunc.GetBytesFromBinaryString(EncMsg);
                textBox2.Text = Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                using (GoppaContext db = new GoppaContext())
                {
                    UserException ue = new UserException { Message = ex.Message, DateTimeexc = DateTime.UtcNow, TargetSite = ex.TargetSite.ToString() };
                    await db.UserExceptions.AddAsync(ue);
                    await db.SaveChangesAsync();
                }
                MessageBox.Show(ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                    throw new Exception("Нечего сохранять");


                saveFileDialog1.Filter = " Текстовые документы (*.txt)| *.txt| Изображения (*.png) | *.png| Аудио (*.mp3) | *.mp3";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                string id;
                using (GoppaContext db = new GoppaContext())
                {
                    EncodedMessage em = new EncodedMessage { Date = DateTime.UtcNow, Path = saveFileDialog1.FileName, Coefficients = coef, Polynomial = polynomial, Primitives = prims };
                    await db.EncodedMessages.AddAsync(em);
                    await db.SaveChangesAsync();
                    var enc = await db.EncodedMessages.ToListAsync();
                    id = (enc.LastIndexOf(em) + 1).ToString();
                }


                await File.WriteAllBytesAsync(saveFileDialog1.FileName, (Encoding.UTF8.GetBytes(EncMsg + id.PadLeft(8, '0'))));
                MessageBox.Show("Файл сохранён");
            }
            catch (Exception ex)
            {
                using (GoppaContext db = new GoppaContext())
                {
                    UserException ue = new UserException { Message = ex.Message, DateTimeexc = DateTime.UtcNow, TargetSite = ex.TargetSite.ToString() };
                    await db.UserExceptions.AddAsync(ue);
                    await db.SaveChangesAsync();
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void EncodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.ReadOnly = false;
            }
            else textBox1.ReadOnly = true;
        }

        private void EncForm_Load(object sender, EventArgs e)
        {
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel3.Text = DateTime.Now.ToLongTimeString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            polynomial = polynomials[comboBox1.SelectedIndex].Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(this);
            f1.ShowDialog();
        }

        public void UpdateComboBox()
        {
            using (GoppaContext db = new GoppaContext())
            {
                polynomials = db.primitivePolynomials.ToArray();
            }
            foreach (var item in polynomials)
            {
                if (item.Value == 285) continue;
                int[] bits = GlobalFunc.ConvertToBits(item.Value, (int)Math.Log2(item.Value) + 1);
                string x = "";
                for (int i = bits.Length - 1; i > 0; i--)
                {
                    if (bits[i] != 0)
                    {
                        x += $"x^({i})+";
                    }
                }
                x += "1";
                comboBox1.Items.Add(x);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var d = EncMsg;
            var x = LinearGoppaCode.GenerateErrors(d, (int)numericUpDown1.Value);
            var data = GlobalFunc.GetBytesFromBinaryString(x);
            textBox2.Text = Encoding.UTF8.GetString(data);
        }
    }
}
