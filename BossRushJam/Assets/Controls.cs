//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player Action Map"",
            ""id"": ""0c02b7e9-f111-4521-b5a2-ba32d2e036e3"",
            ""actions"": [
                {
                    ""name"": ""Locomotion"",
                    ""type"": ""Value"",
                    ""id"": ""fb0726cf-e317-4bc6-ab15-7a5ced874f84"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""c8856e5b-911b-448c-a6f2-f11ee4989391"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CraftingSelectUp"",
                    ""type"": ""Button"",
                    ""id"": ""8205680f-ced2-4776-9223-7e7cb57c7ac2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CraftingSelectDown"",
                    ""type"": ""Button"",
                    ""id"": ""677caf10-4f8e-4811-84d3-00b2f360cdd5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CraftingButton"",
                    ""type"": ""Button"",
                    ""id"": ""584908aa-21d7-499f-817d-9b20cca13a7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c09a6762-6039-43f4-a66c-ac027bd60482"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""051ce51a-6ff6-450d-8b4c-c0c7221960b9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""e8ce15bd-95f4-49a4-b4c2-ab5b34f46e8e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""99e2c190-2590-4820-8041-f2e6b70e73a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72da2379-cb94-4a30-8fa2-47da51d34465"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""46e90f4f-c6dd-4517-9c3d-66b68096fb66"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6ab93e6a-7a61-4722-b5b7-69a2efeb8e05"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""867155af-ab45-4f56-bc1a-f18b0fbf1933"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bb43d611-f004-4700-93c7-eb71d7ca3676"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bcdb708c-b6d1-4dab-985f-fe1cb63d0323"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""5109bd1f-bc2e-4a86-9355-375b289e79b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e4193148-eae8-4f3b-ba90-8774717030d8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""97236fca-daab-485e-9465-49c2e6a88b43"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""71a4a6e4-baae-40aa-9821-69323a64f33b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e1d9ae2c-0333-4164-82d2-ad0a3c5e0597"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e94500e8-9500-4281-8934-5122fdf9f461"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c80ebab8-148e-4484-a260-aa0b10e3d1e8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45f3a5fd-b2a3-4fff-95cd-6fcbcf14155d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CraftingSelectUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5c21d6c-0ca1-495c-b10e-760ba96ee26d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CraftingSelectDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af6a2f9c-62e1-42db-8507-17f8b0afb570"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4be0ed4e-a0c1-48e6-b48e-c16a63ff8595"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e6d2962-0288-40b3-b33c-655417b64125"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47f941f2-fce7-4850-a61b-19af2d7a507d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b87b127-d76c-4b83-b944-bc96d5d014a3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce6addcd-42e8-4761-b373-73cf197d1dde"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CraftingButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be3bd830-14b0-4adc-b814-8a1e6efeede8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""CraftingButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""645ba3fb-0f22-44d2-916c-951a37abdf43"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cc87511-37d8-4a7a-8cf4-d680879f0822"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20c5fd8a-8ea5-449c-a04a-2a9d3b1b4a35"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": [
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
        // Player Action Map
        m_PlayerActionMap = asset.FindActionMap("Player Action Map", throwIfNotFound: true);
        m_PlayerActionMap_Locomotion = m_PlayerActionMap.FindAction("Locomotion", throwIfNotFound: true);
        m_PlayerActionMap_Slide = m_PlayerActionMap.FindAction("Slide", throwIfNotFound: true);
        m_PlayerActionMap_CraftingSelectUp = m_PlayerActionMap.FindAction("CraftingSelectUp", throwIfNotFound: true);
        m_PlayerActionMap_CraftingSelectDown = m_PlayerActionMap.FindAction("CraftingSelectDown", throwIfNotFound: true);
        m_PlayerActionMap_CraftingButton = m_PlayerActionMap.FindAction("CraftingButton", throwIfNotFound: true);
        m_PlayerActionMap_Attack = m_PlayerActionMap.FindAction("Attack", throwIfNotFound: true);
        m_PlayerActionMap_Look = m_PlayerActionMap.FindAction("Look", throwIfNotFound: true);
        m_PlayerActionMap_ScrollWheel = m_PlayerActionMap.FindAction("ScrollWheel", throwIfNotFound: true);
        m_PlayerActionMap_Pause = m_PlayerActionMap.FindAction("Pause", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player Action Map
    private readonly InputActionMap m_PlayerActionMap;
    private IPlayerActionMapActions m_PlayerActionMapActionsCallbackInterface;
    private readonly InputAction m_PlayerActionMap_Locomotion;
    private readonly InputAction m_PlayerActionMap_Slide;
    private readonly InputAction m_PlayerActionMap_CraftingSelectUp;
    private readonly InputAction m_PlayerActionMap_CraftingSelectDown;
    private readonly InputAction m_PlayerActionMap_CraftingButton;
    private readonly InputAction m_PlayerActionMap_Attack;
    private readonly InputAction m_PlayerActionMap_Look;
    private readonly InputAction m_PlayerActionMap_ScrollWheel;
    private readonly InputAction m_PlayerActionMap_Pause;
    public struct PlayerActionMapActions
    {
        private @Controls m_Wrapper;
        public PlayerActionMapActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Locomotion => m_Wrapper.m_PlayerActionMap_Locomotion;
        public InputAction @Slide => m_Wrapper.m_PlayerActionMap_Slide;
        public InputAction @CraftingSelectUp => m_Wrapper.m_PlayerActionMap_CraftingSelectUp;
        public InputAction @CraftingSelectDown => m_Wrapper.m_PlayerActionMap_CraftingSelectDown;
        public InputAction @CraftingButton => m_Wrapper.m_PlayerActionMap_CraftingButton;
        public InputAction @Attack => m_Wrapper.m_PlayerActionMap_Attack;
        public InputAction @Look => m_Wrapper.m_PlayerActionMap_Look;
        public InputAction @ScrollWheel => m_Wrapper.m_PlayerActionMap_ScrollWheel;
        public InputAction @Pause => m_Wrapper.m_PlayerActionMap_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionMapActions instance)
        {
            if (m_Wrapper.m_PlayerActionMapActionsCallbackInterface != null)
            {
                @Locomotion.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLocomotion;
                @Locomotion.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLocomotion;
                @Locomotion.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLocomotion;
                @Slide.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnSlide;
                @CraftingSelectUp.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectUp;
                @CraftingSelectUp.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectUp;
                @CraftingSelectUp.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectUp;
                @CraftingSelectDown.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectDown;
                @CraftingSelectDown.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectDown;
                @CraftingSelectDown.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingSelectDown;
                @CraftingButton.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingButton;
                @CraftingButton.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingButton;
                @CraftingButton.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnCraftingButton;
                @Attack.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnAttack;
                @Look.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnLook;
                @ScrollWheel.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnScrollWheel;
                @Pause.started -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionMapActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Locomotion.started += instance.OnLocomotion;
                @Locomotion.performed += instance.OnLocomotion;
                @Locomotion.canceled += instance.OnLocomotion;
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
                @CraftingSelectUp.started += instance.OnCraftingSelectUp;
                @CraftingSelectUp.performed += instance.OnCraftingSelectUp;
                @CraftingSelectUp.canceled += instance.OnCraftingSelectUp;
                @CraftingSelectDown.started += instance.OnCraftingSelectDown;
                @CraftingSelectDown.performed += instance.OnCraftingSelectDown;
                @CraftingSelectDown.canceled += instance.OnCraftingSelectDown;
                @CraftingButton.started += instance.OnCraftingButton;
                @CraftingButton.performed += instance.OnCraftingButton;
                @CraftingButton.canceled += instance.OnCraftingButton;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActionMapActions @PlayerActionMap => new PlayerActionMapActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    public interface IPlayerActionMapActions
    {
        void OnLocomotion(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnCraftingSelectUp(InputAction.CallbackContext context);
        void OnCraftingSelectDown(InputAction.CallbackContext context);
        void OnCraftingButton(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
