using UnityEngine;
using System;
using UnityEditor;
using Common;

namespace Basic
{
    /// <summary>
    /// ブラウザクラッシャー情報設定レシピ
    /// </summary>
    [CreateAssetMenu (fileName = "BasicBrowserCrasherRecipe",
                      menuName = "BasicBrowserCrasherRecipe")]
    public class BrowserCrasherRecipe : ScriptableObject
    {
        public BrowserCrasher crasher;
        /// <summary>
        /// 表示されているポップアップウィンドウを１つ閉じます
        /// </summary>
        public void Close ()
        {
            // Popupが1つもない場合は、作成した後に削除される
            EditorWindow.GetWindow<Popup> ().Close ();
        }
        /// <summary>
        /// BrowserCrasherで設定された数だけポップアップウィンドウを作成します
        /// </summary>
        public void Crash ()
        {
            var volume = crasher.volume;
            if (volume <= 0)
                Debug.LogWarning ("volumeは1以上の値にしてね");

            if (volume > 500)
            {
                Debug.LogWarning ("生成が一度に500を超えるとヤバそうなので、500にしておくね");
                volume = 500;
            }

            for (int i = 0 ; i < volume ; i++)
                Make ();
        }
        /// <summary>
        /// ポップアップウィンドウを作成します
        /// </summary>
        void Make ()
        {
            var rect = new Rect(0, 0, crasher.window.x, crasher.window.y);

            // ブラクラらしさを出すために表示位置はランダム
            rect.x = UnityEngine.Random.Range (
                0, Screen.currentResolution.width - crasher.window.x);
            rect.y = UnityEngine.Random.Range (
                0, Screen.currentResolution.height - crasher.window.y);

            Popup.Create (crasher.message, rect);
        }
    }

    /// <summary>
    /// ブラウザクラッシャーの設定情報
    /// </summary>
    [Serializable]
    public class BrowserCrasher
    {
        // ウィンドウサイズ
        public Vector2 window;
        // ウィンドウメッセージ
        public string message;
        // ウィンドウを作成する数
        public int volume;
    }
}