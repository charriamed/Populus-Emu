namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayShowActorWithEventMessage : GameRolePlayShowActorMessage
    {
        public new const uint Id = 6407;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ActorEventId { get; set; }

        public GameRolePlayShowActorWithEventMessage(GameRolePlayActorInformations informations, sbyte actorEventId)
        {
            this.Informations = informations;
            this.ActorEventId = actorEventId;
        }

        public GameRolePlayShowActorWithEventMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(ActorEventId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ActorEventId = reader.ReadSByte();
        }

    }
}
