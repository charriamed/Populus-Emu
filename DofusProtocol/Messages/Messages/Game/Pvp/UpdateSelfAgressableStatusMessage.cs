namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateSelfAgressableStatusMessage : Message
    {
        public const uint Id = 6456;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Status { get; set; }
        public int ProbationTime { get; set; }

        public UpdateSelfAgressableStatusMessage(sbyte status, int probationTime)
        {
            this.Status = status;
            this.ProbationTime = probationTime;
        }

        public UpdateSelfAgressableStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Status);
            writer.WriteInt(ProbationTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            Status = reader.ReadSByte();
            ProbationTime = reader.ReadInt();
        }

    }
}
