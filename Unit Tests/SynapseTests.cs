using Bibitinator.Models.Bibites.Brain;

namespace TestProject1
{
    public class SynapseTests
    {
        [Test]
        public void ConstructorFromToStrength_diff1_Works()
        {
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "ABC");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.Input, "ABC");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 1f);
            Assert.Multiple(() =>
            {
                Assert.That(synapse.From, Is.EqualTo(neuron1));
                Assert.That(synapse.To, Is.EqualTo(neuron2));
                Assert.That(synapse.Strength, Is.EqualTo(1f));
            });
        }

        [Test]
        public void ConstructorFromToStrength_strength12_strength10()
        {
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "ABC");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.Input, "ABC");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 12f);
            Assert.That(synapse.Strength, Is.EqualTo(10f));
        }

        [Test]
        public void ConstructorFromToStrength_strengthNeg12_strengthNeg10()
        {
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "ABC");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.Input, "ABC");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, -12f);
            Assert.That(synapse.Strength, Is.EqualTo(-10f));
        }

        [Test]
        public void ConstructorFromToStrength_same1_SameNeuronException()
        {
            try
            {
                BaseNeuron neuron = new TestNeuron(1, NeuronType.Input, "ABC");
                BaseSynapse Synapse = new TestSynapse(neuron, neuron, 1f);
                Assert.Fail();
            }
            catch (SameNeuronException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void ToString_Works()
        {
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "ABC");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.TanH, "DEF");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 1.234f);
            string toString = synapse.ToString();
            Assert.That(synapse.ToString(), Is.EqualTo("ABC : Input --(x1.23)--> DEF : TanH"));
        }

    }

    internal class TestSynapse : BaseSynapse
    {
        public TestSynapse() : base() { }
        public TestSynapse(BaseNeuron from, BaseNeuron to, float strength)
            : base(from, to, strength) { }

        public override string GetSave() { return null; }
    }
}
