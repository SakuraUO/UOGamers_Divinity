using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Mobiles;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Spells.Second;

namespace Server.Items
{
    public enum SpellEffect
    {
        None,

        Clumsy,
        Feeblemind,
        MagicArrow,
        Weaken,
        Harm,
        Paralyze,
        Fireball,
        Curse,
        ManaDrain,
        Lightning,

        ItemID,
        MiniHeal,
        GHeal,

        NightSight,
        Protection,
        Agility,
        Cunning,
        Strength,
        Bless,
        Invis,
        Reflect,

        Teleportation,
    }

    public abstract class SpellCastEffect
    {
        public static void MutateLoot(int maxInt, MagicWand wand)
        {
            double dr = Utility.RandomDouble() * 100.0;

            if (dr < 2 && maxInt >= 80)
                wand.SpellEffect = SpellEffect.ManaDrain;
            else if (dr < 8 && maxInt >= 80)
                wand.SpellEffect = SpellEffect.Lightning;
            else if (dr < 13 && maxInt >= 60)
                wand.SpellEffect = SpellEffect.Fireball;
            else if (dr < 18 && maxInt >= 60)
                wand.SpellEffect = SpellEffect.GHeal;
            else if (dr < 28 && maxInt >= 40)
                wand.SpellEffect = SpellEffect.Harm;
            else if (dr < 38 && maxInt >= 40)
                wand.SpellEffect = SpellEffect.MagicArrow;
            else if (dr < 48 && maxInt >= 20)
                wand.SpellEffect = SpellEffect.Feeblemind;
            else if (dr < 58 && maxInt >= 20)
                wand.SpellEffect = SpellEffect.Clumsy;
            else if (dr < 68 && maxInt >= 20)
                wand.SpellEffect = SpellEffect.MiniHeal;
            else
                wand.SpellEffect = SpellEffect.ItemID;

            if (wand.SpellEffect != SpellEffect.None)
                wand.SpellCharges = SpellCastEffect.GetChargesFor(wand.SpellEffect);
        }

        public static void MutateLoot(int maxInt, BaseWeapon weapon)
        {
            double dr = Utility.RandomDouble() * 100.0;

            if (dr < 2 && maxInt >= 80)
                weapon.SpellEffect = SpellEffect.Lightning;
            else if (dr < 8 && maxInt >= 80)
                weapon.SpellEffect = SpellEffect.ManaDrain;
            else if (dr < 13 && maxInt >= 60)
                weapon.SpellEffect = SpellEffect.Curse;
            else if (dr < 18 && maxInt >= 60)
                weapon.SpellEffect = SpellEffect.Fireball;
            else if (dr < 28 && maxInt >= 40)
                weapon.SpellEffect = SpellEffect.Paralyze;
            else if (dr < 38 && maxInt >= 40)
                weapon.SpellEffect = SpellEffect.Harm;
            else if (dr < 48 && maxInt >= 20)
                weapon.SpellEffect = SpellEffect.Weaken;
            else if (dr < 58 && maxInt >= 20)
                weapon.SpellEffect = SpellEffect.MagicArrow;
            else if (dr < 68)
                weapon.SpellEffect = SpellEffect.Feeblemind;
            else //if ( dr < 78 )
                weapon.SpellEffect = SpellEffect.Clumsy;

            if (weapon.SpellEffect != SpellEffect.None)
                weapon.SpellCharges = SpellCastEffect.GetChargesFor(weapon.SpellEffect);
        }

