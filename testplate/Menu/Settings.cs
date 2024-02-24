using zedmenu.Classes;
using UnityEngine;
using static zedmenu.Menu.Main;

namespace zedmenu
{
    internal class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient { /*colors = GetSolidGradient(Color.black)*/ };
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient { colors = GetSolidGradient(Color.HSVToRGB(0f,0f,0.02f))}, // Disabled
            new ExtGradient { colors = GetSolidGradient(Color.white)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            new Color32(227, 255, 232,255), // Disabled
            Color.black // Enabled
        };

        public static Font currentFont = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool disableNotifications = true;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1.1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;
    }
}
