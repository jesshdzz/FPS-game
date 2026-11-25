using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using Photon.Pun;

public class WeaponChangeBeginner : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;

    private CinemachineCamera cam;
    private GameObject camObject;
    public MultiAimConstraint[] aimObjects;
    public Transform aimTarget;

    public RigBuilder rig;
    public Transform leftTargetWeapon1;
    public Transform rightTargetWeapon1;
    public Transform leftTargetWeapon2;
    public Transform rightTargetWeapon2;
    public Transform leftTargetWeapon3;
    public Transform rightTargetWeapon3;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    void Start()
    {
        camObject = GameObject.Find("PlayerCam");
        aimTarget = GameObject.Find("AimRef").transform;
        if (GetComponent<PhotonView>().IsMine)
        {
            cam = camObject.GetComponent<CinemachineCamera>();
            cam.Target.TrackingTarget = transform;   // reemplaza a cam.Follow
            cam.Target.LookAtTarget = transform;     // reemplaza a cam.LookAt
            Invoke("SetLookAt", 0.1f);
        }
        else
        {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    void SetLookAt()
    {
        if(aimTarget != null)
        {
           for(int i = 0; i < aimObjects.Length; i++)
            {
                var target = aimObjects[i].data.sourceObjects;
                target.SetTransform(0, aimTarget.transform);
                aimObjects[i].data.sourceObjects = target;
            }
            rig.Build();
        }
    }
// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            leftHand.data.target = leftTargetWeapon1;
            rightHand.data.target = rightTargetWeapon1;
            rig.Build();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            leftHand.data.target = leftTargetWeapon2;
            rightHand.data.target = rightTargetWeapon2;
            rig.Build();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown
        (KeyCode.Keypad3))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            leftHand.data.target = leftTargetWeapon3;
            rightHand.data.target = rightTargetWeapon3;
            rig.Build();
        }
    }
}