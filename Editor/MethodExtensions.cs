using System.IO;
using UnityEditor;
using UnityEngine;

namespace Internal.Editor
{
    public static class MethodExtensions
    {
        public static string GetDirectory(this ScriptableObject asset) => Path.GetDirectoryName(AssetDatabase.GetAssetPath(asset));
    }
}