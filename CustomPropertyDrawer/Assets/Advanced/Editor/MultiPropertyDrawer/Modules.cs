using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace Advanced
{
    /// <summary>
    /// 始動モジュール
    /// </summary>
    public class EntryModule
    {
        public IModule module;

        public float widthRatio;

        // 行番号。0スタート
        public int lineIndex;

        protected bool disabled;

        public GUIColorSettings colorSettings;

        public EntryModule (IModule module,
                            float widthRatio = 1f, 
                            int lineIndex = 0, 
                            bool disabled = false,
                            GUIColorSettings colorSettings = null)
        {
            this.module = module;
            this.widthRatio = widthRatio;
            this.lineIndex = lineIndex;
            this.disabled = disabled;
            this.colorSettings = (colorSettings == null)
                ? new GUIColorSettings ()
                : colorSettings;
        }

        public void DrawGUI (SerializedProperty property,
                             Rect rect)
        {
            if (module != null)
            {
                EditorGUI.BeginDisabledGroup (disabled);

                var beforeColor = colorSettings.ChangeGUIColor ();

                module.DrawGUI (this, property, rect);

                beforeColor.RevertGUIColor ();

                EditorGUI.EndDisabledGroup ();
            }
        }
    }

    /// <summary>
    /// モジュールInterface
    /// </summary>
    public interface IModule
    {
        void DrawGUI (EntryModule moduleBase,
                      SerializedProperty property,
                      Rect rect);
    }

    /// <summary>
    /// プロパティ表示
    /// </summary>
    public class PropertyFieldModule : IModule
    {
        public string path;

        public PropertyFieldModule (string path)
        {
            this.path = path;
        }
        public void DrawGUI (EntryModule moduleBase,
                             SerializedProperty property,
                             Rect rect)
        {
            // @todo PropertyFieldにも色設定を行えるようにする。GUISkinでいけるはず…
            // 余計なものを表示させないために、GUIContent.noneを設定
            EditorGUI.PropertyField (rect,
                                     property.FindPropertyRelative (path),
                                     GUIContent.none);
        }
    }

    /// <summary>
    /// ボタン表示
    /// プロパティの値をボタンに表示している
    /// </summary>
    public class ButtonFieldModule : IModule
    {
        public string stringPropertyPath;
        public string buttonMethodName;

        public ButtonFieldModule (string buttonMethodName,
                                  string stringPropertyPath)
        {
            this.stringPropertyPath = stringPropertyPath;

            this.buttonMethodName = buttonMethodName;
        }
        public void DrawGUI (EntryModule moduleBase,
                             SerializedProperty property,
                             Rect rect)
        {
            var buttonProperty =
                property.FindPropertyRelative (stringPropertyPath);

            ButtonUtility.DrawGUI (property,
                                   rect,
                                   buttonProperty.ValueToString (),
                                   buttonMethodName,
                                   moduleBase.colorSettings.contentColor);
        }
    }

    /// <summary>
    /// ボタン表示
    /// 固定文字列をボタンに表示している
    /// </summary>
    public class FixButtonFieldModule : IModule
    {
        public string buttonName;
        public string buttonMethodName;

        public FixButtonFieldModule (string buttonMethodName,
                                     string buttonName)
        {
            this.buttonName = buttonName;
            this.buttonMethodName = buttonMethodName;
        }
        public void DrawGUI (EntryModule moduleBase,
                             SerializedProperty property,
                             Rect rect)
        {
            ButtonUtility.DrawGUI (property,
                                   rect,
                                   buttonName,
                                   buttonMethodName,
                                   moduleBase.colorSettings.contentColor);
        }
    }

    /// <summary>
    /// ラベル表示
    /// EditorGUI.PropertyFieldで表示できるラベルは調整しづらく、
    /// 間延びしやすいため、専用で用意している
    /// </summary>
    public class LabelModule : IModule
    {
        public string text;
        public float widthExtension;

        public LabelModule (string text,
                            float widthExtension = 2f)
        {
            this.text = text;
            this.widthExtension = widthExtension;
        }
        public void DrawGUI (EntryModule moduleBase,
                             SerializedProperty property,
                             Rect rect)
        {
            var style = new GUIStyle(GUI.skin.label);
            if (moduleBase.colorSettings.contentColor != Color.clear)
                style.normal.textColor = moduleBase.colorSettings.contentColor;

            rect.width += widthExtension;
            EditorGUI.LabelField (rect, text, style);
        }
    }

    /// <summary>
    /// ボタン表示Utility
    /// </summary>
    public static class ButtonUtility
    {
        public static void DrawGUI (SerializedProperty property,
                                    Rect rect,
                                    string buttonName,
                                    string buttonMethodName,
                                    Color textColor)
        {
            var style = new GUIStyle(GUI.skin.button);
            if (textColor != Color.clear)
                style.normal.textColor = textColor;

            if (GUI.Button (rect, buttonName, style))
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
                    type.GetMethod (buttonMethodName, bindingAttr);
            
                try
                {
                    method.Invoke (targetObject, new object[0]);
                }
                catch ( Exception e )
                {
                   Debug.LogError (e.ToString());
                }
            }
        }
    }
}