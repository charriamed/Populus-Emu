namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EnterHavenBagRequestMessage : Message
    {
        public const uint Id = 6636;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong HavenBagOwner { get; set; }

        public EnterHavenBagRequestMessage(ulong havenBagOwner)
        {
            this.HavenBagOwner = havenBagOwner;
        }

        public EnterHavenBagRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(HavenBagOwner);
        }

        public override void Deserialize(IDataReader reader)
        {
            HavenBagOwner = reader.ReadVarULong();
        }

    }
}
