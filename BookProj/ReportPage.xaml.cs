using BookLib;
using BookLib.Enums;
using BookLib.Models;
using DbService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        /// <summary>
        /// list after filter
        /// </summary>
        List<Transaction> filterList = new List<Transaction>();

        /// <summary>
        /// textSave instance to save the filterd list
        /// </summary>
        TextSave LogFilterTransaction = new TextSave("LogReportsFilter.txt");

        public ReportPage() {
            InitializeComponent();
            ByCmb.ItemsSource = Store.Instace.FilterByList;
            listView.ItemsSource = new string[1] { "choose a filter and click the search button to display" }; ;
            fromDP.SelectedDate = tillDP.SelectedDate = DateTime.Now;
        }



        private void SearchBtn_Click(object sender, RoutedEventArgs e) {
            if (fromDP.SelectedDate is not DateTime fromDate) return;
            if (tillDP.SelectedDate is not DateTime tillDate) return;

            Predicate<Transaction> predicate;
            var selectedEnum = (FilterBy)ByCmb.SelectedItem;
            if (nameCmb.SelectedItem is null) return;

            string? CmbSelected = nameCmb.SelectedItem.ToString();
            predicate = selectedEnum switch {
                FilterBy.All => x => true,
                FilterBy.Name => x => x.Name is not null && x.Name.Equals(CmbSelected),
                FilterBy.Author => x => x.Author is not null && x.Author.Equals(CmbSelected),
                FilterBy.Publisher => x => x.Publisher is not null && x.Publisher.Equals(CmbSelected),
                FilterBy.Genre => x => x.Genre.ToString().Equals(CmbSelected),
                FilterBy.Item_Type => x => x.ItemType is not null && x.ItemType.Equals(CmbSelected),
                _ => x => true,
            };

            filterList = TransactionManager.Instance.FilterTransactions(fromDate, tillDate, predicate);
            if (filterList is null) return;

            listView.ItemsSource = filterList.Count > 0 ? filterList : new string[] { "there are no results" };
        }

        private void ByCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FilterBy selectedEnum = (FilterBy)ByCmb.SelectedItem;
            //nameSpnl.Visibility = Visibility.Visible;

            nameCmb.ItemsSource = selectedEnum switch {
                FilterBy.All => new string[1] { "All" },
                FilterBy.Name => Store.Instace.AllName,
                FilterBy.Author => Store.Instace.AllAuthor,
                FilterBy.Publisher => Store.Instace.AllPublisher,
                FilterBy.Genre => Store.Instace.GenreList,
                FilterBy.Item_Type => Store.Instace.ItemTypeList,
                _ => null,
            };
            nameCmb.SelectedIndex = 0;
        }

        private void SaveFilterBtn_Click(object sender, RoutedEventArgs e) {
            StringBuilder text = new StringBuilder("file path: \n" + LogFilterTransaction.path + "\n\n");
            //string text = "file path: \n" + LogFilterTransaction.path + "\n\n";
            foreach (var t in filterList) {
                text.AppendLine(t.ToString() + "\n");
            }

            LogFilterTransaction.Save(text.ToString(), false);
            MessageBox.Show("log saved! Opening File...");

            new Process {
                StartInfo = new ProcessStartInfo(LogFilterTransaction.path) {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
