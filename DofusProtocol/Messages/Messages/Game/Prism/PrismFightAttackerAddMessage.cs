namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightAttackerAddMessage : Message
    {
        public const uint Id = 5893;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public ushort FightId { get; set; }
        public CharacterMinimalPlusLookInformations Attacker { get; set; }

        public PrismFightAttackerAddMessage(ushort subAreaId, ushort fightId, CharacterMinimalPlusLookInformations attacker)
        {
            this.SubAreaId = subAreaId;
            this.FightId = fightId;
            this.Attacker = attacker;
        }

        public PrismFightAttackerAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarUShort(FightId);
            writer.WriteShort(Attacker.TypeId);
            Attacker.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            FightId = reader.ReadVarUShort();
            Attacker = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
            Attacker.Deserialize(reader);
        }

    }
}
