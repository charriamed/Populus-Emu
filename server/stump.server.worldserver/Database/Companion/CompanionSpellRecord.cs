using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Companion
{
    [TableName("companion_spells")]
    public class CompanionSpellRecord
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }
        public int SpellId { get; set; }
        public int CompanionId { get; set; }
        public int GradeByLevel { get; set; } 
    }
}