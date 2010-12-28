﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace Stump.Database
{

    [AttributeDatabase(DatabaseService.WorldServer)]
    [ActiveRecord("accounts_enemies")]
    public class AccountEnemyRecord : ActiveRecordBase<AccountEnemyRecord>
    {

        [PrimaryKey(PrimaryKeyType.Native, "Id")]
        public long Id
        {
            get;
            set;
        }

        [Property("AccountId")]
        public uint AccountId
        {
            get;
            set;
        }

        [Property("EnemyAccountId")]
        public uint EnemyAccountId
        {
            get;
            set;
        }


        public WorldAccountRecord Account
        {
            get { return WorldAccountRecord.FindWorldAccountById(AccountId); }
            set { AccountId = value.Id; }
        }

        public WorldAccountRecord EnemyAccount
        {
            get { return WorldAccountRecord.FindWorldAccountById(EnemyAccountId); }
            set { EnemyAccountId = value.Id; }
        }


        public static WorldAccountRecord[] FindEnemiesByAccountId(uint accountId)
        {
            return FindAll(Restrictions.Eq("AccountId", accountId)).Select(w => w.EnemyAccount).ToArray();
        }
    }
}