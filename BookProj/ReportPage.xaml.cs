using BookLib;
using BookLib.Enums;
using BookLib.Models;
using DbService;
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

        List<Transaction> filterList = new List<Transaction>();
        TextSave LogFilterTransaction = new TextSave("LogReportsFilter.txt");

        public ReportPage() {
            InitializeComponent();
            ByCmb.ItemsSource = Store.Instace.FilterByList;
            listView.ItemsSource = TransactionManager.Instance.AllTransactions.Reverse<Transaction>();
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

                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate, x => true);
                    break;
                case FilterBy.Name:
                    if (nameCmb.SelectedItem is null) return;
                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate,
                        x => x.Name is not null && x.Name.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Author:
                    if (nameCmb.SelectedItem is null) return;
                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate,
                        x => x.Author is not null && x.Author.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Publisher:
                    if (nameCmb.SelectedItem is null) return;
                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate,
                        x => x.Publisher is not null && x.Publisher.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Genre:
                    if (nameCmb.SelectedItem is null) return;
                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate,
                        x => x.Genre.ToString().Equals(nameCmb.SelectedItem.ToString()));
                    break;
                case FilterBy.Item_Type:
                    if (nameCmb.SelectedItem is null) return;
                    filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate,
                        x => x.ItemType is not null && x.ItemType.Equals(nameCmb.SelectedItem.ToString()));
                    break;
                default:
                    break;
            }
            if (filterList is not null) {
                listView.ItemsSource = filterList;
            }
        }

        private void ByCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FilterBy selectedEnum = (FilterBy)ByCmb.SelectedItem;
            nameSpnl.Visibility = Visibility.Visible;

            switch (selectedEnum) {
                case FilterBy.All:
                    nameSpnl.Visibility = Visibility.Collapsed;
                    return;

                case FilterBy.Name:
                    nameCmb.ItemsSource = Store.Instace?.AllName;
                    break;
                case FilterBy.Author:
                    nameCmb.ItemsSource = Store.Instace?.AllAuthor;
                    break;
                case FilterBy.Publisher:
                    nameCmb.ItemsSource = Store.Instace?.AllPublisher;
                    break;
                case FilterBy.Genre:
                    nameCmb.ItemsSource = Store.Instace?.GenreList;
                    break;
                case FilterBy.Item_Type:
                    nameCmb.ItemsSource = Store.Instace?.ItemTypeList;
                    break;
                default:
                    break;
            }
            nameCmb.SelectedIndex = 0;
        }

        private void SaveFilterBtn_Click(object sender, RoutedEventArgs e) {

            string text = "";
            foreach (var t in filterList) {
                text += t.ToString() + "\n\n";
            }

            LogFilterTransaction.Save(text, false);
            MessageBox.Show("log saved! ");
        }
    }
}
