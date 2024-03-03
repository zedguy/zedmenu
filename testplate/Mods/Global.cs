using System.Reflection;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static zedmenu.Menu.Main;
using static zedmenu.Classes.RigManager;
using ExitGames.Client.Photon;
using UnityEngine.InputSystem;
using GorillaNetworking;
using PlayFab;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using zedmenu.Notifications;
using GorillaTag;

namespace zedmenu.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
        }
        public static void RandomColorSnowballs()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/SnowballRightAnchor").transform.Find("LMACF.").GetComponent<SnowballThrowable>().randomizeColor = true;
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/SnowballLeftAnchor").transform.Find("LMACE.").GetComponent<SnowballThrowable>().randomizeColor = true;
        }

        public static void NoRandomColorSnowballs()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/SnowballRightAnchor").transform.Find("LMACF.").GetComponent<SnowballThrowable>().randomizeColor = false;
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/SnowballLeftAnchor").transform.Find("LMACE.").GetComponent<SnowballThrowable>().randomizeColor = false;
        }
        public static void SlingshotHelper()
        {
            GameObject slingy = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/Slingshot Anchor/Slingshot");
            if (slingy != null)
            {
                Slingshot yay = slingy.GetComponent<Slingshot>();
                yay.itemState = TransferrableObject.ItemStates.State2;
            }
        }
        public static float lastTime = -1f;
        public static bool antibanworked = false;

        public static void AntiBan()
        {
            if (Time.time > lastTime + 2f)
            {
                if (antibanworked)
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTIBAN</color><color=grey>]</color> <color=white>The anti ban has been enabled successfully.</color>");
                    antibanworked = false;
                    string gamemode = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Replace(GorillaComputer.instance.currentQueue, GorillaComputer.instance.currentQueue + "MODDEDMODDED");
                    ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable
                    {
                        { "gameMode", gamemode }
                    };
                    PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                    GetIndex("Anti Ban").enabled = false;
                }
                else
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> <color=white>The anti ban failed to load. This could be a result of bad internet.</color>");
                }
            }
            if (Time.time > lastTime + 5f)
            {
                lastTime = Time.time;
                NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTIBAN</color><color=grey>]</color> <color=white>Enabling anti ban...</color>");
                if (!PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
                {
                    PlayFabClientAPI.ExecuteCloudScript(new PlayFab.ClientModels.ExecuteCloudScriptRequest
                    {
                        FunctionName = "RoomClosed",
                        FunctionParameter = new
                        {
                            GameId = PhotonNetwork.CurrentRoom.Name,
                            Region = Regex.Replace(PhotonNetwork.CloudRegion, "[^a-zA-Z0-9]", "").ToUpper(),
                            UserId = PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length + 1)].UserId,
                            ActorNr = PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length + 1)],
                            ActorCount = PhotonNetwork.ViewCount,
                            AppVersion = PhotonNetwork.AppVersion
                        },
                    }, result =>
                    {
                        antibanworked = true;
                    }, null);
                }
            }
        }

        public static bool IsModded()
        {
            return PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED");
        }

        public static void FastMaster()
        {
            if (!IsModded() || !PhotonNetwork.InRoom)
            {
                AntiBan();
            }
            else
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }

        public static void ForceEruptLava()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    FastMaster();
                }
            }
            else
            {
                InfectionLavaController controller = InfectionLavaController.Instance;
                System.Type type = controller.GetType();

                FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

                object reliableState = fieldInfo.GetValue(controller);

                FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
                stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Erupting);

                FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
                stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

                fieldInfo.SetValue(controller, reliableState);
            }
        }

        public static void ForceUneruptLava()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    FastMaster();
                }
            }
            else
            {
                InfectionLavaController controller = InfectionLavaController.Instance;
                System.Type type = controller.GetType();

                FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

                object reliableState = fieldInfo.GetValue(controller);

                FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
                stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Draining);

                FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
                stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

                fieldInfo.SetValue(controller, reliableState);
            }
        }
        public static void InfectionGamemode()
        {
            if (!IsModded())
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    AntiBan();
                }
            }
            else
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("gameMode", "forestDEFAULTMODDED_MODDED_INFECTION");
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);
            }
        }

        public static void CasualGamemode()
        {
            if (!IsModded())
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    AntiBan();
                }
            }
            else
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("gameMode", "forestDEFAULTMODDED_MODDED_CASUALCASUAL");
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);
            }
        }

        public static void HuntGamemode()
        {
            if (!IsModded())
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    AntiBan();
                }
            }
            else
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("gameMode", "forestDEFAULTMODDED_MODDED_HUNTHUNT");
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);
            }
        }

        public static void BattleGamemode()
        {
            if (!IsModded())
            {
                if (!GetIndex("Disable Auto Anti Ban").enabled)
                {
                    AntiBan();
                }
            }
            else
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("gameMode", "forestDEFAULTMODDED_MODDED_BATTLEPAINTBRAWL");
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);
            }
        }
        public static void CopyIDGun()
        {
            if (rightGrab)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);
                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if (rightTrigger > 0.5f)
                {
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                    {
                        GUIUtility.systemCopyBuffer = GetPlayerFromVRRig(possibly).UserId;
                    }
                }
            }
        }
        public static void DestroyGun()
        {
            if (rightGrab)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if (rightTrigger > 0.5f )
                {
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                    {
                        Photon.Realtime.Player player = GetPlayerFromVRRig(possibly);
                        PhotonNetwork.CurrentRoom.StorePlayer(player);
                        PhotonNetwork.CurrentRoom.Players.Remove(player.ActorNumber);
                        PhotonNetwork.OpRemoveCompleteCacheOfPlayer(player.ActorNumber);
                        kgDebounce = Time.time + 0.5f;
                    }
                }
            }
        }

        public static void RapidFireSlingshot()
        {
            if (rightPrimary)
            {
                GameObject slingy = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/Slingshot Anchor/Slingshot");
                if (slingy != null)
                {
                    Slingshot yay = slingy.GetComponent<Slingshot>();
                    yay.itemState = TransferrableObject.ItemStates.State2;
                    System.Type type = yay.GetType();
                    FieldInfo fieldInfo = type.GetField("minTimeToLaunch", BindingFlags.NonPublic | BindingFlags.Instance);
                    fieldInfo.SetValue(yay, -1f);
                    ControllerInputPoller.instance.rightControllerIndexFloat = lastSlingThing ? 1f : 0f;
                    lastSlingThing = !lastSlingThing;
                }
            }
        }
    }
}
