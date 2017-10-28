using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Advanced
{
    /// <summary>
    /// GetModulesで取得したモジュールをいい感じに描画してくれるPropertyDrawer
    /// </summary>
    public abstract class MultiPropertyDrawer : PropertyDrawer
    {
        #region override

        /// <summary>
        /// 処理すべきモジュール達を返す
        /// </summary>
        protected abstract IEnumerable<EntryModule> GetModules ();

        protected virtual float lineHeight
        {
            get { return EditorGUIUtility.singleLineHeight; }
        }

        protected virtual float verticalSpacing
        {
            get { return EditorGUIUtility.standardVerticalSpacing; }
        }

        protected virtual float horizontalSpacing
        {
            get { return EditorGUIUtility.standardVerticalSpacing*4; }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        protected virtual Color backGroundColor
        {
            get { return Color.clear; }
        }

        public override float GetPropertyHeight (SerializedProperty prop,
                                                 GUIContent label)
        {
            // 行間を考慮
            return lineLength * lineHeight +
                   (lineLength - 1) * verticalSpacing;
        }
        #endregion

        protected float lineLength
        {
            get { return GetModules ().Max (Module =>
                Module.lineIndex) + 1; }
        }

        protected float GetLinePosition(int lineIndex)
        {
            return lineIndex * (lineHeight + verticalSpacing);
        }

	    public override void OnGUI (Rect position,
                                    SerializedProperty property,
                                    GUIContent label)
        {
		    EditorGUI.BeginProperty (position, label, property);

            EditorGUI.DrawRect(position, backGroundColor);

		    position = EditorGUI.PrefixLabel (
                position,
                GUIUtility.GetControlID (FocusType.Passive),
                label
            );
	
		    var tempIndentLevel = EditorGUI.indentLevel;
		    EditorGUI.indentLevel = 0;
		
            for (int lineIndex = 0 ; lineIndex < lineLength ; lineIndex++)
            {
                var line = GetModules ().Where (Module =>
                    Module.lineIndex == lineIndex);
                float widthRatioSum = line.Sum (Module =>
                    Module.widthRatio);

                // horizontalSpacingの扱いはちょっと手抜きです
                float rectX = position.x;
                var lineCount = line.Count ();
                line.ToList ().ForEach (module =>
                {
                    // 0～1
                    var widthPer = module.widthRatio / widthRatioSum;

                    var rect = new Rect (
                        rectX,
                        position.y + GetLinePosition(lineIndex),
                        position.width * widthPer -
                            (horizontalSpacing * (lineCount-1) / lineCount),
                        lineHeight);

                    module.DrawGUI (property, rect);
                    rectX += widthPer * position.width +
                        (horizontalSpacing / (lineCount-1));
                });
            }
		
		    EditorGUI.indentLevel = tempIndentLevel;
		
		    EditorGUI.EndProperty ();
	    }
    }
}