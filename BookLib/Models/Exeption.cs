using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models
{
    [Serializable]
    public class InvalidInputException : Exception
    {

        public string? FailedProp { get; set; }
        public DateTime? Time { get => DateTime.Now; }

        public InvalidInputException()
        {
        }

        public InvalidInputException(string? message) : base(message)
        {
        }
        public InvalidInputException(string? message, string failedProp) : base(message)
        {
            FailedProp = failedProp;
        }

    }



    [Serializable]
    public class MyException : Exception
    {
        public MyException() { }
        public MyException(string message) : base(message) { }
    }
}
