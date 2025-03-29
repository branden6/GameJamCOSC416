using UnityEngine;

public class UserInput : MonoBehaviour
{
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool Jump { get; private set; }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        Left = Input.GetKey(KeyCode.A);
        Right = Input.GetKey(KeyCode.D);
        Jump = Input.GetKeyDown(KeyCode.Space);
    }

    public void ResetJump()
    {
        Jump = false;
    }
}
