using DG.Tweening;
using UnityEngine;

public class CameraRotateManager : MonoBehaviour
{
    [SerializeField] private float duration = 15;
    [SerializeField] private Vector3 endValue = new Vector3(0,360,0);
    void Awake()
    {
        transform.DORotate(endValue, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
        
}
