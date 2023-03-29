using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] GameObject AttackButton;
    [SerializeField] GameObject ShieldButton;
    [SerializeField] GameObject GadgetButton;

    [SerializeField] Sprite[] sprites;

    PlayerControls PlayerInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        PlayerInput = new PlayerControls();
        PlayerInput.Enable();
        PlayerInput.Player.MeleeAttack.started += PressedAttack;
        PlayerInput.Player.MeleeAttack.canceled += UnpressedAttack;
        PlayerInput.Player.Shield.started += PressedShield;
        PlayerInput.Player.Shield.canceled += UnpressedShield;
        PlayerInput.Player.Gadget.started += PressedGadget;
        PlayerInput.Player.Gadget.canceled += UnpressedGadget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PressedAttack(InputAction.CallbackContext context)
    {
        AttackButton.GetComponent<SpriteRenderer>().sprite = sprites[4];
    }

    void UnpressedAttack(InputAction.CallbackContext context)
    {
        AttackButton.GetComponent<SpriteRenderer>().sprite = sprites[5];
    }

    void PressedShield(InputAction.CallbackContext context)
    {
        ShieldButton.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    void UnpressedShield(InputAction.CallbackContext context)
    {
        ShieldButton.GetComponent<SpriteRenderer>().sprite = sprites[1];
    }

    void PressedGadget(InputAction.CallbackContext context)
    {
        GadgetButton.GetComponent<SpriteRenderer>().sprite = sprites[2];
    }

    void UnpressedGadget(InputAction.CallbackContext context)
    {
        GadgetButton.GetComponent<SpriteRenderer>().sprite = sprites[3];
    }
}
