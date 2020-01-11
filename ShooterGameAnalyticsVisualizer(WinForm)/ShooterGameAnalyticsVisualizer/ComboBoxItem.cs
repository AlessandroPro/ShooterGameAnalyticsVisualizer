using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGameAnalyticsVisualizer
{
    // Represents a simple item with a text and value property
    // Useful in a list as the data source of a combo box
    class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }
    }
}
