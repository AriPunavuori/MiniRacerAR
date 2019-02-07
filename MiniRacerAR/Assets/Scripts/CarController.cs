using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public float maxMoveSpeed;
    public float stopDelay = 1f;
    float delayTimer;

    public GameObject chassis;
    public Joystick joystick;

    ChassisController cc;

    LayerMask mapLayer;
    LayerMask outOfBoundsLayer;
    LayerMask checkpointLayer;

    Vector3 lastCheckpointPosition;
    Quaternion lastCheckpointRotation;

    Camera cam;

    void Start() {
        cc = chassis.GetComponent<ChassisController>();
        cam = Camera.main;

        mapLayer = LayerMask.GetMask("Map");
        outOfBoundsLayer = LayerMask.GetMask("OutOfBounds");
        checkpointLayer = LayerMask.GetMask("Checkpoint");
    }

    void Update() {
        delayTimer -= Time.deltaTime;
        if(delayTimer < 0) {
            if(joystick.Direction != Vector2.zero) {
                Move();
            }
            var inputVector = Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up;
            if(inputVector.magnitude > 0.01f) {
                KeyboardMove();
            }
        }
        GroundCheck();
    }

    void Move() {
        var forward = cam.transform.forward;
        forward.y = 0;
        var right = cam.transform.right;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        var comboDir = forward * joystick.Vertical + right * joystick.Horizontal;

        if (comboDir.magnitude > 1) {
            comboDir.Normalize();
        }
        transform.forward = comboDir;
        transform.position += comboDir * maxMoveSpeed * Time.deltaTime;
    }

    void KeyboardMove() {
        Vector3 comboDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if(comboDir.magnitude > 1) {
            comboDir.Normalize();
        }
        transform.forward = comboDir;
        transform.position += comboDir * maxMoveSpeed * Time.deltaTime;
    }

    void GroundCheck() {

        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.up * Const.mapMaxHeight, -Vector3.up, out hit, Mathf.Infinity, mapLayer)) {
            transform.position = hit.point;
            var r = Vector3.Cross(hit.normal, transform.forward);
            var gf = -Vector3.Cross(hit.normal, r);
            var rot = Quaternion.LookRotation(gf, hit.normal);
            cc.SetTarget(hit.point, rot);
        } else {
            Debug.Log("Palataan Checkpointtiin");
            ReturnToLastCheckpoint();
        }

        if(Physics.Raycast(transform.position + Vector3.up * Const.mapMaxHeight, -Vector3.up, out hit, Mathf.Infinity, checkpointLayer)) {
            SetCheckpoint(hit.transform);
        }
    }

    public void SetCheckpoint(Transform t) {
        lastCheckpointPosition = t.position;
        lastCheckpointRotation = t.rotation;
    }

    public void ReturnToLastCheckpoint() {
        transform.position = lastCheckpointPosition;
        transform.rotation = lastCheckpointRotation;
        delayTimer = stopDelay;
    }
}
