namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AccountHouseMessage : Message
    {
        public const uint Id = 6315;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AccountHouseInformations[] Houses { get; set; }

        public AccountHouseMessage(AccountHouseInformations[] houses)
        {
            this.Houses = houses;
        }

        public AccountHouseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Houses.Count());
            for (var housesIndex = 0; housesIndex < Houses.Count(); housesIndex++)
            {
                var objectToSend = Houses[housesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var housesCount = reader.ReadUShort();
            Houses = new AccountHouseInformations[housesCount];
            for (var housesIndex = 0; housesIndex < housesCount; housesIndex++)
            {
                var objectToAdd = new AccountHouseInformations();
                objectToAdd.Deserialize(reader);
                Houses[housesIndex] = objectToAdd;
            }
        }

    }
}
