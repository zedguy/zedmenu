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
                    if (p && p != null)
                    {
                        GameObject traceholder = new GameObject("Tracers");
                        LineRenderer trace = traceholder.AddComponent<LineRenderer>();
                        trace.endWidth = 0.025f;
                        trace.startWidth = 0.025f;
                        trace.startColor = new Color32(150, 255, 180, 255);
                        trace.endColor = new Color32(150, 255, 180, 255);
                        trace.useWorldSpace = true;
                        trace.SetPosition(0, GorillaLocomotion.Player.Instance.bodyCollider.transform.position + (Vector3.down / 3));
                        trace.SetPosition(1, p.transform.position + (Vector3.down / 3));
                        trace.material.shader = Shader.Find("GUI/Text Shader");
                        UnityEngine.Object.Destroy(traceholder, Time.deltaTime);
                    }
                }
            }
        }
    }
}
