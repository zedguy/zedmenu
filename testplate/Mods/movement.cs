using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using zedmenu.Menu;
using static zedmenu.Menu.Main;

namespace zedmenu.Mods
{
    internal class Locomotion
    {
        public static void DisableJoystick()
        {
            GorillaSnapTurn turning = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer").GetComponent<GorillaSnapTurn>();
            turnAmnt = turning.turnAmount;
            turning.turnAmount = 0f;
        }

        public static void EnableJoystick()
        {
            GorillaSnapTurn turning = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer").GetComponent<GorillaSnapTurn>();
            turning.turnAmount = turnAmnt;
        }
        public static void Plats()
        {
            if (Inputs.leftControllerGripFloat > 0.5f)
            {
                if (leftplat == null)
                {
                    if (platformMode == 0)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        leftplat.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        leftplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    }

                    if (platformMode == 1)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        leftplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    }

                    if (platformMode == 2)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.0125f, 0.28f, 0.3825f);
                        leftplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + (-GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 0.0125f);
                    }

                    if (platformMode == 3)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        leftplat.transform.localScale = new Vector3(0.0125f, 0.28f, 0.3825f);
                        leftplat.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + (-GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 0.0125f);
                    }
                    Object.Destroy(leftplat.GetComponent<Rigidbody>());
                    leftplat.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    leftplat.GetComponent<Renderer>().material.color = new Color32(120, 255, 140, 255);
                    leftplat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 93;
                }
            }
            else
            {
                if (leftplat != null)
                {
                    Object.Destroy(leftplat);
                    leftplat = null;
                }
            }
            if (Inputs.rightControllerGripFloat > 0.5f)
            {
                if (rightplat == null)
                {
                    if (platformMode == 0)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        rightplat.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        rightplat.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    }

                    if (platformMode == 1)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        rightplat.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    }

                    if (platformMode == 2)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.0125f, 0.28f, 0.3825f);
                        rightplat.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + (-GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 0.0125f);
                    }

                    if (platformMode == 3)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        rightplat.transform.localScale = new Vector3(0.0125f, 0.28f, 0.3825f);
                        rightplat.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + (-GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 0.0125f);
                    }
                    Object.Destroy(rightplat.GetComponent<Rigidbody>());
                    rightplat.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    rightplat.GetComponent<Renderer>().material.color = new Color32(120, 255, 140, 255);
                    rightplat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 93;
                }
            }
            else
            {
                if (rightplat != null)
                {
                    UnityEngine.Object.Destroy(rightplat);
                    rightplat = null;
                }
            }
        }

        public static void SpeedBoost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = jspeed;
            GorillaLocomotion.Player.Instance.jumpMultiplier = jmulti;
        }

        public static void GripSpeedBoost()
        {
            if (rightGrab)
            {
                GorillaLocomotion.Player.Instance.maxJumpSpeed = jspeed;
                GorillaLocomotion.Player.Instance.jumpMultiplier = jmulti;
            }
        }

        public static void DisableSpeedBoost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
        }
        public static void ChangeSpeedBoostAmount()
        {
            speedboostCycle++;
            if (speedboostCycle > 3)
            {
                speedboostCycle = 0;
            }

            float[] jspeedamounts = new float[] { 2f, 7.5f, 9f, 200f };
            jspeed = jspeedamounts[speedboostCycle];

            float[] jmultiamounts = new float[] { 0.5f, 1.25f, 2f, 10f };
            jmulti = jmultiamounts[speedboostCycle];

            string[] speedNames = new string[] { "Slow", "Normal", "Fast", "Ultra Fast" };
            GetIndex("Change Speed Boost Amount").overlapText = "Change Speed Boost Amount <color=grey>[</color><color=#96ffb2>" + speedNames[speedboostCycle] + "</color><color=grey>]</color>";
        }

        public static void cleardraw()
        {
            for (int i = 0; i < drawhold.transform.childCount; i++)
            {
                Object.Destroy(drawhold.transform.GetChild(i));
            }
        }

        public static void draw()
        {
            if (Inputs.leftControllerGripFloat > 0.5f)
            {
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                s.transform.localScale = new Vector3(ShootStrength2, ShootStrength2, ShootStrength2);
                UnityEngine.Object.Destroy(s.GetComponent<Rigidbody>());
                s.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                s.GetComponent<Renderer>().material.color = Color.black;
                s.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + (-GorillaLocomotion.Player.Instance.leftControllerTransform.up / 6);
                s.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                s.AddComponent<GorillaSurfaceOverride>().overrideIndex = 56;
                s.transform.SetParent(drawhold.transform);
                UnityEngine.Object.Destroy(s, 60);
            }
            
            if (Inputs.rightControllerGripFloat > 0.5f)
            {
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                s.transform.localScale = new Vector3(ShootStrength2, ShootStrength2, ShootStrength2);
                UnityEngine.Object.Destroy(s.GetComponent<SphereCollider>());
                UnityEngine.Object.Destroy(s.GetComponent<Rigidbody>());
                s.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                s.GetComponent<Renderer>().material.color = Color.black;
                s.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + (-GorillaLocomotion.Player.Instance.rightControllerTransform.up/6);
                s.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                s.AddComponent<GorillaSurfaceOverride>().overrideIndex = 56;
                s.transform.SetParent(drawhold.transform);
                UnityEngine.Object.Destroy(s, 60);
            }
        }
        public static void shoot()
        {
            if (Inputs.rightControllerGripFloat > 0.8f && Inputs.rightControllerIndexFloat > 0.75f)
            {
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                s.transform.localScale = new Vector3(ShootStrength2, ShootStrength2, ShootStrength2);
                s.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");

                AnimationCurve curve = new AnimationCurve();
                {
                    curve.AddKey(0.0f, 1.0f);
                    curve.AddKey(1.0f, 0.0f);
                }
                TrailRenderer trailRenderer = s.AddComponent<TrailRenderer>();
                trailRenderer.widthCurve = curve;
                trailRenderer.time = 0.5f;
                trailRenderer.material = BulletMat;
                trailRenderer.widthMultiplier = ShootStrength2;
                trailRenderer.startColor = Color.yellow;
                trailRenderer.endColor = Color.yellow;
                s.GetComponent<Renderer>().material.color = Color.yellow;
                UnityEngine.Object.Destroy(s.GetComponent<SphereCollider>());
                s.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                s.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                s.AddComponent<Rigidbody>().velocity = -GorillaLocomotion.Player.Instance.rightControllerTransform.up * 50;
                s.GetComponent<Rigidbody>().useGravity = false;
                UnityEngine.Object.Destroy(s,3);
            }
        }
        public static void drawsize()
        {
            shootCycle++;
            if (shootCycle > 2)
            {
                shootCycle = 0;
            }

            float[] ShootStrengthTypes = new float[]
            {
                0.05f,
                0.1f,
                0.15f
            };

            string[] ShootStrengthNames = new string[]
            {
                "Small",
                "Medium",
                "Big"
            };

            ShootStrength2 = ShootStrengthTypes[shootCycle];
            GetIndex("Change Draw Size").overlapText = "Change Draw Size <color=grey>[</color><color=#96ffb2>" + ShootStrengthNames[shootCycle] + "</color><color=grey>]</color>";
        }
        public static void platsize()
        {
            platformMode++;
            if (platformMode > 3)
            {
                platformMode = 0;
            }

            string[] ShootStrengthNames = new string[]
            {
                "Sphere",
                "Square",
                "Classic",
                "Round Classic"
            };

            GetIndex("Change Platform Mode").overlapText = "Change Platform Mode <color=grey>[</color><color=#96ffb2>" + ShootStrengthNames[platformMode] + "</color><color=grey>]</color>";
        }
        
        public static void Noclip()
        {
            if (rightTrigger > 0.5f)
            {
                if (noclip == false)
                {
                    noclip = true;
                    foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        v.enabled = false;
                    }
                }
            }
            else
            {
                if (noclip == true)
                {
                    noclip = false;
                    foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        v.enabled = true;
                    }
                }
            }
        }
        public static void updown()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity +=  new Vector3(0,Inputs.rightControllerIndexFloat + -Inputs.leftControllerIndexFloat,0);
        }

        public static void Drive()
        {
            Vector2 joy = ControllerInputPoller.instance.rightControllerPrimary2DAxis;
            lerpygerpy = Vector2.Lerp(lerpygerpy, joy, 0.05f);

            Vector3 addition = GorillaTagger.Instance.bodyCollider.transform.forward * lerpygerpy.y + (GorillaTagger.Instance.bodyCollider.transform.right * lerpygerpy.x * 0);// + new Vector3(0f, -1f, 0f);
            Physics.Raycast(GorillaTagger.Instance.bodyCollider.transform.position - new Vector3(0f, 0.2f, 0f), Vector3.down, out var Ray, 512);

            if (Ray.distance < 0.2f && (Mathf.Abs(lerpygerpy.x) > 0.05f || Mathf.Abs(lerpygerpy.y) > 0.05f))
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = addition * 10f;
            }
        }

        public static void IronMan()
        {
            if (Inputs.leftControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(flySpeed * GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
            if (Inputs.rightControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(flySpeed * -GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
        }
        public static void FunGun()
        {
            if (Inputs.rightGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);
                if (shouldBePC)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out Ray, 100);
                }

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if (rightTrigger > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = Ray.point + new Vector3(0f, -0.2f, 0f);
                }
            }
            if (Inputs.leftGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.forward, out var Ray);
                if (shouldBePC)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out Ray, 100);
                }

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if (rightTrigger > 0.5f || Mouse.current.leftButton.isPressed)
                {

                }
            }
        }
    }
}
