using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class EditorMap : Editor
{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Map map = target as Map;

            map.GenerateMap();

        }
   
}
