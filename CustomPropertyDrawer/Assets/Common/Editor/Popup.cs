using UnityEngine;
using UnityEditor;

namespace Common
{
    /// <summary>
    /// ポップアップウィンドウ
    /// サンプルなので機能は最小限です
    /// </summary>
    public class Popup : EditorWindow
    {
        string message;

        public static void Create (string message, Rect windowRect)
        {
            var window = CreateInstance<Popup> ();
            window.position = windowRect;
            window.message = message;

            // ShowPopupを呼び出す必要がある
            window.ShowPopup ();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField (message,
                EditorStyles.wordWrappedLabel);

            // OKボタンが押されたら閉じる
            if (GUILayout.Button ("OK"))
                Close ();

            // ESCキーが押されても閉じる
            if (Event.current.keyCode == KeyCode.Escape)
                Close ();
        }
    }
}
