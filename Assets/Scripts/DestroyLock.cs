using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLock : MonoBehaviour
{
    private void OnEnable()
    {
        LockController.OnLockOpened += destroySelf;
    }

    private void OnDestroy()
    {
        LockController.OnLockOpened -= destroySelf;
    }

    void destroySelf()
    {
        Destroy(gameObject);
    }
}
