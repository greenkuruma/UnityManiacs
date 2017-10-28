using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Advanced;

namespace Demo
{
    /// <summary>
    /// BrowserCrasher用CustomPropertyDrawer
    /// </summary>
    [CustomPropertyDrawer (typeof (Treasure))]
    public class TreasureDrawer : MultiPropertyDrawer
    {
        protected override IEnumerable<EntryModule> GetModules ()
        {
            GUIColorSettings buttonColor = new GUIColorSettings ()
            {
                backgroundColor = Color.green
            };

            yield return new EntryModule (
                new LabelModule ("宝箱ID"), 0.1f);
            yield return new EntryModule (
                new PropertyFieldModule ("treasureID"), 0.1f);

            yield return new EntryModule (
                new LabelModule ("説明"), 0.1f);
            yield return new EntryModule (
                new PropertyFieldModule ("description"), 0.7f);

            yield return new EntryModule (
                new PropertyFieldModule ("kinds"), 0.2f, 1);

            yield return new EntryModule (
                new LabelModule ("配置"), 0.1f, 1);
            yield return new EntryModule (
                new PropertyFieldModule ("position"), 0.2f, 1);

            yield return new EntryModule (
                new LabelModule ("値"), 0.1f, 1);
            yield return new EntryModule (
                new PropertyFieldModule ("value"), 0.2f, 1);

            yield return new EntryModule (
                new FixButtonFieldModule ("Preview",
                    "プレビュー"),
                0.2f,
                1,
                false,
                buttonColor);
        }
    }
}