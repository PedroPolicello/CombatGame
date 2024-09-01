using DG.Tweening;
using UnityEngine;

public class CameraRotateManager : MonoBehaviour
{
    void Awake()
    {
        transform.DORotate(new Vector3(0, 360), 15, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}
