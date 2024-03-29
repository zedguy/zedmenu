﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using zedmenu.Classes;
using static zedmenu.Menu.Main;
using static zedmenu.Settings;

namespace zedmenu.Mods
{
    internal class Projectiles
    {
        public static void BetaFireProjectile(string projectileName, Vector3 position, Vector3 velocity, Color color, bool noDelay = false)
        {
            ControllerInputPoller.instance.leftControllerGripFloat = 1f;
            GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(lhelp, 0.1f);
            lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * GorillaTagger.Instance.offlineVRRig.scaleFactor;
            lhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            lhelp.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            int[] overrides = new int[]
            {
                32,
                204,
                231,
                240,
                249
            };
            lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = overrides[Array.IndexOf(fullProjectileNames, projectileName)];
            lhelp.GetComponent<Renderer>().enabled = false;
            if (Time.time > projDebounce)
            {
                try
                {
                    Vector3 startpos = position;
                    Vector3 charvel = velocity;

                    Vector3 oldVel = GorillaTagger.Instance.GetComponent<Rigidbody>().velocity;
                    //SnowballThrowable fart = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/SnowballRightAnchor").transform.Find("LMACF.").GetComponent<SnowballThrowable>();
                    string[] name2 = new string[]
                    {
                        "LMACE.",
                        "LMAEX.",
                        "LMAGD.",
                        "LMAHQ.",
                        "LMAIE."
                    };
                    SnowballThrowable fart = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/" + fullProjectileNames[System.Array.IndexOf(fullProjectileNames, projectileName)] + "LeftAnchor").transform.Find(name2[System.Array.IndexOf(fullProjectileNames, projectileName)]).GetComponent<SnowballThrowable>();
                    Vector3 oldPos = fart.transform.position;
                    fart.randomizeColor = true;
                    fart.transform.position = startpos;
                    //fart.projectilePrefab.tag = projectileName;
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = charvel;
                    GorillaTagger.Instance.offlineVRRig.SetThrowableProjectileColor(true, color);
                    GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/EquipmentInteractor").GetComponent<EquipmentInteractor>().ReleaseLeftHand();
                    //fart.OnRelease(null, null);
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = oldVel;
                    fart.transform.position = oldPos;
                    fart.randomizeColor = false;
                    //fart.projectilePrefab.tag = "SnowballProjectile";
                }
                catch { /*NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> <color=white>Grab a snowball in your left hand and put it in the snow.</color>");*/ }
                if (projDebounceType > 0f && !noDelay)
                {
                    projDebounce = Time.time + projDebounceType;
                }
            }
        }

        public static void SysFireProjectile(string projectilename, string trailname, Vector3 position, Vector3 velocity, float r, float g, float b, bool bluet, bool oranget, bool noDelay = false)
        {
            //if (true)//GetIndex("Legacy Projectiles").enabled)
            //{
            //GameObject stupid = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + projectilename + "(Clone)");
            BetaFireProjectile(projectilename, position, velocity, new Color(r, g, b, 1f), noDelay);
            /*}
            else
            {
                if (Time.time > projDebounce)
                {
                    GameObject projectile = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + projectilename + "(Clone)");
                    GameObject originalprojectile = projectile;
                    projectile = ObjectPools.instance.Instantiate(projectile);

                    GameObject trail;
                    if (trailname == "none")
                    {
                        trail = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/SlingshotProjectileTrail(Clone)");
                    }
                    else
                    {
                        trail = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + trailname + "(Clone)");
                    }

                    SlingshotProjectile comp = projectile.GetComponent<SlingshotProjectile>();

                    int hasha = PoolUtils.GameObjHashCode(projectile);
                    int hashb = PoolUtils.GameObjHashCode(trail);

                    if (trailname == "none")
                    {
                        hashb = -1;
                    }

                    if (!GetIndex("Client Sided Projectiles").enabled)
                    {
                        object[] projectileSendData = new object[11];
                        projectileSendData[0] = position;
                        projectileSendData[1] = velocity;
                        projectileSendData[2] = hasha;
                        projectileSendData[3] = hashb;
                        projectileSendData[4] = false;
                        projectileSendData[5] = 1;
                        projectileSendData[6] = !(bluet || oranget);
                        projectileSendData[7] = r;
                        projectileSendData[8] = g;
                        projectileSendData[9] = b;
                        projectileSendData[10] = 1f;

                        object[] sendEventData = new object[3];
                        sendEventData[0] = PhotonNetwork.ServerTimestamp;
                        sendEventData[1] = (byte)0;
                        sendEventData[2] = projectileSendData;

                        try
                        {
                            PhotonNetwork.RaiseEvent(3, sendEventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
                        }
                        catch { /* wtf * }
                    }
                    RPCProtection();

                    originalprojectile.SetActive(true);

                    if (trailname != "none")
                    {
                        trail.SetActive(true);
                        ObjectPools.instance.Instantiate(trail).GetComponent<SlingshotProjectileTrail>().AttachTrail(projectile, false, false);
                    }

                    comp.Launch(position, velocity, PhotonNetwork.LocalPlayer, bluet, oranget, 1, 1f, true, new UnityEngine.Color(r, g, b, 1f));
                    if (projDebounceType > 0f && !noDelay)
                    {
                        projDebounce = Time.time + projDebounceType;
                    }
                }
            }*/
        }

