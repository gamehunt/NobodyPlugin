using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace Nobody
{
    public class EventHandlers
    {
        public void OnRoundStart()
        {
            if (Plugin.Instance.Config.NobodyAllowSpawnOnStart)
            {
                if (Plugin.Instance.Random.Next(0, 100) < Plugin.Instance.Config.NobodyChance)
                {
                    Timing.CallDelayed(1f, () =>
                    {
                        List<Player> ds = Player.Get(RoleType.ClassD).ToList();
                        Methods.MakeNobody(ds[Plugin.Instance.Random.Next(0, ds.Count)], Methods.GetNobodySpawn(Map.IsLCZDecontaminated));
                    });
                }
            }
            Methods.TrySpawnNobody();
        }

        public void OnRoundEnd()
        {
            Methods.Clear();
        }
    }
}