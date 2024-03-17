using TMPro;

namespace Script
{
    using UnityEngine;

    /// <summary>
    /// 喜欢我写的屎山吗
    /// </summary>
    
    public class KeyChange : MonoBehaviour
    {
        private KeySettingManager _keySettingManager;
        public bool isLeftChanging;
        [SerializeField] private TMP_Text _left;
        public bool isRightChanging;
        [SerializeField] private TMP_Text _right;
        public bool isDashChanging;
        [SerializeField] private TMP_Text _dash;
        public bool isJumpChanging;
        [SerializeField] private TMP_Text _jump;
        public bool isAttackChanging;
        [SerializeField] private TMP_Text _attack;
        public bool isUpChanging;
        [SerializeField] private TMP_Text _up;
        public bool isDownChanging;
        [SerializeField] private TMP_Text _down;
        
        private void Start()
        {
            _keySettingManager = GameObject.FindWithTag("KeySettingManager").GetComponent<KeySettingManager>();
            _attack.text = _keySettingManager.GetKey("Attack").ToString();
            _left.text = _keySettingManager.GetKey("Left").ToString();
            _right.text = _keySettingManager.GetKey("Right").ToString();
            _dash.text = _keySettingManager.GetKey("Dash").ToString();
            _jump.text = _keySettingManager.GetKey("Jump").ToString();
            _up.text = _keySettingManager.GetKey("Up").ToString();
            _down.text = _keySettingManager.GetKey("Down").ToString();
        }

        public void EnableAttackChange()
        {
            isAttackChanging = true;
        }
        public void Attack()
        {
            if (isAttackChanging)
            {
                if (Input.anyKeyDown)
                {
                    isAttackChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _attack.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Attack", pressedKeyCode);
                }
            }
        }

        public void EnableLeftChange()
        {
            isLeftChanging = true;
        }
        public void Left()
        {
            if (isLeftChanging)
            {
                if (Input.anyKeyDown)
                {
                    isLeftChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _left.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Left", pressedKeyCode);
                }
            }
        }
        
        public void EnableRightChange()
        {
            isRightChanging = true;
        }
        public void Right()
        {
            if (isRightChanging)
            {
                if (Input.anyKeyDown)
                {
                    isRightChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _right.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Right", pressedKeyCode);
                }
            }
        }
        
        public void EnablDashChange()
        {
            isDashChanging = true;
        }
        public void Dash()
        {
            if (isDashChanging)
            {
                if (Input.anyKeyDown)
                {
                    isDashChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _dash.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Dash", pressedKeyCode);
                }
            }
        }
        
        public void EnablJumpChange()
        {
            isJumpChanging = true;
        }
        public void Jump()
        {
            if (isJumpChanging)
            {
                if (Input.anyKeyDown)
                {
                    isJumpChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _jump.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Jump", pressedKeyCode);
                }
            }
        }
        
        public void EnablUpChange()
        {
            isUpChanging = true;
        }
        public void Up()
        {
            if (isUpChanging)
            {
                if (Input.anyKeyDown)
                {
                    isUpChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _up.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Up", pressedKeyCode);
                }
            }
        }
        
        public void EnablDownChange()
        {
            isDownChanging = true;
        }
        public void Down()
        {
            if (isDownChanging)
            {
                if (Input.anyKeyDown)
                {
                    isDownChanging = false;
                    KeyCode pressedKeyCode=KeyCode.None;
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (!Input.GetKeyDown(keyCode)) continue;
                        pressedKeyCode = keyCode;
                        break;
                    }

                    _down.text = pressedKeyCode.ToString();
                    _keySettingManager.SetKey("Down", pressedKeyCode);
                }
            }
        }
        void Update()
        {
            Attack();
            Left();
            Right();
            Dash();
            Jump();
            Up();
            Down();
        }
    }

}