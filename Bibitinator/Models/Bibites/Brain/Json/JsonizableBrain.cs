using Bibitinator.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain.Json
{
    public class JsonizableBrain : BaseBrain
    {
        // Example:
        // {
        //  "isReady":true,
        //  "parent":true,
        //  "Nodes":[{Neuron},{Neuron},...{Neuron}],
        //  "Synapses":[{Synapse},{Synapse},...{Synapse}]
        // }

        private const string JSON_FORMAT =
            "{" +
            "\"isReady\":{0}," +
            "\"parent\":\"{1}\"," +
            "\"Nodes\":[{2}]," +
            "\"Synapses\":[{3}]" +
            "}";

        // unused for now, but they're in the json so keep track of them just in case
        private string isReady;
        private string parent;

        public JsonizableBrain() : base()
        {
            isReady = "true";
            parent = "true";
        }

        public JsonizableBrain(string json, int startIndex) : base()
        {
            JsonParser parser = new JsonParser(json, startIndex);
            isReady = parser.getNextValue();
            parent = parser.getNextValue();

            // parse neurons
            parser.parseArray((json, neuronStartIndex) =>
            {
                BaseNeuron neuron = new JsonizableNeuron(json, neuronStartIndex);
                Add(neuron);
            });
            // parse synapses
            parser.parseArray((json, synapseStartIndex) =>
            {
                BaseSynapse synapse = new JsonizableSynapse(json, synapseStartIndex, this);
                Add(synapse);
            });
        }

        public override string GetSave()
        {
            return string.Format(JSON_FORMAT,
                isReady,
                parent,
                neuronsToJson(),
                synapsesToJson());
        }

        // these ToJson functions could probably be refactored to be less duplicately
        // but I'm too lazy right now and this code smell isn't very strong anyway
        private string neuronsToJson()
        {
            string[] neuronJsons = new string[Neurons.Count];
            int i = 0;
            foreach (JsonizableNeuron neuron in Neurons)
            {
                neuronJsons[i++] = neuron.GetSave();
            }
            return string.Join(',', neuronJsons);
        }

        private string synapsesToJson()
        {
            string[] neuronJsons = new string[Synapses.Count];
            int i = 0;
            foreach (JsonizableSynapse synapse in Synapses)
            {
                neuronJsons[i++] = synapse.GetSave();
            }
            return string.Join(',', neuronJsons);
        }
    }

}
