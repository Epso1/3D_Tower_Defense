#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Gaskellgames.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [InitializeOnLoad]
    public class DefineSymbols_GgCore
    {
        #region Variables

        private static readonly string[] ExtraScriptingDefineSymbols = new string[] { "GASKELLGAMES" };

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Constructors

        static DefineSymbols_GgCore()
        {
            AddExtraScriptingDefineSymbols();
            
#if GASKELLGAMES
#else
            //UnityEngine.Debug.LogError("GgCore not detected: All Gaskellgames packages require GgCore. Please download the required package from the unity asset store.");
#endif
        }
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Private Functions
        
        /// <summary>
        /// Add any new ScriptingDefineSymbols in ExtraScriptingDefineSymbols to the current ScriptingDefineSymbols in build settings
        /// </summary>
        private static void AddExtraScriptingDefineSymbols()
        {
            // get all ScriptingDefineSymbols from build settings.
            string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> scriptingDefineSymbols = scriptingDefineSymbolsForGroup.Split(';').ToList();
            
            // add any new ScriptingDefineSymbols from ExtraScriptingDefineSymbols
            scriptingDefineSymbols.AddRangeWithoutDuplicating(ExtraScriptingDefineSymbols.ToList());
            
            // set the ScriptingDefineSymbols in build settings.
            string newScriptingDefineSymbols = string.Join(";", scriptingDefineSymbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, newScriptingDefineSymbols);
        }
        
        #endregion
        
    } // class end
}

#endif