using BookLib;
using BookLib.Enums;
using BookLib.Models;
using BookProj2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for AddNewItem.xaml
    /// </summary>
    public partial class AddNewItem : Page {
        private readonly HomePage? homePage;
        string? photoPath;
        public AddNewItem() {
            InitializeComponent();
        }
        public AddNewItem(HomePage homePage) : this() {
            this.homePage = homePage;
            genreCbx.ItemsSource = Store.Instace.GenreList;
            itemTypeCmb.ItemsSource = Store.Instace.ItemTypeList;
        }

        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e) {
            try {
                ItemType type = (ItemType)itemTypeCmb.SelectedItem;

                switch (type) {
                    case ItemType.Book:
                        Store.Instace?.Add(NewItem<Book>());
                        break;
                    case ItemType.Journal:
                        Store.Instace?.Add(NewItem<Journal>());
                        break;
                    default:
                        break;
                }

                this.photoPath = null;

                homePage?.ResetView();
                screenTbl.Content = "Book added and saved sucssesfuly";
                MessageBox.Show("Navigating Back..");
                HomeWin.MainFrame?.GoBack();
            }
            catch (InvalidInputException ex) {
                screenTbl.Content = $"{ex.Message} \n {ex.FailedProp}";

                string text = $"{DateTime.Now} \n{ex.Message} => {ex.FailedProp}\n";
                Store.Instace.LogError.Save(text, true);
            }
            catch (ArgumentNullException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        T NewItem<T>() where T : Item, new() {
            T item = new() {
                Author = MyValidation.ValidString(authorITC.InputTbx.Text, "Autor"),
                Edition = MyValidation.ValidInt(editionITC.InputTbx.Text, "Edition"),
                Publisher = MyValidation.ValidString(publisherITC.InputTbx.Text, "Publisher"),
                Name = MyValidation.ValidString(nameITC.InputTbx.Text, "Name"),
                Price = MyValidation.ValidDouble(priceITC.InputTbx.Text, "Price"),
                Amount = MyValidation.ValidInt(quantityITC.InputTbx.Text, "Quantity"),
                Genre = (Genre)Enum.Parse(typeof(Genre), genreCbx.Text),
                PublishDate = MyValidation.ValidDate(PublishDateDP.SelectedDate, "publish Date"),
            };
            if (!string.IsNullOrWhiteSpace(photoPath)) {
                item.PhotoPath = photoPath;
            }
            return item;
        }
        private void UploadPhotoBtn_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.Title = "choose Image";
            dlg.Filter = "Image files (*.bmp, *.jpg, *.png, *.jpeg, *.gif)|*.bmp;*.jpg;*.png;*.jpeg;*.gif";
            bool? ans = dlg.ShowDialog();
            if (ans is true) {
                this.photoPath = dlg.FileName;
            }
        }
    }
}
