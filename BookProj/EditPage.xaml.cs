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
        public EditPage(HomePage homePage) : this() {
            this.homePage = homePage;
            try {
                dataGrid.ItemsSource = Store.Instace.Items;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                Store.Instace.LogError.Save(DateTime.Now + "\n" + ex.Message + "\n", true);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            Store.Instace?.Save();
            homePage?.ResetView();
        }

        //private void DataGrid_onCellEditEnd(object sender, DataGridCellEditEndingEventArgs e) {
        //    //MessageBox.Show("cell change save");
        //    //Store.Instace?.Save();
        //}
    }
}
