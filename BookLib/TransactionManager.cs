﻿using BookLib.Models;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib {

    public class TransactionManager {

        public static TransactionManager Instance { get; } = new TransactionManager();
        public List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
        JsonSave<Transaction> LogTransactionsList = new JsonSave<Transaction>("LogTransactionsList.json");

        private TransactionManager() {
            Load();
        }

        public void Add(Transaction transaction) {
            AllTransactions.Add(transaction);
            Save();
        }
        public void Remove(Transaction transaction) {
            AllTransactions.Remove(transaction);
            Save();
        }

        public void Save() {
            LogTransactionsList.Save(AllTransactions);
        }

        public void Load() {
            AllTransactions = LogTransactionsList.Load();
        }

        public List<Transaction> FilterTransactions(DateTime FromDate, DateTime tillDate, Predicate<Transaction> byItem) {
            return (from x in AllTransactions
                    where (x.PurchaseDate.Date <= tillDate && x.PurchaseDate.Date >= FromDate && byItem.Invoke(x))
                    select x).Reverse<Transaction>().ToList();
        }

    }
}
