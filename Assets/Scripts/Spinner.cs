using System.Linq;
using System.Threading;

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    private Image canvas;
    private Image[] images;
    private int stillSpinning;

    private void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        this.canvas = canvas.GetComponent<Image>();
        images = canvas.transform
            .Cast<Transform>()
            .Select(transform => transform.GetComponent<Image>())
            .ToArray();
        stillSpinning = 0;
    }

    private void OnGUI()
    {
        Event @event = Event.current;
        if (@event.type == EventType.KeyDown
            && @event.keyCode == KeyCode.Return
            && Interlocked.CompareExchange(ref stillSpinning, 0, 0) == 0)
        {
            stillSpinning = images.Length;
            SpinAll();
        }
    }

    private void SpinAll()
    {
        for (var i = 0; i < images.Length; i++)
        {
            Spin(images[i]);
        }
    }

    private void Spin(Image image)
    {
        const float Distance = 9F;
        const float Duration = 1F;
        image.rectTransform
            .DOLocalMoveY(-image.rectTransform.rect.size.y * Distance, Duration)
            .SetRelative()
            .OnUpdate(() =>
            {
                if (image.rectTransform.localPosition.y < canvas.rectTransform.rect.yMin)
                {
                    Vector3 localPosition = image.rectTransform.localPosition;
                    localPosition.y += canvas.rectTransform.rect.size.y;
                    image.rectTransform.localPosition = localPosition;
                }
            })
            .OnComplete(() =>
            {
                Interlocked.Decrement(ref stillSpinning);
            });
    }
}
