namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AccountCapabilitiesMessage : Message
    {
        public const uint Id = 6216;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool TutorialAvailable { get; set; }
        public bool CanCreateNewCharacter { get; set; }
        public int AccountId { get; set; }
        public uint BreedsVisible { get; set; }
        public uint BreedsAvailable { get; set; }
        public sbyte Status { get; set; }
        public double UnlimitedRestatEndDate { get; set; }

        public AccountCapabilitiesMessage(bool tutorialAvailable, bool canCreateNewCharacter, int accountId, uint breedsVisible, uint breedsAvailable, sbyte status, double unlimitedRestatEndDate)
        {
            this.TutorialAvailable = tutorialAvailable;
            this.CanCreateNewCharacter = canCreateNewCharacter;
            this.AccountId = accountId;
            this.BreedsVisible = breedsVisible;
            this.BreedsAvailable = breedsAvailable;
            this.Status = status;
            this.UnlimitedRestatEndDate = unlimitedRestatEndDate;
        }

        public AccountCapabilitiesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, TutorialAvailable);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CanCreateNewCharacter);
            writer.WriteByte(flag);
            writer.WriteInt(AccountId);
            writer.WriteVarUInt(BreedsVisible);
            writer.WriteVarUInt(BreedsAvailable);
            writer.WriteSByte(Status);
            writer.WriteDouble(UnlimitedRestatEndDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            TutorialAvailable = BooleanByteWrapper.GetFlag(flag, 0);
            CanCreateNewCharacter = BooleanByteWrapper.GetFlag(flag, 1);
            AccountId = reader.ReadInt();
            BreedsVisible = reader.ReadVarUInt();
            BreedsAvailable = reader.ReadVarUInt();
            Status = reader.ReadSByte();
            UnlimitedRestatEndDate = reader.ReadDouble();
        }

    }
}
