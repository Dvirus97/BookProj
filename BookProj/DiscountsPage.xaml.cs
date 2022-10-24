using BookLib;
using BookLib.Models;
using BookProj2;
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
    /// Interaction logic for DiscountsPage.xaml
    /// </summary>
    public partial class DiscountsPage : Page {
        private readonly Store? store;
        private readonly HomePage? homePage;
        bool isGenre;
        private bool isAll;

        public DiscountsPage() {
            InitializeComponent();
        }

        public DiscountsPage(Store store, HomePage homePage) : this() {
            this.store = store;
            this.homePage = homePage;
            discountbyCmb.ItemsSource = store.DiscountByList;
            genreCmb.ItemsSource = store.GenreList;
            listView.ItemsSource = DiscountManager.AllDiscounts;
        }

        private void AddNewDiscoutnBtn_Click(object sender, RoutedEventArgs e) {
            try {
                string discountName;
                if (isGenre) {
                    if (genreCmb.SelectedItem is not Genre genre) return;
                    discountName = genre.ToString();
                }
                else if (isAll) {
                    discountName = "sale";
                }
                else {
                    discountName = discountNameTbx.InputTbx.Text;
                }
                if (discountbyCmb.SelectedItem is not DiscountBy disBy) return;

                double price = MyValidation.ValidInt(priceTbx.InputTbx.Text, "Discount Price");

                DiscountManager.AllDiscounts.Add(new Discount(disBy, discountName, price));
                screenLbl.Content = "new discount is added";
                DiscountManager.Save();
            }
            catch (InvalidInputException ex) {
                screenLbl.Content = $"{ex.Message} \n {ex.FailedProp}";
            }
        }

        private void discountbyCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (discountbyCmb.SelectedItem is not DiscountBy DB) return;

            if (DB.ToString() == "Genre") {
                GenreSpl.Visibility = Visibility.Visible;
                notGenreSpl.Visibility = Visibility.Collapsed;
                isGenre = true;
                isAll = false;
            }
            else if (DB.ToString() == "AllStore") {
                GenreSpl.Visibility = Visibility.Collapsed;
                notGenreSpl.Visibility = Visibility.Collapsed;
                isGenre = false;
                isAll = true;
            }
            else {
                GenreSpl.Visibility = Visibility.Collapsed;
                notGenreSpl.Visibility = Visibility.Visible;
                isAll = false;
                isGenre = false;
            }
        }

        private void RemoveDiscoutnBtn_Click(object sender, RoutedEventArgs e) {
            if (listView.SelectedItem is not Discount dis) return;

            DiscountManager.AllDiscounts.Remove(dis);
            DiscountManager.Save();
        }
    }
}
