using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public static CursorControl Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        SetCursorLocked();
    }

    public void SetCursorLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetCursorNotLocked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
