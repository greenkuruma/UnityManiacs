using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace Basic
{
    /// <summary>
    /// BrowserCrasher用CustomPropertyDrawer
    /// </summary>
    [CustomPropertyDrawer (typeof (BrowserCrasher))]
    public class BrowserCrasherDrawer : PropertyDrawer
    {
	    public override void OnGUI (Rect position,
                                    SerializedProperty property,
                                    GUIContent label)
        {
		    EditorGUI.BeginProperty (position, label, property);

		    position = EditorGUI.PrefixLabel (
                position,
                GUIUtility.GetControlID (FocusType.Passive),
                label
            );

            // BrowserCrasher.window
            var windowProperty = property.FindPropertyRelative ("window");
            var windowRect = new Rect (
                position.x,
                position.y,
                160,
                position.height
            );
            DrawProperty (windowRect, windowProperty);

            // BrowserCrasher.message
            var messageProperty = property.FindPropertyRelative ("message");
            var messageRect = new Rect (
                windowRect.x + windowRect.width +
                    EditorGUIUtility.standardVerticalSpacing,
                position.y,
                120,
                position.height
            );
            DrawProperty (messageRect, messageProperty);

            // BrowserCrasher.volume
            var volumeProperty = property.FindPropertyRelative ("volume");
            var volumeRect = new Rect (
                messageRect.x + messageRect.width +
                    EditorGUIUtility.standardVerticalSpacing,
                position.y,
                80,
                position.height
            );
            DrawProperty (volumeRect, volumeProperty);

            // BrowserCrasherRecipe.Crash
            var crasherButtonRect = new Rect (
                volumeRect.x + volumeRect.width +
                    EditorGUIUtility.standardVerticalSpacing,
                position.y,
                200,
                position.height
            );
            DrawButton (crasherButtonRect, property, "Crash",
                "Enter 18歳以上なので入場する" );

            // BrowserCrasherRecipe.Close
            var closeButtonRect = new Rect (
                crasherButtonRect.x + crasherButtonRect.width +
                    EditorGUIUtility.standardVerticalSpacing,
                position.y,
                20,
                position.height
            );
            DrawButton (closeButtonRect, property, "Close",
                "×" );

		    EditorGUI.EndProperty ();
	    }
	    public void DrawProperty (Rect rect,
                                  SerializedProperty property)
        {
            // 余計なものを表示させないために、GUIContent.noneを設定
            EditorGUI.PropertyField (rect, property, GUIContent.none);
        }
	    public void DrawButton (Rect rect,
                                SerializedProperty property,
                                string buttonMethodName,
                                string buttonName)
        {
            if (GUI.Button (rect, buttonName))
            {
                var targetObject =
                    property.serializedObject.targetObject;
                var type =
                    targetObject.GetType();
                var bindingAttr =
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic;
                var method =
                    type.GetMethod( buttonMethodName, bindingAttr );
            
                try
                {
                    method.Invoke( targetObject, new object[0] );
                }
                catch ( Exception e )
                {
                   Debug.LogError (e.ToString());
                }
            }
        }
    }
}