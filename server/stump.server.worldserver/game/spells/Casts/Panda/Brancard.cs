using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.STRETCHER_9780)]
    public class Brancard : DefaultSpellCastHandler
    {
        public Brancard(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Handlers[0].Priority = 0;
                if (Caster.GetCarriedActor() != null)
                    Handlers[0].AddAffectedActor(Caster.GetCarriedActor());
                Handlers[1].Priority = 1;
                Handlers[2].Priority = 2;
                Handlers[2].EffectZone.Radius = (byte)Caster.Position.Point.DistanceTo(new MapPoint(TargetedCell));
                Handlers[3].Priority = 3;
                return true;
            }

            return false;
        }
    }
}