using BookLib;
using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProj2 {
    public static class MyValidation {

        public static string ValidString(string text, string propName) {
            if (string.IsNullOrWhiteSpace(text)) {

                throw new InvalidInputException("This Field is Empty", propName);
            }
            return text;
        }


        public static int ValidInt(string text, string propName) {
            int val;
            if (string.IsNullOrWhiteSpace(text)) {
                throw new InvalidInputException("This Field is Empty", propName);
            }
            if (!int.TryParse(text, out val)) {
                throw new InvalidInputException("This Field need number not Text", propName);
            }
            if (val < 0) {
                throw new InvalidInputException("This Field is invalid", propName);
            }
            return val;
        }

        public static double ValidDouble(string text, string propName) {
            double val;
            if (string.IsNullOrWhiteSpace(text)) {
                throw new InvalidInputException("This Field is Empty", propName);
            }
            if (!double.TryParse(text, out val)) {
                throw new InvalidInputException("This Field need number not Text", propName);
            }
            if (val < 0) {
                throw new InvalidInputException("This Field is invalid", propName);
            }

            return val;
        }

        public static DateTime? ValidDate(DateTime? date, string propName) {
            if (date is null) {
                throw new InvalidInputException("this date was empty", propName);
            }
            return date;
        }
    }
}
