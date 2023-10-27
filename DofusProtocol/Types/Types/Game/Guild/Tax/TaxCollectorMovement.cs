namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorMovement
    {
        public const short Id  = 493;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte MovementType { get; set; }
        public TaxCollectorBasicInformations BasicInfos { get; set; }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }

        public TaxCollectorMovement(sbyte movementType, TaxCollectorBasicInformations basicInfos, ulong playerId, string playerName)
        {
            this.MovementType = movementType;
            this.BasicInfos = basicInfos;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
        }

        public TaxCollectorMovement() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(MovementType);
            BasicInfos.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MovementType = reader.ReadSByte();
            BasicInfos = new TaxCollectorBasicInformations();
            BasicInfos.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
        }

    }
}
