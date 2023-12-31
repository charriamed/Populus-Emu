﻿using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Database.Social
{
    public class EmoteRelator
    {
        public static string FetchQuery = "SELECT * FROM emotes";
    }


    [TableName("emotes")]
    [D2OClass("Emoticon", "com.ankamagames.dofus.datacenter.communication")]
    public class Emote : IAutoGeneratedRecord, IAssignedByD2O
    {
        private string m_name;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        public EmotesEnum EmoteId => (EmotesEnum) Id;

        public int NameId
        {
            get;
            set;
        }

        public string Name => m_name ?? (m_name = TextManager.Instance.GetText(NameId));

        public bool Persistancy
        {
            get;
            set;
        }

        public bool Eight_directions
        {
            get;
            set;
        }

        public bool Aura
        {
            get;
            set;
        }

        public int Cooldown
        {
            get;
            set;
        }

        public int Duration
        {
            get;
            set;
        }

        public void AssignFields(object d2oObject)
        {
            var emote = (Emoticon) d2oObject;
            Id = (int) emote.Id;
            NameId = (int) emote.NameId;
            Persistancy = emote.Persistancy;
            Eight_directions = emote.Eight_directions;
            Aura = emote.Aura;
            Cooldown = (int) emote.Cooldown;
            Duration = (int) emote.Duration;
        }

        public ActorLook UpdateEmoteLook(Character character, ActorLook look, bool apply)
        {
            if (!apply)
            {
                if (EmoteId == EmotesEnum.EMOTE_AURA_DE_PUISSANCE ||
                    EmoteId == EmotesEnum.EMOTE_AURA_VAMPYRIQUE ||
                    EmoteId == EmotesEnum.EMOTE_AURA_BLEUTÉE_DE_L_ORNITHORYNQUE_ANCESTRAL ||
                    EmoteId == EmotesEnum.OMEGA_100_AURA ||
                    EmoteId == EmotesEnum.OMEGA_200_AURA ||
                    EmoteId == EmotesEnum.OMEGA_300_AURA ||
                    EmoteId == EmotesEnum.OMEGA_400_AURA ||
                    EmoteId == EmotesEnum.OMEGA_500_AURA ||
                    EmoteId == EmotesEnum.NORRAI_S_AURA ||
                    EmoteId == EmotesEnum.MERIANA_S_AURA ||
                    EmoteId == EmotesEnum.RATATHROSK_S_AURA ||
                    EmoteId == EmotesEnum.DJAUL_S_AURA ||
                    EmoteId == EmotesEnum.SEPTANGEL_S_AURA ||
                    EmoteId == EmotesEnum.RIMANDA_S_AURA ||
                    EmoteId == EmotesEnum.EMOTE_AURA_DE_NELWEEN)
                    look.RemoveSubLook(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND);

                if (EmoteId == EmotesEnum.EMOTE_GUILD)
                {
                    
                    if (character.Guild == null)
                     return look;

                    var playerLook = look.GetRiderLook() ?? look;

                    playerLook.RemoveSkin((short) character.Guild.Emblem.Template.SkinId);

                    playerLook.RemoveColor(7);
                    playerLook.RemoveColor(8);
                    playerLook.RemoveColor(9);
                }

                return look;
            }

            ActorLook auraLook = null;

            switch (EmoteId)
            {
                case EmotesEnum.EMOTE_AURA_DE_PUISSANCE:
                    auraLook = new ActorLook(character.Level >= 200 ? (short) 170 : (short) 169);
                    break;

                case EmotesEnum.EMOTE_AURA_VAMPYRIQUE:
                    auraLook = new ActorLook(171);
                    break;

                case EmotesEnum.EMOTE_AURA_BLEUTÉE_DE_L_ORNITHORYNQUE_ANCESTRAL:
                    auraLook = new ActorLook(1465);
                    break;

                case EmotesEnum.EMOTE_AURA_DE_NELWEEN:
                    auraLook = new ActorLook(1501);
                    break;

                case EmotesEnum.OMEGA_100_AURA:
                    auraLook = new ActorLook(4829);
                    break;
                case EmotesEnum.OMEGA_200_AURA:
                    auraLook = new ActorLook(4830);
                    break;
                case EmotesEnum.OMEGA_300_AURA:
                    auraLook = new ActorLook(4831);
                    break;
                case EmotesEnum.OMEGA_400_AURA:
                    auraLook = new ActorLook(4832);
                    break;
                case EmotesEnum.OMEGA_500_AURA:
                    auraLook = new ActorLook(4833);
                    break;

                case EmotesEnum.SEPTANGEL_S_AURA:
                    auraLook = new ActorLook(5138);
                    break;

                case EmotesEnum.RATATHROSK_S_AURA:
                    auraLook = new ActorLook(5139);
                    break;

                case EmotesEnum.DJAUL_S_AURA:
                    auraLook = new ActorLook(5140);
                    break;

                case EmotesEnum.NORRAI_S_AURA:
                    auraLook = new ActorLook(5141);
                    break;

                case EmotesEnum.RIMANDA_S_AURA:
                    auraLook = new ActorLook(5142);
                    break;

                case EmotesEnum.MERIANA_S_AURA:
                    auraLook = new ActorLook(5143);
                    break;

                case (EmotesEnum)155: //guildwinner
                    {
                        try
                        {

                            if (character.Guild == null)
                                break;
                            var playerLook = look.GetRiderLook() ?? look;
                            //if (character.Guild.Alliance?.Id == null || character.Guild.Alliance?.Id < 1)
                            //{

                            //    playerLook.AddSkin((short)character.Guild.Emblem.Template.SkinId);
                            //    playerLook.AddColor(7, character.Guild.Emblem.BackgroundColor);
                            //    playerLook.AddColor(8, character.Guild.Emblem.SymbolColor);
                            //    playerLook.AddColor(9, character.Guild.Emblem.SymbolColor);
                            //}
                            //else
                            //{
                            //    playerLook.AddSkin((short)(character.Guild.Alliance.Emblem.SymbolShape + 2569));
                            //    playerLook.AddSkin((short)character.Guild.Emblem.Template.SkinId);
                            //    playerLook.AddColor(7, character.Guild.Emblem.BackgroundColor);
                            //    playerLook.AddColor(8, character.Guild.Emblem.SymbolColor);
                            //    playerLook.AddColor(9, character.Guild.Alliance.Emblem.BackgroundColor);
                            //    playerLook.AddColor(10, character.Guild.Alliance.Emblem.SymbolColor);

                            //}
                        }
                        catch { }
                    }
                    break;

                case EmotesEnum.EMOTE_GUILD:
                    {
                        if (character.Guild == null)
                            break;

                        var playerLook = look.GetRiderLook() ?? look;

                        playerLook.AddSkin((short) character.Guild.Emblem.Template.SkinId);

                        playerLook.AddColor(7, character.Guild.Emblem.BackgroundColor);
                        playerLook.AddColor(8, character.Guild.Emblem.SymbolColor);
                        playerLook.AddColor(9, character.Guild.Emblem.SymbolColor);
                    }
                    break;
            }

            if (auraLook != null)
                look.SetSubLook(new SubActorLook(0, SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND, auraLook));

            return look;
        }
    }
}