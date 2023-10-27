namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SetCharacterRestrictionsMessage : Message
    {
        public const uint Id = 170;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ActorId { get; set; }
        public ActorRestrictionsInformations Restrictions { get; set; }

        public SetCharacterRestrictionsMessage(double actorId, ActorRestrictionsInformations restrictions)
        {
            this.ActorId = actorId;
            this.Restrictions = restrictions;
        }

        public SetCharacterRestrictionsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ActorId);
            Restrictions.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ActorId = reader.ReadDouble();
            Restrictions = new ActorRestrictionsInformations();
            Restrictions.Deserialize(reader);
        }

    }
}
