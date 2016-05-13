﻿using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Exchanges.Craft
{
    public class MultiRuneCrafter : Crafter
    {
        public MultiRuneCrafter(BaseCraftDialog exchange, Character character)
            : base(exchange, character)
        {
        }

        protected override void OnReadyStatusChanged(bool isready)
        {
            base.OnReadyStatusChanged(isready);
            ReadyToApply = false;
        }
    }
}