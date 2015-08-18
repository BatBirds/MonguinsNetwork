using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class _PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Camera FPSCharacterCam;

    // Use this for initialization
    void Start() {
        if (isLocalPlayer) {
            GameObject.Find("Scene Camera").SetActive(false);
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
            FPSCharacterCam.enabled = true;
        }
    }
}
