using BookLib;
using BookLib.Enums;
using BookLib.Models;
using BookProj2;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
        public DiscountsPage() {
            InitializeComponent();
        }

        public DiscountsPage(HomePage homePage) : this() {

            this.homePage = homePage;
            discountbyCmb.ItemsSource = Store.Instace.DiscountByList;
            listView.ItemsSource = DiscountManager.Instance.AllDiscounts;
        }

        private void AddNewDiscoutnBtn_Click(object sender, RoutedEventArgs e) {
            try {
                string? temp = NameCmb.SelectedItem.ToString();
                if (temp is null) return;
                string disName = temp;

                if (discountbyCmb.SelectedItem is not DiscountBy disBy) return;

                double price = MyValidation.ValidInt(priceTbx.InputTbx.Text, "Discount Price");

                DiscountManager.Instance.Add(new Discount(disBy, disName, price));
                screenLbl.Content = "new discount is added";
                discountbyCmb.SelectedIndex = 0;

                priceTbx.InputTbx.Text = "";

                homePage?.ResetView();
            }
            catch (InvalidInputException ex) {
                screenLbl.Content = $"{ex.Message} \n {ex.FailedProp}";
            }
        }

        private void discountbyCmb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DiscountBy selectedEnum = (DiscountBy)discountbyCmb.SelectedItem;

            NameCmb.ItemsSource = selectedEnum switch {
                DiscountBy.Item_Type => Store.Instace.ItemTypeList,
                DiscountBy.Name => Store.Instace.AllName,
                DiscountBy.Author => Store.Instace.AllAuthor,
                DiscountBy.Publisher => Store.Instace.AllPublisher,
                DiscountBy.Genre => Store.Instace.GenreList,
                DiscountBy.AllStore => new string[1] { "Sale" },
                _ => null,
            };

            NameCmb.SelectedIndex = 0;
        }

        private void RemoveDiscoutnBtn_Click(object sender, RoutedEventArgs e) {
            if (listView.SelectedItem is not Discount dis) return;

            DiscountManager.Instance.Remove(dis);
            screenLbl.Content = "discount is removed";
            homePage?.ResetView();
        }
    }
}
