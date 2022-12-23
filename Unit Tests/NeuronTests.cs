using Bibitinator.Models.Bibites.Brain;

namespace TestProject1
{
    public class NeuronTests
    {
        [Test]
        public void ConstructorIndexTypeDescription_0InputABC_Works()
        {
            BaseNeuron neuron = new TestNeuron(0, NeuronType.Input, "ABC");
            Assert.Multiple(() =>
            {
                Assert.That(neuron.Index, Is.EqualTo(0));
                Assert.That(neuron.Type, Is.EqualTo(NeuronType.Input));
                Assert.That(neuron.Description, Is.EqualTo("ABC"));
            });
        }

        [Test]
        public void ConstructorIndexTypeDescription_0InputAspaceC_InvalidDescriptionException()
        {
            try
            {
                BaseNeuron neuron = new TestNeuron(40, NeuronType.Input, "A C");
                Assert.Fail();
            }
            catch (InvalidDescriptionException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void ConstructorIndexTypeDescription_40InputABC_InputOutputConflictException()
        {
            try
            {
                BaseNeuron neuron = new TestNeuron(40, NeuronType.Input, "ABC");
                Assert.Fail();
            }
            catch (InputOutputConflictException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.Pass();
            }
        }

        [Test]
        public void IsInput_0Input_True()
        {
            BaseNeuron neuron = new TestNeuron(0, NeuronType.Input, "ABC");
            Assert.That(neuron.IsInput(), Is.True);
        }

        [Test]
        public void IsInput_0TanH_False()
        {
            BaseNeuron neuron = new TestNeuron(0, NeuronType.TanH, "ABC");
            Assert.That(neuron.IsInput(), Is.False);
        }

        [Test]
        public void IsOutput_32TanH_False()
        {
            BaseNeuron neuron = new TestNeuron(32, NeuronType.TanH, "ABC");
            Assert.That(neuron.IsOutput(), Is.False);
        }

        [Test]
        public void IsOutput_33TanH_True()
        {
            BaseNeuron neuron = new TestNeuron(33, NeuronType.TanH, "ABC");
            Assert.That(neuron.IsOutput(), Is.True);
        }

        [Test]
        public void IsOutput_47TanH_True()
        {
            BaseNeuron neuron = new TestNeuron(47, NeuronType.TanH, "ABC");
            Assert.That(neuron.IsOutput(), Is.True);
        }

        [Test]
        public void IsOutput_48TanH_False()
        {
            BaseNeuron neuron = new TestNeuron(48, NeuronType.TanH, "ABC");
            Assert.That(neuron.IsOutput(), Is.False);
        }

        [Test]
        public void ToString_48TanHABC_ABCcolonTanH()
        {
            BaseNeuron neuron = new TestNeuron(48, NeuronType.TanH, "ABC");
            Assert.That(neuron.ToString(), Is.EqualTo("ABC : TanH"));
        }

        [Test]
        public void GetDefaultDescription_48_Hidden48()
        {
            Assert.That(BaseNeuron.GetDefaultDescription(48), Is.EqualTo("Hidden48"));
        }

    }

    internal class TestNeuron : BaseNeuron
    {
        public TestNeuron() : base() { }
        public TestNeuron(int index, NeuronType type, string? description)
            : base(index, type, description) { }

        public override string GetSave() { return null; }
    }
}
