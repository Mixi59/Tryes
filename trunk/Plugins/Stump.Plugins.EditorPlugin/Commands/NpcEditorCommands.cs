﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Commands.Patterns;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Commands;
using Stump.Server.WorldServer.Commands.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.Items.Shops;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Plugins.EditorPlugin.Commands
{
    public class NpcEditorCommands : SubCommandContainer
    {
        public NpcEditorCommands()
        {
            Aliases = new[] { "nedit", "npcedit" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Npc editor";
        }
    }

    public class NpcSpawnCommand : SubCommand
    {
        public NpcSpawnCommand()
        {
            Aliases = new[] { "spawn" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Spawn a npc";
            ParentCommand = typeof(NpcEditorCommands);
            AddParameter("npc", "npc", "Npc Template id", converter: ParametersConverter.NpcTemplateConverter);
            AddParameter("map", "map", "Map id", isOptional: true, converter: ParametersConverter.MapConverter);
            AddParameter<short>("cell", "cell", "Cell id", isOptional: true);
            AddParameter("direction", "dir", "Direction", isOptional: true, converter: ParametersConverter.GetEnumConverter<DirectionsEnum>());
        }

        public override void Execute(TriggerBase trigger)
        {
            var template = trigger.Get<NpcTemplate>("npc");
            ObjectPosition position = null;

            if (trigger.IsArgumentDefined("map") && trigger.IsArgumentDefined("cell") && trigger.IsArgumentDefined("direction"))
            {
                var map = trigger.Get<Map>("map");
                var cell = trigger.Get<short>("cell");
                var direction = trigger.Get<DirectionsEnum>("direction");

                position = new ObjectPosition(map, cell, direction);
            }
            else if (trigger is GameTrigger)
            {
                position = ( trigger as GameTrigger ).Character.Position;
            }

            if (position == null)
            {
                trigger.ReplyError("Position of npc is not defined");
                return;
            }

            WorldServer.Instance.IOTaskPool.AddMessage(
                () =>
                    {
                        var spawn = new NpcSpawn()
                                        {
                                            Template = template,
                                            MapId = position.Map.Id,
                                            CellId = position.Cell.Id,
                                            Direction = position.Direction,
                                            Look = template.Look
                                        };

                        NpcManager.Instance.AddNpcSpawn(spawn);
                        var npc = position.Map.SpawnNpc(spawn);

                        trigger.Reply("Npc {0} spawned", npc.Id);
                    });
        }
    }

    public class NpcUnSpawnCommand : SubCommand
    {
        public NpcUnSpawnCommand()
        {
            Aliases = new[] { "unspawn" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Unspawn the npc by the given contextual id";
            ParentCommand = typeof(NpcEditorCommands);
            AddParameter<sbyte>("npcid", "npc", "Npc Contextual id");
            AddParameter("map", "map", "Npc Map", isOptional: true, converter: ParametersConverter.MapConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            var npcId = trigger.Get<sbyte>("npcid");
            Npc npc = null;

            if (trigger.IsArgumentDefined("map"))
            {
                npc = trigger.Get<Map>("map").GetActor<Npc>(npcId);
            }
            else if (trigger is GameTrigger)
            {
                npc = ( trigger as GameTrigger ).Character.Map.GetActor<Npc>(npcId);
            }
            else
            {
                trigger.ReplyError("Npc Map must be defined !");
                return;
            }


            if (npc.Spawn == null)
            {
                trigger.ReplyError("This npc is not saved in database");
            }

            WorldServer.Instance.IOTaskPool.AddMessage(
                () =>
                {
                    npc.Map.UnSpawnNpc(npc);
                    NpcManager.Instance.RemoveNpcSpawn(npc.Spawn);
                    trigger.Reply("Npc {0} unspawned", npcId);
                });
        }
    }

    public class NpcShopCommand : AddRemoveSubCommand
    {
        public NpcShopCommand()
        {
            Aliases = new[] { "shop" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Manage npc shop";
            ParentCommand = typeof(NpcEditorCommands);
            AddParameter("npc", "npc", "Npc Template id", converter: ParametersConverter.NpcTemplateConverter);
            AddParameter("item", "item", converter: ParametersConverter.ItemTemplateConverter);
            AddParameter<float>("customprice", "price", isOptional:true);
        }

        public override void ExecuteAdd(TriggerBase trigger)
        {
            var template = trigger.Get<NpcTemplate>("npc");
            var itemTemplate = trigger.Get<ItemTemplate>("item");
            var shop = template.Actions.OfType<NpcBuySellAction>().FirstOrDefault();

            WorldServer.Instance.IOTaskPool.AddMessage(
                () =>
                {
                    if (shop == null)
                    {
                        shop = new NpcBuySellAction()
                        {
                            Template = template
                        };

                        NpcManager.Instance.AddNpcAction(shop);
                        template.Actions.Add(shop);
                    }

                    var item = new NpcItem()
                    {
                        Item = itemTemplate,
                        CustomPrice = trigger.IsArgumentDefined("customprice") ? (float?)trigger.Get<float>("customprice") : null,
                        NpcShopId = (int)shop.Id,
                        BuyCriterion = string.Empty
                    };

                    item.Save();
                    shop.Items.Add(item);
                    trigger.Reply("Item '{0}' added to '{1}'s' shop", itemTemplate.Name, template.Name);
                });
        }

        public override void ExecuteRemove(TriggerBase trigger)
        {
            var template = trigger.Get<NpcTemplate>("npc");
            var itemTemplate = trigger.Get<ItemTemplate>("item");
            var shop = template.Actions.OfType<NpcBuySellAction>().FirstOrDefault();

            if (shop == null)
            {
                return;
            }

            WorldServer.Instance.IOTaskPool.AddMessage(
                () =>
                {
                    var items = shop.Items.Where(entry => entry.ItemId == itemTemplate.Id).ToArray();

                    foreach (NpcItem item in items)
                    {
                        item.Delete();
                        shop.Items.Remove(item);
                        trigger.Reply("Item '{0}' removed from '{1}'s' shop", itemTemplate.Name, template.Name);
                    }
                });
        }
    }

}