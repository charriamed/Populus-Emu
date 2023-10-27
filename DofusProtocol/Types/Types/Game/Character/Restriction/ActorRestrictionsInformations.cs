namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ActorRestrictionsInformations
    {
        public const short Id  = 204;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public bool CantBeAggressed { get; set; }
        public bool CantBeChallenged { get; set; }
        public bool CantTrade { get; set; }
        public bool CantBeAttackedByMutant { get; set; }
        public bool CantRun { get; set; }
        public bool ForceSlowWalk { get; set; }
        public bool CantMinimize { get; set; }
        public bool CantMove { get; set; }
        public bool CantAggress { get; set; }
        public bool CantChallenge { get; set; }
        public bool CantExchange { get; set; }
        public bool CantAttack { get; set; }
        public bool CantChat { get; set; }
        public bool CantBeMerchant { get; set; }
        public bool CantUseObject { get; set; }
        public bool CantUseTaxCollector { get; set; }
        public bool CantUseInteractive { get; set; }
        public bool CantSpeakToNPC { get; set; }
        public bool CantChangeZone { get; set; }
        public bool CantAttackMonster { get; set; }
        public bool CantWalk8Directions { get; set; }

        public ActorRestrictionsInformations(bool cantBeAggressed, bool cantBeChallenged, bool cantTrade, bool cantBeAttackedByMutant, bool cantRun, bool forceSlowWalk, bool cantMinimize, bool cantMove, bool cantAggress, bool cantChallenge, bool cantExchange, bool cantAttack, bool cantChat, bool cantBeMerchant, bool cantUseObject, bool cantUseTaxCollector, bool cantUseInteractive, bool cantSpeakToNPC, bool cantChangeZone, bool cantAttackMonster, bool cantWalk8Directions)
        {
            this.CantBeAggressed = cantBeAggressed;
            this.CantBeChallenged = cantBeChallenged;
            this.CantTrade = cantTrade;
            this.CantBeAttackedByMutant = cantBeAttackedByMutant;
            this.CantRun = cantRun;
            this.ForceSlowWalk = forceSlowWalk;
            this.CantMinimize = cantMinimize;
            this.CantMove = cantMove;
            this.CantAggress = cantAggress;
            this.CantChallenge = cantChallenge;
            this.CantExchange = cantExchange;
            this.CantAttack = cantAttack;
            this.CantChat = cantChat;
            this.CantBeMerchant = cantBeMerchant;
            this.CantUseObject = cantUseObject;
            this.CantUseTaxCollector = cantUseTaxCollector;
            this.CantUseInteractive = cantUseInteractive;
            this.CantSpeakToNPC = cantSpeakToNPC;
            this.CantChangeZone = cantChangeZone;
            this.CantAttackMonster = cantAttackMonster;
            this.CantWalk8Directions = cantWalk8Directions;
        }

        public ActorRestrictionsInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, CantBeAggressed);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CantBeChallenged);
            flag = BooleanByteWrapper.SetFlag(flag, 2, CantTrade);
            flag = BooleanByteWrapper.SetFlag(flag, 3, CantBeAttackedByMutant);
            flag = BooleanByteWrapper.SetFlag(flag, 4, CantRun);
            flag = BooleanByteWrapper.SetFlag(flag, 5, ForceSlowWalk);
            flag = BooleanByteWrapper.SetFlag(flag, 6, CantMinimize);
            flag = BooleanByteWrapper.SetFlag(flag, 7, CantMove);
            writer.WriteByte(flag);
            flag = BooleanByteWrapper.SetFlag(flag, 0, CantAggress);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CantChallenge);
            flag = BooleanByteWrapper.SetFlag(flag, 2, CantExchange);
            flag = BooleanByteWrapper.SetFlag(flag, 3, CantAttack);
            flag = BooleanByteWrapper.SetFlag(flag, 4, CantChat);
            flag = BooleanByteWrapper.SetFlag(flag, 5, CantBeMerchant);
            flag = BooleanByteWrapper.SetFlag(flag, 6, CantUseObject);
            flag = BooleanByteWrapper.SetFlag(flag, 7, CantUseTaxCollector);
            writer.WriteByte(flag);
            flag = BooleanByteWrapper.SetFlag(flag, 0, CantUseInteractive);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CantSpeakToNPC);
            flag = BooleanByteWrapper.SetFlag(flag, 2, CantChangeZone);
            flag = BooleanByteWrapper.SetFlag(flag, 3, CantAttackMonster);
            flag = BooleanByteWrapper.SetFlag(flag, 4, CantWalk8Directions);
            writer.WriteByte(flag);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            CantBeAggressed = BooleanByteWrapper.GetFlag(flag, 0);
            CantBeChallenged = BooleanByteWrapper.GetFlag(flag, 1);
            CantTrade = BooleanByteWrapper.GetFlag(flag, 2);
            CantBeAttackedByMutant = BooleanByteWrapper.GetFlag(flag, 3);
            CantRun = BooleanByteWrapper.GetFlag(flag, 4);
            ForceSlowWalk = BooleanByteWrapper.GetFlag(flag, 5);
            CantMinimize = BooleanByteWrapper.GetFlag(flag, 6);
            CantMove = BooleanByteWrapper.GetFlag(flag, 7);
            flag = reader.ReadByte();
            CantAggress = BooleanByteWrapper.GetFlag(flag, 0);
            CantChallenge = BooleanByteWrapper.GetFlag(flag, 1);
            CantExchange = BooleanByteWrapper.GetFlag(flag, 2);
            CantAttack = BooleanByteWrapper.GetFlag(flag, 3);
            CantChat = BooleanByteWrapper.GetFlag(flag, 4);
            CantBeMerchant = BooleanByteWrapper.GetFlag(flag, 5);
            CantUseObject = BooleanByteWrapper.GetFlag(flag, 6);
            CantUseTaxCollector = BooleanByteWrapper.GetFlag(flag, 7);
            flag = reader.ReadByte();
            CantUseInteractive = BooleanByteWrapper.GetFlag(flag, 0);
            CantSpeakToNPC = BooleanByteWrapper.GetFlag(flag, 1);
            CantChangeZone = BooleanByteWrapper.GetFlag(flag, 2);
            CantAttackMonster = BooleanByteWrapper.GetFlag(flag, 3);
            CantWalk8Directions = BooleanByteWrapper.GetFlag(flag, 4);
        }

    }
}
