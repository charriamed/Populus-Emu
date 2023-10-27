namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class RefreshFollowedQuestsOrderRequestMessage : Message
    {
        public const uint Id = 6722;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Quests { get; set; }

        public RefreshFollowedQuestsOrderRequestMessage(ushort[] quests)
        {
            this.Quests = quests;
        }

        public RefreshFollowedQuestsOrderRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Quests.Count());
            for (var questsIndex = 0; questsIndex < Quests.Count(); questsIndex++)
            {
                writer.WriteVarUShort(Quests[questsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var questsCount = reader.ReadUShort();
            Quests = new ushort[questsCount];
            for (var questsIndex = 0; questsIndex < questsCount; questsIndex++)
            {
                Quests[questsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
