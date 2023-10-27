namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachBonusMessage : Message
    {
        public const uint Id = 6800;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectEffectInteger Bonus { get; set; }

        public BreachBonusMessage(ObjectEffectInteger bonus)
        {
            this.Bonus = bonus;
        }

        public BreachBonusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Bonus.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Bonus = new ObjectEffectInteger();
            Bonus.Deserialize(reader);
        }

    }
}
