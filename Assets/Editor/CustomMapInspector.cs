using Arkademy.Game;
using UnityEditor;
using UnityEngine;
namespace Editor
{
    [CustomEditor(typeof(MapManager))]
    public class CustomMapInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var map = (MapManager) target;
            if (GUILayout.Button("Rebuild Map"))
            {
                map.BuildMap();
                //add everthing the button would do.
            }
        }
    }
}