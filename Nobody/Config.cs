using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Nobody
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("Minimum time for Nobody spawn")]
        public int NobodyMinTime { get; set; } = 180;
        [Description("Maximum time for Nobody spawn")]
        public int NobodyMaxTime { get; set; } = 300;
        [Description("Nobody spawn chance")]
        public int NobodyChance { get; set; } = 45;
        [Description("Nobody health")]
        public int NobodyHealth { get; set; } = 150;
        [Description("Nobody's 556 ammo")]
        public uint Nobody5Ammo { get; set; } = 200;
        [Description("Nobody's 762 ammo")]
        public uint Nobody7Ammo { get; set; } = 200;
        [Description("Nobody's 9 ammo")]
        public uint Nobody9Ammo { get; set; } = 200;
        [Description("Nobody's start items")]
        public ItemType[] NobodyStartItems { get; set; } = new ItemType[]{
            ItemType.KeycardO5,
            ItemType.Adrenaline,
            ItemType.Medkit,
            ItemType.Medkit,
            ItemType.GunE11SR,
            ItemType.Radio,
        };
        [Description("Nobody's possible spawns")]
        public string[] NobodyPossibleSpawns { get; set; } = new string[] { };
        [Description("Nobody's possible spawns after decontamination")]
        public string[] NobodyPossibleSpawnsAfterDecontamination { get; set; } = new string[] { };
        [Description("Can Nobody spawn multiple times during the round?")]
        public bool NobodyAllowMultipleSpawns { get; set; } = false;
        [Description("Can Nobody spawn on round start?")]
        public bool NobodyAllowSpawnOnStart { get; set; } = false;
        [Description("Broadcast sent to the nobody on it's spawn")]
        public string NobodyPrivateBroadcast { get; set; } = "You are <color=green>Nobody</color>! Follow your own tasks and do whatever u want";
        [Description("Broadcast sent to the everyone on Nobody spawns")]
        public string NobodyGlobalBroadcast { get; set; } = "";
        [Description("Console message sent to the nobody on it's spawn")]
        public string NobodyCmdMessage { get; set; } = "";
        [Description("Cassie announcement on Nobody spawn")]
        public string NobodyCassieAnnouncement { get; set; } = "";
    }
}