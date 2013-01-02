using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Conditions;

namespace Stump.Server.WorldServer.Database.Npcs
{
    public class NpcActionRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM npcs_actions";
    }

    [TableName("npcs_actions")]
    public class NpcActionRecord : ParameterizableRecord, IAutoGeneratedRecord
    {
        private string m_condition;
        private ConditionExpression m_conditionExpression;
        private NpcTemplate m_template;

        public uint Id
        {
            get;
            set;
        }

        public int NpcId
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        [Ignore]
        public NpcTemplate Template
        {
            get { return m_template ?? (m_template = NpcManager.Instance.GetNpcTemplate(NpcId)); }
            set
            {
                m_template = value;
                NpcId = value.Id;
            }
        }


        public string Condition
        {
            get { return m_condition; }
            set
            {
                m_condition = value;
                m_conditionExpression = null;
            }
        }

        [Ignore]
        public ConditionExpression ConditionExpression
        {
            get
            {
                if (string.IsNullOrEmpty(Condition) || Condition == "null")
                    return null;

                return m_conditionExpression ?? (m_conditionExpression = ConditionExpression.Parse(Condition));
            }
            set
            {
                m_conditionExpression = value;
                Condition = value.ToString();
            }
        }

        public NpcAction GenerateAction()
        {
            return DiscriminatorManager<NpcAction>.Instance.Generate(Type, this);
        }
    }
}