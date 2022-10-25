using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BookLib.Models {


    public class Transaction {

        //public List<Item> List { get; set; } = new List<Item>();
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public double Price { get; set; }
        public Genre Genre { get; set; }
        public int Amount { get; set; }
        public string ItemType { get; set; }
        public DateTime PurchaseDate { get; set; }

        public override string ToString() {
            return $"typt: {ItemType}, name: {Name}, authoer: {Author}, publiser: {Publisher}, price: {Price}, genre: {Genre}, amount: {Amount}, date: {PurchaseDate:d}";
        }

    }



    public static class TransactionManager {

        public static List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
        static JsonSave<Transaction> LogTransactionsList = new JsonSave<Transaction>("LogTransactionsList.json");

        static TransactionManager() {
            Load();
        }

        public static void Save() {
            LogTransactionsList.SaveData(AllTransactions);
        }

        public static void Load() {
            AllTransactions = LogTransactionsList.GetData();
        }

        public static IEnumerable<Transaction> FilterTransactions(DateTime FromDate, DateTime tillDate, Predicate<Transaction> byItem) {
            return from x in AllTransactions where (x.PurchaseDate.Date <= tillDate && x.PurchaseDate.Date >= FromDate && byItem.Invoke(x)) select x;
        }

    }
}
