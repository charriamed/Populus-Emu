namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildEmblem
    {
        public const short Id  = 87;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SymbolShape { get; set; }
        public int SymbolColor { get; set; }
        public sbyte BackgroundShape { get; set; }
        public int BackgroundColor { get; set; }

        public GuildEmblem(ushort symbolShape, int symbolColor, sbyte backgroundShape, int backgroundColor)
        {
            this.SymbolShape = symbolShape;
            this.SymbolColor = symbolColor;
            this.BackgroundShape = backgroundShape;
            this.BackgroundColor = backgroundColor;
        }

        public GuildEmblem() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SymbolShape);
            writer.WriteInt(SymbolColor);
            writer.WriteSByte(BackgroundShape);
            writer.WriteInt(BackgroundColor);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SymbolShape = reader.ReadVarUShort();
            SymbolColor = reader.ReadInt();
            BackgroundShape = reader.ReadSByte();
            BackgroundColor = reader.ReadInt();
        }

    }
}
