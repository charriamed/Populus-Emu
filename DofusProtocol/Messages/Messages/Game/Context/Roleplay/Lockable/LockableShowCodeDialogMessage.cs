namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableShowCodeDialogMessage : Message
    {
        public const uint Id = 5740;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool ChangeOrUse { get; set; }
        public sbyte CodeSize { get; set; }

        public LockableShowCodeDialogMessage(bool changeOrUse, sbyte codeSize)
        {
            this.ChangeOrUse = changeOrUse;
            this.CodeSize = codeSize;
        }

        public LockableShowCodeDialogMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(ChangeOrUse);
            writer.WriteSByte(CodeSize);
        }

        public override void Deserialize(IDataReader reader)
        {
            ChangeOrUse = reader.ReadBoolean();
            CodeSize = reader.ReadSByte();
        }

    }
}
