namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectLadder : ObjectEffectCreature
    {
        public new const short Id = 81;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint MonsterCount { get; set; }

        public ObjectEffectLadder(ushort actionId, ushort monsterFamilyId, uint monsterCount)
        {
            this.ActionId = actionId;
            this.MonsterFamilyId = monsterFamilyId;
            this.MonsterCount = monsterCount;
        }

        public ObjectEffectLadder() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(MonsterCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MonsterCount = reader.ReadVarUInt();
        }

    }
}
