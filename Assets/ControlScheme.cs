// GENERATED AUTOMATICALLY FROM 'Assets/ControlScheme.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControlScheme : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlScheme()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlScheme"",
    ""maps"": [
        {
            ""name"": ""Yeet"",
            ""id"": ""d8bdb67c-1448-4fab-b2ff-b8c37c2c9214"",
            ""actions"": [
                {
                    ""name"": ""MovementX"",
                    ""type"": ""Value"",
                    ""id"": ""560b4258-5895-4ac0-8a06-e52037024836"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementY"",
                    ""type"": ""Value"",
                    ""id"": ""f8d5179f-8b27-4168-9566-a92aa8a7b3f4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""d47e6475-4fc6-42ed-8ae2-a75f1ae599c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectNextCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""0d0a80d1-8643-44cc-9d55-3b76f7568a89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""650a67a1-8cda-4fc4-91f6-e3da08fb7e42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""9f34bf24-b6ac-4bb6-a4bc-bd3be31a93f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Joystick"",
                    ""id"": ""456c5031-0fdd-4af6-b644-5ceecdb2635c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6d4ee702-057a-4130-845b-1befaf57fdf9"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""da238aa5-f124-4b5a-9526-6f6a8b16f6bd"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""238fea36-151e-4c9c-b8f0-13d3496103f6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""391cf686-7ec3-4428-9a7f-1758f72b0803"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""653ff901-0c96-4175-b9a3-e710b5d8c1b4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""6ff7b06c-3b4a-4195-be4f-d68de334effb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2a792340-a424-4db7-9c32-02120ab901bd"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""539ecdc1-bf4b-47e9-bf49-5bcdd7c78159"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""7e90cdb3-f014-4e3c-8808-6c80ff2553e0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""affe95e8-421b-420f-8156-5a52555b9535"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""61ff5935-e443-4041-bd20-31e29336de8a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6d25ff1f-8338-4143-b121-d2a41caa79f0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1929b99c-ead1-4d0e-b08a-9af9eab45102"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c4f7710-99fc-4189-8615-1e84423d3470"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectNextCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dae2facb-4286-495e-ab54-40572f7a6de9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectNextCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c772ea4-6fd9-48a1-a336-dbc5d0d0756e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62684b3d-7e94-4ea6-b4b8-f8056aad5ffa"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""680b83d3-1c44-4a11-935f-fd19b138cb0d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5d940ad-a09c-4451-a6da-69b8489b10ab"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
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
                }
            ]
        }
    ]
}");
        // Yeet
        m_Yeet = asset.FindActionMap("Yeet", throwIfNotFound: true);
        m_Yeet_MovementX = m_Yeet.FindAction("MovementX", throwIfNotFound: true);
        m_Yeet_MovementY = m_Yeet.FindAction("MovementY", throwIfNotFound: true);
        m_Yeet_Action = m_Yeet.FindAction("Action", throwIfNotFound: true);
        m_Yeet_SelectNextCharacter = m_Yeet.FindAction("SelectNextCharacter", throwIfNotFound: true);
        m_Yeet_ZoomOut = m_Yeet.FindAction("ZoomOut", throwIfNotFound: true);
        m_Yeet_PauseGame = m_Yeet.FindAction("PauseGame", throwIfNotFound: true);
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

    // Yeet
    private readonly InputActionMap m_Yeet;
    private IYeetActions m_YeetActionsCallbackInterface;
    private readonly InputAction m_Yeet_MovementX;
    private readonly InputAction m_Yeet_MovementY;
    private readonly InputAction m_Yeet_Action;
    private readonly InputAction m_Yeet_SelectNextCharacter;
    private readonly InputAction m_Yeet_ZoomOut;
    private readonly InputAction m_Yeet_PauseGame;
    public struct YeetActions
    {
        private @ControlScheme m_Wrapper;
        public YeetActions(@ControlScheme wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementX => m_Wrapper.m_Yeet_MovementX;
        public InputAction @MovementY => m_Wrapper.m_Yeet_MovementY;
        public InputAction @Action => m_Wrapper.m_Yeet_Action;
        public InputAction @SelectNextCharacter => m_Wrapper.m_Yeet_SelectNextCharacter;
        public InputAction @ZoomOut => m_Wrapper.m_Yeet_ZoomOut;
        public InputAction @PauseGame => m_Wrapper.m_Yeet_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_Yeet; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(YeetActions set) { return set.Get(); }
        public void SetCallbacks(IYeetActions instance)
        {
            if (m_Wrapper.m_YeetActionsCallbackInterface != null)
            {
                @MovementX.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementX;
                @MovementX.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementX;
                @MovementX.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementX;
                @MovementY.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementY;
                @MovementY.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementY;
                @MovementY.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnMovementY;
                @Action.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnAction;
                @SelectNextCharacter.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnSelectNextCharacter;
                @SelectNextCharacter.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnSelectNextCharacter;
                @SelectNextCharacter.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnSelectNextCharacter;
                @ZoomOut.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnZoomOut;
                @ZoomOut.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnZoomOut;
                @ZoomOut.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnZoomOut;
                @PauseGame.started -= m_Wrapper.m_YeetActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_YeetActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_YeetActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_YeetActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementX.started += instance.OnMovementX;
                @MovementX.performed += instance.OnMovementX;
                @MovementX.canceled += instance.OnMovementX;
                @MovementY.started += instance.OnMovementY;
                @MovementY.performed += instance.OnMovementY;
                @MovementY.canceled += instance.OnMovementY;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @SelectNextCharacter.started += instance.OnSelectNextCharacter;
                @SelectNextCharacter.performed += instance.OnSelectNextCharacter;
                @SelectNextCharacter.canceled += instance.OnSelectNextCharacter;
                @ZoomOut.started += instance.OnZoomOut;
                @ZoomOut.performed += instance.OnZoomOut;
                @ZoomOut.canceled += instance.OnZoomOut;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public YeetActions @Yeet => new YeetActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    public interface IYeetActions
    {
        void OnMovementX(InputAction.CallbackContext context);
        void OnMovementY(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnSelectNextCharacter(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
