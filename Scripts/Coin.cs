using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1).SetLink(gameObject);
    }
}
