using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models {
    public class InvalidInputException : Exception {

        public string? FailedProp { get; set; }

        public InvalidInputException() { }

        public InvalidInputException(string? message) : base(message) { }
        public InvalidInputException(string? message, string failedProp) : base(message) {
            FailedProp = failedProp;
            string text = $"{DateTime.Now} \n{this.Message} => {this.FailedProp}\n";
            Store.Instace.LogError.Save(text, true);
        }
    }
}

