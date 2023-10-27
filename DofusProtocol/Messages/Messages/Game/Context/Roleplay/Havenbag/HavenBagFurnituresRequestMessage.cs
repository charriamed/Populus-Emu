namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagFurnituresRequestMessage : Message
    {
        public const uint Id = 6637;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] CellIds { get; set; }
        public int[] FunitureIds { get; set; }
        public byte[] Orientations { get; set; }

        public HavenBagFurnituresRequestMessage(ushort[] cellIds, int[] funitureIds, byte[] orientations)
        {
            this.CellIds = cellIds;
            this.FunitureIds = funitureIds;
            this.Orientations = orientations;
        }

        public HavenBagFurnituresRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)CellIds.Count());
            for (var cellIdsIndex = 0; cellIdsIndex < CellIds.Count(); cellIdsIndex++)
            {
                writer.WriteVarUShort(CellIds[cellIdsIndex]);
            }
            writer.WriteShort((short)FunitureIds.Count());
            for (var funitureIdsIndex = 0; funitureIdsIndex < FunitureIds.Count(); funitureIdsIndex++)
            {
                writer.WriteInt(FunitureIds[funitureIdsIndex]);
            }
            writer.WriteShort((short)Orientations.Count());
            for (var orientationsIndex = 0; orientationsIndex < Orientations.Count(); orientationsIndex++)
            {
                writer.WriteByte(Orientations[orientationsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var cellIdsCount = reader.ReadUShort();
            CellIds = new ushort[cellIdsCount];
            for (var cellIdsIndex = 0; cellIdsIndex < cellIdsCount; cellIdsIndex++)
            {
                CellIds[cellIdsIndex] = reader.ReadVarUShort();
            }
            var funitureIdsCount = reader.ReadUShort();
            FunitureIds = new int[funitureIdsCount];
            for (var funitureIdsIndex = 0; funitureIdsIndex < funitureIdsCount; funitureIdsIndex++)
            {
                FunitureIds[funitureIdsIndex] = reader.ReadInt();
            }
            var orientationsCount = reader.ReadUShort();
            Orientations = new byte[orientationsCount];
            for (var orientationsIndex = 0; orientationsIndex < orientationsCount; orientationsIndex++)
            {
                Orientations[orientationsIndex] = reader.ReadByte();
            }
        }

    }
}
