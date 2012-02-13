﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Commands.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class InfoMapCommand : SubCommand
    {
        public InfoMapCommand()
        {
            Aliases = new [] { "map" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Give informations about a map";
            ParentCommand = typeof(InfoCommand);
            AddParameter("map", "map", "Target map", isOptional: true, converter: ParametersConverter.MapConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            Map map = null;
            if (trigger.IsArgumentDefined("map"))
            {
                map = trigger.Get<Map>("map");
            }
            else if (trigger is GameTrigger)
            {
                map = ( trigger as GameTrigger ).Character.Map;
            }

            if (map == null)
            {
                trigger.ReplyError("Map not defined");
                return;
            }

            trigger.Reply("Map {0} (relative : {1})", trigger.Bold(map.Id), trigger.Bold(map.RelativeId));
            trigger.Reply("X:{0}, Y:{1}", trigger.Bold(map.Position.X), trigger.Bold(map.Position.Y));
            trigger.Reply("SubArea:{0}, Area:{1}, SuperArea:{2}", trigger.Bold(map.SubArea.Id), trigger.Bold(map.Area.Id), trigger.Bold(map.SuperArea.Id));
            var actors = map.GetActors<RolePlayActor>().ToArray();
            trigger.Reply("Actors ({0}) :", trigger.Bold(actors.Length));

            foreach (var actor in actors)
            {
                trigger.Reply("{0} : {1}", trigger.Bold(actor.GetType().Name), actor);
            }
        }
    }

    public class InfoAreaCommand : SubCommand
    {
        public InfoAreaCommand()
        {
            Aliases = new[] { "area" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Give informations about an area";
            ParentCommand = typeof(InfoCommand);
            AddParameter("area", "area", "Target area", isOptional: true, converter: ParametersConverter.AreaConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            Area area = null;
            if (trigger.IsArgumentDefined("area"))
            {
                area = trigger.Get<Area>("area");
            }
            else if (trigger is GameTrigger)
            {
                area = ( trigger as GameTrigger ).Character.Area;
            }

            if (area == null)
            {
                trigger.ReplyError("Area not defined");
                return;
            }

            trigger.Reply("Area {0}", trigger.Bold(area.Id));
            trigger.Reply("Enabled : {0}", trigger.Bold(area.IsRunning));
            trigger.Reply("Objects : {0}", trigger.Bold(area.ObjectCount));
            trigger.Reply("Update interval : {0}ms", trigger.Bold(area.UpdateDelay));
            trigger.Reply("AvgUpdateTime : {0}ms", trigger.Bold((int)area.AverageUpdateTime));
            trigger.Reply("LastUpdate : {0}", trigger.Bold(area.LastUpdateTime));
        }
    }

    public class InfoCharacterCommand : SubCommand
    {
        public InfoCharacterCommand()
        {
            Aliases = new[] { "character", "char", "player" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Give informations about a player";
            ParentCommand = typeof(InfoCommand);
            AddParameter("target", "t", "Targeted player", isOptional: true, converter: ParametersConverter.CharacterConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            Character character = null;
            if (trigger.IsArgumentDefined("target"))
            {
                character = trigger.Get<Character>("target");
            }
            else if (trigger is GameTrigger)
            {
                character = ( trigger as GameTrigger ).Character;
            }

            if (character == null)
            {
                trigger.ReplyError("Target not defined");
                return;
            }

            trigger.Reply("{0} ({1})", trigger.Bold(character.Name), trigger.Bold(character.Id));
            trigger.Reply("Account : {0} ({1}) - {2}", trigger.Bold(character.Client.Account.Login), trigger.Bold(character.Client.Account.Id), trigger.Bold(character.Client.Account.Role));
            trigger.Reply("Level : {0}", trigger.Bold(character.Level));
            trigger.Reply("Map : {0}, Cell : {1}, Direction : {2}", trigger.Bold(character.Map.Id), trigger.Bold(character.Cell.Id), trigger.Bold(character.Direction));
            trigger.Reply("Items : {0}", character.Inventory.Count);
        }
    }
}