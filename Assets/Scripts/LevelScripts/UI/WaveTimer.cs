using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    public System.Action OnTimeUp;
    public System.Action<float> OnTimeDecreased;

    private Coroutine _routine;

    public void Enable(float seconds)
    {
        if(_routine != null)
            StopCoroutine(_routine);
        
        _slider.maxValue = seconds;
        _slider.value = seconds;

        _text.text = SecondsIntoMinutes((int)seconds);
        
        _slider.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
        
        _routine = StartCoroutine(StartTimer(seconds));
    }

    public void Disable()
    {
        _slider.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);

        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
    }

    private string SecondsIntoMinutes(int totalSeconds)
    {
        var seconds = totalSeconds % 60;
        
        if (seconds == 0)
            return $"{totalSeconds / 60}:00";

        if (totalSeconds > 60)
            return $"{(totalSeconds - seconds) / 60}:{seconds}";
        else
            return $"0:{(seconds > 9 ? seconds : "0" + seconds)}";
    }

    private IEnumerator StartTimer(float timeLeft)
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.01f);
            
            OnTimeDecreased?.Invoke(0.01f);
            timeLeft -= 0.01f;
            _slider.value = timeLeft;
            _text.text = SecondsIntoMinutes((int)timeLeft);
        }

        _routine = null;
        OnTimeUp?.Invoke();
    }
}