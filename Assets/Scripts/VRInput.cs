using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
    public Camera eventCamera = null;
    public OVRInput.Button clickButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.All;

    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;

    }

    //public override bool GetMouseButton(int button)
    //{
    //    return OVRInput.Get(OVRInput.Button.Back);
    //}

    public override bool GetMouseButtonDown(int button)
    {
        return OVRInput.GetDown(OVRInput.Button.Back);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return OVRInput.GetUp(OVRInput.Button.Back);
    }


    public override Vector2 mousePosition
    {
        get
        {
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }

}
