using BepInEx;
using System.ComponentModel;

namespace zedmenu.Patches
{
    [Description(zedmenu.PluginInfo.Description)]
    [BepInPlugin(zedmenu.PluginInfo.GUID, zedmenu.PluginInfo.Name, zedmenu.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }
    }
}
