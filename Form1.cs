using System.Text.RegularExpressions;
namespace GoppaCodes
{
    public partial class Form1 : Form
    {
        public EncForm f;
        public Form1(EncForm form)
        {
            InitializeComponent();
            f = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                using (GoppaContext db = new GoppaContext())
                {
                    PrimitivePolynomial pp = new PrimitivePolynomial() { Value = (int)numericUpDown1.Value };
                    db.primitivePolynomials.Add(pp);
                    db.SaveChanges();
                }
            }
            else
            {
                Regex regex = new Regex(@"\((\d*)\)");
                MatchCollection matches = regex.Matches(textBox1.Text);
                int val = 0;
                if(matches.Count > 0)
                {
                    foreach(Match match in matches)
                    {
                        int m = Int32.Parse(match.Value[1..^1]);
                        val += (int)Math.Pow(2, m);
                    }
                }
                using(GoppaContext db = new GoppaContext())
                {
                    var polys = db.primitivePolynomials.ToList();
                    if (polys.Exists(x => x.Value == val)) 
                    {
                        MessageBox.Show("Такой полином уже есть в базе");
                        return;
                    }
                    PrimitivePolynomial pp = new PrimitivePolynomial() { Value = val };
                    db.primitivePolynomials.Add(pp);
                    db.SaveChanges();
                }
            }
            f.UpdateComboBox();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.Visible = false;
                numericUpDown1.Visible = true;
            }
            textBox1.Visible = true;
            numericUpDown1.Visible = false;
        }
    }
}
