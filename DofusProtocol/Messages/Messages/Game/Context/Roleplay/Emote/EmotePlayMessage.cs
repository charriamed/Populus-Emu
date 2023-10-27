namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EmotePlayMessage : EmotePlayAbstractMessage
    {
        public new const uint Id = 5683;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ActorId { get; set; }
        public int AccountId { get; set; }

        public EmotePlayMessage(byte emoteId, double emoteStartTime, double actorId, int accountId)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = emoteStartTime;
            this.ActorId = actorId;
            this.AccountId = accountId;
        }

        public EmotePlayMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(ActorId);
            writer.WriteInt(AccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ActorId = reader.ReadDouble();
            AccountId = reader.ReadInt();
        }

    }
}
