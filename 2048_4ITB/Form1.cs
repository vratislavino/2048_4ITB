namespace _2048_4ITB
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            CreateNumbers();
            AdjustSizes();
            GenerateRandomStart();
        }

        private void GenerateRandomStart() {
            // TODO: 30% šance na jednu 4, jinak dvì 2
        }

        private void CreateNumbers() {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    Number n = new Number(j, i);
                    panel1.Controls.Add(n);
                }
            }
        }

        private void AdjustSizes() {
            panel1.Size = new Size(4 * Number.size, 4 * Number.size);
            this.Size = new Size(
                panel1.Width + 58 + panel1.Location.X,
                panel1.Height + 100 + panel1.Location.Y);
        }
    }
}