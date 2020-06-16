// GENERATED AUTOMATICALLY FROM 'Assets/KeyboardInputSystem/PlayerControll.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControll : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControll()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControll"",
    ""maps"": [
        {
            ""name"": ""MainGame"",
            ""id"": ""fa87e8c0-48ce-47ff-a7bd-b132e3992c6c"",
            ""actions"": [
                {
                    ""name"": ""LeftMove"",
                    ""type"": ""Button"",
                    ""id"": ""ed174128-589c-4892-9dee-8cc305b126c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMove"",
                    ""type"": ""Button"",
                    ""id"": ""964fe75b-a7a3-499a-8912-2e73f8d39df2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpMove"",
                    ""type"": ""Button"",
                    ""id"": ""eae88091-321a-4867-8cad-47cd33f26621"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""35135d80-0a66-4e61-93c0-5719bd89f8d5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LeftMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11103a9d-e417-4012-a2fc-42213a612471"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LeftMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6318394-da74-4fe2-bebf-e627ee53d9bc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a0b0e13-9280-4e94-b591-ab63838fce5c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25ea4007-9cd9-4990-b131-ce0ec09f887a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UpMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MainGame
        m_MainGame = asset.FindActionMap("MainGame", throwIfNotFound: true);
        m_MainGame_LeftMove = m_MainGame.FindAction("LeftMove", throwIfNotFound: true);
        m_MainGame_RightMove = m_MainGame.FindAction("RightMove", throwIfNotFound: true);
        m_MainGame_UpMove = m_MainGame.FindAction("UpMove", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainGame
    private readonly InputActionMap m_MainGame;
    private IMainGameActions m_MainGameActionsCallbackInterface;
    private readonly InputAction m_MainGame_LeftMove;
    private readonly InputAction m_MainGame_RightMove;
    private readonly InputAction m_MainGame_UpMove;
    public struct MainGameActions
    {
        private @PlayerControll m_Wrapper;
        public MainGameActions(@PlayerControll wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftMove => m_Wrapper.m_MainGame_LeftMove;
        public InputAction @RightMove => m_Wrapper.m_MainGame_RightMove;
        public InputAction @UpMove => m_Wrapper.m_MainGame_UpMove;
        public InputActionMap Get() { return m_Wrapper.m_MainGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainGameActions set) { return set.Get(); }
        public void SetCallbacks(IMainGameActions instance)
        {
            if (m_Wrapper.m_MainGameActionsCallbackInterface != null)
            {
                @LeftMove.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLeftMove;
                @LeftMove.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLeftMove;
                @LeftMove.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLeftMove;
                @RightMove.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnRightMove;
                @RightMove.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnRightMove;
                @RightMove.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnRightMove;
                @UpMove.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnUpMove;
                @UpMove.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnUpMove;
                @UpMove.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnUpMove;
            }
            m_Wrapper.m_MainGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftMove.started += instance.OnLeftMove;
                @LeftMove.performed += instance.OnLeftMove;
                @LeftMove.canceled += instance.OnLeftMove;
                @RightMove.started += instance.OnRightMove;
                @RightMove.performed += instance.OnRightMove;
                @RightMove.canceled += instance.OnRightMove;
                @UpMove.started += instance.OnUpMove;
                @UpMove.performed += instance.OnUpMove;
                @UpMove.canceled += instance.OnUpMove;
            }
        }
    }
    public MainGameActions @MainGame => new MainGameActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMainGameActions
    {
        void OnLeftMove(InputAction.CallbackContext context);
        void OnRightMove(InputAction.CallbackContext context);
        void OnUpMove(InputAction.CallbackContext context);
    }
}
