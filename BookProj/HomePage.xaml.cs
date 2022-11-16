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
        private readonly HomeWin? homeWin;
        Predicate<Item>? predicate;
        Item? selectedItem;
        private RadioButton? radioButton;

        public HomePage() {
            InitializeComponent();
            listView.DataContext = this;
        }
        public HomePage(HomeWin homeWin) : this() {
            this.homeWin = homeWin;

            switch (Store.Instace.IsAdmin) {
                case true:
                    AdminStackPanel.Visibility = Visibility.Visible;
                    break;
                case false:
                    AdminStackPanel.Visibility = Visibility.Collapsed;
                    break;
                default:
            }
            predicate = x => true;
            ResetView();
            SetFilterButtons();
            listView.ItemsSource = filter;
        }

        void SetFilterButtons() {
            showAllBtn.Tag = new Predicate<Item>(x => true);
            showBooksBtn.Tag = new Predicate<Item>(x => x is Book);
            showJournalsBtn.Tag = new Predicate<Item>(x => x is Journal);
            showDiscountBtn.Tag = new Predicate<Item>(x => x.Discount > 0);
        }

        public void ResetView() {
            if (predicate is null) return;


            List<Item>? temp = Store.Instace?.FilterList(predicate);
            if (temp is null) return;
            filter = temp;
            if (filter.Count <= 0) {
                listView.ItemsSource = null;
                DateilsLbl.Content = "There is not result";
            }
            else {
                listView.ItemsSource = filter;
                DateilsLbl.Content = selectedItem?.ToString() ?? "Details";
            }
            selectedItem = null;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (listView.SelectedItem is not Item item) return;
            this.selectedItem = item;
            ResetView();
        }

        private void PlusBtn_click(object sender, RoutedEventArgs e) {
            if (sender is not Button btn) return;

            if (btn.DataContext is not Item item) return;

            if (item.ShopCount < item.Amount)
                item.ShopCount++;
            ResetView();
        }

        private void MinusBtn_click(object sender, RoutedEventArgs e) {
            if (sender is not Button btn) return;
            if (btn.DataContext is not Item item) return;

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
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new EditPage(this);
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.Show();
            homeWin?.Close();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e) {
            if (listView.SelectedItem is not Item item) return;
            Store.Instace?.Remove(item);
            ResetView();
        }

        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e) {
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new AddNewItem(this);
        }

        private void DiscountsBtn_Click(object sender, RoutedEventArgs e) {
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new DiscountsPage(this);
        }

        private void SellBtn_Click(object sender, RoutedEventArgs e) {
            for (int i = 0 ; i < Store.Instace?.Count ; i++) {
                Store.Instace[i].Amount -= Store.Instace[i].ShopCount;
                if (Store.Instace[i].ShopCount > 0) {
                    Transaction tra = NewTransaction(Store.Instace[i]);
                    TransactionManager.Instance.Add(tra);
                }
                if (Store.Instace[i].Amount <= 0) {
                    Store.Instace.Remove(Store.Instace[i]);
                }
                else {
                    Store.Instace[i].ShopCount = 0;
                }
            }
            MessageBox.Show("All Shopping Cart Has been bought. ");
            ResetView();
            Store.Instace?.Save();
        }

        Transaction NewTransaction(Item item) {
            return new Transaction() {
                Name = item.Name,
                Amount = item.ShopCount,
                Author = item.Author,
                Genre = item.Genre,
                Price = item.Price,
                Publisher = item.Publisher,
                PurchaseDate = DateTime.Now.Date,
                ItemType = item.GetType().Name
            };
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e) {
            if (HomeWin.MainFrame is null) return;
            HomeWin.MainFrame.Content = new ReportPage();
        }


        private void SearchTbx_TextChanged(object sender, TextChangedEventArgs e) {
            if (this.radioButton is null) return;
            string text = SearchTbx.Text.ToLower().Trim();
            string? radioText = radioButton.Content.ToString();
            switch (radioText) {
                case "Author":
                    predicate = (x => {
                        if (string.IsNullOrEmpty(x.Author)) return false;
                        else return x.Author.ToLower().StartsWith(text);
                    });
                    break;
                case "Publisher":
                    predicate = (x => {
                        if (string.IsNullOrEmpty(x.Author)) return false;
                        else return x.Publisher is not null && x.Publisher.ToLower().StartsWith(text);
                    });
                    break;
                case "Name":
                    predicate = (x => {
                        if (string.IsNullOrEmpty(x.Name)) return false;
                        else return x.Name is not null && x.Name.ToLower().StartsWith(text);
                    });
                    break;
                default:
                    break;
            }
            ResetView();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {
            if (sender is not RadioButton rb) return;
            this.radioButton = rb;
        }
    }
}
