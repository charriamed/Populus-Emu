namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectDice : ObjectEffect
    {
        public new const short Id = 73;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint DiceNum { get; set; }
        public uint DiceSide { get; set; }
        public uint DiceConst { get; set; }

        public ObjectEffectDice(ushort actionId, uint diceNum, uint diceSide, uint diceConst)
        {
            this.ActionId = actionId;
            this.DiceNum = diceNum;
            this.DiceSide = diceSide;
            this.DiceConst = diceConst;
        }

        public ObjectEffectDice() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(DiceNum);
            writer.WriteVarUInt(DiceSide);
            writer.WriteVarUInt(DiceConst);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DiceNum = reader.ReadVarUInt();
            DiceSide = reader.ReadVarUInt();
            DiceConst = reader.ReadVarUInt();
        }

    }
}
