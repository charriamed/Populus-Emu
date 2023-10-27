using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.HavenBags;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items.Player.Custom.LivingObjects;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler : WorldHandlerContainer
    {
        [WorldHandler(ObjectSetPositionMessage.Id)]
        public static void HandleObjectSetPositionMessage(WorldClient client, ObjectSetPositionMessage message)
        {
            if (!Enum.IsDefined(typeof(CharacterInventoryPositionEnum), (int)message.Position))
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.MoveItem(item, (CharacterInventoryPositionEnum)message.Position);

        }

        [WorldHandler(ObjectDeleteMessage.Id)]
        public static void HandleObjectDeleteMessage(WorldClient client, ObjectDeleteMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.RemoveItem(item, (int)message.Quantity);
        }

        [WorldHandler(ObjectUseMessage.Id)]
        public static void HandleObjectUseMessage(WorldClient client, ObjectUseMessage message)
        {
            var character = (client as WorldClient).Character;
            var Database = WorldServer.Instance.DBAccessor.Database;


            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item);

            switch (item.Template.Id)
            {
                #region VIP
                case 13032:
                    if (!character.VipS && !character.VipB && !character.VipG && !character.VipD && client.WorldAccount.Vip2 > 0 && client.WorldAccount.Vip2 < 2)
                    {
                        if (!character.IsFighting())
                        {
                            character.WorldAccount.Tokens += 500;
                            character.SendServerMessage("Tu as gagné 500 Orgine.");
                            character.Record.VipB = true;
                            character.Record.VipRank = 2;
                            character.AddTitle(678);
                            character.AddOrnament(340);
                            character.SendServerMessage("Vous avez gagné Title VIP Silver+Ornement pourfendeur squelettique.");
                            Database.Update(character.Record);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_DE_NELWEEN);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }
                    }
                    else
                        client.Character.OpenPopup("Tu es déjà VIP, tu ne pourras pas devenir plus vip que tu ne l'es déjà");



                    break;

                case 13023:
                    if (!character.VipS && !character.VipB && !character.VipG && !character.VipD && client.WorldAccount.Vip2 > 0 && client.WorldAccount.Vip2 < 2)
                    {
                        if (!character.IsFighting())
                        {
                            character.WorldAccount.Tokens += 1000;
                            character.SendServerMessage("Tu as gagné 1000 Orgine.");
                            character.Record.VipS = true;
                            character.Record.VipRank2 = 2;
                            character.AddTitle(679);
                            character.AddOrnament(336);
                            character.SendServerMessage("Vous avez gagné Title VIP Gold+Ornement pourfendeur démoniaque.");
                            Database.Update(character.Record);
                            character.AddEmote(EmotesEnum.REUNIFICATION_OF_THE_SIX_DOFUS);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_DE_NELWEEN);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }
                    }
                    else
                        client.Character.OpenPopup("Tu es déjà VIP, tu ne pourras pas devenir plus vip que tu ne l'es déjà. si tu veux changer V.I.P Contacter nous sur discord ");

                    break;

                case 13026:
                    if (!character.VipB && !character.VipS && !character.VipG && !character.VipD && client.WorldAccount.Vip2 > 0 && client.WorldAccount.Vip2 < 2)
                    {
                        if (!character.IsFighting())
                        {
                            character.WorldAccount.Tokens += 1500;
                            character.SendServerMessage("Tu as gagné 1500 Orgine.");
                            character.Record.VipRank3 = 2;
                            character.AddTitle(680);
                            character.AddOrnament(341);
                            character.SendServerMessage("Vous avez gagné Title VIP Platinum+Ornement pourfendeur infernal.");
                            Database.Update(character.Record);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_DE_NELWEEN);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_BLEUTÉE_DE_L_ORNITHORYNQUE_ANCESTRAL);
                            character.AddEmote(EmotesEnum.RIMANDA_S_AURA);
                            character.AddEmote(EmotesEnum.SEPTANGEL_S_AURA);
                            character.AddEmote(EmotesEnum.DJAUL_S_AURA);
                            character.AddEmote(EmotesEnum.RATATHROSK_S_AURA);
                            character.AddEmote(EmotesEnum.MERIANA_S_AURA);
                            character.AddEmote(EmotesEnum.NORRAI_S_AURA);
                            character.AddEmote(EmotesEnum.REUNIFICATION_OF_THE_SIX_DOFUS);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }
                    }
                    else
                        client.Character.OpenPopup("Tu es déjà VIP, tu ne pourras pas devenir plus vip que tu ne l'es déjà. si tu veux changer V.I.P Contacter nous sur discord");



                    break;

                case 10275:
                    if (!character.VipB && !character.VipS && !character.VipG && !character.VipD && client.WorldAccount.Vip2 > 0 && client.WorldAccount.Vip2 < 2)
                    {
                        if (!character.IsFighting())
                        {
                            character.WorldAccount.Tokens += 2000;
                            character.SendServerMessage("Tu as gagné 2000 Orgine.");
                            character.Record.VipD = true;
                            character.Record.VipRank4 = 2;
                            character.AddTitle(685);
                            character.AddOrnament(315);
                            character.SendServerMessage("Vous avez gagné Title VIP Diamond+Ornement Pourfendeur Glorieux.");
                            Database.Update(character.Record);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_DE_NELWEEN);
                            character.AddEmote(EmotesEnum.EMOTE_AURA_BLEUTÉE_DE_L_ORNITHORYNQUE_ANCESTRAL);
                            character.AddEmote(EmotesEnum.RIMANDA_S_AURA);
                            character.AddEmote(EmotesEnum.SEPTANGEL_S_AURA);
                            character.AddEmote(EmotesEnum.DJAUL_S_AURA);
                            character.AddEmote(EmotesEnum.RATATHROSK_S_AURA);
                            character.AddEmote(EmotesEnum.MERIANA_S_AURA);
                            character.AddEmote(EmotesEnum.NORRAI_S_AURA);
                            character.AddEmote(EmotesEnum.REUNIFICATION_OF_THE_SIX_DOFUS);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }
                    }
                    else
                        client.Character.OpenPopup("Tu es déjà VIP, tu ne pourras pas devenir plus vip que tu ne l'es déjà. si tu veux changer V.I.P Contacter nous sur discord");

                    break;
                #endregion

                case 12332: //add level < 25
                    if (client.Character.Level < 25)
                    {
                        client.Character.LevelUp(1);
                        client.Character.Inventory.RemoveItem(item, 1);
                    }

                    break;
                case 12333://add level <50
                    if (client.Character.Level < 50)
                    {
                        client.Character.LevelUp(1);
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    break;
                case 684://add kolichas
                    var itemTemplate = Singleton<ItemManager>.Instance.TryGetTemplate(12736);
                    client.Character.Inventory.AddItem(itemTemplate, 1000);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ---------------- Bolsitas ------------------//
                case 16819:
                    var itemTemplate1 = Singleton<ItemManager>.Instance.TryGetTemplate(2331); //Berenjena
                    client.Character.Inventory.AddItem(itemTemplate1, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16820:
                    var itemTemplate2 = Singleton<ItemManager>.Instance.TryGetTemplate(1984); //Cenizas Eternas
                    client.Character.Inventory.AddItem(itemTemplate2, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16821:
                    var itemTemplate3 = Singleton<ItemManager>.Instance.TryGetTemplate(1734); //Cerezas
                    client.Character.Inventory.AddItem(itemTemplate3, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16822:
                    var itemTemplate4 = Singleton<ItemManager>.Instance.TryGetTemplate(1736); //Limones
                    client.Character.Inventory.AddItem(itemTemplate4, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16825:
                    var itemTemplate5 = Singleton<ItemManager>.Instance.TryGetTemplate(1977); //Especias
                    client.Character.Inventory.AddItem(itemTemplate5, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16826:
                    var itemTemplate6 = Singleton<ItemManager>.Instance.TryGetTemplate(1974); //Enchalada (Lechuga e-e)
                    client.Character.Inventory.AddItem(itemTemplate6, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16827:
                    var itemTemplate7 = Singleton<ItemManager>.Instance.TryGetTemplate(1983); //Grasa Gelatinosa
                    client.Character.Inventory.AddItem(itemTemplate7, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16828:
                    var itemTemplate8 = Singleton<ItemManager>.Instance.TryGetTemplate(6671); //Alubias
                    client.Character.Inventory.AddItem(itemTemplate8, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16830:
                    var itemTemplate9 = Singleton<ItemManager>.Instance.TryGetTemplate(1978); //Pimienta
                    client.Character.Inventory.AddItem(itemTemplate9, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16831:
                    var itemTemplate10 = Singleton<ItemManager>.Instance.TryGetTemplate(1730); //Sal
                    client.Character.Inventory.AddItem(itemTemplate10, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16832:
                    var itemTemplate11 = Singleton<ItemManager>.Instance.TryGetTemplate(1975); //Cebolla
                    client.Character.Inventory.AddItem(itemTemplate11, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16833:
                    var itemTemplate12 = Singleton<ItemManager>.Instance.TryGetTemplate(519); //Polvos Magicos
                    client.Character.Inventory.AddItem(itemTemplate12, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16834:
                    var itemTemplate13 = Singleton<ItemManager>.Instance.TryGetTemplate(1986); //Polvo Temporal
                    client.Character.Inventory.AddItem(itemTemplate13, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16835:
                    var itemTemplate14 = Singleton<ItemManager>.Instance.TryGetTemplate(1985); //Resina
                    client.Character.Inventory.AddItem(itemTemplate14, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ----------------- Tonel ---------------- //
                case 16823:
                    var itemTemplate15 = Singleton<ItemManager>.Instance.TryGetTemplate(1731); //Zumo Sabroso
                    client.Character.Inventory.AddItem(itemTemplate15, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16824:
                    var itemTemplate16 = Singleton<ItemManager>.Instance.TryGetTemplate(311); //Agua
                    client.Character.Inventory.AddItem(itemTemplate16, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16829:
                    var itemTemplate17 = Singleton<ItemManager>.Instance.TryGetTemplate(1973); //Aceite para freir
                    client.Character.Inventory.AddItem(itemTemplate17, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16836:
                    var itemTemplate18 = Singleton<ItemManager>.Instance.TryGetTemplate(2012); //Sangre de Scorbuto
                    client.Character.Inventory.AddItem(itemTemplate18, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;

                // ----------------- Pociones ---------------- //
                case 6965://Pócima de ciudad: Bonta
                    tpPlayer(client.Character, 5506048, 359, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 6964://Pócima de ciudad: Brakmar
                    tpPlayer(client.Character, 13631488, 370, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 996: //Multigly
                    tpPlayer(client.Character, 98566657, 43, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 548: //pocima de recuerdo
                    var objectPosition = client.Character.GetSpawnPoint();
                    var NextMap = objectPosition.Map;
                    var Cell = objectPosition.Cell;
                    client.Character.Teleport(NextMap, Cell);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 14485://mimobionte
                    client.Character.Inventory.Save();
                    client.Send(new ClientUIOpenedByObjectMessage(3, (uint)item.Guid));
                    break;
            }
        }

        [WorldHandler(ObjectUseMultipleMessage.Id)]
        public static void HandleObjectUseMultipleMessage(WorldClient client, ObjectUseMultipleMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, (int)message.Quantity);
        }

        [WorldHandler(ObjectUseOnCellMessage.Id)]
        public static void HandleObjectUseOnCellMessage(WorldClient client, ObjectUseOnCellMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            var cell = client.Character.Map.GetCell(message.Cells);

            if (cell == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, cell);
        }

        [WorldHandler(ObjectUseOnCharacterMessage.Id)]
        public static void HandleObjectUseOnCharacterMessage(WorldClient client, ObjectUseOnCharacterMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            if (!item.Template.Targetable)
            {
                return;
            }

            var character = client.Character.Map.GetActor<Character>((int)message.CharacterId);

            if (character == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, character);
        }

        [WorldHandler(ObjectFeedMessage.Id)]
        public static void HandleObjectFeedMessage(WorldClient client, ObjectFeedMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);
            var food = client.Character.Inventory.TryGetItem((int)message.Meal.First().ObjectUID);

            if (item == null || food == null)
            {
                return;
            }

            if (food.Stack < message.Meal.First().Quantity)
            {
                message.Meal.First().Quantity = (ushort)food.Stack;
            }

            if (item.Stack > 1)
                item.Owner.Inventory.CutItem(item, (int)item.Stack - 1);

            var i = 0;
            while (i < message.Meal.First().Quantity && item.Feed(food))
            {
                i++;
            }

            client.Character.Inventory.RemoveItem(food, i);
        }

        [WorldHandler(LivingObjectChangeSkinRequestMessage.Id)]
        public static void HandleLivingObjectChangeSkinRequestMessage(WorldClient client, LivingObjectChangeSkinRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.LivingUID);

            if (!(item is CommonLivingObject))
            {
                return;
            } ((CommonLivingObject)item).SelectedLevel = (short)message.SkinId;
        }

        [WorldHandler(LivingObjectDissociateMessage.Id)]
        public static void HandleLivingObjectDissociateMessage(WorldClient client, LivingObjectDissociateMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.LivingUID);

            (item as BoundLivingObjectItem)?.Dissociate();
        }

        [WorldHandler(ObjectDropMessage.Id)]
        public static void HandleObjectDropMessage(WorldClient client, ObjectDropMessage message)
        {
            if (client.Character.IsInFight() || client.Character.IsInExchange())
            {
                return;
            }

            client.Character.DropItem((int)message.ObjectUID, (int)message.Quantity);
        }

        [WorldHandler(MimicryObjectFeedAndAssociateRequestMessage.Id)]
        public static void HandleMimicryObjectFeedAndAssociateRequestMessage(WorldClient client, MimicryObjectFeedAndAssociateRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var character = client.Character;

            var host = character.Inventory.TryGetItem((int)message.HostUID);
            var food = character.Inventory.TryGetItem((int)message.FoodUID);
            var mimisymbic = character.Inventory.TryGetItem(ItemIdEnum.MIMIBIOTE_14485);

            if (host == null || food == null)
            {
                SendMimicryObjectErrorMessage(client, host == null ? MimicryErrorEnum.NO_VALID_HOST : MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }

            if (mimisymbic == null)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_MIMICRY);
                return;
            }

            if (host.Effects.Any(x => x.EffectId == EffectsEnum.Effect_LivingObjectId || x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper)
                || !host.Template.Type.Mimickable)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_HOST);
                return;
            }

            if (food.Effects.Any(x => x.EffectId == EffectsEnum.Effect_LivingObjectId || x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper)
                || !food.Template.Type.Mimickable)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }

            if (food.Template.Id == host.Template.Id)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.SAME_SKIN);
                return;
            }

            if (food.Template.Level > host.Template.Level)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_LEVEL);
                return;
            }

            if (food.Template.TypeId != host.Template.TypeId)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_TYPE);
                return;
            }

            var modifiedItem = ItemManager.Instance.CreatePlayerItem(character, host);
            modifiedItem.Effects.Add(new EffectInteger(EffectsEnum.Effect_Appearance, (short)food.Template.Id));
            modifiedItem.Stack = 1;

            if (message.Preview)
            {
                SendMimicryObjectPreviewMessage(client, modifiedItem);
            }
            else
            {
                character.Inventory.UnStackItem(food, 1);
                character.Inventory.UnStackItem(mimisymbic, 1);
                character.Inventory.UnStackItem(host, 1);
                character.Inventory.AddItem(modifiedItem);

                SendMimicryObjectAssociatedMessage(client, modifiedItem);
            }
        }

        [WorldHandler(MimicryObjectEraseRequestMessage.Id)]
        public static void HandleMimicryObjectEraseRequestMessage(WorldClient client, MimicryObjectEraseRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var host = client.Character.Inventory.TryGetItem((int)message.HostUID);

            if (host == null)
            {
                return;
            }

            host.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_Appearance);
            host.Invalidate();

            client.Character.Inventory.RefreshItem(host);
            client.Character.UpdateLook();

            SendInventoryWeightMessage(client);
        }

        [WorldHandler(WrapperObjectDissociateRequestMessage.Id)]
        public static void HandleWrapperObjectDissociateRequestMessage(WorldClient client, WrapperObjectDissociateRequestMessage message)
        {
            if (client.Character.IsInFight() || client.Character.IsInExchange())
            {
                return;
            }

            var host = client.Character.Inventory.TryGetItem((int)message.HostUID);

            if (host == null)
            {
                return;
            }

            var apparenceWrapper = host.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Apparence_Wrapper) as EffectInteger;

            if (apparenceWrapper == null)
            {
                return;
            }

            var wrapperItemTemplate = ItemManager.Instance.TryGetTemplate(apparenceWrapper.Value);

            host.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_Apparence_Wrapper);

            host.Invalidate();
            client.Character.Inventory.RefreshItem(host);
            host.OnObjectModified();

            var wrapperItem = ItemManager.Instance.CreatePlayerItem(client.Character, wrapperItemTemplate, 1);

            client.Character.Inventory.AddItem(wrapperItem);
            client.Character.UpdateLook();

            SendInventoryWeightMessage(client);
        }

        public static void SendWrapperObjectAssociatedMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new WrapperObjectAssociatedMessage((uint)host.Guid));
        }

        public static void SendMimicryObjectAssociatedMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new MimicryObjectAssociatedMessage((uint)host.Guid));
        }

        public static void SendMimicryObjectPreviewMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new MimicryObjectPreviewMessage(host.GetObjectItem()));
        }

        public static void SendMimicryObjectErrorMessage(IPacketReceiver client, MimicryErrorEnum error)
        {
            client.Send(new MimicryObjectErrorMessage((sbyte)ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR, (sbyte)error, true));
        }

        public static void SendGameRolePlayPlayerLifeStatusMessage(IPacketReceiver client)
        {
            client.Send(new GameRolePlayPlayerLifeStatusMessage());
        }

        public static void SendInventoryContentMessage(WorldClient client)
        {
            client.Send(new InventoryContentMessage(client.Character.Inventory.Select(entry => entry.GetObjectItem()).ToArray(),
                (ulong)client.Character.Inventory.Kamas));
        }

        public static void SendInventoryContentAndPresetMessage(WorldClient client)
        {
            client.Send(new InventoryContentMessage(client.Character.Inventory.Select(entry => entry.GetObjectItem()).ToArray(),
                (ulong)client.Character.Inventory.Kamas));
        }

        public static void SendInventoryWeightMessage(WorldClient client)
        {
            client.Send(new InventoryWeightMessage((uint)client.Character.Inventory.Weight,
                                                   client.Character.Inventory.WeightTotal));
        }

        public static void SendExchangeKamaModifiedMessage(IPacketReceiver client, bool remote, ulong kamasAmount)
        {
            client.Send(new ExchangeKamaModifiedMessage(remote, kamasAmount));
        }

        public static void SendObjectAddedMessage(IPacketReceiver client, IItem addedItem)
        {
            client.Send(new ObjectAddedMessage(addedItem.GetObjectItem(), 0));
        }

        public static void SendObjectsAddedMessage(IPacketReceiver client, IEnumerable<ObjectItem> addeditems)
        {
            client.Send(new ObjectsAddedMessage(addeditems.ToArray()));
        }

        public static void SendObjectsQuantityMessage(IPacketReceiver client, IEnumerable<ObjectItemQuantity> itemQuantity)
        {
            client.Send(new ObjectsQuantityMessage(itemQuantity.ToArray()));
        }

        public static void SendObjectDeletedMessage(IPacketReceiver client, int guid)
        {
            client.Send(new ObjectDeletedMessage((uint)guid));
        }

        public static void SendObjectsDeletedMessage(IPacketReceiver client, IEnumerable<int> guids)
        {
            client.Send(new ObjectsDeletedMessage(guids.Select(entry => (uint)entry).ToArray()));
        }

        public static void SendObjectModifiedMessage(IPacketReceiver client, IItem item)
        {
            client.Send(new ObjectModifiedMessage(item.GetObjectItem()));
        }

        public static void SendObjectMovementMessage(IPacketReceiver client, BasePlayerItem movedItem)
        {
            client.Send(new ObjectMovementMessage((uint)movedItem.Guid, (sbyte)movedItem.Position));
        }

        public static void SendObjectQuantityMessage(IPacketReceiver client, BasePlayerItem item)
        {
            client.Send(new ObjectQuantityMessage((uint)item.Guid, (uint)item.Stack, 0));
        }

        public static void SendObjectErrorMessage(IPacketReceiver client, ObjectErrorEnum error)
        {
            client.Send(new ObjectErrorMessage((sbyte)error));
        }

        public static void SendSetUpdateMessage(WorldClient client, ItemSetTemplate itemSet)
        {
            client.Send(new SetUpdateMessage((ushort)itemSet.Id,
                client.Character.Inventory.GetItemSetEquipped(itemSet).Select(entry => (ushort)entry.Template.Id).ToArray(),
                client.Character.Inventory.GetItemSetEffects(itemSet).Select(entry => entry.GetObjectEffect()).ToArray()));
        }

        public static void SendExchangeShopStockMovementUpdatedMessage(IPacketReceiver client, MerchantItem item)
        {
            client.Send(new ExchangeShopStockMovementUpdatedMessage(item.GetObjectItemToSell()));
        }

        public static void SendExchangeShopStockMovementRemovedMessage(IPacketReceiver client, MerchantItem item)
        {
            client.Send(new ExchangeShopStockMovementRemovedMessage((uint)item.Guid));
        }

        public static void SendObtainedItemMessage(IPacketReceiver client, ItemTemplate item, int count)
        {
            client.Send(new ObtainedItemMessage((ushort)item.Id, (uint)count));
        }

        public static void SendObtainedItemWithBonusMessage(IPacketReceiver client, ItemTemplate item, int count, int bonus)
        {
            client.Send(new ObtainedItemWithBonusMessage((ushort)item.Id, (uint)count, (uint)bonus));
        }

        public static void SendExchangeObjectPutInBagMessage(IPacketReceiver client, bool remote, IItem item)
        {
            client.Send(new ExchangeObjectPutInBagMessage(remote, item.GetObjectItem()));
        }

        public static void SendExchangeObjectModifiedInBagMessage(IPacketReceiver client, bool remote, IItem item)
        {
            client.Send(new ExchangeObjectModifiedInBagMessage(remote, item.GetObjectItem()));
        }

        public static void SendExchangeObjectRemovedFromBagMessage(IPacketReceiver client, bool remote, int guid)
        {
            client.Send(new ExchangeObjectRemovedFromBagMessage(remote, (uint)guid));
        }

        public static void tpPlayer(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }
    }
}