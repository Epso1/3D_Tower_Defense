using System.Collections.Generic;
using UnityEngine;

namespace Gaskellgames.CameraSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    public static class CameraList
    {
        #region Variables

        private static List<CameraRig> cameraRigs = new List<CameraRig>();
        
        private static List<CameraRig> shakableRigs = new List<CameraRig>();

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Public Functions

        /// <summary>
        /// Get the global cameraRig list (all registered cameraRigs)
        /// </summary>
        /// <returns></returns>
        public static List<CameraRig> GetCameraRigList()
        {
            return cameraRigs;
        }

        /// <summary>
        /// Add a cameraRig to the global cameraRig list
        /// </summary>
        /// <param name="cameraRig"></param>
        public static void Register(CameraRig cameraRig)
        {
            if (cameraRigs.Contains(cameraRig)) { return; }
            
            cameraRigs.Add(cameraRig);
            Debug.Log(cameraRig.name + " registered. " + cameraRigs.Count + " total registered camera rigs");
        }

        /// <summary>
        /// Remove a cameraRig from the global cameraRig list
        /// </summary>
        /// <param name="cameraRig"></param>
        public static void Unregister(CameraRig cameraRig)
        {
            if (!cameraRigs.Contains(cameraRig)) { return; }
            
            cameraRigs.Remove(cameraRig);
        }
        
        /// <summary>
        /// Get the shakable cameraRig list
        /// </summary>
        /// <returns></returns>
        public static List<CameraRig> GetShakableRigList()
        {
            return shakableRigs;
        }
        
        /// <summary>
        /// Add a cameraRig to the shakable cameraRig list
        /// </summary>
        /// <param name="cameraRig"></param>
        public static void SetShakable(CameraRig cameraRig)
        {
            if (shakableRigs.Contains(cameraRig)) { return; }
            
            shakableRigs.Add(cameraRig);
        }

        /// <summary>
        /// Remove a cameraRig from the shakable cameraRig list
        /// </summary>
        /// <param name="cameraRig"></param>
        public static void UnsetShakable(CameraRig cameraRig)
        {
            if (!shakableRigs.Contains(cameraRig)) { return; }
            
            shakableRigs.Remove(cameraRig);
        }

        #endregion

    } //class end
}
