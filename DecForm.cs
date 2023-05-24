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
    public partial class DecForm : Form
    {
        private string encMsg;
        private int id;
        private int[] coef;
        private int[] prims;
        private int poly;
        public DecForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = " Текстовые документы (*.txt)| *.txt| Изображения (*.png) | *.png| Аудио (*.mp3) | *.mp3";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            encMsg = Encoding.UTF8.GetString(File.ReadAllBytes(filename));
            textBox1.Text = Encoding.UTF8.GetString(GlobalFunc.GetBytesFromBinaryString(encMsg[..^8]));
            string id_tmp = encMsg[^8..];
            for (int i = 0; i < id_tmp.Length; i++)
            {
                if (id_tmp[i] != 0)
                {
                    id_tmp = id_tmp[i..];
                    id = Int32.Parse(id_tmp);
                }
            }
            MessageBox.Show("Файл открыт");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "") throw new Exception("Текст для декодирования отсутствует");
                using (GoppaContext db = new GoppaContext())
                {
                    var enc = db.EncodedMessages.ToList();
                    var find = enc.Find(x => x.Id.Equals(id));
                    coef = find.Coefficients;
                    prims = find.Primitives;
                    poly = find.Polynomial;
                }

                GF field = new GF(poly, 256, 1);
                var dec = LinearGoppaCode.Decode(encMsg[..^8], field, coef, prims);
                textBox2.Text = Encoding.UTF8.GetString(GlobalFunc.GetBytesFromBinaryString(dec));
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
                using (GoppaContext db = new GoppaContext())
                {
                    DecodedMessage dm = new DecodedMessage { Date = DateTime.UtcNow, Path = saveFileDialog1.FileName, Polynomial = poly };
                    await db.DecodedMessages.AddAsync(dm);
                    await db.SaveChangesAsync();
                }


                await File.WriteAllBytesAsync(saveFileDialog1.FileName, (Encoding.UTF8.GetBytes(textBox2.Text)));
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

        private void DecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
