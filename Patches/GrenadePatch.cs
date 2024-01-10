using Aki.Reflection.Patching;
using EFT;
using EFT.UI;
using System;
using System.Linq;
using System.Reflection;

namespace QuickThrowGrenades.Patches
{
    internal class GrenadePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethods().First(m =>
                m.Name == "SetInHands" && m.GetParameters()[0].Name == "throwWeap");
        }

        [PatchPrefix]
        private static bool Prefix(Player __instance, GrenadeClass throwWeap)
        {

#if DEBUG
            ConsoleScreen.Log($"Is Keybind enabled: {Plugin.EnableKeybind.Value}");
            ConsoleScreen.Log($"Is Keybind down: {Plugin.Keybind.Value.IsDown()}");
#endif

            // Dont use keyboard shortcut
            if (Plugin.MainPlayer != null && Plugin.Enable.Value 
                && !Plugin.EnableKeybind.Value)
            {
                __instance.SetInHandsForQuickUse(throwWeap, null);
            }

            // Use keyboard shortcut
            if (Plugin.MainPlayer != null && Plugin.Enable.Value
                && Plugin.EnableKeybind.Value && Plugin.Keybind.Value.IsDown())
            {
                __instance.SetInHandsForQuickUse(throwWeap, null);
            }

            if (Plugin.EnableKeybind.Value && !Plugin.Keybind.Value.IsDown())
            {
                return true;
            }

            return !Plugin.Enable.Value;
        }
    }
}
