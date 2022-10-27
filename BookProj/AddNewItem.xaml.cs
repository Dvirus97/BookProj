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
        //private readonly Store? Store.instanc;
        private readonly HomePage? homePage;
        string? photoPath;
        public AddNewItem() {
            InitializeComponent();
        }
        public AddNewItem(/*Store Store.instanc,*/ HomePage homePage) : this() {
            //this.Store.instanc = Store.instanc;
            this.homePage = homePage;
            genreCbx.ItemsSource = Store.Instace.GenreList;
            itemTypeCmb.ItemsSource = Store.Instace.ItemTypeList;
        }

        private void SaveBookBtn_Click(object sender, RoutedEventArgs e) {
            try {
                if (itemTypeCmb.SelectedIndex == 0) {
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
                        //MyValidation.ValidDate(addDateDP.SelectedDate, "Add to Store.instanc date")

                    };
                    book.PhotoPath = photoPath is not null ? photoPath : "";
                    this.photoPath = null;
                    Store.Instace?.Add(book);
                }
                if (itemTypeCmb.SelectedIndex == 1) {
                    Journal journal = new Journal() {

                        Author = MyValidation.ValidString(authorITC.InputTbx.Text, "Autor"),
                        Edition = MyValidation.ValidInt(editionITC.InputTbx.Text, "Edition"),
                        Publisher = MyValidation.ValidString(publisherITC.InputTbx.Text, "Publisher"),
                        Name = MyValidation.ValidString(nameITC.InputTbx.Text, "Name"),
                        Price = MyValidation.ValidDouble(priceITC.InputTbx.Text, "Price"),
                        Amount = MyValidation.ValidInt(quantityITC.InputTbx.Text, "Quantity"),
                        Genre = (Genre)Enum.Parse(typeof(Genre), genreCbx.Text),
                        PublishDate = MyValidation.ValidDate(PublishDateDP.SelectedDate, "publish Date"),
                        //Discount = MyValidation.ValidDouble(discountITC.InputTbx.Text, "Discount"),
                        //MyValidation.ValidDate(addDateDP.SelectedDate, "Add to Store.instanc date")

                    };
                    journal.PhotoPath = photoPath is not null ? photoPath : "";
                    this.photoPath = null;
                    Store.Instace?.Add(journal);
                }

                homePage?.ResetView();
                screenTbl.Content = "Book added and saved sucssesfuly";
                MessageBox.Show("Navigating Back..");
                HomeWin.MainFrame?.GoBack();
            }
            catch (InvalidInputException ex) {
                screenTbl.Content = $"{ex.Message} \n {ex.FailedProp}";

                string text = $"{DateTime.Now} \n{ex.Message} => {ex.FailedProp}\n";
                Store.Instace.TextSave.Save(text, true);
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
