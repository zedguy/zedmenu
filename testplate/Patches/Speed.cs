using HarmonyLib;
using System;
using GorillaLocomotion;
using UnityEngine;
using static zedmenu.Menu.Main;

namespace Bark.Patches
{
    [HarmonyPatch(typeof(GorillaTagManager))]
    [HarmonyPatch("LocalPlayerSpeed", MethodType.Normal)]
    internal class TagSpeedPatch
    {
        private static void Postfix(GorillaTagManager __instance, ref float[] __result)
        {
            try
            {
                if (!GetIndex("Speed Boost").enabled) return;

                for (int i = 0; i < __result.Length; i++)
                    __result[i] *= jmulti;
            }
            catch { }
        }
    }

    [HarmonyPatch(typeof(GorillaGameManager))]
    [HarmonyPatch("LocalPlayerSpeed", MethodType.Normal)]
    internal class GenericSpeedPatch
    {
        private static void Postfix(GorillaGameManager __instance, ref float[] __result)
        {
            try
            {
                if (!GetIndex("Speed Boost").enabled) return;

                for (int i = 0; i < __result.Length; i++)
                    __result[i] *= jmulti;
            }
            catch { }
        }
    }

    [HarmonyPatch(typeof(GorillaBattleManager))]
    [HarmonyPatch("LocalPlayerSpeed", MethodType.Normal)]
    internal class BattleSpeedPatch
    {
        private static void Postfix(GorillaBattleManager __instance, ref float[] __result)
        {
            try
            {
                if (!GetIndex("Speed Boost").enabled) return;

                for (int i = 0; i < __result.Length; i++)
                    __result[i] *= jmulti;
            }
            catch { }
        }
    }

    [HarmonyPatch(typeof(GorillaHuntManager))]
    [HarmonyPatch("LocalPlayerSpeed", MethodType.Normal)]
    internal class HuntSpeedPatch
    {
        private static void Postfix(GorillaHuntManager __instance, ref float[] __result)
        {
            try
            {
                if (!GetIndex("Speed Boost").enabled) return;

                for (int i = 0; i < __result.Length; i++)
                    __result[i] *= jmulti;
            }
            catch { }
        }
    }
}