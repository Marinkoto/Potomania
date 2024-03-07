
using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey.MonoBehaviours {
    public class CameraFollowSetup : MonoBehaviour {

        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] public Transform followTransform;
        [SerializeField] private float zoom;

        private void Start() {
            followTransform = FindFirstObjectByType<PlayerStats>().transform;
            if (followTransform == null) {
                
                cameraFollow.Setup(() => Vector3.zero, () => zoom, true, true);
            } else {
                cameraFollow.Setup(() => followTransform.position, () => zoom, true, true);
            }
        }
    }

}