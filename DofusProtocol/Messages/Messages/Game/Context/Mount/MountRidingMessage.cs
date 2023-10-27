namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountRidingMessage : Message
    {
        public const uint Id = 5967;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsRiding { get; set; }
        public bool IsAutopilot { get; set; }

        public MountRidingMessage(bool isRiding, bool isAutopilot)
        {
            this.IsRiding = isRiding;
            this.IsAutopilot = isAutopilot;
        }

        public MountRidingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, IsRiding);
            flag = BooleanByteWrapper.SetFlag(flag, 1, IsAutopilot);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            IsRiding = BooleanByteWrapper.GetFlag(flag, 0);
            IsAutopilot = BooleanByteWrapper.GetFlag(flag, 1);
        }

    }
}
