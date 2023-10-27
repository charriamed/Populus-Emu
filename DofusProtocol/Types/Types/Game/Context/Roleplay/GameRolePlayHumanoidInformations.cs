namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayHumanoidInformations : GameRolePlayNamedActorInformations
    {
        public new const short Id = 159;
        public override short TypeId
        {
            get { return Id; }
        }
        public HumanInformations HumanoidInfo { get; set; }
        public int AccountId { get; set; }

        public GameRolePlayHumanoidInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name, HumanInformations humanoidInfo, int accountId)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
            this.HumanoidInfo = humanoidInfo;
            this.AccountId = accountId;
        }

        public GameRolePlayHumanoidInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(HumanoidInfo.TypeId);
            HumanoidInfo.Serialize(writer);
            writer.WriteInt(AccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            HumanoidInfo = ProtocolTypeManager.GetInstance<HumanInformations>(reader.ReadShort());
            HumanoidInfo.Deserialize(reader);
            AccountId = reader.ReadInt();
        }

    }
}
