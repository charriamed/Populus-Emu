namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class NpcDialogQuestionMessage : Message
    {
        public const uint Id = 5617;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint @messageId { get; set; }
        public string[] DialogParams { get; set; }
        public uint[] VisibleReplies { get; set; }

        public NpcDialogQuestionMessage(uint @messageId, string[] dialogParams, uint[] visibleReplies)
        {
            this.@messageId = @messageId;
            this.DialogParams = dialogParams;
            this.VisibleReplies = visibleReplies;
        }

        public NpcDialogQuestionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(@messageId);
            writer.WriteShort((short)DialogParams.Count());
            for (var dialogParamsIndex = 0; dialogParamsIndex < DialogParams.Count(); dialogParamsIndex++)
            {
                writer.WriteUTF(DialogParams[dialogParamsIndex]);
            }
            writer.WriteShort((short)VisibleReplies.Count());
            for (var visibleRepliesIndex = 0; visibleRepliesIndex < VisibleReplies.Count(); visibleRepliesIndex++)
            {
                writer.WriteVarUInt(VisibleReplies[visibleRepliesIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            @messageId = reader.ReadVarUInt();
            var dialogParamsCount = reader.ReadUShort();
            DialogParams = new string[dialogParamsCount];
            for (var dialogParamsIndex = 0; dialogParamsIndex < dialogParamsCount; dialogParamsIndex++)
            {
                DialogParams[dialogParamsIndex] = reader.ReadUTF();
            }
            var visibleRepliesCount = reader.ReadUShort();
            VisibleReplies = new uint[visibleRepliesCount];
            for (var visibleRepliesIndex = 0; visibleRepliesIndex < visibleRepliesCount; visibleRepliesIndex++)
            {
                VisibleReplies[visibleRepliesIndex] = reader.ReadVarUInt();
            }
        }

    }
}
