using UnityEngine;

namespace Advanced
{
    /// <summary>
    /// GUIの色設定
    /// </summary>
    public class GUIColorSettings
    {
		public Color backgroundColor = Color.clear;
        public Color contentColor = Color.clear;

        public Color color = Color.clear;

        public GUIColorSettings ChangeGUIColor ()
        {
            var beforeColorSettings = new GUIColorSettings ()
            {
                backgroundColor = GUI.backgroundColor,
                contentColor = GUI.contentColor,
                color = GUI.color
            };

            if (backgroundColor != Color.clear)
        	    GUI.backgroundColor = backgroundColor;

            // 設定はしているが何も変化してない気がする
            if (contentColor != Color.clear)
        	    GUI.contentColor = contentColor;

            if (color != Color.clear)
        	    GUI.color = color;

            return beforeColorSettings;
        }

        public void RevertGUIColor ()
        {
        	GUI.backgroundColor = backgroundColor;

        	GUI.contentColor = contentColor;

        	GUI.color = color;
        }
    }
}
