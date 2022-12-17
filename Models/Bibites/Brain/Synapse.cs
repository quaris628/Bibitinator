using Bibitinator.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class Synapse : Jsonizable
    {
        private static int idCount = 0;
        public int Index { get; set; }
        public Neuron From { get; set; }
        public Neuron To { get; set; }
        public float Strength { get; set; }

        // unused for now
        private string inov;
        private string En;
        private string en;

        public Synapse(Neuron from, Neuron to, float strength)
        {
            if (from == null || to == null) { throw new ArgumentNullException(); }
            if (from.Equals(to)) { throw new SameNeuronException(
                "Cannot create a synapse that connects a neuron to itself"); }
            strength = Math.Max(-10f, Math.Min(strength, 10));

            Index = idCount++;
            From = from;
            To = to;
            Strength = Math.Max(-10f, Math.Min(strength, 10));
        }

        public override string ToString()
        {
            return From.ToString() + " --(x" + Math.Round(Strength, 2) + ")--> " + To.ToString();
        }

        // --------------------------------
        //  JSON Stuff
        // --------------------------------

        // Example:
        // {"Inov":0,"NodeIn":0,"NodeOut":48,"Weight":1.0,"En":true,"en":true}

        private const string JSON_FORMAT =
            "{" +
            "\"Inov\":{0}," +
            "\"NodeIn\":{1}," +
            "\"NodeOut\":{2}," +
            "\"Weight\":{3}," +
            "\"En\":{4}," +
            "\"en\":{5}" +
            "}";

        public Synapse(string json, int startIndex, Brain brain)
            : base(json, startIndex)
        {
            inov = getNextValue();
            int fromIndex = int.Parse(getNextValue());
            int toIndex = int.Parse(getNextValue());
            Strength = float.Parse(getNextValue());
            En = getNextValue();
            en = getNextValue();
            CleanupJson();

            From = brain.GetNeuron(fromIndex);
            To = brain.GetNeuron(toIndex);
        }

        public override string ToJson()
        {
            return string.Format(JSON_FORMAT,
                inov,
                From.Index,
                To.Index,
                Strength,
                En,
                en);
        }
    }

    public class SameNeuronException : Exception {
        public SameNeuronException() : base() { }
        public SameNeuronException(string? message) : base(message) { }
        public SameNeuronException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
