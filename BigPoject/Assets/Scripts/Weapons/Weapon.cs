using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] private string _name = "";                        // Contains name of the weapon.
    [SerializeField] private string _descrpition = "";                 // Contains description of the weapon.
    [SerializeField] private Sprite _inGameImage = null;               // Contains sprite of the weapon that used directly in game.
    [SerializeField] private Sprite _inSelectionMenuImage = null;      // Contains sprite of the weapon that used in weapon selection menu.
    [SerializeField] private int _bulletsAmount = 0;                   // Contains bullet amount of the weapon.
    [SerializeField] private float _fireRate = 0f;                     // Contains fire rate of the weapon.

    // Properties of current class fields.
    #region Properties
    public string Name
    {
        get
        {
            return _name;
        }
    }

    public string Description
    {
        get
        {
            return _descrpition;
        }
    }

    public Sprite InGameImage
    {
        get
        {
            return _inGameImage;
        }
    }

    public Sprite InSelectionMenuImage
    {
        get
        {
            return _inSelectionMenuImage;
        }
    }

    public int BulletsAmount
    {
        get
        {
            return _bulletsAmount;
        }
    }

    public float FireRate
    {
        get
        {
            return _fireRate;
        }
    }
    #endregion Properties

    public abstract void Shoot();
}
