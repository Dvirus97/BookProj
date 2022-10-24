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

namespace BookProj {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Store store = new Store();
        public MainWindow() {
            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e) {
            bool isLogIn = true;
            User user = new User("user", "1234");
            User Admin = new User("admin", "admin");

            //if (user.UserName.Equals(NameTbx.Text.ToLower()) &&
            //    user.Password.Equals(PasswordPass.Password.ToLower())) {
            //    store.IsAdmin = false;
            //    isLogIn = true;
            //}
            //else if (Admin.UserName.Equals(NameTbx.Text.ToLower()) &&
            //    Admin.Password.Equals(PasswordPass.Password.ToLower())) {
            //    store.IsAdmin = true;
            //    isLogIn = true;
            //}
            //else {
            //    MessageBox.Show("Log in Failed");
            //    return;
            //}

            if (isLogIn) {
                HomeWin homeWin = new HomeWin(store);
                homeWin.Show();
                this.Close();
            }
        }

    }

    class User {
        public User(string userName, string password) {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }


    }
}
