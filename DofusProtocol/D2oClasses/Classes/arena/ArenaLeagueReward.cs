using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("ArenaLeagueReward", "com.ankamagames.dofus.datacenter.arena")]
    [Serializable]
    public class ArenaLeagueReward : IDataObject, IIndexedData
    {
        public const String MODULE = "ArenaLeagueRewards";
        public int id;
        public uint seasonId;
        public uint leagueId;
        public List<uint> titlesRewards;
        public Boolean endSeasonRewards;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public uint SeasonId
        {
            get { return this.seasonId; }
            set { this.seasonId = value; }
        }
        [D2OIgnore]
        public uint LeagueId
        {
            get { return this.leagueId; }
            set { this.leagueId = value; }
        }
        [D2OIgnore]
        public List<uint> TitlesRewards
        {
            get { return this.titlesRewards; }
            set { this.titlesRewards = value; }
        }
        [D2OIgnore]
        public Boolean EndSeasonRewards
        {
            get { return this.endSeasonRewards; }
            set { this.endSeasonRewards = value; }
        }
    }
}
