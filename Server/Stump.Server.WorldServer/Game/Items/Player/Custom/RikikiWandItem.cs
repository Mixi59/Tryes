﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId(ItemIdEnum.BAGUETTE_RIKIKI_15990)]
    public class RikikiWandItem : BasePlayerItem
    {
        public RikikiWandItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        /*public override ActorLook UpdateItemSkin(ActorLook characterLook)
        {
            var look = characterLook.GetRiderLook() ?? characterLook;

            look.SetScales(Owner.Breed.GetLook(Owner.Sex).Scales.ToArray());

            if (IsEquiped())
                look.Rescale(0.8);

            if (characterLook.GetRiderLook() != null)
                characterLook.SetRiderLook(look);
            else
                characterLook = look;

            return characterLook;
        }*/

        public override ActorLook UpdateItemSkin(ActorLook characterLook)
        {
            (characterLook.GetRiderLook() ?? characterLook).SetScales(Owner.Breed.GetLook(Owner.Sex).Scales.ToArray());

            if (IsEquiped())
            {
                characterLook.Rescale(0.8);

                foreach (var subLook in characterLook.SubLooks)
                    subLook.Look.Rescale(0.8);
            }

            return characterLook;
        }
    }
}