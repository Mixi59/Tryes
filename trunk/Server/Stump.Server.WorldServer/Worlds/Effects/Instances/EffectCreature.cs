﻿using System;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Worlds.Effects.Instances
{
    [Serializable]
    public class EffectCreature : EffectBase
    {
        protected short m_monsterfamily;

        public EffectCreature()
        {
            
        }

        public EffectCreature(short id, short monsterfamily)
            : base(id)
        {
            m_monsterfamily = monsterfamily;
        }

        public EffectCreature(EffectInstanceCreature effect)
            : base(effect)
        {
            m_monsterfamily = (short) effect.monsterFamilyId;
        }

        public override int ProtocoleId
        {
            get { return 71; }
        }

        public override byte SerializationIdenfitier
        {
            get
            {
                return 2;
            }
        }

        public short MonsterFamily
        {
            get { return m_monsterfamily; }
        }

        public override object[] GetValues()
        {
            return new object[] {m_monsterfamily};
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectCreature(Id, MonsterFamily);
        }

        protected override void InternalSerialize(ref System.IO.BinaryWriter writer)
        {
            base.InternalSerialize(ref writer);

            writer.Write(MonsterFamily);
        }

        protected override void InternalDeserialize(ref System.IO.BinaryReader reader)
        {
            base.InternalDeserialize(ref reader);

            m_monsterfamily = reader.ReadInt16();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectCreature))
                return false;
            return base.Equals(obj) && m_monsterfamily == (obj as EffectCreature).m_monsterfamily;
        }

        public static bool operator ==(EffectCreature a, EffectCreature b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EffectCreature a, EffectCreature b)
        {
            return !(a == b);
        }

        public bool Equals(EffectCreature other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.m_monsterfamily == m_monsterfamily;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ m_monsterfamily.GetHashCode();
            }
        }
    }
}