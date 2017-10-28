using UnityEditor;

namespace Advanced
{
    /// <summary>
    /// SerializedProperty要の拡張メソッド
    /// </summary>
    public static class SerializedPropertyExtension
    {
        /// <summary>
        /// SerializedPropertyの値を文字列に
        /// </summary>
        public static string ValueToString (this SerializedProperty property)
        {
            switch (property.propertyType)
            {
            case SerializedPropertyType.Integer:
                return property.intValue.ToString ();
            case SerializedPropertyType.Boolean:
                return property.boolValue.ToString ();
            case SerializedPropertyType.Float:
                return property.floatValue.ToString ();
            case SerializedPropertyType.String:
                return property.stringValue;
            case SerializedPropertyType.Color:
                return property.colorValue.ToString ();
            case SerializedPropertyType.Enum:
                return property.enumNames[property.enumValueIndex];
            }

            return "";
        }
    }
}