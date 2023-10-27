namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MoodSmileyResultMessage : Message
    {
        public const uint Id = 6196;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ResultCode { get; set; }
        public ushort SmileyId { get; set; }

        public MoodSmileyResultMessage(sbyte resultCode, ushort smileyId)
        {
            this.ResultCode = resultCode;
            this.SmileyId = smileyId;
        }

        public MoodSmileyResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ResultCode);
            writer.WriteVarUShort(SmileyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ResultCode = reader.ReadSByte();
            SmileyId = reader.ReadVarUShort();
        }

    }
}
