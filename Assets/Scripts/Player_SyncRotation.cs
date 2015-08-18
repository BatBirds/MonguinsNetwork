using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncRotation : NetworkBehaviour {

    [SyncVar]
    Quaternion syncPlayerRotation;
    [SyncVar]
    Quaternion syncCamRotation;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform camTransform;
    [SerializeField]
    float lerpRate = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitRotations();
        LerpRotations();
	}

    void LerpRotations() {
        if (!isLocalPlayer) {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot) {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [Client]
    void TransmitRotations() {
        if (isLocalPlayer) {
            CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
        }
    }
}
