﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs.Customs;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States
{
    [EffectHandler(EffectsEnum.Effect_ChangeAppearance)]
    [EffectHandler(EffectsEnum.Effect_ChangeAppearance_335)]
    public class ChangeSkin : SpellEffectHandler
    {
        public ChangeSkin(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var look = actor.Look.Clone();
                var driverLook = look.SubLooks.FirstOrDefault(x => x.BindingCategory == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER);

                short skinId = -1;
                short scale = -1;
                short bonesId = -1;
                short[] skins = null;
                Dictionary<int, Color> colors = null;

                switch (Dice.Value)
                {
                    case 0:
                        break;
                    case 667: //Pandawa - Picole
                        bonesId = (short)(driverLook != null ? 1084 : 44);
                        break;
                    case 729: //Xelor - Momification
                        bonesId = (short)(driverLook != null ? 1068 : 113);
                        break;
                    case 103: //Zobal - Pleutre
                    case 106:
                        skinId = 1449;
                        if (driverLook == null)
                            bonesId = 1576;
                        break;
                    case 102: //Zobal - Psychopathe
                    case 105:
                        skinId = 1443;
                        if (driverLook == null)
                            bonesId = 1575;
                        break;
                    case 1035: //Steamer - Scaphrandre
                        skinId = 1955;
                        break;
                    case 874: //Pandawa - Colère de Zatoïshwan
                        bonesId = (short)(driverLook != null ? 1202 : 453);
                        scale = (short)(driverLook != null ? 60 : 80);
                        break;
                    case 1177: // Arbre - Feuillage, Arbre de vie
                        bonesId = 3164;
                        scale = 80;
                        break;
                    case 1171:
                        bonesId = 3166;
                        scale = 80;
                        break;
                    case 1234: //Osamodas - Fusion Dragonnet
                        bonesId = 3716;
                        scale = 150;
                        driverLook = null;
                        break;
                    case 1235: //Osamodas - Fusion Tofu
                        bonesId = 3669;
                        scale = 130;
                        driverLook = null;
                        break;
                    case 1236: //Osamodas - Fusion Bouftou
                        bonesId = 3670;
                        scale = 60;
                        driverLook = null;
                        break;
                    case 1335: //osamodas - fusion sapo
                        bonesId = 4811;
                        scale = 110;
                        driverLook = null;
                        break;
                    case 1255: //Sacrier - Castigo do Esfolado
                        bonesId = 3865;
                        scale = 100;
                        driverLook = null;
                        break;
                    case 1256: //Sacrier - Castigo Viajante
                        bonesId = 3821;
                        scale = 100;
                        driverLook = null;
                        break;
                    case 1260: // skin uginak
                        bonesId = 3906;
                        scale = 150;
                        driverLook = null;
                        break;
                    case 1266: //Sacirer (Espada) - Castigo Veloz
                        bonesId = 3870;
                        scale = 100;
                        driverLook = null;
                        break;
                    case 1267: //Sacirer (Espada) - Castigo Sanguinário
                        bonesId = 3872;
                        scale = 100;
                        driverLook = null;
                        break;
                    case 1268: //Sacirer (Espada) - Castigo Robusto
                        bonesId = 3868;
                        scale = 100;
                        driverLook = null;
                        break;
                    case 1318: //Sacirer (Espada) - Castigo Robusto
                        bonesId = 4321;
                        scale = 135;
                        driverLook = null;
                        break;
                    case 1334: //Sacrieur - Souffrance Positive
                        bonesId = 4828;
                        if(driverLook != null)
                        {
                            skins = driverLook.Look.Skins.ToArray();
                            colors = driverLook.Look.Colors.ToDictionary(x => x.Key, x => x.Value);
                        }
                        scale = 145;
                        driverLook = null;
                        break;
                    case 1298: //Sacrieur - Souffrance Negative
                        bonesId = 4115;
                        if (driverLook != null)
                        {
                            skins = driverLook.Look.Skins.ToArray();
                            colors = driverLook.Look.Colors.ToDictionary(x => x.Key, x => x.Value);
                        }
                        scale = 145;
                        driverLook = null;
                        break;
                    case 1326: // Bambou panda
                        bonesId = 4576;
                        driverLook = null;
                        break;

                    default:
                        return false;
                }

                if (driverLook != null)
                {
                    if (skinId != -1)
                        driverLook.Look.AddSkin(skinId);

                    if (scale != -1)
                    {
                        driverLook.Look.SetScales(scale);
                        driverLook.Look.SetDefaultScales(scale);
                    }

                    if (bonesId != -1)
                        driverLook.Look.BonesID = bonesId;

                    look.SetRiderLook(driverLook.Look);
                }
                else
                {
                    if (skinId != -1)
                        look.AddSkin(skinId);

                    if(skins != null)
                    {
                        foreach(var skin in skins)
                        {
                            look.AddSkin(skin);
                        }
                    }

                    if(colors != null)
                    {
                        look.SetColors(colors);
                    }

                    if (scale != -1)
                    {
                        look.SetScales(scale);
                        look.SetDefaultScales(scale);
                    }

                    if (bonesId != -1)
                        look.BonesID = bonesId;
                }

                if (Dice.Value >= 0)
                {
                    //Dispell old skin buffs
                    //foreach (var skinBuff in actor.GetBuffs(x => x is SkinBuff))
                    //    skinBuff.Dispell();

                    if (Dice.Value == 0)
                        return true;

                    var buff = new SkinBuff(actor.PopNextBuffId(), actor, Caster, this, look, Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH);
                    actor.AddBuff(buff);
                }
                else
                {
                    var buff = actor.GetBuffs(x => x is SkinBuff && ((SkinBuff)x).Look.BonesID == Math.Abs(Dice.Value)).FirstOrDefault();
                    actor.RemoveBuff(buff);
                }
            }

            return true;
        }
    }
}