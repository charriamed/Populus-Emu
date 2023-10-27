namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionTitle : HumanOption
    {
        public new const short Id = 408;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort TitleId { get; set; }
        public string TitleParam { get; set; }

        public HumanOptionTitle(ushort titleId, string titleParam)
        {
            this.TitleId = titleId;
            this.TitleParam = titleParam;
        }

        public HumanOptionTitle() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(TitleId);
            writer.WriteUTF(TitleParam);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TitleId = reader.ReadVarUShort();
            TitleParam = reader.ReadUTF();
        }

    }
}
