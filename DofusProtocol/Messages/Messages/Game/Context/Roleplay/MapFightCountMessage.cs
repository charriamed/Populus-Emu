namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapFightCountMessage : Message
    {
        public const uint Id = 210;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightCount { get; set; }

        public MapFightCountMessage(ushort fightCount)
        {
            this.FightCount = fightCount;
        }

        public MapFightCountMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightCount = reader.ReadVarUShort();
        }

    }
}
