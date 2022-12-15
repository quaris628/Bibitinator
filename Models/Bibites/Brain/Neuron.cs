using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class Neuron
    {
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

        private static int idCount = 0;

        public int Index { get; }
        public Types Type { get; set; }
        public string Description { get; set; } // more like a name

        public Neuron(Types type, string? description)
        {
            if (description == null) { description = "Hidden" + Index; }
            else if (!description.All(char.IsLetterOrDigit)) { throw new InvalidDescException(); }
            // TODO check if a neuron of that name already exists?

            Index = idCount++;
            Type = type;
            Description = description;
        }

        public override string ToString()
        {
            // TODO test if Type.ToString() returns what I think it does
            return Description + Type.ToString();
        }
    }
    public class InvalidDescException : Exception { }
}
