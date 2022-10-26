using BookLib.Models;
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
using BookProj.UserControls;

namespace BookProj {
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page {
        public List<Item> filter = new List<Item>();
        Store? store;
        private readonly HomeWin? homeWin;
        Predicate<Item>? predicate;
        Item? selectedItem;

        public HomePage() {
            InitializeComponent();
            listView.DataContext = this;
        }
        public HomePage(Store store, HomeWin homeWin) : this() {
            this.store = store;
            this.homeWin = homeWin;

            switch (store.IsAdmin) {
                case true:
                    AdminStackPanel.Visibility = Visibility.Visible;
                    break;
                case false:
                    AdminStackPanel.Visibility = Visibility.Collapsed;
                    break;
                default:
            }
            predicate = x => true;
            filter = store.FilterList(predicate);
            ResetView();
            SetButtons();
            listView.ItemsSource = filter;
        }

        void SetButtons() {
            showAllBtn.Tag = new Predicate<Item>(x => true);
            showBooksBtn.Tag = new Predicate<Item>(x => x.GetType() == typeof(Book));
            showJournalsBtn.Tag = new Predicate<Item>(x => x.GetType() == typeof(Journal));
            showDiscountBtn.Tag = new Predicate<Item>(x => x.Discount > 0);
        }

        public void ResetView() {
            if (predicate is null) return;

            List<Item>? temp = store?.FilterList(predicate);
            if (temp is not null)
                filter = temp;
            listView.ItemsSource = filter;
            DateilsLbl.Content = selectedItem?.ToString();

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (listView.SelectedItem is not Item item) return;
            this.selectedItem = item;
            ResetView();
            //DateilsLbl.Content = item.ToString();
        }

        private void PlusBtn_click(object sender, RoutedEventArgs e) {
            if (sender is not Button btn) return;

            if (btn.Tag is not Item item) return;

            if (item.ShopCount < item.Amount)
                item.ShopCount++;
            ResetView();
        }

        private void MinusBtn_click(object sender, RoutedEventArgs e) {
            if (sender is not Button btn) return;

            if (btn.Tag is not Item item) return;

            if (item.ShopCount > 0)
                item.ShopCount--;
            ResetView();
        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e) {
            if (sender is not MyButton btn) return;

            if (btn.Tag is not Predicate<Item> pred) return;
            this.predicate = pred;
            ResetView();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e) {
            if (store is null) return;
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new EditPage(store, this);
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.Show();
            homeWin?.Close();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e) {
            if (listView.SelectedItem is not Item item) return;
            store?.Remove(item);
            ResetView();
            //store.Save();
        }

        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e) {
            if (store is null) return;
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new AddNewItem(store, this);
        }

        private void DiscountsBtn_Click(object sender, RoutedEventArgs e) {
            if (store is null) return;
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new DiscountsPage(store, this);
        }

        private void SellBtn_Click(object sender, RoutedEventArgs e) {
            for (int i = 0 ; i < store?.Count ; i++) {
                store[i].Amount -= store[i].ShopCount;
                if (store[i].ShopCount > 0) {
                    Transaction tra = new Transaction() {
                        Name = store[i].Name,
                        Amount = store[i].ShopCount,
                        Author = store[i].Author,
                        Genre = store[i].Genre,
                        Price = store[i].Price,
                        Publisher = store[i].Publisher,
                        PurchaseDate = DateTime.Now.Date,
                        ItemType = store[i].GetType().Name
                    };
                    TransactionManager.AllTransactions.Add(tra);
                    TransactionManager.Save();
                }
                if (store[i].Amount <= 0) {
                    store.Remove(store[i]);
                }
                else {
                    store[i].ShopCount = 0;
                }
            }
            MessageBox.Show("All Sopping Cart Has bin bought. ");
            ResetView();
            //store?.Save();
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e) {
            if (store is null) return;
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new ReportPage(store);
        }
    }
}
