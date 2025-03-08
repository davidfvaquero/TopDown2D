using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    void Awake()
    {
        InitializeDict();
    }

    private void InitializeDict()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (var group in soundEffectGroups)
        {
            soundDictionary[group.name] = group.clips;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> clips = soundDictionary[name];
            if (clips.Count > 0)
            {
                return clips[Random.Range(0, clips.Count)];
            }
        }
        return null;
    }
}

[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> clips;

}