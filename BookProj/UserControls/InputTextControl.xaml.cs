using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookProj.UserControls {
    /// <summary>
    /// Interaction logic for InputTextControl.xaml
    /// </summary>
    public partial class InputTextControl : UserControl {
        public InputTextControl() {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// the title of the textBox
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// max number of chars 
        /// </summary>
        public int MaxLength { get; set; }


    }

}
