

// Generated on 12/12/2013 16:57:35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class AbstractSocialGroupInfos
    {
        public const short Id = 416;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        
        public AbstractSocialGroupInfos()
        {
        }
        
        
        public virtual void Serialize(IDataWriter writer)
        {
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
        }
        
        public virtual int GetSerializationSize()
        {
            return 0;
            ;
        }
        
    }
    
}