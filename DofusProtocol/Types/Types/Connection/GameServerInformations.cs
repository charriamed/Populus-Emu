namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameServerInformations
    {
        public const short Id  = 25;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public bool IsMonoAccount { get; set; }
        public bool IsSelectable { get; set; }
        public ushort ObjectId { get; set; }
        public sbyte Type { get; set; }
        public sbyte Status { get; set; }
        public sbyte Completion { get; set; }
        public sbyte CharactersCount { get; set; }
        public sbyte CharactersSlots { get; set; }
        public double Date { get; set; }

        public GameServerInformations(bool isMonoAccount, bool isSelectable, ushort objectId, sbyte type, sbyte status, sbyte completion, sbyte charactersCount, sbyte charactersSlots, double date)
        {
            this.IsMonoAccount = isMonoAccount;
            this.IsSelectable = isSelectable;
            this.ObjectId = objectId;
            this.Type = type;
            this.Status = status;
            this.Completion = completion;
            this.CharactersCount = charactersCount;
            this.CharactersSlots = charactersSlots;
            this.Date = date;
        }

        public GameServerInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, IsMonoAccount);
            flag = BooleanByteWrapper.SetFlag(flag, 1, IsSelectable);
            writer.WriteByte(flag);
            writer.WriteVarUShort(ObjectId);
            writer.WriteSByte(Type);
            writer.WriteSByte(Status);
            writer.WriteSByte(Completion);
            writer.WriteSByte(CharactersCount);
            writer.WriteSByte(CharactersSlots);
            writer.WriteDouble(Date);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            IsMonoAccount = BooleanByteWrapper.GetFlag(flag, 0);
            IsSelectable = BooleanByteWrapper.GetFlag(flag, 1);
            ObjectId = reader.ReadVarUShort();
            Type = reader.ReadSByte();
            Status = reader.ReadSByte();
            Completion = reader.ReadSByte();
            CharactersCount = reader.ReadSByte();
            CharactersSlots = reader.ReadSByte();
            Date = reader.ReadDouble();
        }

    }
}
