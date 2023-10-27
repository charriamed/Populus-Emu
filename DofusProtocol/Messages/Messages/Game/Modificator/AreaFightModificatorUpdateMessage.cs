namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AreaFightModificatorUpdateMessage : Message
    {
        public const uint Id = 6493;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int SpellPairId { get; set; }

        public AreaFightModificatorUpdateMessage(int spellPairId)
        {
            this.SpellPairId = spellPairId;
        }

        public AreaFightModificatorUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(SpellPairId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellPairId = reader.ReadInt();
        }

    }
}
