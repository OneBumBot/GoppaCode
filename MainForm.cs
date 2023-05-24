namespace GoppaCodes
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public EncForm EncForm
        {
            get => default;
            set
            {
            }
        }

        public DecForm DecForm
        {
            get => default;
            set
            {
            }
        }

        private void EncodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void DecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            EncForm ef = new EncForm();
            ef.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DecForm df = new DecForm();
            df.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel3.Text = DateTime.Now.ToLongTimeString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}