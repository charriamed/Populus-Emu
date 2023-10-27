namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayCharacterInformations : GameRolePlayHumanoidInformations
    {
        public new const short Id = 36;
        public override short TypeId
        {
            get { return Id; }
        }
        public ActorAlignmentInformations AlignmentInfos { get; set; }

        public GameRolePlayCharacterInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name, HumanInformations humanoidInfo, int accountId, ActorAlignmentInformations alignmentInfos)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
            this.HumanoidInfo = humanoidInfo;
            this.AccountId = accountId;
            this.AlignmentInfos = alignmentInfos;
        }

        public GameRolePlayCharacterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AlignmentInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AlignmentInfos = new ActorAlignmentInformations();
            AlignmentInfos.Deserialize(reader);
        }

    }
}
