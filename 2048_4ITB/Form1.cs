using System.Diagnostics;

namespace _2048_4ITB
{
    public partial class Form1 : Form
    {
        int[,] testSetup = new int[4, 4] {
            { 0,4,4,4},
            { 16,16,0,0},
            { 2,0,2,0},
            { 2,2,4,0},
        };

        Number[,] numbers = new Number[4, 4];
        Random generator = new Random();

        public Form1() {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e) {
            CreateNumbers();
            AdjustSizes();
            GenerateRandomStart();

            TestSetup();
        }

        private void TestSetup() {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    numbers[i, j].CurrentValue = testSetup[i, j];
                }
            }
        }

        private void GenerateRandomStart() {

            int x = generator.Next(4);
            int y = generator.Next(4);
            numbers[x, y].CurrentValue = 2;

            while (true) {
                int sx = generator.Next(4);
                int sy = generator.Next(4);

                if (sx != x && sy != y) {
                    numbers[sx, sy].CurrentValue = GetRandomValue();
                    break;
                }
            }
        }

        private int GetRandomValue() {
            return generator.Next(0, 10) < 3 ? 4 : 2;
        }

        private void CreateNumbers() {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    Number n = new Number(j, i);
                    numbers[i, j] = n;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e) {

            switch (e.KeyCode) {
                case Keys.Left:
                case Keys.A:
                MoveNumbers(-1, 0); break;
                case Keys.Right:
                case Keys.D:
                MoveNumbers(1, 0); break;
                case Keys.Up:
                case Keys.W:
                MoveNumbers(0, -1); break;
                case Keys.Down:
                case Keys.S:
                MoveNumbers(0, 1); break;
            }
        }

        private void MoveNumbers(int x, int y) {
            List<int> nums;

            if (x < 0 && y == 0) {
                for (int i = 0; i < 4; i++) { // vezmi každý øádek
                    nums = new List<int>();
                    GetRow(nums, i);
                    // algoritmus pro merge sloupeèkù
                    MergeWhatYouCan(nums);
                    // TODO: vložit nums do numbers
                }
            }
        }

        private void GetRow(List<int> row, int rowIndex) {
            for (int i = 0; i < 4; i++) {
                if (numbers[rowIndex, i].CurrentValue > 0)
                    row.Add(numbers[rowIndex, i].CurrentValue);
            }
        }

        private void MergeWhatYouCan(List<int> nums) {
            for (int j = 0; j < nums.Count - 1; j++) {
                if (nums[j] == nums[j+1]) {
                    nums[j] *= 2;
                    nums.RemoveAt(j + 1);
                }
            }
        }
    }
}