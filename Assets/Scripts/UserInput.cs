using UnityEngine;

public class UserInput : MonoBehaviour
{
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool Jump { get; private set; }
    public bool Up { get; private set; }
    public bool Down { get; private set; }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        Left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        Right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        Jump = Input.GetKeyDown(KeyCode.Space);

        Up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        Down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    public void ResetJump()
    {
        Jump = false;
    }
}
