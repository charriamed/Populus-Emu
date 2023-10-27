namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintanceInformation : AbstractContactInformations
    {
        public new const short Id = 561;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte PlayerState { get; set; }

        public AcquaintanceInformation(int accountId, string accountName, sbyte playerState)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerState = playerState;
        }

        public AcquaintanceInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PlayerState);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerState = reader.ReadSByte();
        }

    }
}
