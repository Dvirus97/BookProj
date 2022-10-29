using BookLib;
using BookLib.Enums;
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

        private readonly HomePage? homePage;
        bool isGenre;
        private bool isAll;

        public DiscountsPage() {
            InitializeComponent();
        }

        public DiscountsPage(HomePage homePage) : this() {

            this.homePage = homePage;
            discountbyCmb.ItemsSource = Store.Instace.DiscountByList;
            genreCmb.ItemsSource = Store.Instace.GenreList;
            listView.ItemsSource = DiscountManager.Instance.AllDiscounts;
        }

        private void AddNewDiscoutnBtn_Click(object sender, RoutedEventArgs e) {
            try {
                string disName;
                if (isGenre) {
                    if (genreCmb.SelectedItem is not Genre genre) return;
                    disName = genre.ToString();
                }
                else if (isAll) {
                    disName = "sale";
                }
                else {
                    disName = discountNameTbx.InputTbx.Text;
                }
                if (discountbyCmb.SelectedItem is not DiscountBy disBy) return;

                double price = MyValidation.ValidInt(priceTbx.InputTbx.Text, "Discount Price");

                DiscountManager.Instance.Add(new Discount(disBy, disName, price));
                screenLbl.Content = "new discount is added";
                discountbyCmb.SelectedIndex = 0;

                discountNameTbx.InputTbx.Text = "";
                priceTbx.InputTbx.Text = "";

                homePage?.ResetView();
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
                genreCmb.SelectedIndex = 0;
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

            DiscountManager.Instance.Remove(dis);
            screenLbl.Content = "discount is removed";
            homePage?.ResetView();
        }
    }
}
