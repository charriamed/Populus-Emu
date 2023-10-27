namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachBudgetMessage : Message
    {
        public const uint Id = 6786;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Bugdet { get; set; }

        public BreachBudgetMessage(uint bugdet)
        {
            this.Bugdet = bugdet;
        }

        public BreachBudgetMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Bugdet);
        }

        public override void Deserialize(IDataReader reader)
        {
            Bugdet = reader.ReadVarUInt();
        }

    }
}
