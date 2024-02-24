using BepInEx;
using ExitGames.Client.Photon;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using zedmenu.Classes;
using zedmenu.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static zedmenu.Menu.Buttons;
using static zedmenu.Settings;
using UnityEngine.XR;

namespace zedmenu.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static void Prefix()
        {
            // Initialize Menu
            try
            {
                bool toOpen = (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) || (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton);
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen)
                    {
                        CreateMenu();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if ((toOpen))
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                    }
                    else
                    {
                        UnityEngine.Object.Destroy(menu, Time.deltaTime);
                        menu = null;

                        UnityEngine.Object.Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            // Constant
            try
            {
                // Pre-Execution
                fpsObject.text = "";

                // Execute Enabled mods
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            {
                hasRemovedThisFrame = false;

                try
                {
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/motdscreen").GetComponent<MeshRenderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/Wall Monitors Screens/wallmonitorcanyon").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/Wall Monitors Screens/wallmonitorcosmetics").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/Wall Monitors Screens/wallmonitorcave").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/Wall Monitors Screens/wallmonitorforest").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/Wall Monitors Screens/wallmonitorskyjungle").GetComponent<Renderer>().material = OrangeUI;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/campgroundstructure/scoreboard/REMOVE board").GetComponent<Renderer>().material = OrangeUI;

                    //GameObject.Find("Mountain/UI/Text/monitor").GetComponent<Renderer>().material = OrangeUI;
                    //GameObject.Find("skyjungle/UI/-- Clouds PhysicalComputer UI --/monitor (1)").GetComponent<Renderer>().material = OrangeUI;
                    //GameObject.Find("TreeRoom/TreeRoomInteractables/UI/-- PhysicalComputer UI --/monitor").GetComponent<Renderer>().material = OrangeUI;
                    //GameObject.Find("Beach/BeachComputer/UI FOR BEACH COMPUTER/Text/monitor").GetComponent<Renderer>().material = OrangeUI;
                }
                catch (Exception exception)
                {
                    UnityEngine.Debug.LogError(string.Format("iiMenu <b>COLOR ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                }
                try
                {
                    OrangeUI.color = GetBGColor(0f);
                    BulletMat.color = Color.yellow;
                    GameObject motdText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/motd");
                    Text motdTC = motdText.GetComponent<Text>();
                    RectTransform motdTRC = motdText.GetComponent<RectTransform>();
                    motdTC.text = "zed's menu";
                    GameObject myfavorite = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/motd/motdtext");
                    Text motdTextB = myfavorite.GetComponent<Text>();
                    motdTextB.text = @"zedmenu v"+PluginInfo.Version + "\n" + "this menu prob sucks ngl" + "\n" + "this menu is designed to be oldschool menus. (2021-2022)" + "\n" + "dont expect much from it";
                }
                catch { }

                rightPrimary = ControllerInputPoller.instance.rightControllerPrimaryButton || UnityInput.Current.GetKey(KeyCode.E);
                rightSecondary = ControllerInputPoller.instance.rightControllerSecondaryButton || UnityInput.Current.GetKey(KeyCode.R);
                leftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton || UnityInput.Current.GetKey(KeyCode.F);
                leftSecondary = ControllerInputPoller.instance.leftControllerSecondaryButton || UnityInput.Current.GetKey(KeyCode.G);
                leftGrab = ControllerInputPoller.instance.leftGrab || UnityInput.Current.GetKey(KeyCode.LeftBracket);
                rightGrab = ControllerInputPoller.instance.rightGrab || UnityInput.Current.GetKey(KeyCode.RightBracket);
                leftTrigger = ControllerInputPoller.TriggerFloat(XRNode.LeftHand);
                rightTrigger = ControllerInputPoller.TriggerFloat(XRNode.RightHand);
                if (UnityInput.Current.GetKey(KeyCode.Minus))
                {
                    leftTrigger = 1f;
                }
                if (UnityInput.Current.GetKey(KeyCode.Equals))
                {
                    rightTrigger = 1f;
                }
                shouldBePC = UnityInput.Current.GetKey(KeyCode.E) || UnityInput.Current.GetKey(KeyCode.R) || UnityInput.Current.GetKey(KeyCode.F) || UnityInput.Current.GetKey(KeyCode.G) || UnityInput.Current.GetKey(KeyCode.LeftBracket) || UnityInput.Current.GetKey(KeyCode.RightBracket) || UnityInput.Current.GetKey(KeyCode.Minus) || UnityInput.Current.GetKey(KeyCode.Equals) || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed;

            }
        }

        // Functions

        public static void NightTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }

        public static void RPCProtection()
        {
            if (hasRemovedThisFrame == false)
            {
                hasRemovedThisFrame = true;
                {
                    RaiseEventOptions options = new RaiseEventOptions();
                    options.CachingOption = EventCaching.RemoveFromRoomCache;
                    options.TargetActors = new int[1] { PhotonNetwork.LocalPlayer.ActorNumber };
                    RaiseEventOptions optionsdos = options;
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(200, null, optionsdos, SendOptions.SendReliable);
                }
                {
                    GorillaNot.instance.rpcErrorMax = int.MaxValue;
                    GorillaNot.instance.rpcCallLimit = int.MaxValue;
                    GorillaNot.instance.logErrorMax = int.MaxValue;
                    // GorillaGameManager.instance.maxProjectilesToKeepTrackOfPerPlayer = int.MaxValue;

                    PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                    PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig);
                    PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
                    PhotonNetwork.RemoveRPCsInGroup(int.MaxValue);
                    PhotonNetwork.SendAllOutgoingCommands();
                    GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void FakeName(string PlayerName)
        {
            try
            {
                GorillaComputer.instance.currentName = PlayerName;
                PhotonNetwork.LocalPlayer.NickName = PlayerName;
                GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                GorillaComputer.instance.savedName = PlayerName;
                PlayerPrefs.SetString("playerName", PlayerName);
                PlayerPrefs.SetString("GorillaLocomotion.PlayerName", PlayerName);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }

        public static void ChangeName(string PlayerName)
        {
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                    {
                        GorillaComputer.instance.currentName = PlayerName;
                        PhotonNetwork.LocalPlayer.NickName = PlayerName;
                        GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                        GorillaComputer.instance.savedName = PlayerName;
                        PlayerPrefs.SetString("playerName", PlayerName);
                        PlayerPrefs.Save();

                        ChangeColor(GorillaTagger.Instance.offlineVRRig.playerColor);
                    }
                    else
                    {
                        isUpdatingValues = true;
                        valueChangeDelay = Time.time + 0.5f;
                        changingName = true;
                        nameChange = PlayerName;
                    }
                }
                else
                {
                    GorillaComputer.instance.currentName = PlayerName;
                    PhotonNetwork.LocalPlayer.NickName = PlayerName;
                    GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                    GorillaComputer.instance.savedName = PlayerName;
                    PlayerPrefs.SetString("playerName", PlayerName);
                    PlayerPrefs.Save();

                    ChangeColor(GorillaTagger.Instance.offlineVRRig.playerColor);
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }

        public static void ChangeColor(Color color)
        {
            if (PhotonNetwork.InRoom)
            {
                if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                {
                    PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                    PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                    PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

                    //GorillaTagger.Instance.offlineVRRig.mainSkin.material.color = color;
                    GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                    PlayerPrefs.Save();

                    GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
                    RPCProtection();
                }
                else
                {
                    isUpdatingValues = true;
                    valueChangeDelay = Time.time + 0.5f;
                    changingColor = true;
                    colorChange = color;
                }
            }
            else
            {
                PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

                //GorillaTagger.Instance.offlineVRRig.mainSkin.material.color = color;
                GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                PlayerPrefs.Save();

                //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
                //RPCProtection();
            }
        }


        public static Color GetBGColor(float offset)
        {
            Color oColor = new Color32(120, 255, 180, 255);

            //return oColor;
            return Color.black;
        }


        public static void CreateMenu()
        {
            // Menu Holder
                menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
                menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);
                drawhold = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(drawhold.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(drawhold.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(drawhold.GetComponent<Renderer>());
            drawhold.transform.localScale = Vector3.one;

            // Menu Background
                menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
                menuBackground.transform.parent = menu.transform;
                menuBackground.transform.rotation = Quaternion.identity;
                menuBackground.transform.localScale = menuSize;
                menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

                ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
                colorChanger.colorInfo = backgroundColor;
                colorChanger.Start();

            // Canvas
                canvasObject = new GameObject();
                canvasObject.transform.parent = menu.transform;
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
                canvasObject.AddComponent<GraphicRaycaster>();
                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

            // Title and FPS
                Text text = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                text.font = currentFont;
                text.text = PluginInfo.Name + " <color=grey>[</color>" + (pageNumber + 1).ToString() + "<color=grey>]</color>";
                text.fontSize = 1;
                text.color = new Color32(190, 255, 190, 255);
                text.supportRichText = true;
                text.fontStyle = FontStyle.Normal;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.28f, 0.05f);
                component.position = new Vector3(0.06f, 0f, 0.165f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (fpsCounter)
                {
                    fpsObject = new GameObject
                    {
                        transform =
                    {
                        parent = canvasObject.transform
                    }
                    }.AddComponent<Text>();
                    fpsObject.font = currentFont;
                    fpsObject.text = " ";
                    fpsObject.color = textColors[0];
                    fpsObject.fontSize = 1;
                    fpsObject.supportRichText = true;
                    fpsObject.fontStyle = FontStyle.Normal;
                    fpsObject.alignment = TextAnchor.MiddleCenter;
                    fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                    fpsObject.resizeTextForBestFit = true;
                    fpsObject.resizeTextMinSize = 0;
                    RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                    component2.localPosition = Vector3.zero;
                    component2.sizeDelta = new Vector2(0.28f, 0.02f);
                    component2.position = new Vector3(0.06f, 0f, 0.135f);
                    component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
            lastobj = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            lastobj.font = currentFont;
            lastobj.text = lastCommand;
            lastobj.fontSize = 1;
            lastobj.color = textColors[0];
            lastobj.supportRichText = true;
            lastobj.fontStyle = FontStyle.Normal;
            lastobj.alignment = TextAnchor.MiddleCenter;
            lastobj.resizeTextForBestFit = true;
            lastobj.resizeTextMinSize = 0;
            RectTransform ocm2p = text.GetComponent<RectTransform>();
            ocm2p.localPosition = new Vector3(0,-0.9f,0);
            ocm2p.sizeDelta = new Vector2(0.28f, 0.05f);
            ocm2p.position = new Vector3(0.06f, 0f, 0.165f);
            ocm2p.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            // Buttons
            // Disconnect
            if (disconnectButton)
                    {
                        GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        if (!UnityInput.Current.GetKey(KeyCode.Q))
                        {
                            disconnectbutton.layer = 2;
                        }
                        UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
                        disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                        disconnectbutton.transform.parent = menu.transform;
                        disconnectbutton.transform.rotation = Quaternion.identity;
                        disconnectbutton.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
                        disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
                        disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                        disconnectbutton.AddComponent<Classes.Button>().relatedText = "Disconnect";

                        colorChanger = disconnectbutton.AddComponent<ColorChanger>();
                        colorChanger.colorInfo = buttonColors[0];
                        colorChanger.Start();

                        Text discontext = new GameObject
                        {
                            transform =
                            {
                                parent = canvasObject.transform
                            }
                        }.AddComponent<Text>();
                        discontext.text = "Disconnect";
                        discontext.font = currentFont;
                        discontext.fontSize = 1;
                        discontext.color = textColors[0];
                        discontext.alignment = TextAnchor.MiddleCenter;
                        discontext.resizeTextForBestFit = true;
                        discontext.resizeTextMinSize = 0;

                        RectTransform rectt = discontext.GetComponent<RectTransform>();
                        rectt.localPosition = Vector3.zero;
                        rectt.sizeDelta = new Vector2(0.2f, 0.03f);
                        rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
                        rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                    }

                // Page Buttons
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                    {
                        gameObject.layer = 2;
                    }
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    gameObject.transform.parent = menu.transform;
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                    gameObject.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
                    gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                    gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";

                    colorChanger = gameObject.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = buttonColors[0];
                    colorChanger.Start();

                    text = new GameObject
                    {
                        transform =
                        {
                            parent = canvasObject.transform
                        }
                    }.AddComponent<Text>();
                    text.font = currentFont;
                    text.text = "<";
                    text.fontSize = 1;
                    text.color = textColors[0];
                    text.alignment = TextAnchor.MiddleCenter;
                    text.resizeTextForBestFit = true;
                    text.resizeTextMinSize = 0;
                    component = text.GetComponent<RectTransform>();
                    component.localPosition = Vector3.zero;
                    component.sizeDelta = new Vector2(0.2f, 0.03f);
                    component.localPosition = new Vector3(0.064f, 0.195f, 0f);
                    component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                    gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                    {
                        gameObject.layer = 2;
                    }
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    gameObject.transform.parent = menu.transform;
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                    gameObject.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
                    gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                    gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";

                    colorChanger = gameObject.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = buttonColors[0];
                    colorChanger.Start();

                    text = new GameObject
                    {
                        transform =
                        {
                            parent = canvasObject.transform
                        }
                    }.AddComponent<Text>();
                    text.font = currentFont;
                    text.text = ">";
                    text.fontSize = 1;
                    text.color = textColors[0];
                    text.alignment = TextAnchor.MiddleCenter;
                    text.resizeTextForBestFit = true;
                    text.resizeTextMinSize = 0;
                    component = text.GetComponent<RectTransform>();
                    component.localPosition = Vector3.zero;
                    component.sizeDelta = new Vector2(0.2f, 0.03f);
                    component.localPosition = new Vector3(0.064f, -0.195f, 0f);
                    component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                // Mod Buttons
                    ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
                    for (int i = 0; i < activeButtons.Length; i++)
                    {
                        CreateButton(i * 0.1f, activeButtons[i]);
                    }
        }

        public static void CreateButton(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            else
            {
                colorChanger.colorInfo = buttonColors[0];
            }
            colorChanger.Start();

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = textColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Normal;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                    bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
                    GameObject.Destroy(bg, Time.deltaTime);
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            } else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                } else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                    //    lastCommand = target.toolTip;
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
                            {
                                try { target.method.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Menu.Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

        // Variables
            // Important
                // Objects
                    public static GameObject menu;
                    public static GameObject drawhold;
                    public static GameObject menuBackground;   
                    public static GameObject reference;
                    public static GameObject canvasObject;

                    public static SphereCollider buttonCollider;
                    public static Camera TPC;
                    public static Text fpsObject;
        public static Text lastobj;

        // Data
        public static int pageNumber = 0;
            public static int buttonsType = 0;

        // the variable warehouse
        public static ControllerInputPoller Inputs = ControllerInputPoller.instance;
        public static bool lockdown = false;
        public static bool HasLoaded = false;
        public static float internetFloat = 3f;
        public static bool hasRemovedThisFrame = false;
        public static bool frameFixColliders = false;
        public static float buttonCooldown = 0f;
        public static bool noti = true;
        public static int pageButtonType = 1;
        public static float buttonOffset = 2;
        public static int fullModAmount = -1;
        public static int fontCycle = 0;
        public static bool rightHand = false;
        public static bool isRightHand = false;
        public static bool bothHands = false;
        public static bool wristThing = false;
        public static bool wristOpen = false;
        public static bool lastChecker = false;
        public static bool FATMENU = false;
        public static bool longmenu = false;
        public static bool isCopying = false;
        public static bool disorganized = false;
        public static bool hasAntiBanned = false;

        public static bool shouldBePC = false;
        public static bool rightPrimary = false;
        public static bool rightSecondary = false;
        public static bool leftPrimary = false;
        public static bool leftSecondary = false;
        public static bool leftGrab = false;
        public static bool rightGrab = false;
        public static float leftTrigger = 0f;
        public static float rightTrigger = 0f;

        public static string ownerPlayerId = "E19CE8918FD9E927";
        public static string questPlayerId = "86A10278DF9691BE";
        public static string gtxt = "<color=grey>[</color><color=#96ffb2>G</color><color=grey>]</color>";
        public static string ttxt = "<color=grey>[</color><color=#96ffb2>T</color><color=grey>]</color>";
        public static string atxt = "<color=grey>[</color><color=#96ffb2>A</color><color=grey>]</color>";
        public static string btxt = "<color=grey>[</color><color=#96ffb2>B</color><color=grey>]</color>";

        public static GameObject cam = null;
        public static GameObject canvasObj = null;
        public static AssetBundle assetBundle = null;
        public static Text fpsCount = null;
        public static Text title = null;
        public static VRRig whoCopy = null;

        public static bool plats = false;
        public static GameObject leftplat = null;
        public static GameObject rightplat = null;

        public static GameObject leftThrow = null;
        public static GameObject rightThrow = null;

        public static GameObject stickpart = null;

        public static GameObject CheckPoint = null;
        public static GameObject BombObject = null;
        public static GameObject ProjBombObject = null;

        public static GameObject airSwimPart = null;

        public static List<ForceVolume> fvol = new List<ForceVolume> { };
        public static List<GameObject> leaves = new List<GameObject> { };
        public static List<GameObject> lights = new List<GameObject> { };
        public static List<GameObject> cosmetics = new List<GameObject> { };
        public static List<GameObject> holidayobjects = new List<GameObject> { };

        public static Vector3 rightgrapplePoint;
        public static Vector3 leftgrapplePoint;
        public static SpringJoint rightjoint;
        public static SpringJoint leftjoint;
        public static bool isLeftGrappling = false;
        public static bool isRightGrappling = false;

        public static Material OrangeUI = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material BulletMat = new Material(Shader.Find("GUI/Text Shader"));
        public static Material glass = null;

        public static List<string> favorites = new List<string> { "Exit Favorite Mods" };

        public static List<GorillaNetworkJoinTrigger> triggers = new List<GorillaNetworkJoinTrigger> { };

        public static Vector3 offsetLH = Vector3.zero;
        public static Vector3 offsetRH = Vector3.zero;
        public static Vector3 offsetH = Vector3.zero;

        public static Vector3 longJumpPower = Vector3.zero;
        public static Vector2 lerpygerpy = Vector2.zero;

        public static Vector3[] lastLeft = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        public static Vector3[] lastRight = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        public static string[] letters = new string[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M"
        };
        public static int[] bones = new int[] {
            4, 3, 5, 4, 19, 18, 20, 19, 3, 18, 21, 20, 22, 21, 25, 21, 29, 21, 31, 29, 27, 25, 24, 22, 6, 5, 7, 6, 10, 6, 14, 6, 16, 14, 12, 10, 9, 7
        };

        /*public static string[] fullProjectileNames = new string[]
        {
            "SlingshotProjectile",
            "SnowballProjectile",
            "WaterBalloonProjectile",
            "LavaRockProjectile",
            "HornsSlingshotProjectile_PrefabV",
            "CloudSlingshot_Projectile",
            "CupidArrow_Projectile",
            "IceSlingshotProjectile_PrefabV Variant",
            "ElfBow_Projectile",
            "MoltenRockSlingshot_Projectile",
            "SpiderBowProjectile Variant",
            "BucketGift_Cane_Projectile Variant",
            "BucketGift_Coal_Projectile Variant",
            "BucketGift_Roll_Projectile Variant",
            "BucketGift_Round_Projectile Variant",
            "BucketGift_Square_Projectile Variant",
            "ScienceCandyProjectile Variant"
        };*/
        public static string[] menutitles = new string[]
        {
            "ZedPad",
            "ZedMenu",
            "Zed's Menu",
            "zedmenu",
            "zedpad",
            "zed menu thingy",
        };
        public static string[] fullProjectileNames = new string[]
        {
            "Snowball",
            "WaterBalloon",
            "LavaRock",
            "ThrowableGift",
            "ScienceCandy",
        };

        public static string[] fullTrailNames = new string[]
        {
            "SlingshotProjectileTrail",
            "HornsSlingshotProjectileTrail_PrefabV",
            "CloudSlingshot_ProjectileTrailFX",
            "CupidArrow_ProjectileTrailFX",
            "IceSlingshotProjectileTrail Variant",
            "ElfBow_ProjectileTrail",
            "MoltenRockSlingshotProjectileTrail",
            "SpiderBowProjectileTrail Variant",
            "none"
        };

        public static int themeType = 1;
        public static Color bgColorA = new Color32(180, 255, 200, 255);
        public static Color bgColorB = new Color32(255, 102, 0, 128);

        public static Color buttonDefaultA = new Color32(170, 85, 0, 255);
        public static Color buttonDefaultB = new Color32(170, 85, 0, 255);

        public static Color buttonClickedA = new Color32(85, 42, 0, 255);
        public static Color buttonClickedB = new Color32(85, 42, 0, 255);

        public static Color textColor = new Color32(255, 190, 125, 255);
        public static Color colorChange = Color.black;

        public static Vector3 walkPos;
        public static Vector3 walkNormal;

        public static Vector3 closePosition;

        public static Vector3 pointerOffset = new Vector3(0f, -0.1f, 0f);
        public static int pointerIndex = 0;

        public static bool noclip = false;
        public static float tagAuraDistance = 1.666f;
        public static int tagAuraIndex = 1;

        public static bool lastSlingThing = false;

        public static bool lastInRoom = false;
        public static bool lastMasterClient = false;
        public static string lastRoom = "";

        public static int platformMode = 0;
        public static int platformShape = 0;

        public static bool customSoundOnJoin = false;
        public static float partDelay = 0f;

        public static float delaythinggg = 0f;
        public static float debounce = 0f;
        public static float kgDebounce = 0f;
        public static float nameCycleDelay = 0f;
        public static float stealIdentityDelay = 0f;
        public static float beesDelay = 0f;
        public static float laggyRigDelay = 0f;
        public static float jrDebounce = 0f;
        public static float projDebounce = 0f;
        public static float projDebounceType = 0.1f;
        public static float soundDebounce = 0f;
        public static float colorChangerDelay = 0f;
        public static float teleDebounce = 0f;
        public static float splashDel = 0f;
        public static float headspazDelay = 0f;
        public static float autoSaveDelay = Time.time + 60f;

        public static bool isUpdatingValues = false;
        public static float valueChangeDelay = 0f;

        public static bool changingName = false;
        public static bool changingColor = false;
        public static string nameChange = "";

        public static int projmode = 0;
        public static int trailmode = 0;

        public static int notificationDecayTime = 1000;

        public static float oldSlide = 0f;

        public static int accessoryType = 0;
        public static int hat = 0;

        public static int soundId = 0;

        public static float red = 1f;
        public static float green = 0.5f;
        public static float blue = 0f;

        public static bool lastOwner = false;
        public static string inputText = "";
        public static string lastCommand = "";

        public static int shootCycle = 1;
        public static float ShootStrength = 19.44f;
        public static float ShootStrength2 = 0.1f;

        public static int flySpeedCycle = 1;
        public static float flySpeed = 10f;

        public static int speedboostCycle = 1;
        public static float jspeed = 7.5f;
        public static float jmulti = 1.25f;

        public static int longarmCycle = 2;
        public static float armlength = 1.25f;

        public static int nameCycleIndex = 0;

        public static bool lastprimaryhit = false;
        public static bool idiotfixthingy = false;

        public static int crashAmount = 2;

        public static bool isJoiningRandom = false;

        public static int colorChangeType = 0;
        public static bool strobeColor = false;

        public static bool AntiCrashToggle = false;
        public static bool AntiSoundToggle = false;
        public static bool AntiCheatSelf = true;
        public static bool AntiCheatAll = false;

        public static bool lastHit = false;
        public static bool lastHit2 = false;
        public static bool lastRG;

        public static bool ghostMonke = false;
        public static bool invisMonke = false;

        public static int tindex = 1;

        public static bool antiBanEnabled = false;

        public static bool lastHitL = false;
        public static bool lastHitR = false;
        public static bool lastHitLP = false;
        public static bool lastHitRP = false;
        public static bool lastHitRS = false;

        public static bool plastLeftGrip = false;
        public static bool plastRightGrip = false;
        public static bool spazLavaType = false;

        public static bool EverythingSlippery = false;
        public static bool EverythingGrippy = false;

        public static bool headspazType = false;

        public static float subThingy = 0f;

        public static float sizeScale = 1f;

        public static float turnAmnt = 0f;
        public static float TagAuraDelay = 0f;
        public static float startX = -1f;
    }
}
