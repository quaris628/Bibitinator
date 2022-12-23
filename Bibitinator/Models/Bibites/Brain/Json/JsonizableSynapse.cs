using Bibitinator.Json;
using Bibitinator.Models.Bibites.Brain.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models.Bibites.Brain
{
    public class JsonizableSynapse : BaseSynapse
    {

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

        // unused for now
        private string inov;
        private string En;
        private string en;

        public JsonizableSynapse(JsonizableNeuron from, JsonizableNeuron to, float strength)
            : base(from, to, strength)
        {
            inov = "0";
            En = "true";
            en = "true";
        }

        public JsonizableSynapse(string json, int startIndex, JsonizableBrain brain)
            : base()
        {
            JsonParser parser = new JsonParser(json, startIndex);

            inov = parser.getNextValue();
            From = brain.GetNeuron(int.Parse(parser.getNextValue()));
            To = brain.GetNeuron(int.Parse(parser.getNextValue()));
            Strength = float.Parse(parser.getNextValue());
            En = parser.getNextValue();
            en = parser.getNextValue();
        }

        public override string GetSave()
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
}
