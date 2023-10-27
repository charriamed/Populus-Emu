namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StartupActionFinishedMessage : Message
    {
        public const uint Id = 1304;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }
        public bool AutomaticAction { get; set; }
        public int ActionId { get; set; }

        public StartupActionFinishedMessage(bool success, bool automaticAction, int actionId)
        {
            this.Success = success;
            this.AutomaticAction = automaticAction;
            this.ActionId = actionId;
        }

        public StartupActionFinishedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Success);
            flag = BooleanByteWrapper.SetFlag(flag, 1, AutomaticAction);
            writer.WriteByte(flag);
            writer.WriteInt(ActionId);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Success = BooleanByteWrapper.GetFlag(flag, 0);
            AutomaticAction = BooleanByteWrapper.GetFlag(flag, 1);
            ActionId = reader.ReadInt();
        }

    }
}
