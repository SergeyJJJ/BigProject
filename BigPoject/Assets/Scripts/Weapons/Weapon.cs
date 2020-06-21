using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _description = "";
        [SerializeField] private Sprite _inGameSprite = null;

        #region Protperties

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public Sprite InGameSprite
        {
            get { return _inGameSprite; }
        }


        #endregion Properties
    }
}
