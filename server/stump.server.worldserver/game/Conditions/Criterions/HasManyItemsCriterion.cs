using System.Linq;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions
{
    public class HasManyItemsCriterion : Criterion
    {
        public const string Identifier = "POQ";
        public short Item { get; set; }
        public byte Quantity { get; set; }

        public override bool Eval(Character character)
        {
            var firstOrDefault = character.Inventory.Where(x => x.Template.Id == Item);
            var basePlayerItems = firstOrDefault as BasePlayerItem[] ?? firstOrDefault.ToArray();
            if (!basePlayerItems.Any())
                return false;
            var sum = basePlayerItems.Sum(x => x.Stack);
            return sum >= Quantity;
        }

        public override void Build()
        {
            var condition = Literal.Split('-');
            short item;
            byte quantity;
            if (!short.TryParse(condition[0], out item))
            {
                throw new System.Exception($"Cannot build HasManyItemsCriterion, {Literal} is not a valid item id");
            }
            if (!byte.TryParse(condition[1], out quantity))
            {
                throw new System.Exception($"Cannot build HasManyItemsCriterion, {Literal} is not a valid quantity");
            }
            Item = item;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return base.FormatToString("POQ");
        }
    }
}