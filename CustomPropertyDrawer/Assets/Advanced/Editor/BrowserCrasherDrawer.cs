using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Advanced
{
    /// <summary>
    /// BrowserCrasher用CustomPropertyDrawer
    /// </summary>
    [CustomPropertyDrawer (typeof (BrowserCrasher))]
    public class BrowserCrasherDrawer : MultiPropertyDrawer
    {
        protected override IEnumerable<EntryModule> GetModules ()
        {
            yield return new EntryModule (
                new LabelModule ("window"), 0.1f);
            yield return new EntryModule (
                new PropertyFieldModule ("window"), 0.3f);

            yield return new EntryModule (
                new LabelModule ("message"), 0.1f);
            yield return new EntryModule (
                new PropertyFieldModule ("message"), 0.2f);

            yield return new EntryModule (
                new LabelModule ("volume"), 0.1f);
            yield return new EntryModule (
                new PropertyFieldModule ("volume"), 0.2f);

            yield return new EntryModule (
                new FixButtonFieldModule ("Crash",
                    "Enter 18歳以上なので入場する"),
                    0.2f,
                    1,
                    false,
                    new GUIColorSettings()
                    {
                        color = new Color (1, 0.5f, 0.5f, 1)
                    });

            yield return new EntryModule (
                new FixButtonFieldModule ("Close",
                    "×"),
                    0.2f,
                    1);
        }

        // 背景色を緑にしておけば心が豊かになります
        protected override Color backGroundColor
        {
            get { return Color.green; }
        }
    }
}