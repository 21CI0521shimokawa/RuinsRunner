using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager
{
    public static Vector2 GetGamepadStickL()
    {
        //“|‚µ‚½•ûŒü‚ğæ“¾
        Gamepad gamepad = Gamepad.current;

        Vector2 stickValue = Vector2.zero;

        if (gamepad != null)
        {
            stickValue = gamepad.leftStick.ReadValue();
        }


        //ƒL[“ü—Í‚É‰‚¶‚Ä‘‚«Š·‚¦
        Keyboard keyboard = Keyboard.current;

        if (keyboard.rightArrowKey.isPressed) { stickValue.x += 1.0f; }
        if (keyboard.leftArrowKey.isPressed) { stickValue.x -= 1.0f; }
        if (keyboard.upArrowKey.isPressed) { stickValue.y += 1.0f; }
        if (keyboard.downArrowKey.isPressed) { stickValue.y -= 1.0f; }

        return stickValue;
    }
}
