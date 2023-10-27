namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightShowFighterRandomStaticPoseMessage : GameFightShowFighterMessage
    {
        public new const uint Id = 6218;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameFightShowFighterRandomStaticPoseMessage(GameFightFighterInformations informations)
        {
            this.Informations = informations;
        }

        public GameFightShowFighterRandomStaticPoseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
