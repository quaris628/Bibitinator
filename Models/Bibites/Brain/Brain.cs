using Bibitinator.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class Brain : Jsonizable
    {
        public LinkedList<Neuron> Neurons { get; }
        private Dictionary<int, Neuron> neuronsIndex;
        public LinkedList<Synapse> Synapses { get; }
        private Dictionary<(int, int), Synapse> synapsesIndex;

        // unused for now
        private string isReady;
        private string parent;

        public Brain() {
            Neurons = new LinkedList<Neuron>();
            Synapses = new LinkedList<Synapse>();
            neuronsIndex = new Dictionary<int, Neuron>();
            synapsesIndex = new Dictionary<(int, int), Synapse>();
        }

        public void Add(Neuron neuron)
        {
            if (neuronsIndex.ContainsKey(neuron.Index))
            {
                throw new ContainsDuplicateException(
                    "This brain already has a neuron with the same index" +
                    " (" + neuron.Index + ")");
            }

            Neurons.AddLast(neuron);
            neuronsIndex.Add(neuron.Index, neuron);
        }

        public void Add(Synapse synapse)
        {
            (int, int) key = (synapse.From.Index, synapse.To.Index);
            if (synapsesIndex.ContainsKey(key))
            {
                throw new ContainsDuplicateException(
                    "This brain already has a synapse that connects the same neurons " +
                    " ('" + synapse.From + "' to '" + synapse.To + "')");
            }

            Synapses.AddLast(synapse);
            synapsesIndex.Add(key, synapse);
        }

        public Neuron GetNeuron(int index)
        {
            return neuronsIndex[index];
        }

        public Synapse GetSynapse(Neuron from, Neuron to)
        {
            return GetSynapse(from.Index, to.Index);
        }

        public Synapse GetSynapse(int fromIndex, int toIndex)
        {
            return synapsesIndex[(fromIndex, toIndex)];
        }

        // --------------------------------
        //  JSON Stuff
        // --------------------------------

        // Example:
        // {"isReady":true,"parent":true,
        //  "Nodes":[{Neuron},{Neuron},...{Neuron}],
        //  "Synapses":[{Synapse},{Synapse},...{Synapse}]}

        private const string JSON_FORMAT =
            "{" +
            "\"isReady\":{0}," +
            "\"parent\":\"{1}\"," +
            "\"Nodes\":[{2}]," +
            "\"Synapses\":[{3}]" +
            "}";

        public Brain(string json, int startIndex) : base(json, startIndex)
        {
            isReady = getNextValue();
            parent = getNextValue();
            CleanupJson();

            // parse neurons
            int neuronsEndIndex = parseJsonArray(json, startIndex, (json, neuronStartIndex) =>
            {
                Neuron neuron = new Neuron(json, neuronStartIndex);
                Add(neuron);
            });
            // parse synapses
            parseJsonArray(json, neuronsEndIndex, (json, synapseStartIndex) =>
            {
                Synapse synapse = new Synapse(json, synapseStartIndex, this);
                Add(synapse);
            });
        }

        private int parseJsonArray(string json, int startIndex, Action<string, int> eachItem)
        {
            int arrayStartIndex = json.IndexOf('[', startIndex);
            int arrayEndIndex = json.IndexOf(']', arrayStartIndex);
            int itemStartIndex = json.IndexOf('{', arrayStartIndex);
            while (0 < itemStartIndex && itemStartIndex < arrayEndIndex)
            {
                eachItem.Invoke(json, itemStartIndex);
                itemStartIndex = json.IndexOf('{', itemStartIndex + 1);
            }
            return arrayEndIndex;
        }

        public override string ToJson()
        {
            return string.Format(JSON_FORMAT,
                isReady,
                parent,
                neuronsToJson(),
                synapsesToJson());
        }

        // these ToJson functions could probably be refactored to be less duplicately
        // but I'm too lazy right now and the smelliness isn't very strong anyway
        private string neuronsToJson()
        {
            string[] neuronJsons = new string[Neurons.Count];
            int i = 0;
            foreach (Neuron neuron in Neurons)
            {
                neuronJsons[i++] = neuron.ToJson();
            }
            return string.Join(',',neuronJsons);
        }

        private string synapsesToJson()
        {
            string[] neuronJsons = new string[Synapses.Count];
            int i = 0;
            foreach (Synapse synapse in Synapses)
            {
                neuronJsons[i++] = synapse.ToJson();
            }
            return string.Join(',', neuronJsons);
        }
    }

    public class ContainsDuplicateException : Exception {
        public ContainsDuplicateException() : base() { }
        public ContainsDuplicateException(string? message) : base(message) { }
        public ContainsDuplicateException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

}
