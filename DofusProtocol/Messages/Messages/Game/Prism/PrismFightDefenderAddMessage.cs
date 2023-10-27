namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightDefenderAddMessage : Message
    {
        public const uint Id = 5895;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public ushort FightId { get; set; }
        public CharacterMinimalPlusLookInformations Defender { get; set; }

        public PrismFightDefenderAddMessage(ushort subAreaId, ushort fightId, CharacterMinimalPlusLookInformations defender)
        {
            this.SubAreaId = subAreaId;
            this.FightId = fightId;
            this.Defender = defender;
        }

        public PrismFightDefenderAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarUShort(FightId);
            writer.WriteShort(Defender.TypeId);
            Defender.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            FightId = reader.ReadVarUShort();
            Defender = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
            Defender.Deserialize(reader);
        }

    }
}
