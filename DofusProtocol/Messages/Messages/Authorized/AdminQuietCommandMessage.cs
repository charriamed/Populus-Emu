﻿namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AdminQuietCommandMessage : AdminCommandMessage
    {
        public new const uint Id = 5662;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AdminQuietCommandMessage(string content)
        {
            this.Content = content;
        }

        public AdminQuietCommandMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
