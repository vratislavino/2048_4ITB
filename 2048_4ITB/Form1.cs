using System.Diagnostics;

namespace _2048_4ITB
{
    public partial class Form1 : Form
    {
        int[,] testSetup = new int[4, 4] {
            { 1024,1024,1024,1024},
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

            if (y == 0) { // horizont�ln� sm�r

                for (int i = 0; i < 4; i++) { // vezmi ka�d� ��dek
                    nums = new List<int>();
                    GetRow(nums, i); // z�sk�n� ��dku
                    if (x > 0) nums.Reverse();
                    MergeWhatYouCan(nums); // merge stejn�ch ��sel 
                    FillEmptyNumbers(nums);
                    if (x > 0) nums.Reverse();
                    ReturnValuesToRow(nums, i); // vr�cen� ��sel z listu do 2D pole
                }
            } else { // vertik�ln� sm�r
                for (int i = 0; i < 4; i++) {
                    nums = new List<int>();
                    GetColumn(nums, i);
                    if (y > 0) nums.Reverse();
                    MergeWhatYouCan(nums);
                    FillEmptyNumbers(nums);
                    if (y > 0) nums.Reverse();
                    ReturnValuesToColumn(nums, i); // change
                }
            }

            if (CheckWin()) {
                MessageBox.Show("P�kn� pr�ce, zkus to znovu!");
                Application.Restart();
            }

            AddNewNumber();
            if (IsFull()) {
                CheckMergePossibility();
            }
        }

        private bool CheckWin() {
            foreach (var num in numbers) {
                if (num.CurrentValue == 2048)
                    return true;
            }
            return false;
        }

        private void CheckMergePossibility() {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (numbers[i, j].CurrentValue == numbers[i + 1, j].CurrentValue)
                        return;
                    if (numbers[i, j].CurrentValue == numbers[i, j + 1].CurrentValue)
                        return;
                }
            }
            MessageBox.Show("This is the end for you my friend");
            Application.Restart();
        }

        private bool IsFull() {
            foreach (var num in numbers) {
                if (num.CurrentValue == 0) return false;
            }
            return true;
        }

        private void AddNewNumber() {
            if (IsFull())
                return;

            int x = generator.Next(4);
            int y = generator.Next(4);
            while (numbers[x, y].CurrentValue != 0) {
                x = generator.Next(4);
                y = generator.Next(4);
            }
            numbers[x, y].CurrentValue = GetRandomValue();
            return;
        }

        private void GetColumn(List<int> column, int columnIndex) {
            for (int i = 0; i < 4; i++) {
                if (numbers[i, columnIndex].CurrentValue > 0)
                    column.Add(numbers[i, columnIndex].CurrentValue);
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
                if (nums[j] == nums[j + 1]) {
                    nums[j] *= 2;
                    nums.RemoveAt(j + 1);
                }
            }
        }

        private void FillEmptyNumbers(List<int> nums) {
            nums.AddRange(new int[4 - nums.Count]);
        }

        private void ReturnValuesToRow(List<int> nums, int rowIndex) {
            for (int i = 0; i < 4; i++) {
                numbers[rowIndex, i].CurrentValue = nums[i];
            }
        }

        private void ReturnValuesToColumn(List<int> nums, int columnIndex) {
            for (int i = 0; i < 4; i++) {
                numbers[i, columnIndex].CurrentValue = nums[i];
            }
        }
    }
}