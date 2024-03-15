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
using zedmenu.Classes;
using UnityEngine.UIElements;
using Photon.Voice.Unity.UtilityScripts;
using POpusCodec.Enums;
using OVR.OpenVR;

namespace zedmenu.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
            pageNumber = 0;
        }

        public static void autorpc()
        {
            if (Time.time > rpctime)
            {
                RPCProtection();
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(84, true, 0.03f);
                rpctime = Time.time + rpclearcool;
            }
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

        public static void DisableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
        }

        public static void EnableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
        }

        public static void TrapModders()
        {
            if (IsModded() && PhotonNetwork.InRoom)
            {
                foreach (VRRig p in GorillaParent.instance.vrrigs)
                {
                    if (p && p != null && p != GorillaTagger.Instance.offlineVRRig)
                    {
                        PhotonNetwork.RaiseEvent(69, new object[2] { p.headMesh.transform.position, p.headMesh.transform.rotation }, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
                        PhotonNetwork.RaiseEvent(69, new object[2] { p.transform.position, p.transform.rotation }, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
                    }
                }
            }
        }

        public static void TagGun()
        {
            if (rightGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);
                if (shouldBePC)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out Ray, 100);
                }

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                NewPointer.GetComponent<Renderer>().material.color = (isCopying || (rightTrigger > 0.5f || Mouse.current.leftButton.isPressed)) ? buttonClickedA : buttonDefaultA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = isCopying ? whoCopy.transform.position : Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);

                GameObject line = new GameObject("Line");
                LineRenderer liner = line.AddComponent<LineRenderer>();
                liner.material.shader = Shader.Find("GUI/Text Shader");
                liner.startColor = GetBGColor(0f);
                liner.endColor = GetBGColor(0.5f);
                liner.startWidth = 0.025f;
                liner.endWidth = 0.025f;
                liner.positionCount = 2;
                liner.useWorldSpace = true;
                liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                liner.SetPosition(1, isCopying ? whoCopy.transform.position : Ray.point);
                UnityEngine.Object.Destroy(line, Time.deltaTime);

                if (isCopying && whoCopy != null)
                {
                    if (!whoCopy.mainSkin.material.name.Contains("fected"))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                        GorillaTagger.Instance.offlineVRRig.transform.position = whoCopy.transform.position - new Vector3(0f, 3f, 0f);
                        GorillaTagger.Instance.myVRRig.transform.position = whoCopy.transform.position - new Vector3(0f, 3f, 0f);

                        GorillaLocomotion.Player.Instance.rightControllerTransform.position = whoCopy.transform.position;

                        GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                        l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                        GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                        r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                        l.GetComponent<Renderer>().material.color = bgColorA;
                        r.GetComponent<Renderer>().material.color = bgColorA;

                        UnityEngine.Object.Destroy(l, Time.deltaTime);
                        UnityEngine.Object.Destroy(r, Time.deltaTime);
                    }
                    else
                    {
                        isCopying = false;
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
                if (rightTrigger > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig && !possibly.mainSkin.material.name.Contains("fected"))
                    {
                        if (PhotonNetwork.LocalPlayer.IsMasterClient)
                        {
                            foreach (GorillaTagManager gorillaTagManager in GameObject.FindObjectsOfType<GorillaTagManager>())
                            {
                                if (!gorillaTagManager.currentInfected.Contains(RigManager.GetPlayerFromVRRig(possibly)))
                                {
                                    gorillaTagManager.currentInfected.Add(RigManager.GetPlayerFromVRRig(possibly));
                                }
                            }
                        }
                        else
                        {
                            isCopying = true;
                            whoCopy = possibly;
                        }
                    }
                }
            }
            else
            {
                if (isCopying)
                {
                    isCopying = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
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
                    GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/EquipmentInteractor").GetComponent<EquipmentInteractor>().ReleaseRightHand();
                    lastSlingThing = !lastSlingThing;
                }
            }
        }
        public static void LucyTest()
        {
            GameObject.Find("Environment Objects/PersistentObjects_Prefab/Halloween Ghost").SetActive(true);
            GameObject.Find("Environment Objects/PersistentObjects_Prefab/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>().photonView.ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            GameObject.Find("Environment Objects/PersistentObjects_Prefab/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>().photonView.OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            GameObject.Find("Environment Objects/PersistentObjects_Prefab/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>().currentState = HalloweenGhostChaser.ChaseState.Gong;
            GameObject.Find("Environment Objects/PersistentObjects_Prefab/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>().UpdateState();

        }
        public static void PrimDisc()
        {
            if (leftPrimary && PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
            }
        }
        public static void ChangeRPCSpeed()
        {

            rpcclearcyucle++;
            string[] speedNames = new string[] { "10s", "30s", "60s", "120s" };
            if (rpcclearcyucle > speedNames.Length - 1)
            {
                rpcclearcyucle = 0;
            }

            float[] jspeedamounts = new float[] { 10f, 30f, 60f, 120f };
            rpclearcool = jspeedamounts[rpcclearcyucle];
            GetIndex("Change RPC Clear Speed").overlapText = "Change RPC Clear Speed <color=grey>[</color><color=#96ffb2>" + speedNames[rpcclearcyucle] + "</color><color=grey>]</color>";
        }
    }
}
