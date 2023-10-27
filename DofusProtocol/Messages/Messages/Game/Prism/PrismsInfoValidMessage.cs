namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PrismsInfoValidMessage : Message
    {
        public const uint Id = 6451;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PrismFightersInformation[] Fights { get; set; }

        public PrismsInfoValidMessage(PrismFightersInformation[] fights)
        {
            this.Fights = fights;
        }

        public PrismsInfoValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Fights.Count());
            for (var fightsIndex = 0; fightsIndex < Fights.Count(); fightsIndex++)
            {
                var objectToSend = Fights[fightsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var fightsCount = reader.ReadUShort();
            Fights = new PrismFightersInformation[fightsCount];
            for (var fightsIndex = 0; fightsIndex < fightsCount; fightsIndex++)
            {
                var objectToAdd = new PrismFightersInformation();
                objectToAdd.Deserialize(reader);
                Fights[fightsIndex] = objectToAdd;
            }
        }

    }
}
