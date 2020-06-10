using UnityEngine;

public class ScriptController : MonoBehaviour
{
    [SerializeField] private CharacterJump _characterJump = null;


    public void DisableJumpScript()
    {
        if (_characterJump != null)
        {
            _characterJump.enabled = false;
        }
    }

    
    public void EnableJumpScript()
    {
        if (_characterJump != null)
        {
            _characterJump.enabled = true;
        }
    }
}
