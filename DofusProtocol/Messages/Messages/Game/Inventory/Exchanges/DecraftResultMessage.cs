namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DecraftResultMessage : Message
    {
        public const uint Id = 6569;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DecraftedItemStackInfo[] Results { get; set; }

        public DecraftResultMessage(DecraftedItemStackInfo[] results)
        {
            this.Results = results;
        }

        public DecraftResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Results.Count());
            for (var resultsIndex = 0; resultsIndex < Results.Count(); resultsIndex++)
            {
                var objectToSend = Results[resultsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var resultsCount = reader.ReadUShort();
            Results = new DecraftedItemStackInfo[resultsCount];
            for (var resultsIndex = 0; resultsIndex < resultsCount; resultsIndex++)
            {
                var objectToAdd = new DecraftedItemStackInfo();
                objectToAdd.Deserialize(reader);
                Results[resultsIndex] = objectToAdd;
            }
        }

    }
}
