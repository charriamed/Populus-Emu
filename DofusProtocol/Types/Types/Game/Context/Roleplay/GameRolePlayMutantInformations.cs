namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations
    {
        public new const short Id = 3;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort MonsterId { get; set; }
        public sbyte PowerLevel { get; set; }

        public GameRolePlayMutantInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name, HumanInformations humanoidInfo, int accountId, ushort monsterId, sbyte powerLevel)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
            this.HumanoidInfo = humanoidInfo;
            this.AccountId = accountId;
            this.MonsterId = monsterId;
            this.PowerLevel = powerLevel;
        }

        public GameRolePlayMutantInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(MonsterId);
            writer.WriteSByte(PowerLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MonsterId = reader.ReadVarUShort();
            PowerLevel = reader.ReadSByte();
        }

    }
}
