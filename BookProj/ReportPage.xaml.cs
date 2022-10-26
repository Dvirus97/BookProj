using BookLib;
using BookLib.Models;
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
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page {
        private readonly Store? store;
        //DateTime fromDate;
        //DateTime tillDate;
        public ReportPage() {
            InitializeComponent();
        }
        public ReportPage(Store store) : this() {
            this.store = store;
            ByCmb.ItemsSource = store.FilterByList;
            listView.ItemsSource = TransactionManager.AllTransactions;
            fromDP.SelectedDate = tillDP.SelectedDate = DateTime.Now;

        }


        private void SerchBtn_Click(object sender, RoutedEventArgs e) {
            if (fromDP.SelectedDate is null) return;
            if (tillDP.SelectedDate is null) return;

            if (fromDP.SelectedDate is not DateTime fromDate) return;
            if (tillDP.SelectedDate is not DateTime tillDate) return;

            var selectedEnum = (FilterBy)ByCmb.SelectedItem;

            switch (selectedEnum) {
                case FilterBy.All:
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => true);
                    break;
                case FilterBy.Name:
                    if (nameCmb.SelectedItem is null) return;
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate,
                        x => x.Name is not null && x.Name.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Author:
                    if (nameCmb.SelectedItem is null) return;
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate,
                        x => x.Author is not null && x.Author.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Publisher:
                    if (nameCmb.SelectedItem is null) return;
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate,
                        x => x.Publisher is not null && x.Publisher.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Genre:
                    if (nameCmb.SelectedItem is null) return;
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate,
                        x => x.Genre.ToString().Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Item_Type:
                    if (nameCmb.SelectedItem is null) return;
                    listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate,
                        x => x.ItemType.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                default:
                    break;
            }
        }

        private void ByCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FilterBy selectedEnum = (FilterBy)ByCmb.SelectedItem;
            nameSpnl.Visibility = Visibility.Visible;

            switch (selectedEnum) {
                case FilterBy.All:
                    nameSpnl.Visibility = Visibility.Collapsed;
                    break;
                case FilterBy.Name:
                    nameCmb.ItemsSource = store?.AllName;
                    break;
                case FilterBy.Author:
                    nameCmb.ItemsSource = store?.AllAuthor;
                    break;
                case FilterBy.Publisher:
                    nameCmb.ItemsSource = store?.AllPublisher;
                    break;
                case FilterBy.Genre:
                    nameCmb.ItemsSource = store?.GenreList;
                    break;
                case FilterBy.Item_Type:
                    nameCmb.ItemsSource = store?.ItemTypeList;
                    break;
                default:
                    break;
            }
        }

        //private void nameCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        //    if (fromDP.SelectedDate is null) return;
        //    if (fromDP.SelectedDate is not DateTime fromDate) return;

        //    if (tillDP.SelectedDate is null) return;
        //    if (tillDP.SelectedDate is not DateTime tillDate) return;

        //    if (nameCmb.SelectedItem is null) return;

        //    var selectedEnum = (FilterBy)ByCmb.SelectedItem;
        //    switch (selectedEnum) {
        //        case FilterBy.Undefine:
        //            break;
        //        case FilterBy.All:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => true);
        //            break;
        //        case FilterBy.Name:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => x.Name.Equals(nameCmb.SelectedItem.ToString()));
        //            break;
        //        case FilterBy.Author:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => x.Author.Equals(nameCmb.SelectedItem.ToString()));
        //            break;
        //        case FilterBy.Publisher:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => x.Publisher.Equals(nameCmb.SelectedItem.ToString()));
        //            break;
        //        case FilterBy.Price:
        //            break;
        //        case FilterBy.Genre:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => x.Genre.ToString().Equals(nameCmb.SelectedItem.ToString()));
        //            break;
        //        case FilterBy.Item_Type:
        //            listView.ItemsSource = TransactionManager.FilterTransactions(fromDate, tillDate, x => x.ItemType.Equals(nameCmb.SelectedItem.ToString()));
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private void fromDP_CalendarClosed(object sender, RoutedEventArgs e) {
        //    if (fromDP.SelectedDate is null) return;
        //    if (fromDP.SelectedDate is not DateTime fromDate) return;

        //    this.fromDate = fromDate;
        //    nameCmb_SelectionChanged(null, null);
        //}

        //private void tillDP_CalendarClosed(object sender, RoutedEventArgs e) {
        //    if (tillDP.SelectedDate is null) return;
        //    if (tillDP.SelectedDate is not DateTime tillDate) return;

        //    this.tillDate = tillDate;
        //    nameCmb_SelectionChanged(null, null);
        //}
    }
}
