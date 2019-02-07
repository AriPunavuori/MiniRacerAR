using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChassisController : MonoBehaviour {

    Vector3 targetPos;
    Quaternion targetRot;

    public float rotateSpeed = 3600;
    public float dropSpeed = 0.1f;
    public float dropRotSpeedReducerFactor = 20;

    private void Update() {
        ChassisPlacement();
    }

    public void SetTarget(Vector3 pos, Quaternion rot) {
        targetPos = pos;
        targetRot = rot;
    }

    public void ChassisPlacement() {

        var rd = rotateSpeed * Time.deltaTime;
        var dd = dropSpeed * Time.deltaTime;
        Vector3 setPos;
        if (targetPos.y < transform.position.y) {
            setPos = new Vector3(targetPos.x, transform.position.y - dd, targetPos.z);
            rd = rd / dropRotSpeedReducerFactor;
        } else {
            setPos = targetPos;
        }

        transform.position = setPos;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rd);
    }
}