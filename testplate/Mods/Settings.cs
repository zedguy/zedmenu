using static zedmenu.Menu.Main;
using static zedmenu.Settings;

namespace zedmenu.Mods
{
    internal class SettingsMods
    {
        public static void EnterSettings()
        {
            buttonsType = 1;
            pageNumber = 0;
        }

        public static void MenuSettings()
        {
            buttonsType = 2;
            pageNumber = 0;
        }

        public static void MovementSettings()
        {
            buttonsType = 3;
            pageNumber = 0;
        }

        public static void ProjectileSettings()
        {
            buttonsType = 4;
            pageNumber = 0;
        }

        public static void HitSettings()
        {
            buttonsType = 5;
            pageNumber = 0;
        }

        public static void Disguises()
        {
            buttonsType = 6;
            pageNumber = 0;
        }

        public static void Trolls()
        {
            buttonsType = 7;
            pageNumber = 0;
        }
        public static void visual()
        {
            buttonsType = 8;
            pageNumber = 0;
        }

        public static void bannable()
        {
            buttonsType = 9;
            pageNumber = 0;
        }

        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }

        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }

        public static void EnableNotifications()
        {
            disableNotifications = false;
        }

        public static void DisableNotifications()
        {
            disableNotifications = true;
        }

        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }

        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }
    }
}
