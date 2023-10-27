namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionEmote : HumanOption
    {
        public new const short Id = 407;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }
        public double EmoteStartTime { get; set; }

        public HumanOptionEmote(byte emoteId, double emoteStartTime)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = emoteStartTime;
        }

        public HumanOptionEmote() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(EmoteId);
            writer.WriteDouble(EmoteStartTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EmoteId = reader.ReadByte();
            EmoteStartTime = reader.ReadDouble();
        }

    }
}
