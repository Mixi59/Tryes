using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tool;
using Stump.Server.WorldServer.Database.I18n;

namespace Stump.Server.WorldServer.Database.Spells
{
    [ActiveRecord("spells")]
    [D2OClass("Spell", "com.ankamagames.dofus.datacenter.spells")]
    public sealed class SpellTemplate : WorldBaseRecord<SpellTemplate>
    {
        private string m_description;
        private string m_name;
        private IList<SpellLevelTemplate> m_spellLevels;

        [D2OField("id")]
        [PrimaryKey(PrimaryKeyType.Assigned, "Id")]
        public int Id
        {
            get;
            set;
        }

        [D2OField("nameId")]
        [Property("NameId")]
        public uint NameId
        {
            get;
            set;
        }

        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        [D2OField("descriptionId")]
        [Property("DescriptionId")]
        public uint DescriptionId
        {
            get;
            set;
        }


        public string Description
        {
            get { return m_description ?? (m_description = TextManager.Instance.GetText(NameId)); }
        }

        [D2OField("typeId")]
        [Property("TypeId")]
        public uint TypeId
        {
            get;
            set;
        }

        [D2OField("scriptParams")]
        [Property("ScriptParams")]
        public String ScriptParams
        {
            get;
            set;
        }

        [D2OField("scriptParamsCritical")]
        [Property("ScriptParamsCritical")]
        public String ScriptParamsCritical
        {
            get;
            set;
        }

        [D2OField("scriptId")]
        [Property("ScriptId")]
        public int ScriptId
        {
            get;
            set;
        }

        [D2OField("scriptIdCritical")]
        [Property("ScriptIdCritical")]
        public int ScriptIdCritical
        {
            get;
            set;
        }

        [D2OField("iconId")]
        [Property("IconId")]
        public uint IconId
        {
            get;
            set;
        }

        [HasMany(typeof (SpellLevelTemplate))]
        public IList<SpellLevelTemplate> SpellLevels
        {
            get { return m_spellLevels ?? (m_spellLevels = new List<SpellLevelTemplate>()); }
            set { m_spellLevels = value; }
        }

        [D2OField("useParamCache")]
        [Property("UseParamCache")]
        public Boolean UseParamCache
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Name);
        }
    }
}