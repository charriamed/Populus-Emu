namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EntityMovementInformations
    {
        public const short Id  = 63;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int ObjectId { get; set; }
        public sbyte[] Steps { get; set; }

        public EntityMovementInformations(int objectId, sbyte[] steps)
        {
            this.ObjectId = objectId;
            this.Steps = steps;
        }

        public EntityMovementInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ObjectId);
            writer.WriteShort((short)Steps.Count());
            for (var stepsIndex = 0; stepsIndex < Steps.Count(); stepsIndex++)
            {
                writer.WriteSByte(Steps[stepsIndex]);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadInt();
            var stepsCount = reader.ReadUShort();
            Steps = new sbyte[stepsCount];
            for (var stepsIndex = 0; stepsIndex < stepsCount; stepsIndex++)
            {
                Steps[stepsIndex] = reader.ReadSByte();
            }
        }

    }
}
