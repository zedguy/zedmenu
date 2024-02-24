using GorillaNetworking;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Diagnostics;
using UnityEngine;
using static zedmenu.Menu.Main;

namespace zedmenu.Mods
{
    internal class HitSounds
    {
        public static float cooldown = 0f;
        public static void SetHitsounds(int index, bool IsRandom = false)
        {
            foreach (GorillaSurfaceOverride gorillaSurfaceOverride in Resources.FindObjectsOfTypeAll<GorillaSurfaceOverride>())
            {
                gorillaSurfaceOverride.overrideIndex = index;
                if (IsRandom)
                {
                    cooldown = Time.time;
                    gorillaSurfaceOverride.overrideIndex = UnityEngine.Random.Range(0, 243);
                }
            }
        }

        public static void randomize()
        {
            if (Time.time > cooldown + 0.5f)
            {
                foreach (GorillaSurfaceOverride gorillaSurfaceOverride in Resources.FindObjectsOfTypeAll<GorillaSurfaceOverride>())
                {
                    cooldown = Time.time;
                    gorillaSurfaceOverride.overrideIndex = UnityEngine.Random.Range(0, 243);
                }
            }
        }
        public static void PlatHit(int index, bool ran = false)
        {
            {
                GameObject lol = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lol.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                lol.transform.localPosition = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, -0.05f, 0f);
                lol.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                UnityEngine.Object.Destroy(lol.GetComponent<Rigidbody>());
                lol.AddComponent<GorillaSurfaceOverride>().overrideIndex = index;
                if (ran)
                {
                    lol.GetComponent<GorillaSurfaceOverride>().overrideIndex = UnityEngine.Random.Range(0, 231);
                }
                UnityEngine.Object.Destroy(lol, Time.deltaTime);
            }
        }

        public static void RPCHit(int index, bool ran = false)
        {
            if (ran)
                index = UnityEngine.Random.Range(0, 231);
            GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.All, new object[]{
                index,
                false,
                9999f
            });
            RPCProtection();
        }

        public static void rsp()
        {
            if (rightGrab)
            {
                RPCHit(0, true);
            }
        }
    }
}