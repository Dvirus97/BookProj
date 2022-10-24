using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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
    /// Interaction logic for MyButton.xaml
    /// </summary>
    public partial class MyButton : UserControl {
        public MyButton() {
            InitializeComponent();
            DataContext = this;
        }
        public string? Text { get; set; }
        public event RoutedEventHandler? Click;

        private void Button_Click(object sender, RoutedEventArgs e) {
            Click?.Invoke(this, e);
        }
    }
}
