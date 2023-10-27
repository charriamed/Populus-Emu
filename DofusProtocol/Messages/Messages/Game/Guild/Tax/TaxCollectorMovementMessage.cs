namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorMovementMessage : Message
    {
        public const uint Id = 5633;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte MovementType { get; set; }
        public TaxCollectorBasicInformations BasicInfos { get; set; }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }

        public TaxCollectorMovementMessage(sbyte movementType, TaxCollectorBasicInformations basicInfos, ulong playerId, string playerName)
        {
            this.MovementType = movementType;
            this.BasicInfos = basicInfos;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
        }

        public TaxCollectorMovementMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(MovementType);
            BasicInfos.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
        }

        public override void Deserialize(IDataReader reader)
        {
            MovementType = reader.ReadSByte();
            BasicInfos = new TaxCollectorBasicInformations();
            BasicInfos.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
        }

    }
}