        public static void MutateLoot(int maxInt, BaseClothing clothes)
        {
            double dr = Utility.RandomDouble() * 100.0;

            if (dr < 2 && maxInt >= 90)
                clothes.SpellEffect = SpellEffect.Reflect;
            else if (dr < 3 && maxInt >= 90)
                clothes.SpellEffect = SpellEffect.Invis;
            else if (dr < 8 && maxInt >= 80)
                clothes.SpellEffect = SpellEffect.Curse;
            else if (dr < 13 && maxInt >= 80)
                clothes.SpellEffect = SpellEffect.Bless;
            else if (dr < 28 && maxInt >= 60)
                clothes.SpellEffect = SpellEffect.Weaken;
            else if (dr < 38 && maxInt >= 60)
                clothes.SpellEffect = SpellEffect.Clumsy;
            else if (dr < 48 && maxInt >= 60)
                clothes.SpellEffect = SpellEffect.Feeblemind;
            else if (dr < 58 && maxInt >= 20)
                clothes.SpellEffect = SpellEffect.Strength;
            else if (dr < 68 && maxInt >= 20)
                clothes.SpellEffect = SpellEffect.Cunning;
            else if (dr < 78 && maxInt >= 20)
                clothes.SpellEffect = SpellEffect.Agility;
            else if (dr < 88)
                clothes.SpellEffect = SpellEffect.Protection;
            else
                clothes.SpellEffect = SpellEffect.NightSight;

            if (clothes is GoldRing && dr < 2 && maxInt >= 99)
            {
                if (Utility.RandomBool())
                    clothes.SpellEffect = SpellEffect.Teleportation;
                else
                    clothes.SpellEffect = SpellEffect.Invis;
            }

            if (clothes.SpellEffect != SpellEffect.None)
                clothes.SpellCharges = SpellCastEffect.GetChargesFor(clothes.SpellEffect);
        }

        private static SpellCastEffect[] m_Effects = new SpellCastEffect[]
			{
				null, // none

				new CurseEffect( StatType.Dex, 0x1DF, 0x3779, 10, 15, 5002, EffectLayer.Head ),
				new CurseEffect( StatType.Int, 0x1E4, 0x3779, 10, 15, 5004, EffectLayer.Head ),
				new DamageEffect( SpellCircle.First, 0x1E5, 0x36E4, 5, 3006, 4006, 0 ), // magic arrow
				new CurseEffect( StatType.Str, 0x1E6, 0x3779, 10, 15, 5009, EffectLayer.Waist ),
				new DamageEffect( SpellCircle.Second, 0x1F1, 0x374A, 10, 15, 5013, EffectLayer.Waist ), // harm
				new ParalyzeEffect(),
				new DamageEffect( SpellCircle.Third, 0x15E, 0x36D4, 7, 9502, 4019, 0x160 ), // fireball
				new CurseEffect( StatType.All, 0x1EA, 0x374A, 10, 15, 5028, EffectLayer.Waist ),
				new ManaDrainEffect(),
				new DamageEffect( SpellCircle.Fourth, 0, 0, 0, 0, 0, EffectLayer.Head ), // lightning
				
				new ItemIDEffect(),
				new HealEffect( 1, 10 ),
				new HealEffect( 20, 35 ),
		
				new NightSightEffect(),
				new ProtectionEffect(),
				new BlessEffect( StatType.Dex, 0x28E, 0x375A, 10, 15, 5010, EffectLayer.Waist ),
				new BlessEffect( StatType.Int, 0x1EB, 0x375A, 10, 15, 5011, EffectLayer.Head ),
				new BlessEffect( StatType.Str, 0x1EE, 0x375A, 10, 15, 5017, EffectLayer.Waist ),
				new BlessEffect( StatType.All, 0x1EA, 0x373A, 10, 15, 5018, EffectLayer.Waist ),
				new InvisibilityEffect(),
				new MagicReflectEffect(),
				new TeleportationEffect(),
			};

