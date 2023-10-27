namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyIdol : Idol
    {
        public new const short Id = 490;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong[] OwnersIds { get; set; }

        public PartyIdol(ushort objectId, ushort xpBonusPercent, ushort dropBonusPercent, ulong[] ownersIds)
        {
            this.ObjectId = objectId;
            this.XpBonusPercent = xpBonusPercent;
            this.DropBonusPercent = dropBonusPercent;
            this.OwnersIds = ownersIds;
        }

        public PartyIdol() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)OwnersIds.Count());
            for (var ownersIdsIndex = 0; ownersIdsIndex < OwnersIds.Count(); ownersIdsIndex++)
            {
                writer.WriteVarULong(OwnersIds[ownersIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var ownersIdsCount = reader.ReadUShort();
            OwnersIds = new ulong[ownersIdsCount];
            for (var ownersIdsIndex = 0; ownersIdsIndex < ownersIdsCount; ownersIdsIndex++)
            {
                OwnersIds[ownersIdsIndex] = reader.ReadVarULong();
            }
        }

    }
}
