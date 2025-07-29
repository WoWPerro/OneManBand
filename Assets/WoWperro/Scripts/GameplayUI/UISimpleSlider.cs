using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISimpleSlider: MonoBehaviour,IPointerUpHandler 
{
    public ButtonActions BA;
    public AudioManager AM;
    private Slider slider;
    public SoundType targetSoundType;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(targetSoundType == SoundType.Song)
        {
            AM.ChangeVolumeSongs(slider.value);
        }
        else
        {
            AM.ChangeVolumeAFX(slider.value);
        }
        BA.ReproducePlaySound();
    }
}