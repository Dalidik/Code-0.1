using Data.JSON;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerIconInShopWindow", menuName = "Shop/Icon")]
    public class PlayerIcon : ScriptableObject
    {
        public string Id;
        public Sprite IconSprite;
        public int Stars;
        public int Cost;
        
        public GameResource ResourceType;
    }
}