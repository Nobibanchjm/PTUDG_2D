using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenepersist : MonoBehaviour
{
    void Awake()
    {
        // 1. Ki?m tra xem có bao nhiêu phiên b?n GameSession ?ang t?n t?i trong c?nh (scene).
        int numScenePersist = FindObjectsOfType<Scenepersist>().Length;
        // 2. N?u ?ã có h?n 1 GameSession (khi quay l?i m?t scene khác ho?c reload scene), thì phá h?y ??i t??ng hi?n t?i.
        if (numScenePersist > 1)
        {
            Destroy(gameObject);// H?y ??i t??ng hi?n t?i vì ?ã có GameSession khác t?n t?i.
        }
        else
        {
            // 3. N?u không có GameSession khác, ??i t??ng này s? ???c gi? l?i khi t?i màn ch?i m?i.
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
