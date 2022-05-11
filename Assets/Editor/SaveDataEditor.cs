using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveDataEditor : EditorWindow
{
    SaveData saveData;
    const string saveDataFileName = "SaveData.json";
    bool loaded;
    [MenuItem("Window/SaveDataEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SaveDataEditor));
    }

    private void OnGUI()
    {
        int height = 20;
        if (loaded)
        {
            // Soft coins.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Soft Coins", GUILayout.Height(height), GUILayout.Width(200));
            saveData.softCoins = EditorGUILayout.IntField(saveData.softCoins, GUILayout.Height(height), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            // Hard coins.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Hard Coins", GUILayout.Height(height), GUILayout.Width(200));
            saveData.hardCoins = EditorGUILayout.IntField(saveData.hardCoins, GUILayout.Height(height), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            // Energy.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Energy", GUILayout.Height(height), GUILayout.Width(200));
            saveData.energy = EditorGUILayout.IntField(saveData.energy, GUILayout.Height(height), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            // Last Connection.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Last Connection", GUILayout.Height(height), GUILayout.Width(200));
            saveData.lastConnection = EditorGUILayout.TextField(saveData.lastConnection, GUILayout.Height(height), GUILayout.Width(200)); 
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Formato: dd/mm/yyyy hh:mm:ss", GUILayout.Height(height), GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();

            // Experience.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Experience", GUILayout.Height(height), GUILayout.Width(200));
            saveData.experience = EditorGUILayout.IntField(saveData.experience, GUILayout.Height(height), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        
            // Inventory
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Inventory", GUILayout.Height(height), GUILayout.Width(200));
            saveData.inventory = EditorGUILayout.TextField(saveData.inventory, GUILayout.Height(height), GUILayout.Width(200)); 
            EditorGUILayout.EndHorizontal();

            // Inventory
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Equipment", GUILayout.Height(height), GUILayout.Width(200));
            saveData.equipment = EditorGUILayout.TextField(saveData.equipment, GUILayout.Height(height), GUILayout.Width(200)); 
            EditorGUILayout.EndHorizontal();

            //Talents.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Talents", GUILayout.Height(height), GUILayout.Width(200));
            for (int i = 0; i < saveData.talentsList.Length; i++)
            {
                saveData.talentsList[i] = EditorGUILayout.IntField(saveData.talentsList[i], GUILayout.Height(height), GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();

            // Current World.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Current World", GUILayout.Height(height), GUILayout.Width(200));
            saveData.currentWorld = EditorGUILayout.IntField(saveData.currentWorld, GUILayout.Height(height), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            // Highest Stage Reached.
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Highest Stage Reached", GUILayout.Height(height), GUILayout.Width(200));
            for (int i = 0; i < saveData.highestStageReached.Length; i++)
            {
                saveData.highestStageReached[i] = EditorGUILayout.IntField(saveData.highestStageReached[i], GUILayout.Height(height), GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();

            // //Ejemplo de variable bool
            // EditorGUILayout.BeginHorizontal();
            // GUILayout.Label("bool Example", GUILayout.Height(height), GUILayout.Width(200));
            // saveData.boolExample = EditorGUILayout.Toggle(saveData.boolExample, GUILayout.Height(height), GUILayout.Width(20));
            // EditorGUILayout.EndHorizontal();

            // //Ejemplo de variable int
            // EditorGUILayout.BeginHorizontal();
            // GUILayout.Label("int Example", GUILayout.Height(height), GUILayout.Width(200));
            // saveData.intExample = EditorGUILayout.IntField(saveData.intExample, GUILayout.Height(height), GUILayout.Width(20));
            // EditorGUILayout.EndHorizontal();

            // //Ejemplo de variable string
            // EditorGUILayout.BeginHorizontal();
            // GUILayout.Label("string example", GUILayout.Height(height), GUILayout.Width(200));
            // saveData.stringExample = EditorGUILayout.TextField(saveData.stringExample, GUILayout.Height(height), GUILayout.Width(500));
            // EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save Data"))
            {
                File.WriteAllText(Application.persistentDataPath + "/" + saveDataFileName, JsonUtility.ToJson(saveData));
                loaded = false;
            }
        }
        else
        {
            if (GUILayout.Button("Change SaveData Values"))
            {
                saveData =SaveDataController.GetSaveData();
                loaded= true;
            }
        }

    }
}
