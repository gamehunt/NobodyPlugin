using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;
using UnityEngine;

namespace Nobody.Commands
{
    [CommandHandler(typeof(CommandSystem.RemoteAdminCommandHandler))]
    internal class SpawnNobodyCommand : ICommand
    {
        public string Command => "spawn_nobody";

        public string[] Aliases => new string[] { "s_nb" };

        public string Description => "Spawns nobody";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("nobody.spawn"))
            {
                response = "Access denied!";
                return false;
            }
            if (arguments.Count == 0)
            {
                Methods.Clear();
                Methods.SpawnNobody();
            }
            else if (arguments.Count == 1)
            {
                Player p = Player.Get(arguments.At(0));
                if (p != null)
                {
                    Methods.MakeNobody(p, Methods.GetNobodySpawn(Map.IsLCZDecontaminated));
                }
                else
                {
                    response = "Player not found!";
                    return false;
                }
            }
            else
            {
                Vector3 default_pos = Methods.GetNobodySpawn(Map.IsLCZDecontaminated);
                if (sender is RemoteAdmin.PlayerCommandSender ply)
                {
                    Player s = Player.Get(ply.PlayerId);
                    if (s != null) {
                        default_pos = s.Position;
                    }
                }
                Player p = Player.Get(arguments.At(0));
                Vector3 spawn = UnityEngine.Object.FindObjectsOfType<Rid>().Where(rd => rd.id == arguments.At(1)).FirstOrDefault()?.gameObject.transform.position ?? default_pos;
                if (p != null)
                {
                    Methods.MakeNobody(p, Methods.GetNobodySpawn(Map.IsLCZDecontaminated));
                }
                else
                {
                    response = "Player not found!";
                    return false;
                }
            }
            response = "Spawned Nobody";
            return true;
        }
    }
}