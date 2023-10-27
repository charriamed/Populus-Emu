namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
    {
        public new const short Id = 383;
        public override short TypeId
        {
            get { return Id; }
        }
        public GameRolePlayNpcQuestFlag QuestFlag { get; set; }

        public GameRolePlayNpcWithQuestInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, ushort npcId, bool sex, ushort specialArtworkId, GameRolePlayNpcQuestFlag questFlag)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.NpcId = npcId;
            this.Sex = sex;
            this.SpecialArtworkId = specialArtworkId;
            this.QuestFlag = questFlag;
        }

        public GameRolePlayNpcWithQuestInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            QuestFlag.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            QuestFlag = new GameRolePlayNpcQuestFlag();
            QuestFlag.Deserialize(reader);
        }

    }
}
