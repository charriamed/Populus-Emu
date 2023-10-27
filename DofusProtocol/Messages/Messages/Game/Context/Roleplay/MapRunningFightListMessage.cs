namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapRunningFightListMessage : Message
    {
        public const uint Id = 5743;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FightExternalInformations[] Fights { get; set; }

        public MapRunningFightListMessage(FightExternalInformations[] fights)
        {
            this.Fights = fights;
        }

        public MapRunningFightListMessage() { }

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
            Fights = new FightExternalInformations[fightsCount];
            for (var fightsIndex = 0; fightsIndex < fightsCount; fightsIndex++)
            {
                var objectToAdd = new FightExternalInformations();
                objectToAdd.Deserialize(reader);
                Fights[fightsIndex] = objectToAdd;
            }
        }

    }
}
