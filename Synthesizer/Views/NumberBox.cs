using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    class NumberBox : TextBox
    {
        [Category(Configuration.Application.CategoryName)]
        public int? MinValue { get; set; }
        [Category(Configuration.Application.CategoryName)]
        public int? MaxValue { get; set; }
        [Category(Configuration.Application.CategoryName)]
        public int Value
        {
            get
            {
                bool status = int.TryParse(Text, out int parsed);
                if (status)
                    return parsed;
                else
                    return 0;
            }
            set
            {
                Text = value.ToString();
            }
        }

        private const string AllowedChars = "-0123456789\b"; // ostatni znak - backspace - również przychodzi w evencie

        protected override void OnKeyPress(KeyPressEventArgs args)
        {
            bool minusDisallowed = args.KeyChar == '-' && Text.Length > 0;
            if (!AllowedChars.Contains(args.KeyChar.ToString()) || minusDisallowed)
            {
                args.Handled = true;
                return;
            }

            base.OnKeyPress(args);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            bool success = int.TryParse(Text, out int parsed);

            if (success)
            {
                if (MinValue != null && parsed < MinValue)
                    Text = MinValue.ToString();
                else if (MaxValue != null && parsed > MaxValue)
                    Text = MaxValue.ToString();
            }
            else
            {
                Text = MinValue.ToString();
            }

            base.OnLostFocus(e);
        }
    }
}
