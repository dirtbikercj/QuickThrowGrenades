using EFT;
using System;
using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using DrakiaXYZ.VersionChecker;
using QuickThrowGrenades.Patches;
using Comfort.Common;

namespace QuickThrowGrenades
{
    [BepInPlugin("com.dirtbikercj.QuickThrowGrenades", "Quick Throw Grenades", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 26535;

        internal static ConfigEntry<bool> Enable;

        internal static Player MainPlayer = null;

        private void Awake()
        {
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            Enable = Config.Bind(
               "General",
               "Quick Throw Grenades",
               false,
               "Instantly throw grenades");

            new GrenadePatch().Enable();
        }

        private void Update()
        {
            if (Singleton<GameWorld>.Instantiated && MainPlayer == null)
            {
                MainPlayer = Singleton<GameWorld>.Instance.MainPlayer;
            }
        }
    }
}
