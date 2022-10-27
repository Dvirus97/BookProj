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
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public double Price { get; set; }
        public Genre Genre { get; set; }
        public int Amount { get; set; }
        public string? ItemType { get; set; }
        public DateTime PurchaseDate { get; set; }

        public override string ToString() {
            return $"date: {PurchaseDate:d} \ntype: {ItemType}, name: {Name}, authoer: {Author}, publiser: {Publisher}, price: {Price}, genre: {Genre}, amount: {Amount}";
        }
    }



    public class TransactionManager {

        public static TransactionManager Instance { get; } = new TransactionManager();
        public List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
        JsonSave<Transaction> LogTransactionsList = new JsonSave<Transaction>("LogTransactionsList.json");


        private TransactionManager() {
            Load();
        }

        public void Save() {
            LogTransactionsList.SaveData(AllTransactions);
        }

        public void Load() {
            AllTransactions = LogTransactionsList.GetData();
        }

        public List<Transaction> FilterTransactions(DateTime FromDate, DateTime tillDate, Predicate<Transaction> byItem) {
            return (from x in AllTransactions where (x.PurchaseDate.Date <= tillDate && x.PurchaseDate.Date >= FromDate && byItem.Invoke(x)) select x).ToList();
        }

    }
}
