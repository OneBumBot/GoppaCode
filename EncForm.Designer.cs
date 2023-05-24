namespace GoppaCodes
{
    partial class EncForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            EncodeToolStripMenuItem = new ToolStripMenuItem();
            CloseToolStripMenuItem = new ToolStripMenuItem();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            checkBox1 = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            comboBox1 = new ComboBox();
            button4 = new Button();
            button5 = new Button();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { OpenToolStripMenuItem, SaveToolStripMenuItem, EncodeToolStripMenuItem, CloseToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(1042, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OpenToolStripMenuItem.Size = new Size(81, 24);
            OpenToolStripMenuItem.Text = "Открыть";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveToolStripMenuItem.Size = new Size(97, 24);
            SaveToolStripMenuItem.Text = "Сохранить";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // EncodeToolStripMenuItem
            // 
            EncodeToolStripMenuItem.Name = "EncodeToolStripMenuItem";
            EncodeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            EncodeToolStripMenuItem.Size = new Size(120, 24);
            EncodeToolStripMenuItem.Text = "Закодировать";
            EncodeToolStripMenuItem.Click += EncodeToolStripMenuItem_Click;
            // 
            // CloseToolStripMenuItem
            // 
            CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            CloseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F4;
            CloseToolStripMenuItem.Size = new Size(80, 24);
            CloseToolStripMenuItem.Text = "Закрыть";
            CloseToolStripMenuItem.Click += CloseToolStripMenuItem_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 71);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(383, 315);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(405, 71);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(625, 315);
            textBox2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(107, 40);
            label1.Name = "label1";
            label1.Size = new Size(190, 28);
            label1.TabIndex = 3;
            label1.Text = "Изначальный файл";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(493, 40);
            label2.Name = "label2";
            label2.Size = new Size(225, 28);
            label2.TabIndex = 4;
            label2.Text = "Закодированный файл";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(10, 395);
            button1.Name = "button1";
            button1.Size = new Size(130, 29);
            button1.TabIndex = 5;
            button1.Text = "Выбрать файл";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(0, 461);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1042, 26);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(166, 20);
            toolStripStatusLabel1.Text = "Текущие дата и время:";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(0, 20);
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(146, 397);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(204, 24);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Вписать самостоятельно";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(405, 389);
            button2.Name = "button2";
            button2.Size = new Size(130, 29);
            button2.TabIndex = 8;
            button2.Text = "Закодировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Location = new Point(541, 389);
            button3.Name = "button3";
            button3.Size = new Size(130, 29);
            button3.TabIndex = 9;
            button3.Text = "Сохранить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 427);
            label3.Name = "label3";
            label3.Size = new Size(195, 20);
            label3.TabIndex = 10;
            label3.Text = "Неприводимый полином :";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "x^(8)+x^(4)+x^(3)+x^(2)+1" });
            comboBox1.Location = new Point(213, 424);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(322, 28);
            comboBox1.TabIndex = 11;
            comboBox1.Text = "x^(8)+x^(4)+x^(3)+x^(2)+1";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button4
            // 
            button4.Location = new Point(558, 429);
            button4.Name = "button4";
            button4.Size = new Size(291, 29);
            button4.TabIndex = 12;
            button4.Text = "Добавить полином";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(677, 389);
            button5.Name = "button5";
            button5.Size = new Size(187, 29);
            button5.TabIndex = 13;
            button5.Text = "Сгенерировать ошибки";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(870, 393);
            label4.Name = "label4";
            label4.Size = new Size(149, 20);
            label4.TabIndex = 14;
            label4.Text = "Количество ошибок";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(870, 424);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 15;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // EncForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1042, 487);
            Controls.Add(numericUpDown1);
            Controls.Add(label4);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(statusStrip1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "EncForm";
            Text = "Кодер";
            Load += EncForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem EncodeToolStripMenuItem;
        private ToolStripMenuItem CloseToolStripMenuItem;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Button button1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private CheckBox checkBox1;
        private Button button2;
        private Button button3;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private Label label3;
        private ComboBox comboBox1;
        private Button button4;
        private Button button5;
        private Label label4;
        private NumericUpDown numericUpDown1;
    }
}