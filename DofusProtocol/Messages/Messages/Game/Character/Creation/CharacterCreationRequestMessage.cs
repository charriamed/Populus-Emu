﻿//Generated by Nexeption's ProtocolBuilder, on 28/10/2017 15:04:02
//For Aerticia Server AND STILL  USING IT in 2.51 26 April 2019 JAJAJAJAJAJA
//Full debugados jajajajaja
namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using System.Collections.Generic;
    using Stump.Core.IO;

    public class CharacterCreationRequestMessage : Message
    {
        public const uint Id = 160;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public IEnumerable<int> Colors { get; set; }
        public ushort CosmeticId { get; set; }

        public CharacterCreationRequestMessage(string name, sbyte breed, bool sex, IEnumerable<int> colors, ushort cosmeticId)
        {
            Name = name;
            Breed = breed;
            Sex = sex;
            Colors = colors;
            CosmeticId = cosmeticId;
        }

        public CharacterCreationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
            foreach (var entry in Colors)
            {
                writer.WriteInt(entry);
            }
            writer.WriteVarUShort(CosmeticId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
            var colors_ = new int[5];
            for (int i = 0; i < 5; i++)
            {
                colors_[i] = reader.ReadInt();
            }
            Colors = colors_;
            CosmeticId = reader.ReadVarUShort();
        }

    }
}
