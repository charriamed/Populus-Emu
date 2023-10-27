namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightSummonMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5825;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameFightFighterInformations[] Summons { get; set; }

        public GameActionFightSummonMessage(ushort actionId, double sourceId, GameFightFighterInformations[] summons)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.Summons = summons;
        }

        public GameActionFightSummonMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Summons.Count());
            for (var summonsIndex = 0; summonsIndex < Summons.Count(); summonsIndex++)
            {
                var objectToSend = Summons[summonsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var summonsCount = reader.ReadUShort();
            Summons = new GameFightFighterInformations[summonsCount];
            for (var summonsIndex = 0; summonsIndex < summonsCount; summonsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Summons[summonsIndex] = objectToAdd;
            }
        }

    }
}
