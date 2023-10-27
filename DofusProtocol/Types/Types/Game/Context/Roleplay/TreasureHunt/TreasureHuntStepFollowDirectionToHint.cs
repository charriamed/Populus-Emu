namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntStepFollowDirectionToHint : TreasureHuntStep
    {
        public new const short Id = 472;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Direction { get; set; }
        public ushort NpcId { get; set; }

        public TreasureHuntStepFollowDirectionToHint(sbyte direction, ushort npcId)
        {
            this.Direction = direction;
            this.NpcId = npcId;
        }

        public TreasureHuntStepFollowDirectionToHint() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Direction);
            writer.WriteVarUShort(NpcId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Direction = reader.ReadSByte();
            NpcId = reader.ReadVarUShort();
        }

    }
}
