using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nobody
{
    public class Methods
    {
        public static Vector3 GetNobodySpawn(bool afterDecont)
        {
            Vector3 spawn_pos = Vector3.zero;
            string[] spawns = afterDecont ? Plugin.Instance.Config.NobodyPossibleSpawnsAfterDecontamination : Plugin.Instance.Config.NobodyPossibleSpawns;
            if (spawns.Length > 0)
            {
                string type = Plugin.Instance.Config.NobodyPossibleSpawns[Plugin.Instance.Random.Next(0, spawns.Length)];
                Rid r = UnityEngine.Object.FindObjectsOfType<Rid>().Where(rd => rd.id == type).FirstOrDefault();
                if (r != null)
                {
                    spawn_pos = r.gameObject.transform.position;
                }
            }

            if (spawn_pos == Vector3.zero)
            {
                List<Rid> r = UnityEngine.Object.FindObjectsOfType<Rid>().Where(rd => rd.transform.position.y < -200f || rd.transform.position.y > 200f || !Map.IsLCZDecontaminated).ToList();
                spawn_pos = r[Plugin.Instance.Random.Next(0, r.Count)].gameObject.transform.position;
            }
            return spawn_pos;
        }

        private static CoroutineHandle handle;

        public static void MakeNobody(Player nobody, Vector3 spawn_pos)
        {
            nobody.Role = RoleType.Tutorial;
            nobody.MaxHealth = Plugin.Instance.Config.NobodyHealth;
            nobody.Health = nobody.MaxHealth;
            foreach (ItemType item in Plugin.Instance.Config.NobodyStartItems)
            {
                nobody.AddItem(item);
            }
            nobody.Ammo[(int)AmmoType.Nato556] = Plugin.Instance.Config.Nobody5Ammo;
            nobody.Ammo[(int)AmmoType.Nato762] = Plugin.Instance.Config.Nobody7Ammo;
            nobody.Ammo[(int)AmmoType.Nato9] = Plugin.Instance.Config.Nobody9Ammo;

            Timing.CallDelayed(0.5f, () =>
            {
                nobody.ReferenceHub.playerMovementSync.AddSafeTime(1.0f);
                nobody.Position = spawn_pos + new Vector3(0, 0.75f, 0);
            });

            nobody.Broadcast(5, Plugin.Instance.Config.NobodyPrivateBroadcast);
            if (Plugin.Instance.Config.NobodyCmdMessage.Length != 0)
            {
                nobody.SendConsoleMessage(Plugin.Instance.Config.NobodyCmdMessage, "yellow");
            }
            Map.Broadcast(5, Plugin.Instance.Config.NobodyGlobalBroadcast);

            if (Plugin.Instance.Config.NobodyCassieAnnouncement.Length != 0)
            {
                Cassie.Message(Plugin.Instance.Config.NobodyCassieAnnouncement, false, false);
            }
        }

        public static void SpawnNobody()
        {
            if (Warhead.IsDetonated)
            {
                return;
            }
            List<Player> spects = Player.Get(RoleType.Spectator).ToList();
            if (spects.Count == 0)
            {
                TrySpawnNobody();
                return;
            }
            Player nobody = spects[Plugin.Instance.Random.Next(0, spects.Count())];

            MakeNobody(nobody, Methods.GetNobodySpawn(Map.IsLCZDecontaminated));

            if (Plugin.Instance.Config.NobodyAllowMultipleSpawns)
            {
                TrySpawnNobody();
            }
        }

        public static void TrySpawnNobody()
        {
            if (Plugin.Instance.Random.Next(0, 100) < Plugin.Instance.Config.NobodyChance)
            {
                handle = Timing.CallDelayed(Plugin.Instance.Random.Next(Plugin.Instance.Config.NobodyMinTime, Plugin.Instance.Config.NobodyMaxTime), () => SpawnNobody());
            }
        }

        public static void Clear()
        {
            Timing.KillCoroutines(handle);
        }
    }
}