using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        var s = other.GetComponent<CarController>();
        s.SetCheckpoint(transform);
    }
}
