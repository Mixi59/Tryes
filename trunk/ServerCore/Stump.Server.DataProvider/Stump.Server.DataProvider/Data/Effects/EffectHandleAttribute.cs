﻿
using System;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.WorldServer.Effects
{
    public class EffectHandleAttribute : Attribute
    {
        public EffectHandleAttribute(params EffectsEnum[] effects)
        {
            Effects = effects;
        }

        public EffectsEnum[] Effects
        {
            get;
            set;
        }
    }
}