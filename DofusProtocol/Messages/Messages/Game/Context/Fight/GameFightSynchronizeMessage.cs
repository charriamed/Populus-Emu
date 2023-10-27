namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightSynchronizeMessage : Message
    {
        public const uint Id = 5921;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameFightFighterInformations[] Fighters { get; set; }

        public GameFightSynchronizeMessage(GameFightFighterInformations[] fighters)
        {
            this.Fighters = fighters;
        }

        public GameFightSynchronizeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Fighters.Count());
            for (var fightersIndex = 0; fightersIndex < Fighters.Count(); fightersIndex++)
            {
                var objectToSend = Fighters[fightersIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var fightersCount = reader.ReadUShort();
            Fighters = new GameFightFighterInformations[fightersCount];
            for (var fightersIndex = 0; fightersIndex < fightersCount; fightersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Fighters[fightersIndex] = objectToAdd;
            }
        }

    }
}
