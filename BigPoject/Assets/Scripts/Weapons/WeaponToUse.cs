using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponToUse : MonoBehaviour
{
    [SerializeField] private Weapon _currenWeapon = null;               // Contains data of the weapon that currently used.
    private SpriteRenderer _spriteRenderer = null;                      // Contains spriteRendere component of current gameObject.


    private void Awake()
    {
        IntializeSpriteRendererComponent();
    }


    private void IntializeSpriteRendererComponent()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _spriteRenderer.sprite = _currenWeapon.InGameImage;
    }
}
