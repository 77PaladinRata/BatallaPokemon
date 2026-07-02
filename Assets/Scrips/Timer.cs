using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TimerData[] timerData;
    [SerializeField]
    private Image timerImage;
    private Animator animator; ///* intentar ponerlo en gif
    [SerializeField]
    private UnityEvent onTimerEnd;
    private void Awake()///* agregando nuevo
    { 
        animator = timerImage.GetComponent<Animator>();
    }
    public void StartTimer(int duration)
    {
        StartCoroutine(TimerCoroutine(duration));
    }
    private IEnumerator TimerCoroutine(int duration)
    {
        while (duration > 0)
        {
            SoundManager.instance.Play(timerData[duration - 1].soundName);
            timerImage.sprite = timerData[duration - 1].texture;
            timerImage.SetNativeSize();
            animator.Play("Show", 0, 0f); ///* nuevo para el conteo
            yield return new WaitForSeconds(1f);
            duration --;
        }
        onTimerEnd.Invoke();
    }
    public void StopTimer()
    {
        animator.Play("Hidden", 0, 0f); ///* nuevo para el conteo
        StopAllCoroutines();
    }
    [System.Serializable]
    public class TimerData
    {
        public string soundName;
        public Sprite texture;
    }
}
