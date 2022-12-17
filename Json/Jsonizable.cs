using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Json
{
    public abstract class Jsonizable
    {
        public abstract string ToJson();

        protected Jsonizable() { }

        // child classes should probably have a constructor that takes a json string

        // if extending from this base constructor for the proposed above constructor,
        // child classes can use the getNextValue method for a much easier implementation
        private string json;
        private int index;
        protected Jsonizable(string json, int index)
        {
            this.json = json;
            this.index = index;
        }

        // helper method for constructing a class from json
        protected string getNextValue()
        {
            if (index < 0) { index = 0; }
            int indexLeft = json.IndexOf(':', index) + 1;
            if (indexLeft < 0) {
                throw new NoNextValueException("No ':' found after index " + index);
            }
            int indexRight = json.IndexOf(',', indexLeft) - 1;
            if (indexRight < 0) { indexRight = json.IndexOf('}'); }
            if (indexLeft < 0) {
                throw new NoNextValueException("No ',' or '}' found after index " + indexLeft);
            }
            int length = indexRight - indexLeft;
            return json.Substring(indexLeft, length);
        }

        // run if you know you won't use getNextValue again,
        // to (probably) remove the large json string from memory
        protected void CleanupJson() { json = ""; }
    }

    public class NoNextValueException : Exception {
        public NoNextValueException() : base() { }
        public NoNextValueException(string? message) : base(message) { }
        public NoNextValueException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
