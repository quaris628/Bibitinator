using Bibitinator.Models.Bibites;
using Bibitinator.Models.Bibites.Brain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Controllers
{
    internal class FormsBibiteBrain : IBibiteBrain
    {
        public bool AddNeuron(Neuron neuron)
        {
            throw new NotImplementedException();
        }

        public bool AddSynapse(Neuron from, Neuron to, float strength)
        {
            throw new NotImplementedException();
        }

        public Collection<Neuron> GetNeurons()
        {
            throw new NotImplementedException();
        }

        public Collection<Synapse> GetSynapses()
        {
            throw new NotImplementedException();
        }

        public bool RemoveNeuron(Neuron neuron)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSynapse(Synapse synapse)
        {
            throw new NotImplementedException();
        }
    }
}
