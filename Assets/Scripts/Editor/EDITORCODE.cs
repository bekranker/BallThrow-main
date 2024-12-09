using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteChanger))]
public class SpriteChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // SpriteChanger referansını al
        SpriteChanger spriteChanger = (SpriteChanger)target;

        // Random toggle için checkbox
        spriteChanger.randomToggle = EditorGUILayout.Toggle("Random Mode", spriteChanger.randomToggle);

        // "Sprites to Find" listesi
        EditorGUILayout.LabelField("Sprites to Find");
        SerializedProperty spritesToFindProperty = serializedObject.FindProperty("spritesToFind");
        EditorGUILayout.PropertyField(spritesToFindProperty, true); // Listeyi düzenlenebilir yap
        serializedObject.ApplyModifiedProperties();

        if (spriteChanger.randomToggle)
        {
            // Random Mode aktif: Random sprite listesi
            EditorGUILayout.LabelField("Random Sprite List");
            SerializedProperty randomSpritesProperty = serializedObject.FindProperty("randomSprites");
            EditorGUILayout.PropertyField(randomSpritesProperty, true); // Listeyi düzenlenebilir yap
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            // Random Mode kapalı: Tek bir sprite ile değiştir
            spriteChanger.spriteToReplace = (Sprite)EditorGUILayout.ObjectField("Sprite to Replace", spriteChanger.spriteToReplace, typeof(Sprite), false);
        }

        // Buton ekle
        if (GUILayout.Button("Change Sprites"))
        {
            // ChangeTheSprite fonksiyonunu çağır
            spriteChanger.ChangeTheSprite();
        }
    }
}