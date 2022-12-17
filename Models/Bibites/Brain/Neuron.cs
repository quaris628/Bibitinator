using Bibitinator.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class Neuron : Jsonizable
    {
        public int Index { get; }
        public Types Type { get; set; }
        public string Description { get; set; }
        
        // unused for now
        private string inov;
        private string value;
        private string lastInput;
        private string lastOutput;

        public Neuron(int index, Types type, string? description)
        {
            if (index < 0) {
                throw new ArgumentOutOfRangeException("Index cannot be negative"); }
            if (description == null) {
                description = "Hidden" + index;
            } else if (!description.All(char.IsLetterOrDigit)) {
                throw new InvalidDescriptionException(
                    "Neuron description must be alphanumeric");
            }

            Index = index;
            Type = type;
            Description = description;
            inov = "0";
            value = "0";
            lastInput = "0.0";
            lastOutput = "0.0";
        }

        public override string ToString()
        {
            return Description + " : " + Type.ToString();
        }

        public enum Types
        {
            Input = 0,
            Sigmoid = 1,
            Linear = 2,
            TanH = 3,
            Sine = 4,
            ReLu = 5,
            Gaussian = 6,
            Latch = 7,
            Differential = 8,
            Abs = 9,
        }

        // --------------------------------
        //  JSON Stuff
        // --------------------------------

        // Example:
        // {"Type":5,"TypeName":"ReLu","Index":41,"Inov":0,"Desc":"PhereOut1",
        //  "Value":0.0,"LastInput":0.0,"LastOutput":0.0}

        private const string JSON_FORMAT =
            "{" +
            "\"Type\":{0}," +
            "\"TypeName\":\"{1}\"," +
            "\"Index\":{2}," +
            "\"Inov\":0," +
            "\"Desc\":\"{3}\"," +
            "\"Value\":\"{4}\"," +
            "\"LastInput\":{5}," +
            "\"LastOutput\":{6}" +
            "}";

        public Neuron(string json, int startIndex) : base(json, startIndex)
        {
            Type = (Neuron.Types)Enum.Parse(typeof(Neuron.Types),getNextValue());
            Index = int.Parse(getNextValue());
            inov = getNextValue();
            Description = getNextValue();
            value = getNextValue();
            lastInput = getNextValue();
            lastOutput = getNextValue();
            CleanupJson();
        }

        public override string ToJson()
        {
            return string.Format(JSON_FORMAT,
                (int)Type,
                Type.ToString(),
                Index,
                inov,
                Description,
                value,
                lastInput,
                lastOutput);
        }
    }
    public class InvalidDescriptionException : Exception {
        public InvalidDescriptionException() : base() { }
        public InvalidDescriptionException(string? message) : base(message) { }
        public InvalidDescriptionException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
