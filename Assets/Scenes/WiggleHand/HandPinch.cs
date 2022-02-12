using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandPinch : MonoBehaviour
{
    private GameObject chosenObj;
    private GameObject pinchedObj;
    private Transform pinchPosition;
    private Transform pinchedParent;

    private Sequence objSequence;

    //public GameObject 

    void Start()
    {
        pinchPosition = GetComponent<Transform>();
    }


    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Hands))
        {
            if (chosenObj != null) // pinch when you select(fingers collide with) something
            {
                pinchedObj = chosenObj;
                pinchedParent = pinchedObj.GetComponent<Transform>().parent;
                if (pinchedParent != null && pinchedParent.name == "Canvas") // grab object out of canvas
                {
                    UpdateScale();
                }
                pinchedObj.GetComponent<Transform>().parent = pinchPosition; // parent object to inde finger
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.Hands) && pinchedObj != null) //release the pinched object
        {
            Debug.Log(pinchedParent);
            if (!pinchedParent || pinchedParent.name != "Canvas")
            {
                pinchedObj.GetComponent<Transform>().parent = pinchedParent;
            }
            else
            {
                pinchedObj.GetComponent<Transform>().parent = null;
            }

            if (pinchedObj.transform.childCount > 0)
            {
                pinchedObj.transform.Find("handle").gameObject.AddComponent<OVRGrabbable>();
                pinchedObj.transform.Find("handle").gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            pinchedObj = null;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<OVRGrabbable>()) return;
        else if (pinchedObj != null) return; //  can't choose other obj if splayer is already grabbing something
        chosenObj = other.gameObject;

        //add selected animation

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<OVRGrabbable>()) return;
        chosenObj = null;

        //add selected animation
    }

    private void UpdateScale()
    {
        objSequence?.Kill();
        objSequence = DOTween.Sequence();
        objSequence.Append(pinchedObj.GetComponent<Transform>().DOScale(new Vector3(2f, 2f, 2f), 0.1f)
            .SetEase(Ease.InOutQuad)
            );
    }

}

