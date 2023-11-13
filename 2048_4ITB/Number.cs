using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_4ITB
{
    internal class Number : Label
    {
        public const int size = 200;

        private int currentValue;

        public int CurrentValue {
            get { return currentValue; }
            set { 
                currentValue = value;
                Text = currentValue.ToString();

                if (currentValue == 0) Text = "";
            }
        }

        public Number(int x, int y) : base() {
            AutoSize = false;
            Size = new Size(size, size);
            Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Text = "2";
            TextAlign = ContentAlignment.MiddleCenter;
            BorderStyle = BorderStyle.FixedSingle;
            Location = new Point(x * size, y * size);

            CurrentValue = 0;
        }
    }
}
