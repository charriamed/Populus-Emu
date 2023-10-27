namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionOrnament : HumanOption
    {
        public new const short Id = 411;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort OrnamentId { get; set; }
        public ushort Level { get; set; }
        public short LeagueId { get; set; }
        public int LadderPosition { get; set; }

        public HumanOptionOrnament(ushort ornamentId, ushort level, short leagueId, int ladderPosition)
        {
            this.OrnamentId = ornamentId;
            this.Level = level;
            this.LeagueId = leagueId;
            this.LadderPosition = ladderPosition;
        }

        public HumanOptionOrnament() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(OrnamentId);
            writer.WriteVarUShort(Level);
            writer.WriteVarShort(LeagueId);
            writer.WriteInt(LadderPosition);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            OrnamentId = reader.ReadVarUShort();
            Level = reader.ReadVarUShort();
            LeagueId = reader.ReadVarShort();
            LadderPosition = reader.ReadInt();
        }

    }
}
