namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagDailyLoteryMessage : Message
    {
        public const uint Id = 6644;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ReturnType { get; set; }
        public string GameActionId { get; set; }

        public HavenBagDailyLoteryMessage(sbyte returnType, string gameActionId)
        {
            this.ReturnType = returnType;
            this.GameActionId = gameActionId;
        }

        public HavenBagDailyLoteryMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ReturnType);
            writer.WriteUTF(GameActionId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ReturnType = reader.ReadSByte();
            GameActionId = reader.ReadUTF();
        }

    }
}
