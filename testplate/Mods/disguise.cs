using UnityEngine;
using static zedmenu.Menu.Main;
using static zedmenu.Settings;

namespace zedmenu.Mods
{
    internal class Disguise
    {
        public static void SpazAccessories()
        {
            int rando = UnityEngine.Random.Range(1, 9);

            if (rando == 1)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivationWithHand(false);
            }
            if (rando == 2)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivationWithHand(false);
            }
            if (rando == 3)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivationWithHand(false);
            }

            if (rando == 4)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeLeftButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 5)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeRightItem").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }

            if (rando == 6)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardobeHatButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 7)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeFaceButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 8)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeBadgeButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 9)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Wardrobe/WardrobeHoldableButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
        }
        public static void BecomePBBV()
        {
            ChangeName("PBBV");
            ChangeColor(new Color32(230, 127, 102, 255));
        }

        public static void BecomeJ3VU()
        {
            ChangeName("J3VU");
            ChangeColor(Color.green);
        }

        public static void BecomeECHO()
        {
            ChangeName("ECHO");
            ChangeColor(new Color32(0, 150, 255, 255));
        }

        public static void BecomeDAISY09()
        {
            ChangeName("DAISY09");
            ChangeColor(new Color32(255, 81, 231, 255));
        }

        public static void BecomeVoid()
        {
            ChangeName("VOID");
            ChangeColor(new Color32(0,0,0,0));
        }
    }
}
