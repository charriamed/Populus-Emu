namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayShowChallengeMessage : Message
    {
        public const uint Id = 301;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FightCommonInformations CommonsInfos { get; set; }

        public GameRolePlayShowChallengeMessage(FightCommonInformations commonsInfos)
        {
            this.CommonsInfos = commonsInfos;
        }

        public GameRolePlayShowChallengeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            CommonsInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            CommonsInfos = new FightCommonInformations();
            CommonsInfos.Deserialize(reader);
        }

    }
}
