namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectCreature : ObjectEffect
    {
        public new const short Id = 71;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort MonsterFamilyId { get; set; }

        public ObjectEffectCreature(ushort actionId, ushort monsterFamilyId)
        {
            this.ActionId = actionId;
            this.MonsterFamilyId = monsterFamilyId;
        }

        public ObjectEffectCreature() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(MonsterFamilyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MonsterFamilyId = reader.ReadVarUShort();
        }

    }
}
