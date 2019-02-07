using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualWorldSpawner : MonoBehaviour
{
    public GameObject prefabObject;
    bool worldCreated;

    void Update() {
        if(!worldCreated) {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    GameObject clone = Instantiate(prefabObject, hit.point, Quaternion.identity);
                    worldCreated = true;
                }
            }
        }
    }
}
