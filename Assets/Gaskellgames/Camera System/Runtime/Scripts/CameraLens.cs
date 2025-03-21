using UnityEngine;

namespace Gaskellgames.CameraSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [System.Serializable]
    public class CameraLens
    {
        [Tooltip("The camera's view angle measured in degrees along vertical axis.")]
        public float verticalFOV = 50;
        
        [Tooltip("The closest point to the camera where drawing occurs.")]
        public float nearClipPlane = 0.1f;
        
        [Tooltip("The furthest point to the camera where drawing occurs.")]
        public float farClipPlane = 1000f;
        
        [Tooltip("Show/hide the frustum gizmo.")]
        public bool showFrustum = true;
        
        [Range(-180, 180)]
        [Tooltip("The rotational offset used to tilt the cameraRig.")]
        public float tilt = 0;
        
        [Tooltip("Which layers the camera renders.")]
        public LayerMask cullingMask = ~0;
        
    } // class end
}