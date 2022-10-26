using BookLib;
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

namespace BookProj {
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page {
        private readonly HomePage? homePage;

        public EditPage() {
            InitializeComponent();
        }
        public EditPage(/*Store store,*/ HomePage homePage) : this() {
            //this.store = store;
            this.homePage = homePage;
            dataGrid.ItemsSource = Store.store.items;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            Store.store?.Save();
            homePage?.ResetView();
        }
    }
}