        public static void BetaFireImpact(Vector3 position, float r, float g, float b, bool noDelay = false)
        {
            if (Time.time > projDebounce)
            {
                object[] impactSendData = new object[6];
                impactSendData[0] = position;
                impactSendData[1] = r;
                impactSendData[2] = g;
                impactSendData[3] = b;
                impactSendData[4] = 1f;
                impactSendData[5] = 1;

                object[] sendEventData = new object[3];
                sendEventData[0] = PhotonNetwork.ServerTimestamp;
                sendEventData[1] = (byte)1;
                sendEventData[2] = impactSendData;
                try
                {
                    PhotonNetwork.RaiseEvent(3, sendEventData, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
                }
                catch { /*wtf*/ }
                if (projDebounceType > 0f && !noDelay)
                {
                    projDebounce = Time.time + projDebounceType;
                }
            }
        }

        public static void spam()
        {
            if (rightGrab)
            {
                SysFireProjectile("Snowball", "none", GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward * (UnityEngine.Random.Range(150, 200) / 10), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), false, false);
            }
        }
        public static void bspam()
        {
            if (rightGrab)
            {
                SysFireProjectile("WaterBalloon", "none", GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward * (UnityEngine.Random.Range(150, 200) / 10), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), false, false);
            }
        }
        public static void pspam()
        {
            if (rightGrab)
            {
                SysFireProjectile("ThrowableGift", "none", GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward * (UnityEngine.Random.Range(150, 200) / 10), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), false, false);
            }
        }
        public static void mspam()
        {
            if (rightGrab)
            {
                SysFireProjectile("ScienceCandy", "none", GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward * (UnityEngine.Random.Range(150, 200) / 10), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), false, false);
            }
        }
        public static void lspam()
        {
            if (rightGrab)
            {
                SysFireProjectile("LavaRock", "none", GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward * (UnityEngine.Random.Range(150, 200) / 10), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), false, false);
            }
        }
        public static void pee()
        {
            if (rightGrab)
            {
                SysFireProjectile("Snowball", "none", GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.forward / 12 + (Vector3.down / 4.75f)), GorillaTagger.Instance.bodyCollider.transform.forward * (UnityEngine.Random.Range(150, 200) / 10), 255, 255, 0, false, false);
            }
        }
        public static void cumm()
        {
            if (rightGrab)
            {
                SysFireProjectile("Snowball", "none", GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.forward/12 + (Vector3.down/4.75f)), GorillaTagger.Instance.bodyCollider.transform.forward * (UnityEngine.Random.Range(150, 200) / 10), 255, 255, 255, false, false);
            }
        }
        public static void imspam()
        {
            if (rightGrab)
            {
                float h = (Time.frameCount / 180f) % 1f;
                UnityEngine.Color c = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                BetaFireImpact(GorillaTagger.Instance.rightHandTransform.position, c.r, c.g, c.b, false);
            }
            if (leftGrab)
            {
                float h = (Time.frameCount / 180f) % 1f;
                UnityEngine.Color c = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                BetaFireImpact(GorillaTagger.Instance.leftHandTransform.position, c.r, c.g, c.b, false);
            }
        }
        public static void GiveProj(string proj)
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
                        isCopying = true;
                        whoCopy = possibly;
                    }
                }
                if (leftTrigger > 0.5f)
                {
                    if (whoCopy && isCopying)
                    {
                        whoCopy = null;
                        isCopying = false;
                    }
                }
            }
            if (isCopying && whoCopy != null && whoCopy != GorillaTagger.Instance.offlineVRRig)
            {
                VRRig vrrig = whoCopy;
                float c1 = UnityEngine.Random.Range(0.0f, 1000.0f / 100);
                SysFireProjectile(proj,"none",vrrig.rightHandTransform.position, vrrig.rightHandTransform.forward * c1, UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500), UnityEngine.Random.Range(0.0f, 1000.0f / 500),false,false);
            }
        }
    }
}