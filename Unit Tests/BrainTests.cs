using Bibitinator.Models.Bibites.Brain;

namespace TestProject1
{
    public class BrainTests
    {
        [Test]
        public void Constructor_Works()
        {
            BaseBrain brain = new TestBrain();
            Assert.That(brain.Neurons.Count, Is.EqualTo(0));
            Assert.That(brain.Synapses.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddNeuron_Works()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron = new TestNeuron(0, NeuronType.Input, "A");
            brain.Add(neuron);
            Assert.That(brain.Neurons.Count, Is.EqualTo(1));
            Assert.That(brain.Synapses.Count, Is.EqualTo(0));
            Assert.That(brain.Neurons.First(), Is.EqualTo(neuron));
        }

        [Test]
        public void AddNeuron_DuplicateIndex_ContainsDuplicateException()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(0, NeuronType.Input, "B");
            brain.Add(neuron1);
            try
            {
                brain.Add(neuron2);
                Assert.Fail();
            }
            catch (ContainsDuplicateException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void AddNeuron_DuplicateDescription_ContainsDuplicateException()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.Input, "A");
            brain.Add(neuron1);
            try
            {
                brain.Add(neuron2);
                Assert.Fail();
            }
            catch (ContainsDuplicateException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void AddSynapse_Works()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.TanH, "B");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 1f);
            brain.Add(neuron1);
            brain.Add(neuron2);
            brain.Add(synapse);
            Assert.That(brain.Neurons.Count, Is.EqualTo(2));
            Assert.That(brain.Synapses.Count, Is.EqualTo(1));
            Assert.That(brain.Synapses.First(), Is.EqualTo(synapse));
        }

        [Test]
        public void AddSynapse_NoToNeuron_ElementNotFoundException()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.TanH, "B");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 1f);
            brain.Add(neuron2);
            
            try
            {
                brain.Add(synapse);
                Assert.Fail();
            }
            catch (ElementNotFoundException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void AddSynapse_NoFromNeuron_ElementNotFoundException()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.TanH, "B");
            BaseSynapse synapse = new TestSynapse(neuron1, neuron2, 1f);
            brain.Add(neuron1);

            try
            {
                brain.Add(synapse);
                Assert.Fail();
            }
            catch (ElementNotFoundException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void AddSynapse_SynapseAlreadyConnects_ContainsDuplicateException()
        {
            BaseBrain brain = new TestBrain();
            BaseNeuron neuron1 = new TestNeuron(0, NeuronType.Input, "A");
            BaseNeuron neuron2 = new TestNeuron(1, NeuronType.TanH, "B");
            BaseSynapse synapse1 = new TestSynapse(neuron1, neuron2, 1f);
            BaseSynapse synapse2 = new TestSynapse(neuron1, neuron2, -1f);
            brain.Add(neuron1);
            brain.Add(neuron2);
            brain.Add(synapse1);
            try
            {
                brain.Add(synapse2);
                Assert.Fail();
            }
            catch (ContainsDuplicateException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }



    }

    internal class TestBrain : BaseBrain
    {
        public TestBrain() : base() { }

        public override string GetSave() { return null; }
    }

}
