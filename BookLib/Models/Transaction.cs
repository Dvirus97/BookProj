using BookLib.Enums;
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
            return $"date: {PurchaseDate:d} \ntype: {ItemType}, name: {Name}, author: {Author}, publiser: {Publisher}, price: {Price:c}, genre: {Genre}, amount: {Amount}";
        }
    }
}
