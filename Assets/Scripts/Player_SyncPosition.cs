﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncPosition : NetworkBehaviour {

    [SyncVar]
    Vector3 syncPos;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 10;

	// Update is called once per frame
    void FixedUpdate() {
        TransmitPosition();
        LerpPosition();
	}

    void LerpPosition() {
        if (!isLocalPlayer) {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer) {
            CmdProvidePositionToServer(myTransform.position);
        }
    }
}
