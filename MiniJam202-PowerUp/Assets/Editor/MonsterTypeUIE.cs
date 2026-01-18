using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(MonsterType))]
public class MonsterTypeUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        //VisualElement container = base.CreatePropertyGUI(property);

        var popup = new UnityEngine.UIElements.PopupWindow();

        popup.text = "Monsters";
        popup.Add(new PropertyField(property.FindPropertyRelative("Name"), "Monster Type Name"));
        popup.Add(new PropertyField(property.FindPropertyRelative("Variants"), "Monsters variants"));
        popup.Add(new PropertyField(property.FindPropertyRelative("SpawnPoints"), "Spawn Points"));


        return popup;
    }
}

