using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
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
                        trace.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        trace.GetComponent<Renderer>().material.color = p.playerColor;
                        trace.transform.localScale = new Vector3(0.05f,1000f,0.05f);
                        trace.transform.position = p.transform.position;
                        UnityEngine.Object.Destroy(trace, Time.deltaTime);
                    }
                }
            }
        }
    }
}
