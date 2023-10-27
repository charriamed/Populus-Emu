namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkJobIndexMessage : Message
    {
        public const uint Id = 5819;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] Jobs { get; set; }

        public ExchangeStartOkJobIndexMessage(uint[] jobs)
        {
            this.Jobs = jobs;
        }

        public ExchangeStartOkJobIndexMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Jobs.Count());
            for (var jobsIndex = 0; jobsIndex < Jobs.Count(); jobsIndex++)
            {
                writer.WriteVarUInt(Jobs[jobsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var jobsCount = reader.ReadUShort();
            Jobs = new uint[jobsCount];
            for (var jobsIndex = 0; jobsIndex < jobsCount; jobsIndex++)
            {
                Jobs[jobsIndex] = reader.ReadVarUInt();
            }
        }

    }
}
