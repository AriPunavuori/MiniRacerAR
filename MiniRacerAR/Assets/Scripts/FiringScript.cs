using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript : MonoBehaviour {

   public GameObject prefab;

   public void FireWeapon() {
        Instantiate(prefab, transform.position, transform.rotation);
   }
}
