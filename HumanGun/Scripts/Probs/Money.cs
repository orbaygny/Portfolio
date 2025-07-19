using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(CanvasController.Instance.moneyText.rectTransform.position, 0.5f).SetEase(Ease.OutSine));
        sequence.Join(transform.DOScale(1, 0.25f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo));

        sequence.OnComplete(() => DestroyThis());

    }
    public void DestroyThis()
    {
        CanvasController.Instance.AddMoney();
       Destroy(gameObject);
    }
}
