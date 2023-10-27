namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionAlliance : HumanOption
    {
        public new const short Id = 425;
        public override short TypeId
        {
            get { return Id; }
        }
        public AllianceInformations AllianceInformations { get; set; }
        public sbyte Aggressable { get; set; }

        public HumanOptionAlliance(AllianceInformations allianceInformations, sbyte aggressable)
        {
            this.AllianceInformations = allianceInformations;
            this.Aggressable = aggressable;
        }

        public HumanOptionAlliance() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceInformations.Serialize(writer);
            writer.WriteSByte(Aggressable);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceInformations = new AllianceInformations();
            AllianceInformations.Deserialize(reader);
            Aggressable = reader.ReadSByte();
        }

    }
}
