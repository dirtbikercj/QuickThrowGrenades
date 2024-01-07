using Aki.Reflection.Patching;
using EFT;
using EFT.UI;
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
            if (Plugin.MainPlayer != null && Plugin.Enable.Value)
            {
                __instance.SetInHandsForQuickUse(throwWeap, null);
            }
            return !Plugin.Enable.Value;
        }
    }
}
