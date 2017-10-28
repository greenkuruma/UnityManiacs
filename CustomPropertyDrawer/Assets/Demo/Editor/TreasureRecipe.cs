using UnityEngine;
using System;
using UnityEditor;
using Common;

namespace Demo
{
    /// <summary>
    /// 宝箱情報のレシピ
    /// </summary>
    [CreateAssetMenu (fileName = "TreasureRecipe",
                      menuName = "TreasureRecipe")]
    public class TreasureRecipe : ScriptableObject
    {
        public string mapID;
        public string description;
        public Treasure[] treasures;
        public void Preview ()
        {
            // @todo シーン上に配置
        }
    }

    /// <summary>
    /// 宝箱情報
    /// 2Dゲーム想定
    /// </summary>
    [Serializable]
    public class Treasure
    {
        public enum Kind
        {
            Item,
            Money,
            Monster,
            Event,
            Flag
        }

        public string description;
        public string treasureID;
        public Kind kinds;
        public Vector2 position;
        public int value;
    }
}