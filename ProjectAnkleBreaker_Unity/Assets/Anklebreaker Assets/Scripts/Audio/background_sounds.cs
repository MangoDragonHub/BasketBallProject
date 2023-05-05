using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_sounds : MonoBehaviour
{

    public AudioClip[] audio_background_clips;

    public AudioSource background_sound_source;

    private int clip_num = 0;

    private int recen_clip_num = 0;

    private List<int> aduio_nums;

    // Start is called before the first frame update
    void Start()
    {

        Create_list();
        background_sound_source.clip = audio_background_clips[clip_num];
        Invoke("Next_music", (background_sound_source.clip.length));
        play_sound();

    }

    private void Create_list()
    {
        for(int q = 0; q < audio_background_clips.Length; q++)
        {
            aduio_nums.Add(q);
        }
    }

    private void Aduio_list_file(int gone)
    {
        for(int q = 0; q < aduio_nums.Count; q++)
        {
            if (aduio_nums[q] == gone)
            {
                aduio_nums.Remove(q);

                q = q + aduio_nums.Count;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Next_music()
    {

        if(aduio_nums.Count <= 0)
        {
            Create_list();
        }

        clip_num = aduio_nums[Random.Range(0, aduio_nums.Count)];


        Aduio_list_file(clip_num);


        background_sound_source.clip = audio_background_clips[clip_num];
        Invoke("Next_music", (background_sound_source.clip.length));
        play_sound();
        /*
        if(clip_num == recen_clip_num)
        {
            if(clip_num >= (audio_background_clips.Length - 1))
            {
                clip_num = 0;
            }
            else
            {
                clip_num = clip_num + 1;
            }
            background_sound_source.clip = audio_background_clips[clip_num];
            Invoke("Next_music", (background_sound_source.clip.length));
            play_sound();
            recen_clip_num = clip_num;
        }
        else
        {
            background_sound_source.clip = audio_background_clips[clip_num];
            Invoke("Next_music", (background_sound_source.clip.length));
            play_sound();
            recen_clip_num = clip_num;
        }
        */
    }

    private void play_sound()
    {
        background_sound_source.Play();
    }
}
