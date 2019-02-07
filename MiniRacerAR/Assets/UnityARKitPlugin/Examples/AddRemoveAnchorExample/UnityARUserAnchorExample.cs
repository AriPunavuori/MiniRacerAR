using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.iOS;

public class UnityARUserAnchorExample : MonoBehaviour {


    public GameObject prefabObject;
    // Distance in Meters
    public int distanceFromCamera = 1;
    private HashSet<string> m_Clones;
    bool worldCreated;
    private float m_TimeUntilRemove = 5.0f;

    void Awake() {
        UnityARSessionNativeInterface.ARUserAnchorAddedEvent += ExampleAddAnchor;
        UnityARSessionNativeInterface.ARUserAnchorRemovedEvent += AnchorRemoved;
        m_Clones = new HashSet<string>();
    }

    public void ExampleAddAnchor(ARUserAnchor anchor) {
        if(m_Clones.Contains(anchor.identifier)) {
            Console.WriteLine("Our anchor was added!");
        }
    }

    public void AnchorRemoved(ARUserAnchor anchor) {
        if(m_Clones.Contains(anchor.identifier)) {
            m_Clones.Remove(anchor.identifier);
            Console.WriteLine("AnchorRemovedExample: " + anchor.identifier);
        }
    }

    // Update is called once per frame
    void Update() {
        if(!worldCreated) {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    GameObject clone = Instantiate(prefabObject, hit.point, Quaternion.identity);
                    //UnityARUserAnchorComponent component = clone.GetComponent<UnityARUserAnchorComponent>();
                    //m_Clones.Add(component.AnchorId);
                    //m_TimeUntilRemove = 4.0f;
                    worldCreated = true;
                }
            }
        }
    }
}