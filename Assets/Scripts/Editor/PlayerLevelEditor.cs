using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class PlayerLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameController myTarget = (GameController)target;

        //myTarget.expLevel = EditorGUILayout.IntField("Experiencia total", myTarget.expLevel);
        //myTarget.expPoints = (int)EditorGUILayout.Slider(myTarget.expPoints, 1, 1000);
        //EditorGUILayout.LabelField("Nivel", "hola mi nivel es " + myTarget.Level.ToString());
        //GUILayout.Button("asdasdasdads Datos");

        if (GUILayout.Button("LevelUp"))
        {
            myTarget.LevelUp();
        }

        if (GUILayout.Button("Ganar Juego"))
        {
            myTarget.GameOver(true);
        }

        if (GUILayout.Button("Perder Juego"))
        {
            myTarget.GameOver(false);
        }

        if (GUILayout.Button("Reiniciar Juego"))
        {
            myTarget.RestartGame();
        }

    }
}
