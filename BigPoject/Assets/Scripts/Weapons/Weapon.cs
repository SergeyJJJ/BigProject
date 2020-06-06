using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] private string _name = "";
    [SerializeField] private string _descrpition = "";
    [SerializeField] private Sprite _inHandImage = null;
    [SerializeField] private Sprite _inSelectionMenuImage = null;
    [SerializeField] private int _bulletsAmount = 0;
    [SerializeField] private float _fireRate = 0f;


    public abstract void Shoot();
}
