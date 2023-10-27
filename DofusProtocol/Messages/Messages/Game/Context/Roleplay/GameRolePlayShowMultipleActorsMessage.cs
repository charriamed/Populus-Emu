namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayShowMultipleActorsMessage : Message
    {
        public const uint Id = 6712;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameRolePlayActorInformations[] InformationsList { get; set; }

        public GameRolePlayShowMultipleActorsMessage(GameRolePlayActorInformations[] informationsList)
        {
            this.InformationsList = informationsList;
        }

        public GameRolePlayShowMultipleActorsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)InformationsList.Count());
            for (var informationsListIndex = 0; informationsListIndex < InformationsList.Count(); informationsListIndex++)
            {
                var objectToSend = InformationsList[informationsListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var informationsListCount = reader.ReadUShort();
            InformationsList = new GameRolePlayActorInformations[informationsListCount];
            for (var informationsListIndex = 0; informationsListIndex < informationsListCount; informationsListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                InformationsList[informationsListIndex] = objectToAdd;
            }
        }

    }
}
