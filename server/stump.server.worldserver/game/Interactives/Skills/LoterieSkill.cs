using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs;
using Stump.Server.WorldServer.Game.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Loterie", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class LoterieSkill : CustomSkill
    {
        public LoterieSkill(int id, InteractiveCustomSkillRecord skillTemplate, InteractiveObject interactiveObject)
            : base(id, skillTemplate, interactiveObject)
        {
        }

        public override int StartExecute(Character character)
        {
            if (character.WorldAccount.LastLoterieDate != null && character.WorldAccount.LastLoterieDate.Date == DateTime.Now.Date)
            {
                var ticket = character.Inventory.GetItems().FirstOrDefault(x => x.Template.Id == 7220);
                if (ticket != null)
                {
                    character.Inventory.RemoveItem(ticket, 1);
                    character.SendServerMessage("Vous venez d'utiliser un ticket de loterie ! Écaflip prie pour que le hasard soit avec vous !", Color.YellowGreen);
                    GetPrice(character);
                }
                else
                    character.OpenPopup("La loterie est gratuite une fois par jour uniquement, si vous souhaitez recommencer veuillez attendre demain ou achetez un ticket de loterie.");
            }
            else
            {
                GetPrice(character);
            }

            return base.StartExecute(character);
        }

        private void GetPrice(Character character)
        {
            Dictionary<int, double> Items = new Dictionary<int, double>();

            //AJOUT DES ITEMS ID / Chance/100
            //Items Chacha
            Items.Add(15752, 70);
            Items.Add(15753, 70);
            Items.Add(15755, 70);
            Items.Add(15756, 60);

            //Items Pika
            Items.Add(15757, 60);
            Items.Add(15761, 60);
            Items.Add(15758, 60);
            Items.Add(15763, 55);

            //Items Chantier
            Items.Add(15767, 55);
            Items.Add(15766, 55);
            Items.Add(15769, 55);
            Items.Add(15765, 55);

            //Items éclatants
            Items.Add(15819, 48.5);
            Items.Add(15770, 48.5);
            Items.Add(15826, 48.5);

            //Items Groom
            Items.Add(15775, 50);
            Items.Add(15774, 50);
            Items.Add(15777, 50);
            Items.Add(15772, 50);

            //Items Nonos
            Items.Add(15787, 45);
            Items.Add(15785, 45);
            Items.Add(15786, 45);
            Items.Add(15788, 45);
            Items.Add(15790, 40);

            //Items Empereur
            Items.Add(15812, 45.5);
            Items.Add(15813, 45.5);
            Items.Add(15797, 45.5);
            Items.Add(15792, 45.5);
            Items.Add(15814, 35.5);

            //Items Droïde
            Items.Add(15815, 50);
            Items.Add(15817, 50);
            Items.Add(15816, 50);

            //Items Droïde éclaire
            Items.Add(30909, 20);
            Items.Add(30908, 20);

            //Items Raja
            Items.Add(15820, 50);
            Items.Add(15821, 50);
            Items.Add(15822, 50);
            Items.Add(15825, 50);

            //Items Os Croisés
            Items.Add(15846, 45);
            Items.Add(15847, 45);
            Items.Add(15830, 45);
            Items.Add(15829, 45);
            Items.Add(15849, 40);

            //Items Plume
            Items.Add(15856, 50);
            Items.Add(15857, 50);
            Items.Add(15858, 50);
            Items.Add(15859, 50);
            Items.Add(15861, 35);

            //Items Engrenage
            Items.Add(15862, 50);
            Items.Add(15863, 50);
            Items.Add(15864, 50);
            Items.Add(15865, 50);

            //Items XIV
            Items.Add(15867, 40);
            Items.Add(15869, 40);
            Items.Add(15881, 40);
            Items.Add(15883, 40);

            //Items Ninja Vert
            Items.Add(19196, 50);
            Items.Add(19197, 50);
            Items.Add(19198, 50);
            Items.Add(19199, 50);
            Items.Add(19200, 45);

            //Items Chevalier Aérien
            Items.Add(19202, 40);
            Items.Add(19203, 40);
            Items.Add(19204, 40);
            Items.Add(19205, 40);
            Items.Add(19201, 30);

            //Items Az'Hassin
            Items.Add(20300, 40);
            Items.Add(20301, 40);
            Items.Add(20302, 40);
            Items.Add(20303, 40);
            Items.Add(20304, 40);

            //Shigekax
            Items.Add(11690, 80);
            Items.Add(11689, 80);
            Items.Add(11688, 80);
            Items.Add(11687, 80);
            Items.Add(9643, 60);
            Items.Add(9642, 60);
            Items.Add(9641, 80);
            Items.Add(9640, 80);
            Items.Add(9639, 80);
            Items.Add(9638, 80);
            Items.Add(9636, 80);
            Items.Add(9635, 80);
            Items.Add(8955, 30);
            Items.Add(8954, 60);
            Items.Add(8953, 60);
            Items.Add(8951, 80);
            Items.Add(8950, 80);
            Items.Add(8949, 80);
            Items.Add(8948, 80);
            Items.Add(8694, 60);

            Random rnd = new Random();
            var randomfinded = rnd.Next(0, 1000) / 100d;

            var Sort = Items.Where(x => x.Value >= randomfinded);

            character.WorldAccount.LastLoterieDate = DateTime.Now;
            if (Sort != null)
            {
                character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(Sort.RandomElementOrDefault().Key), 1);
                character.OpenPopup("Vous avez remporté un nouvel objet ! Allez le découvrir tout de suite dans votre inventaire !");
            }
            else
            {
                character.OpenPopup("Nous sommes désolé mais vous n'avez rien gagné réessayez demain ...");
            }
        }
    }
}
