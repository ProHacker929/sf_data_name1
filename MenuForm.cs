using System;
using System.Windows.Forms;

namespace Football5._0
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new GameForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Верхние челы + очки к харизме.\n" +
                "Нижние челы + очки к кринжу.\n" +
                "У тебя 69 сек.\n");
        }
    }
}
