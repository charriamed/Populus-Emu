﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Fights.Teams
{
    public class FightPlayerTeam : FightTeamWithLeader<CharacterFighter>
    {
        public FightPlayerTeam(TeamEnum id, Cell[] placementCells) : base(id, placementCells)
        {
        }

        public FightPlayerTeam(TeamEnum id, Cell[] placementCells, AlignmentSideEnum alignmentSide)
            : base(id, placementCells, alignmentSide)
        {
        }

        public override TeamTypeEnum TeamType
        {
            get { return TeamTypeEnum.TEAM_TYPE_PLAYER; }
        }

        public override FighterRefusedReasonEnum CanJoin(Character character)
        {
            if (IsRestrictedToParty)
            {
                if (!Leader.Character.IsInParty(PartyTypeEnum.PARTY_TYPE_CLASSICAL) || !Leader.Character.Party.IsInGroup(character))
                    return FighterRefusedReasonEnum.TEAM_LIMITED_BY_MAINCHARACTER;
            }

            if(this == Fight.DefendersTeam)
            {
                if(Fight.Map.GetBlueFightPlacement().Count() < this.Fighters.Count() + 1) return FighterRefusedReasonEnum.TEAM_LIMITED_BY_MAINCHARACTER;
            }
            else
            {
                if (Fight.Map.GetRedFightPlacement().Count() < this.Fighters.Count() + 1) return FighterRefusedReasonEnum.TEAM_LIMITED_BY_MAINCHARACTER;
            }

            if (Fight.IsMultiAccountRestricted && Fighters.Where(x => x is CharacterFighter).Any(x => (x as CharacterFighter).Character.Client.IP == character.Client.IP))
                return FighterRefusedReasonEnum.MULTIACCOUNT_NOT_ALLOWED;

            return base.CanJoin(character);
        }
    }
}