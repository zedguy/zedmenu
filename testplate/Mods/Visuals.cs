using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using static zedmenu.Menu.Main;

namespace zedmenu.Mods
{
    internal class Visuals
    {
        public static void ChangeSpeedBoostAmount()
        {
            string[] speedNames = new string[] { "Player Color", "Red", "Green", "Blue", "Purple", "White", "Gray", "Black" };
            visualColor++;
            if (visualColor > speedNames.Length)
            {
                visualColor = 0;
            }

            GetIndex("Change Visual Colors").overlapText = "Change Visual Colors <color=grey>[</color><color=#96ffb2>" + speedNames[visualColor] + "</color><color=grey>]</color>";
        }
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
                        trace.endWidth = 0.05f;
                        trace.startWidth = 0.01f;
                        trace.useWorldSpace = true;
                        trace.SetPosition(0, GorillaLocomotion.Player.Instance.bodyCollider.transform.position + (Vector3.down / 3));
                        trace.SetPosition(1, p.transform.position + (Vector3.down / 2.75f));
                        trace.material.shader = Shader.Find("GUI/Text Shader");
                        if (visualColor == 0)
                        {
                            trace.material.color = p.playerColor;
                        }
                        if (visualColor == 1)
                        {
                            trace.material.color = Color.red;
                        }
                        if (visualColor == 2)
                        {
                            trace.material.color = Color.green;
                        }
                        if (visualColor == 3)
                        {
                            trace.material.color = Color.blue;
                        }
                        if (visualColor == 4)
                        {
                            trace.material.color = new Color32(200, 0, 222, 255);
                        }
                        if (visualColor == 5)
                        {
                            trace.material.color = Color.white;
                        }
                        if (visualColor == 6)
                        {
                            trace.material.color = Color.gray;
                        }
                        if (visualColor == 7)
                        {
                            trace.material.color = Color.black;
                        }
                        if (p.mainSkin.material.name.Contains("fected"))
                        {
                            trace.material.color = new Color32(255, 110, 0, 255);
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
                        if (visualColor == 0)
                        {
                            mat.color = p.playerColor;
                        }
                        if (visualColor == 1)
                        {
                            mat.color = Color.red;
                        }
                        if (visualColor == 2)
                        {
                            mat.color = Color.green;
                        }
                        if (visualColor == 3)
                        {
                            mat.color = Color.blue;
                        }
                        if (visualColor == 4)
                        {
                            mat.color = new Color32(200, 0, 222, 255);
                        }
                        if (visualColor == 5)
                        {
                            mat.color = Color.white;
                        }
                        if (visualColor == 6)
                        {
                            mat.color = Color.gray;
                        }
                        if (visualColor == 7)
                        {
                            mat.color = Color.black;
                        }
                        if (p.mainSkin.material.name.Contains("fected"))
                        {
                            mat.color = new Color32(255, 110, 0, 255);
                        }
                        UnityEngine.Object.Destroy(trace, Time.deltaTime);
                    }
                }
            }
        }
        public static void Box()
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
                        trace.transform.localScale = new Vector3(0.4f, 0.625f, 0.4f);
                        trace.transform.position = p.transform.position;
                        if (visualColor == 0)
                        {
                            mat.color = p.playerColor;
                        }
                        if (visualColor == 1)
                        {
                            mat.color = Color.red;
                        }
                        if (visualColor == 2)
                        {
                            mat.color = Color.green;
                        }
                        if (visualColor == 3)
                        {
                            mat.color = Color.blue;
                        }
                        if (visualColor == 4)
                        {
                            mat.color = new Color32(200, 0, 222, 255);
                        }
                        if (visualColor == 5)
                        {
                            mat.color = Color.white;
                        }
                        if (visualColor == 6)
                        {
                            mat.color = Color.gray;
                        }
                        if (visualColor == 7)
                        {
                            mat.color = Color.black;
                        }
                        if (p.mainSkin.material.name.Contains("fected"))
                        {
                            mat.color = new Color32(255, 110, 0, 255);
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
                    Material mat = vrrig.mainSkin.material;
                    if (visualColor == 0)
                    {
                        mat.color = vrrig.playerColor;
                    }
                    if (visualColor == 1)
                    {
                        mat.color = Color.red;
                    }
                    if (visualColor == 2)
                    {
                        mat.color = Color.green;
                    }
                    if (visualColor == 3)
                    {
                        mat.color = Color.blue;
                    }
                    if (visualColor == 4)
                    {
                        mat.color = new Color32(200, 0, 222, 255);
                    }
                    if (visualColor == 5)
                    {
                        mat.color = Color.white;
                    }
                    if (visualColor == 6)
                    {
                        mat.color = Color.gray;
                    }
                    if (visualColor == 7)
                    {
                        mat.color = Color.black;
                    }
                    if (vrrig.mainSkin.material.name.Contains("fected"))
                    {
                        vrrig.mainSkin.material.color = new Color32(255, 110, 0, 255);
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
