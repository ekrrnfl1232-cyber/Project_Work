using UnityEngine;

public enum ClipType
{
    Click,
    Attack,
    Hit
}
public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public class Clip
    {
        public ClipType type = ClipType.Click;
        public AudioClip clip;
    }
    [SerializeField] private Clip[] audioClips;
    [SerializeField] private AudioSource[] effectSources;

    private int effectPlayIndex = 0;
    public void EffectSound(ClipType clipType)
    {
        foreach (Clip clip in audioClips)
        {
            if(clip.type == clipType)
            {
                if(effectSources.Length <= effectPlayIndex)
                    effectPlayIndex = 0;

                effectSources[effectPlayIndex].clip = clip.clip;
                effectPlayIndex++;
                effectSources[effectPlayIndex].Play();


                break;
            }
        }
    }
}
