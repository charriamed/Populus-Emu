namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PlayerStatusExtended : PlayerStatus
    {
        public new const short Id = 414;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Message { get; set; }

        public PlayerStatusExtended(sbyte statusId, string message)
        {
            this.StatusId = statusId;
            this.Message = message;
        }

        public PlayerStatusExtended() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Message);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Message = reader.ReadUTF();
        }

    }
}
