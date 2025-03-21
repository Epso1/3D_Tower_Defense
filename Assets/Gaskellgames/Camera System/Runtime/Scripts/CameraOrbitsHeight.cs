using UnityEngine;

namespace Gaskellgames.CameraSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [System.Serializable]
    public class CameraOrbitsHeight
    {
        [SerializeField, Min(0)]
        [Tooltip("The vertical offset distance of the top radius orbit, relative to the middle orbit.")]
        public float up;
        
        [SerializeField, Max(0)]
        [Tooltip("The vertical offset distance of the bottom radius orbit, relative to the middle orbit.")]
        public float down;

        /// <summary>
        /// Initialise the values of CameraOrbitsHeight
        /// </summary>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public CameraOrbitsHeight(float up = 3, float down = -1)
        {
            this.up = up;
            this.down = down;
        }
        
    } // class end
}