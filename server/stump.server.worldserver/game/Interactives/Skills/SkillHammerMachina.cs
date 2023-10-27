using Stump.DofusProtocol.Enums;
using Stump.ORM.SubSonic.Extensions;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Visual;
using System;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("HammerMachina", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class SkillHammerMachina : CustomSkill
    {
        public SkillHammerMachina(int id, InteractiveCustomSkillRecord skillTemplate, InteractiveObject interactiveObject)
            : base(id, skillTemplate, interactiveObject)
        {
        }

        public override int StartExecute(Character character)
        {
            var miniGame = MiniGamesManager.Instance.GetCharacterMiniGames(character);

            if(miniGame != null)
            {
                if (miniGame.Hammer != null && miniGame.Hammer.Date == DateTime.Now.Date)
                {
                    character.OpenPopup("L'épreuve de force est disponible une fois par jour.<br>Retentez votre chance demain !");
                    return -1;
                }
            }            

            var cac = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);
            if (cac == null || cac.Template.TypeId != (int)ItemTypeEnum.MARTEAU)
            {
                character.OpenPopup("Vous devez vous équipper d'un marteau pour jouer à ce jeu.");
                return -1;
            }

            MiniGamesManager.Instance.RecordHammerGame(character);

            if(cac.Template.Level == 200)
            {
                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_MULTICOLOURED_FAIRYWORK);
                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_CRACKLING_MULTICOLOURED_FAIRYWORK);
                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_SPRINKLING_MULTICOLOURED_FAIRYWORK);
            }

            if(cac.Template.Level > 149)
                character.Map.ForEach(x => VisualHandler.SendGameRolePlaySpellAnimMessage(x.Client, character, InteractiveObject.Cell.Id, 159));
            else
                character.Map.ForEach(x => VisualHandler.SendGameRolePlaySpellAnimMessage(x.Client, character, InteractiveObject.Cell.Id, 366));

            Random rnd = new Random();
            var nbr = rnd.Next((int)Math.Round(cac.Template.Level / 2d, MidpointRounding.AwayFromZero), (int)cac.Template.Level);

            character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(1749), nbr);

            character.OpenPopup("La machine affiche le score de " + nbr + " !<br>Vous remportez donc " + nbr + " Jetons !<br>Félicitations à vous !");

            return base.StartExecute(character);
        }
    }
}
