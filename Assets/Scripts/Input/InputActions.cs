// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""GamePlayer"",
            ""id"": ""b9190056-c1d7-4683-a73b-644ff967e5e5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""79ce1a38-e4cf-4fe1-b8ab-98827deafc82"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d451b147-1f86-4530-a62b-b1e636798fba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge1"",
                    ""type"": ""Button"",
                    ""id"": ""59c3859c-b3b9-4cdf-a63a-e8626ff17bbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2922fc52-2bde-4664-9ad3-381135932f78"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d52ca89-69cb-43c5-8a4c-c69a4cbbd9a2"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9544e418-d49d-42c7-bf68-e562aa245195"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d0e83be6-1e97-480b-a520-6c4ae4d7ec56"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""33e1b42d-db65-4709-8f54-9eba1104fbb1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0283d760-ada0-458b-a5e8-fef8265cf10c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f1461c5-205c-4da8-8269-4c57a7517d83"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""958e90f3-cff0-456f-97f5-342dbc1a7b2e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01c71986-37e1-4dff-8d98-4623cac5dff8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Dodge1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f500eb57-9a59-473a-8fbb-11d20f44f93f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Dodge1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlayer
        m_GamePlayer = asset.FindActionMap("GamePlayer", throwIfNotFound: true);
        m_GamePlayer_Move = m_GamePlayer.FindAction("Move", throwIfNotFound: true);
        m_GamePlayer_Fire = m_GamePlayer.FindAction("Fire", throwIfNotFound: true);
        m_GamePlayer_Dodge1 = m_GamePlayer.FindAction("Dodge1", throwIfNotFound: true);
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

    // GamePlayer
    private readonly InputActionMap m_GamePlayer;
    private IGamePlayerActions m_GamePlayerActionsCallbackInterface;
    private readonly InputAction m_GamePlayer_Move;
    private readonly InputAction m_GamePlayer_Fire;
    private readonly InputAction m_GamePlayer_Dodge1;
    public struct GamePlayerActions
    {
        private @InputActions m_Wrapper;
        public GamePlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlayer_Move;
        public InputAction @Fire => m_Wrapper.m_GamePlayer_Fire;
        public InputAction @Dodge1 => m_Wrapper.m_GamePlayer_Dodge1;
        public InputActionMap Get() { return m_Wrapper.m_GamePlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayerActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayerActions instance)
        {
            if (m_Wrapper.m_GamePlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnFire;
                @Dodge1.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDodge1;
                @Dodge1.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDodge1;
                @Dodge1.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDodge1;
            }
            m_Wrapper.m_GamePlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Dodge1.started += instance.OnDodge1;
                @Dodge1.performed += instance.OnDodge1;
                @Dodge1.canceled += instance.OnDodge1;
            }
        }
    }
    public GamePlayerActions @GamePlayer => new GamePlayerActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IGamePlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnDodge1(InputAction.CallbackContext context);
    }
}
