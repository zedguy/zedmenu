using UnityEngine;
using zedmenu.Classes;
using static zedmenu.Menu.Main;

namespace zedmenu.Mods
{
    internal class Rig
    {
        public static float Cooldown;
        public static void Ghost()
        {
            if (Inputs.rightControllerPrimaryButton && Time.time > Cooldown)
            {
                Cooldown = Time.time + 0.24f;
                if (!ghostMonke)
                {
                    ghostMonke = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
                else
                {
                    ghostMonke = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
        public static void WeirdArms()
        {
            if (Inputs.rightControllerPrimaryButton && Time.time > Cooldown)
            {
                Cooldown = Time.time + 0.24f;
                if (!ghostMonke)
                {
                    ghostMonke = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
                else
                {
                    ghostMonke = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            if (ghostMonke)
            {
                Physics.Raycast(GorillaTagger.Instance.offlineVRRig.transform.position + (GorillaTagger.Instance.offlineVRRig.transform.right / 4), Vector3.down, out var Ray);
                Physics.Raycast(GorillaTagger.Instance.offlineVRRig.transform.position - (GorillaTagger.Instance.offlineVRRig.transform.right / 4), Vector3.down, out var Ray2);
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = Ray2.point;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = Ray.point;
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            }
        }
        public static void CREEPY()
        {
            if (Inputs.rightControllerPrimaryButton && Time.time > Cooldown)
            {
                Cooldown = Time.time + 0.24f;
                if (!ghostMonke)
                {
                    ghostMonke = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
                else
                {
                    ghostMonke = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            if (ghostMonke)
            {
                GorillaTagger.Instance.offlineVRRig.transform.position = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/motdscreen").transform.position;
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000));
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position;
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000)); 
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000), UnityEngine.Random.Range(0, 1000));
                var s = "";
                for (int i = 0; i < 10; i++)
                {
                    s = s + (letters[UnityEngine.Random.Range(0,letters.Length)]);
                }
                ChangeName(s);
            }
        }
        public static void RigFreeze()
        {
            if (Inputs.rightControllerPrimaryButton && Time.time > Cooldown)
            {
                Cooldown = Time.time + 0.25f;
                if (!ghostMonke)
                {
                    ghostMonke = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;

                }
                else
                {
                    ghostMonke = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    GorillaTagger.Instance.offlineVRRig.transform.parent = GameObject.Find("Player Objects").transform;
                }
            }
        }
        public static void Orbit()
        {
            if (Inputs.rightControllerPrimaryButton && Time.time > Cooldown)
            {
                Cooldown = Time.time + 0.2f;
                if (!ghostMonke)
                {
                    ghostMonke = true;
                    isCopying = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    whoCopy = RigManager.GetRandomVRRig(false);
                }
                else
                {
                    ghostMonke = false;
                    isCopying = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    GorillaTagger.Instance.offlineVRRig.transform.parent = GameObject.Find("Player Objects").transform;
                }
            }
            if (ghostMonke && isCopying && whoCopy != null && whoCopy != GorillaTagger.Instance.offlineVRRig)
            {
                VRRig vrrig = whoCopy;
                Transform target = vrrig.transform;
                float angle = (Time.time * 6) % (Mathf.PI * 2);
                float x = Mathf.Cos(angle);
                float z = Mathf.Sin(angle);
                Vector3 offset = new Vector3(x, z / 6, z) * 1f;
                GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + offset;
                GorillaTagger.Instance.offlineVRRig.transform.LookAt(vrrig.transform.position);
            }
        }
        public static void disablecopy()
        {
            whoCopy = null;
            isCopying = false;
            GorillaTagger.Instance.offlineVRRig.enabled = true;
            GorillaTagger.Instance.offlineVRRig.headConstraint.rotation = Quaternion.identity;
            GorillaTagger.Instance.offlineVRRig.head.trackingPositionOffset = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset = Vector3.zero;
        }

        public static void OrbitGun()
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
                        ghostMonke = true;
                        isCopying = true;
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        whoCopy = possibly;
                    }
                }
                if (leftTrigger > 0.5f)
                {
                    if (whoCopy && isCopying)
                    {
                        whoCopy = null;
                        ghostMonke = false;
                        isCopying = false;
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }
            if (ghostMonke && isCopying && whoCopy != null && whoCopy != GorillaTagger.Instance.offlineVRRig)
            {
                VRRig vrrig = whoCopy;
                Transform target = vrrig.transform;
                float angle = (Time.time * 6) % (Mathf.PI * 2);
                float x = Mathf.Cos(angle);
                float z = Mathf.Sin(angle);
                Vector3 offset = new Vector3(x, z / 6, z) * 1f;
                GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + offset;
                GorillaTagger.Instance.offlineVRRig.transform.LookAt(vrrig.transform.position);
            }
        }
        public static void RigGun()
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
                    {
                        ghostMonke = true;
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = Ray.point;
                    }
                }
                else
                {
                    ghostMonke = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
    }
}
