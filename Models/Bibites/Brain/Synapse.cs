using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class Synapse
    {
        private static int idCount = 0;
        public int Index { get; set; }
        public Neuron From { get; set; }
        public Neuron To { get; set; }
        public float Strength { get; set; }

        public Synapse(Neuron from, Neuron to, float strength)
        {
            if (from == null || to == null) { throw new ArgumentNullException(); }
            if (from.Equals(to)) { throw new SameNeuronException(); }
            strength = Math.Max(-10f, Math.Min(strength, 10));

            Index = idCount++;
            From = from;
            To = to;
            Strength = Math.Max(-10f, Math.Min(strength, 10));
        }

        public override string ToString()
        {
            return From.ToString() + " " + To.ToString() + " " + Math.Round(Strength,2);
        }
    }

    public class SameNeuronException : Exception { }
}
