namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MigratedServerListMessage : Message
    {
        public const uint Id = 6731;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] MigratedServerIds { get; set; }

        public MigratedServerListMessage(ushort[] migratedServerIds)
        {
            this.MigratedServerIds = migratedServerIds;
        }

        public MigratedServerListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)MigratedServerIds.Count());
            for (var migratedServerIdsIndex = 0; migratedServerIdsIndex < MigratedServerIds.Count(); migratedServerIdsIndex++)
            {
                writer.WriteVarUShort(MigratedServerIds[migratedServerIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var migratedServerIdsCount = reader.ReadUShort();
            MigratedServerIds = new ushort[migratedServerIdsCount];
            for (var migratedServerIdsIndex = 0; migratedServerIdsIndex < migratedServerIdsCount; migratedServerIdsIndex++)
            {
                MigratedServerIds[migratedServerIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