        public static bool InvokeEffect(SpellEffect effect, Mobile from, Mobile target)
        {
            int e = (int)effect;
            if (e >= 0 && e < m_Effects.Length && m_Effects[e] != null)
            {
                if (m_Effects[e].DoEffect(from, target))
                {
                    return true;
                }
                else
                {
                    //from.FixedEffect( 0x3735, 6, 30 );
                    //from.PlaySound( 0x5C );
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string GetName(SpellEffect e)
        {
            switch (e)
            {
                case SpellEffect.Clumsy:
                    return "clumsiness";
                case SpellEffect.Feeblemind:
                    return "feeblemindedness";
                case SpellEffect.MagicArrow:
                    return "burning";
                case SpellEffect.Weaken:
                    return "weakness";
                case SpellEffect.Harm:
                    return "wounding";
                case SpellEffect.Paralyze:
                    return "ghoul's touch";
                case SpellEffect.Fireball:
                    return "daemon's breath";
                case SpellEffect.Curse:
                    return "evil";
                case SpellEffect.ManaDrain:
                    return "mage's bane";
                case SpellEffect.Lightning:
                    return "thunder";

                case SpellEffect.ItemID:
                    return "identification";
                case SpellEffect.MiniHeal:
                    return "healing";
                case SpellEffect.GHeal:
                    return "great healing";

                case SpellEffect.NightSight:
                    return "night eyes";
                case SpellEffect.Protection:
                    return "protection";
                case SpellEffect.Agility:
                    return "agility";
                case SpellEffect.Cunning:
                    return "cunning";
                case SpellEffect.Strength:
                    return "strength";
                case SpellEffect.Bless:
                    return "blessings";
                case SpellEffect.Reflect:
                    return "spell reflection";
                case SpellEffect.Invis:
                    return "invisibility";

                case SpellEffect.Teleportation:
                    return "teleportation";

                default:
                    return "errorness";
            }
        }

        public static int GetChargesFor(SpellEffect e)
        {
            switch (e)
            {
                case SpellEffect.Clumsy:
                case SpellEffect.Feeblemind:
                case SpellEffect.MagicArrow:
                    return Utility.Random(25) + 15;
                case SpellEffect.Weaken:
                case SpellEffect.Harm:
                    return Utility.Random(15) + 10;
                case SpellEffect.Paralyze:
                    return Utility.Random(15) + 5;
                case SpellEffect.Fireball:
                case SpellEffect.Curse:
                    return Utility.Random(10) + 10;
                case SpellEffect.ManaDrain:
                case SpellEffect.Lightning:
                    return Utility.Random(10) + 5;

                case SpellEffect.ItemID:
                    return Utility.Random(150) + 25;
                case SpellEffect.MiniHeal:
                    return Utility.Random(15) + 10;
                case SpellEffect.GHeal:
                    return Utility.Random(15) + 5;

                case SpellEffect.NightSight:
                    return Utility.Random(25) + 25;
                case SpellEffect.Protection:
                case SpellEffect.Agility:
                case SpellEffect.Cunning:
                case SpellEffect.Strength:
                    return Utility.Random(20) + 10;
                case SpellEffect.Bless:
                    return Utility.Random(15) + 10;
                case SpellEffect.Reflect:
                    return Utility.Random(15) + 1;
                case SpellEffect.Invis:
                case SpellEffect.Teleportation:
                    return Utility.Random(10) + 1;

                case SpellEffect.None:
                default:
                    return 0;
            }
        }

        public static bool IsRepeatingEffect(SpellEffect effect)
        {
            int e = (int)effect;
            if (e >= 0 && e < m_Effects.Length && m_Effects[e] != null)
                return m_Effects[e].RepeatingEffect;
            else
                return false;
        }


        public static bool InvokeEffect(SpellEffect effect, Mobile from, Item target)
        {
            int e = (int)effect;
            if (e >= 0 && e < m_Effects.Length && m_Effects[e] != null)
            {
                if (m_Effects[e].DoEffect(from, target))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public virtual bool RepeatingEffect
        {
            get
            {
                return true;
            }
        }

        public virtual bool DoEffect(Mobile from, Mobile target)
        {
            return false;
        }

        public virtual bool DoEffect(Mobile from, Item target)
        {
            return false;
        }
    }

    public class HealEffect : SpellCastEffect
    {
        private int m_Min, m_Max;
        public HealEffect(int min, int max)
        {
            m_Min = min;
            m_Max = max;
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (target.Alive && target.Hits < target.HitsMax && from.CanBeBeneficial(target) && from.CanSee(target) && from.InLOS(target))
            {
                from.DoBeneficial(target);
                SpellHelper.Turn(from, target);

                int toHeal = Utility.RandomMinMax(m_Min, m_Max);
                if (from != target && from.NetState != null)
                    from.NetState.Send(new MessageLocalizedAffix(Serial.MinusOne, -1, MessageType.Label, 0x3B2, 3, 1008158, "", AffixType.Append | AffixType.System, (target.Hits + toHeal > target.HitsMax ? target.HitsMax - target.Hits : toHeal).ToString(), ""));
                target.Heal(toHeal);

                target.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
                target.PlaySound(0x202);
                return true;
            }

            return false;
        }

        public override bool RepeatingEffect
        {
            get
            {
                return false;
            }
        }
    }

    public class ParalyzeEffect : SpellCastEffect
    {
        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (target.Frozen || target.Paralyzed)
            {
                return false; // The target is already frozen.
            }
            else if (target.Alive && from.CanBeHarmful(target) && from.CanSee(target) && from.InLOS(target))
            {
                from.DoHarmful(target);
                SpellHelper.Turn(from, target);
                SpellHelper.CheckReflect(5, from, ref target);

                target.Damage(1);

                double duration = Utility.Random(10) + 15;
                if (target.CheckSkill(SkillName.MagicResist, 10, 50)) // check resisted easy
                    duration *= 0.5;

                target.Paralyze(TimeSpan.FromSeconds(duration));
                target.PlaySound(0x204);
                target.FixedEffect(0x376A, 6, 1);
                return true;
            }

            return false;
        }
    }

    public class ItemIDEffect : SpellCastEffect
    {
        public override bool RepeatingEffect
        {
            get
            {
                return false;
            }
        }

        public override bool DoEffect(Mobile from, Item target)
        {
            return true;
        }
    }

    public class ManaDrainEffect : SpellCastEffect
    {
        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (from.CanSee(target) && from.CanBeHarmful(target) && from.InLOS(target) && target.Alive)
            {
                ManaDrainSpell.DoEffect(from, target, (int)SpellCircle.Fifth);

                return true;
            }

            return false;
        }
    }

    public class DamageEffect : SpellCastEffect
    {
        private SpellCircle m_Circle;

        private int m_Snd, m_EffIID, m_Spd, m_Eff, m_Exp, m_ExpSnd;

        public DamageEffect(SpellCircle circle, int snd, int effiid, int spd, int eff, int exp, int expsnd)
        {
            m_Circle = circle;

            m_Snd = snd;
            m_EffIID = effiid;
            m_Spd = spd;
            m_Eff = eff;
            m_Exp = exp;
            m_ExpSnd = expsnd;
        }

        public DamageEffect(SpellCircle circle, int snd, int effiid, int spd, int dur, int eff, EffectLayer layer)
        {
            m_Circle = circle;

            m_Snd = snd;
            m_EffIID = effiid;
            m_Spd = spd;
            m_Exp = dur;
            m_Eff = eff;
            m_ExpSnd = -(((int)layer) + 1);
        }

        public override bool RepeatingEffect
        {
            get
            {
                return false;
            }
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (from.CanSee(target) && from.CanBeHarmful(target) && from.InLOS(target) && target.Alive)
            {
                from.DoHarmful(target);

                Mobile source = from;
                SpellHelper.Turn(source, target);

                SpellHelper.CheckReflect(2, ref source, ref target);

                //double damage = Utility.RandomMinMax( m_Min, m_Max );
                double damage = MagerySpell.GetPreUORDamage(m_Circle);
                double scalar;

                double atkDmgVal = (100.0 * ((int)m_Circle)) / 7.0;
                double dfnDefVal = target.Skills[SkillName.MagicResist].Value;

                if (dfnDefVal > atkDmgVal)
                    scalar = 1.0 + (atkDmgVal - dfnDefVal) / 200;
                else
                    scalar = 1.0 + (atkDmgVal - dfnDefVal) / 500;

                if (scalar < 0.0)
                    scalar = 0.01;

                if (target is BaseCreature)
                    ((BaseCreature)target).AlterDamageScalarFrom(from, ref scalar);

                if (from is BaseCreature)
                    ((BaseCreature)from).AlterDamageScalarTo(target, ref scalar);

                target.Region.SpellDamageScalar(from, target, ref scalar);

                if (MagerySpell.CheckResistedStatic(target, from, (int)m_Circle))
                {
                    damage *= 0.5;
                    target.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                damage *= scalar;
                if (damage < 0)
                    damage = 0;

                if (m_ExpSnd >= 0)
                {
                    source.MovingParticles(target, m_EffIID, m_Spd, 0, false, true, m_Eff, m_Exp, m_ExpSnd);
                }
                else
                {
                    if (m_EffIID == 0 && m_Spd == 0)
                        target.BoltEffect(m_Eff);
                    else
                        target.FixedParticles(m_EffIID, m_Spd, m_Exp, m_Eff, (EffectLayer)(-(m_ExpSnd - 1)));
                }

                if (m_Snd > 0)
                    source.PlaySound(m_Snd);

                SpellHelper.Damage(TimeSpan.Zero, target, from, damage, 0, 100, 0, 0, 0);

                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public class CurseEffect : SpellCastEffect
    {
        private StatType m_Stat;
        int m_Snd, m_EffIID, m_EffSpd, m_Dur, m_Eff;
        EffectLayer m_ELayer;

        public CurseEffect(StatType stat, int sound, int eid, int speed, int duraction, int eff, EffectLayer layer)
        {
            m_Stat = stat;
            m_Snd = sound;
            m_EffIID = eid;
            m_EffSpd = speed;
            m_Dur = duraction;
            m_Eff = eff;
            m_ELayer = layer;
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            bool result = false;
            if (!SpellHelper.HasStatEffect(target, m_Stat) && from.CanSee(target) && from.InLOS(target) && target.Alive && from.CanBeHarmful(target))
            {
                from.DoHarmful(target);
                SpellHelper.Turn(from, target);

                SpellHelper.CheckReflect(2, from, ref target);

                if (m_Stat != StatType.All)
                {
                    SpellHelper.AddStatCurse(null, target, m_Stat);
                    result = true;
                }
                else
                {
                    if (from.Player && target.Player && !CurseSpell.UnderEffect( target ))
                    {
                        SpellHelper.AddStatCurse(null, target, StatType.Str); SpellHelper.DisableSkillCheck = true;
                        SpellHelper.AddStatCurse(null, target, StatType.Dex);
                        SpellHelper.AddStatCurse(null, target, StatType.Int); SpellHelper.DisableSkillCheck = false;

                        TimeSpan duration = SpellHelper.GetDuration(from, target);
                        CurseSpell.PutUnderEffect( target, Timer.DelayCall(duration, new TimerStateCallback(CurseSpell.RemoveEffect), target) );
                        target.UpdateResistances();

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

                if (result)
                {
                    target.Paralyzed = false;

                    target.FixedParticles(m_EffIID, m_EffSpd, m_Dur, m_Eff, m_ELayer);
                    target.PlaySound(m_Snd);
                }
            }

            return result;
        }
    }

    public class NightSightEffect : SpellCastEffect
    {
        public NightSightEffect()
        {
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (target.BeginAction(typeof(LightCycle)))
            {
                new LightCycle.NightSightTimer(target).Start();
                int level = (int)Math.Abs(LightCycle.DungeonLevel * (0.5 + Utility.RandomDouble() * 0.5));

                if (level > 25 || level < 0)
                    level = 25;

                target.LightLevel = level;

                target.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                target.PlaySound(0x1E3);

                return true;
            }

            return false;
        }
    }

    public class ProtectionEffect : SpellCastEffect
    {
        public ProtectionEffect()
        {
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (from.CanBeBeneficial(target) && target.Alive && from.Alive && from.InLOS(target) && !ProtectionSpell.Registry.ContainsKey(target))
            {
                from.DoBeneficial(target);
                SpellHelper.Turn(from, target);

                int val = Utility.Random(5) + 6;

                target.VirtualArmorMod += val;
                ProtectionSpell.Registry.Add(target, val);
                new ProtectionSpell.InternalTimer(TimeSpan.FromSeconds( 60 ),target, val).Start();

                target.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                target.PlaySound(0x1F7);

                return true;
            }

            return false;
        }
    }

    public class TeleportationEffect : SpellCastEffect
    {
        public TeleportationEffect()
        {
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            from.BeginTarget(12, true, Targeting.TargetFlags.None, new TargetCallback(OnTarget));
            return true;
        }

        public override bool RepeatingEffect
        {
            get
            {
                return false;
            }
        }

        private void OnTarget(Mobile from, object targeted)
        {
            IPoint3D p = targeted as IPoint3D;
            Map map = from.Map;

            SpellHelper.GetSurfaceTop(ref p);

            Point3D to = new Point3D(p);

            if (Server.Misc.WeightOverloading.IsOverloaded(from))
            {
                from.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
            }
            else if (map == null || !map.CanFit(p.X, p.Y, p.Z, 16))
            {
                from.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (SpellHelper.CheckMulti(to, map))
            {
                from.SendLocalizedMessage(501942); // That location is blocked.
            }
            else
            {
                SpellHelper.Turn(from, p);

                Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);

                Regions.HouseRegion destRgn = Region.Find(to, from.Map) as Regions.HouseRegion;
                if (destRgn != null && destRgn.House != null)
                {
                    if ((from.Region == destRgn && destRgn.House is Multis.LargePatioHouse) || (from.Region != destRgn && destRgn.House.IsInside(to, 15)))
                    {
                        from.SendLocalizedMessage(501942); // That location is blocked.
                        return;
                    }
                }

                from.Location = to;
                from.ProcessDelta();

                Effects.SendLocationParticles(EffectItem.Create(to, from.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);

                from.PlaySound(0x1FE);
            }
        }
    }

    public class InvisibilityEffect : SpellCastEffect
    {
        public InvisibilityEffect()
        {
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (!target.Hidden && from.InLOS(target) && target.Alive && from.CanBeBeneficial(target))
            {
                from.DoBeneficial(target);
                SpellHelper.Turn(from, target);

                Effects.SendLocationParticles(EffectItem.Create(new Point3D(target.X, target.Y, target.Z + 16), target.Map, EffectItem.DefaultDuration), 0x376A, 10, 15, 5045);
                target.PlaySound(0x3C4);

                target.Hidden = true;

                InvisibilitySpell.RemoveTimer(target);

                Timer t = new InvisibilitySpell.InternalTimer(target, TimeSpan.FromSeconds(30 + Utility.Random(91)));
                t.Start();
                InvisibilitySpell.m_Table[target] = t;

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class MagicReflectEffect : SpellCastEffect
    {
        public MagicReflectEffect()
        {
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (target.MagicDamageAbsorb > 0)
            {
                return false;
            }
            else
            {
                target.MagicDamageAbsorb = 1;

                target.FixedParticles(0x375A, 10, 15, 5037, EffectLayer.Waist);
                target.PlaySound(0x1E9);

                return true;
            }
        }
    }

    public class BlessEffect : SpellCastEffect
    {
        private StatType m_Stat;
        int m_Snd, m_EffIID, m_EffSpd, m_Dur, m_Eff;
        EffectLayer m_ELayer;

        public BlessEffect(StatType stat, int sound, int eid, int speed, int duraction, int eff, EffectLayer layer)
        {
            m_Stat = stat;
            m_Snd = sound;
            m_EffIID = eid;
            m_EffSpd = speed;
            m_Dur = duraction;
            m_Eff = eff;
            m_ELayer = layer;
        }

        public override bool DoEffect(Mobile from, Mobile target)
        {
            if (!SpellHelper.HasStatEffect(from, m_Stat) && from.InLOS(target) && target.Alive && from.CanBeBeneficial(target))
            {
                from.DoBeneficial(target);
                SpellHelper.Turn(from, target);

                if (m_Stat != StatType.All)
                {
                    SpellHelper.AddStatBonus(null, target, m_Stat);
                }
                else
                {
                    SpellHelper.AddStatBonus(null, target, StatType.Str); SpellHelper.DisableSkillCheck = true;
                    SpellHelper.AddStatBonus(null, target, StatType.Dex);
                    SpellHelper.AddStatBonus(null, target, StatType.Int); SpellHelper.DisableSkillCheck = false;
                }

                target.FixedParticles(m_EffIID, m_EffSpd, m_Dur, m_Eff, m_ELayer);
                target.PlaySound(m_Snd);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

