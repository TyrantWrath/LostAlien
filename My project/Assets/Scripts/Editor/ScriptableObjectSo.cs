using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponIntractSO))]
public class WeaponIntractSOCustomEditor : Editor
{
    private bool isHealthItem = false;
    private bool isWeaponItem = false;
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        WeaponIntractSO _weaponIntractSO = (WeaponIntractSO)target;
        GUILayout.Space(20);

        Rect rect = new Rect(5, 50, 800, 110);
        Rect rect2 = new Rect(5, 100, 800, 4);

        EditorGUI.DrawRect(rect, new Color(0, 0, 0, 255));
        EditorGUI.DrawRect(rect2, new Color(255, 0, 0, 255));
        /* if (GUILayout.Button("Health Item", GUILayout.Height(50)))
         {
             isHealthItem = true;
             isWeaponItem = false;

         }
         else if (GUILayout.Button("Weapon Item", GUILayout.Height(50)))
         {
             isHealthItem = false;
             isWeaponItem = true;
         }*/
        //if (isWeaponItem)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            GUILayout.Label("MyWeaponIndex", GUILayout.Width(120));
            _weaponIntractSO.myWeaponIndex = EditorGUILayout.IntField(_weaponIntractSO.myWeaponIndex);
            GUILayout.Space(5);

            GUILayout.Space(5);
            GUILayout.Label("DurabilityAmount", GUILayout.Width(150));
            _weaponIntractSO.durabilyAmountToAdd = EditorGUILayout.FloatField(_weaponIntractSO.durabilyAmountToAdd);
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
            GUILayout.Space(40);
            // }
            // if (isHealthItem)
            // {
            GUILayout.BeginHorizontal();

            GUILayout.Space(5);
            GUILayout.Label("isHealthItem", GUILayout.Width(120));
            _weaponIntractSO.isHealthItem = EditorGUILayout.Toggle(_weaponIntractSO.isHealthItem);
            GUILayout.Space(5);

            GUILayout.Space(5);
            GUILayout.Label("HealthAmount", GUILayout.Width(150));
            _weaponIntractSO.healthAmountToAdd = EditorGUILayout.FloatField(_weaponIntractSO.healthAmountToAdd);
            GUILayout.Space(5);

            GUILayout.EndHorizontal();

            //}



        }
    }
}