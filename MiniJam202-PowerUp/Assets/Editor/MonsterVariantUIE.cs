using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


[CustomPropertyDrawer(typeof(MonsterVariant))]
public class MonsterVariantUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        //VisualElement container = base.CreatePropertyGUI(property);

        var popup = new UnityEngine.UIElements.PopupWindow();

        popup.text = "Monster Variant";
        popup.Add(new PropertyField(property.FindPropertyRelative("Monster"),"Monster"));
        popup.Add(new PropertyField(property.FindPropertyRelative("SpawnCurve"),"Spawn Curve"));
        

        return popup;
    }
}
