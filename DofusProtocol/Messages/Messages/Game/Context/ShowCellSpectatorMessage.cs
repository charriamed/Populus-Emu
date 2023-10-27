namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShowCellSpectatorMessage : ShowCellMessage
    {
        public new const uint Id = 6158;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string PlayerName { get; set; }

        public ShowCellSpectatorMessage(double sourceId, ushort cellId, string playerName)
        {
            this.SourceId = sourceId;
            this.CellId = cellId;
            this.PlayerName = playerName;
        }

        public ShowCellSpectatorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(PlayerName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerName = reader.ReadUTF();
        }

    }
}
