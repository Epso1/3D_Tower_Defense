using UnityEngine;

namespace Gaskellgames.CameraSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [System.Serializable]
    public class CameraOrbitsRadius
    {
        [SerializeField, Min(0.01f)]
        [Tooltip("The horizontal distance of the top orbit position.")]
        public float top;
        
        [SerializeField, Min(0.01f)]
        [Tooltip("The horizontal distance of the middle orbit position.")]
        public float middle;
        
        [SerializeField, Min(0.01f)]
        [Tooltip("The horizontal distance of the bottom orbit position.")]
        public float bottom;

        /// <summary>
        /// Initialise the values of CameraOrbitsRadius
        /// </summary>
        /// <param name="top"></param>
        /// <param name="middle"></param>
        /// <param name="bottom"></param>
        public CameraOrbitsRadius(float top = 2, float middle = 4, float bottom = 3)
        {
            this.top = top;
            this.middle = middle;
            this.bottom = bottom;
        }
        
    } // class end
}