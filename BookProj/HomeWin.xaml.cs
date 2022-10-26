using BookLib;
using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookProj {
    /// <summary>
    /// Interaction logic for HomeWin.xaml
    /// </summary>
    public partial class HomeWin : Window {

        public static Frame? MainFrame;

        public HomeWin() {
            InitializeComponent();
            MainFrame = mainFrame;
            MainFrame.Content = new HomePage(this);
        }
    }
}
