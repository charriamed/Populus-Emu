namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CinematicMessage : Message
    {
        public const uint Id = 6053;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CinematicId { get; set; }

        public CinematicMessage(ushort cinematicId)
        {
            this.CinematicId = cinematicId;
        }

        public CinematicMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CinematicId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CinematicId = reader.ReadVarUShort();
        }

    }
}
