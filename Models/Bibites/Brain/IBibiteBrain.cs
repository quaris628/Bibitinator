using Bibitinator.Models.Bibites.Brain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites
{
    public interface IBibiteBrain
    {
        Collection<Neuron> GetNeurons();
        Collection<Synapse> GetSynapses();
        bool AddNeuron(Neuron neuron);
        bool AddSynapse(Neuron from, Neuron to, float strength);
        bool RemoveNeuron(Neuron neuron);
        bool RemoveSynapse(Synapse synapse);
    }
}
