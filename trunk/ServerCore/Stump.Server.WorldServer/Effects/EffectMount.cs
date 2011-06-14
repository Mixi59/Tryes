﻿
using System;
using Stump.DofusProtocol.Classes;
using EffectMountEx = Stump.DofusProtocol.D2oClasses.EffectInstanceMount;


namespace Stump.Server.WorldServer.Effects
{
    [Serializable]
    public class EffectMount : EffectBase
    {
        protected double m_date;
        protected int m_modelId;
        protected int m_mountId;

        public EffectMount(uint id, int mountid, double date, int modelid)
            : base(id)
        {
            m_mountId = mountid;
            m_date = date;
            m_modelId = modelid;
        }

        public override int ProtocoleId
        {
            get { return 179; }
        }

        public override object[] GetValues()
        {
            return new object[] {m_mountId, m_date, m_modelId};
        }

        public override ObjectEffect ToNetworkEffect()
        {
            return new ObjectEffectMount((uint) EffectId, (uint) m_mountId, m_date, (uint) m_modelId);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectMount))
                return false;
            var b = obj as EffectMount;
            return base.Equals(obj) && m_mountId == b.m_mountId && m_date == b.m_date && m_modelId == b.m_modelId;
        }

        public static bool operator ==(EffectMount a, EffectMount b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EffectMount a, EffectMount b)
        {
            return !(a == b);
        }

        public bool Equals(EffectMount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.m_date.Equals(m_date) && other.m_modelId == m_modelId &&
                   other.m_mountId == m_mountId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result*397) ^ m_date.GetHashCode();
                result = (result*397) ^ m_modelId;
                result = (result*397) ^ m_mountId;
                return result;
            }
        }
    }
}