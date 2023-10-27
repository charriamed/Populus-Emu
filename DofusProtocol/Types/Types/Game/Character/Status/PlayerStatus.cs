namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PlayerStatus
    {
        public const short Id  = 415;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte StatusId { get; set; }

        public PlayerStatus(sbyte statusId)
        {
            this.StatusId = statusId;
        }

        public PlayerStatus() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(StatusId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            StatusId = reader.ReadSByte();
        }

    }
}
