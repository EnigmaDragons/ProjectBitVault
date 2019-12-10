using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ScriptableExtensions
{
    //CANT SOLVE HOW TO EXCLUDE FROM BUILD SO COMMENTED OUT WHEN NOT USING IT
    /*public static List<T> GetAllInstances<T>() where T : ScriptableObject
        => AssetDatabase.FindAssets("t:" + typeof(T).Name)
            .Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x))).ToList();*/
}
