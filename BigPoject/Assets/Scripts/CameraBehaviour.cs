using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{ 
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = null;
    private CinemachineFramingTransposer _cinemachineFramingTransposer = null;
    private bool _isDampingStored = false;

    private struct OldCameraDampingStorage
    {
        public float XDamping{ get; set; }
        public float YDamping{ get; set; }
        public float ZDamping{ get; set; }
    }
    private OldCameraDampingStorage _oldCameraDampingStorage;

    
    private void Awake()
    {
        InitializeComponents();   
    }


    private void Start()
    {
        InitializeOldCameraDampingStorage();
    }


    public void DiasableDamping()
    {
        if (!_isDampingStored)
        {
            // Store current damping value in Storage.
            _oldCameraDampingStorage.XDamping = _cinemachineFramingTransposer.m_XDamping;
            _oldCameraDampingStorage.YDamping = _cinemachineFramingTransposer.m_YDamping;
            _oldCameraDampingStorage.ZDamping = _cinemachineFramingTransposer.m_ZDamping;

            _isDampingStored = true;
        }

        // Set damping to zero.
        _cinemachineFramingTransposer.m_XDamping = 0;
        _cinemachineFramingTransposer.m_YDamping = 0;
        _cinemachineFramingTransposer.m_ZDamping = 0;
    }


    public void RestoreDamping()
    {
        // Restore current damping values from Storage;
        _cinemachineFramingTransposer.m_XDamping = _oldCameraDampingStorage.XDamping;
        _cinemachineFramingTransposer.m_YDamping = _oldCameraDampingStorage.YDamping;
        _cinemachineFramingTransposer.m_ZDamping = _oldCameraDampingStorage.ZDamping;

        _isDampingStored = false;
    }


    private void InitializeComponents()
    {
        _cinemachineFramingTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }


    private void InitializeOldCameraDampingStorage()
    {
        _oldCameraDampingStorage.XDamping = _cinemachineFramingTransposer.m_XDamping;
        _oldCameraDampingStorage.YDamping = _cinemachineFramingTransposer.m_YDamping;
        _oldCameraDampingStorage.ZDamping = _cinemachineFramingTransposer.m_ZDamping;
    }
}
