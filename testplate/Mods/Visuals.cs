using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace zedmenu.Mods
{
    internal class Visuals
    {
        public static void Tracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig p in GorillaParent.instance.vrrigs)
                {
                    if (p && p != null && p != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject traceholder = new GameObject("Tracers");
                        LineRenderer trace = traceholder.AddComponent<LineRenderer>();
                        trace.endWidth = 0.025f;
                        trace.startWidth = 0.01f;
                        trace.startColor = p.playerColor; trace.endColor = p.playerColor;
                        trace.useWorldSpace = true;
                        trace.SetPosition(0, GorillaLocomotion.Player.Instance.bodyCollider.transform.position + (Vector3.down / 3));
                        trace.SetPosition(1, p.transform.position + (Vector3.down / 2.75f));
                        trace.material.shader = Shader.Find("GUI/Text Shader");
                        if (p.mainSkin.material.name.Contains("fected"))
                        {
                            trace.startColor = new Color32(255, 150, 10, 255); trace.endColor = new Color32(255, 120, 0, 255);
                        }
                        UnityEngine.Object.Destroy(traceholder, Time.deltaTime);
                    }
                }
            }
        }
        public static void Beacons()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig p in GorillaParent.instance.vrrigs)
                {
                    if (p && p != null && p != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject trace = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(trace.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(trace.GetComponent<BoxCollider>());
                        Material mat = trace.GetComponent<Renderer>().material;
                        mat.shader = Shader.Find("GUI/Text Shader");
                        mat.color = p.playerColor;
                        trace.transform.localScale = new Vector3(0.05f,1000f,0.05f);
                        trace.transform.position = p.transform.position;
                        if (p.mainSkin.material.name.Contains("fected"))
                        {
                            mat.color = new Color32(255, 120, 0, 255);
                        }
                        UnityEngine.Object.Destroy(trace, Time.deltaTime);
                    }
                }
            }
        }
        public static void CasualChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    vrrig.mainSkin.material.color = vrrig.playerColor;
                    if (vrrig.mainSkin.material.name.Contains("fected"))
                    {
                        vrrig.mainSkin.material.color = new Color32(255, 120, 0, 255);
                    }
                }
            }
        }
        public static void DisableChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                    vrrig.mainSkin.material.color= vrrig.playerColor;
                    if (vrrig.mainSkin.material.name.Contains("fected"))
                    {
                        vrrig.mainSkin.material.color = Color.white;
                    }
                }
            }
        }

    }
}
