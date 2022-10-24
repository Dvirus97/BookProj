using BookLib;
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
        private readonly Store? store;
        private readonly HomePage? homePage;
        string? photoPath;
        public AddNewItem() {
            InitializeComponent();
        }
        public AddNewItem(Store store, HomePage homePage) : this() {
            this.store = store;
            this.homePage = homePage;
            genreCbx.ItemsSource = store.GenreList;
        }

        private void SaveBookBtn_Click(object sender, RoutedEventArgs e) {
            try {
                Book book = new Book() {

                    Author = MyValidation.ValidString(authorITC.InputTbx.Text, "Autor"),
                    Edition = MyValidation.ValidInt(editionITC.InputTbx.Text, "Edition"),
                    Publisher = MyValidation.ValidString(publisherITC.InputTbx.Text, "Publisher"),
                    Name = MyValidation.ValidString(nameITC.InputTbx.Text, "Name"),
                    Price = MyValidation.ValidDouble(priceITC.InputTbx.Text, "Price"),
                    Amount = MyValidation.ValidInt(quantityITC.InputTbx.Text, "Quantity"),
                    Genre = (Genre)Enum.Parse(typeof(Genre), genreCbx.Text),
                    PublishDate = MyValidation.ValidDate(PublishDateDP.SelectedDate, "publish Date"),
                    //Discount = MyValidation.ValidDouble(discountITC.InputTbx.Text, "Discount"),
                    //MyValidation.ValidDate(addDateDP.SelectedDate, "Add to store date")

                };

                book.PhotoPath = photoPath is not null ? photoPath : "";
                this.photoPath = null;

                store?.Add(book);
                store?.Save();
                homePage?.ResetView();
                screenTbl.Text = "Book added and saved sucssesfuly";
                MessageBox.Show("Navigating Back..");
                HomeWin.MainFrame?.GoBack();
            }
            catch (InvalidInputException ex) {
                screenTbl.Text = $"{ex.Message} \n {ex.FailedProp}";
                //store?.ErrorLog($"Book: {ex.Message} => {ex.FailedProp}", true);
                //store?.LogErrorSave.SaveData($"{DateTime.Now} Book: {ex.Message} => {ex.FailedProp}", true);
                //store?.ErrorLogSave.SaveData(ex, true);
            }
            catch (ArgumentNullException ex) {

                MessageBox.Show(ex.Message);
            }

        }

        private void UploadPhotoBtn_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.Title = "choose Image";
            dlg.Filter = "Image files (*.bmp, *.jpg, *.png, *.jpeg, *.gif)|*.bmp;*.jpg;*.png;*.jpeg;*.gif";
            bool? ans = dlg.ShowDialog();
            if (ans is true) {
                photoPath = dlg.FileName;
            }
        }
    }
}
