using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SoundObject{
   public string name;
   public SoundId soundId;
   public GameObject gameObject;

}


public enum SoundId{
   HappyMusic,
   InGameMusic,
   MainMusic,
   EnemyHitSound

}



public class SoundManager : MonoBehaviour
{
    public List<SoundObject> sounds;
    public GameObject currentMusic;
    Vector2 targetPitch;
    Vector2 pitch;
    public float damp;
    Vector2 velocity;
    public bool changePitch;
    public void IncreasePitch() {
        changePitch = true;
        targetPitch.x = 1;
    }
    public void ReducePitch() {
        changePitch = true;
        targetPitch.x = 0.6f;
    }
    private void Update()
    {
        if (changePitch) {
            pitch = Vector2.SmoothDamp(pitch, targetPitch, ref velocity, damp);
           
            if (currentMusic != null) {
                currentMusic.GetComponent<AudioSource>().pitch = pitch.x;
            }
            if (pitch.x == targetPitch.x)
            {
                changePitch = false;
            }
        }
    }


    public void PlaySound(SoundId id){
        List < SoundObject > ss = sounds.FindAll(x => x.soundId == id && !x.gameObject.activeSelf);
        if (ss != null && ss.Count > 0)
        {
            SoundObject s = ss[UnityEngine.Random.Range(0, ss.Count - 1)];

            if (s != null)
            {
                s.gameObject.SetActive(true);

            }
        }
   
   
   }
   public void PlayMusic(SoundId id){
       SoundObject s=sounds.Find(x=>x.soundId==id);
       if(currentMusic!=null){
       currentMusic.SetActive(false);
       
       }

        if(s!=null){
         s.gameObject.SetActive(true);
            currentMusic = s.gameObject;
       }
   
   
   }
    public void StopMusic(SoundId id)
    {
        SoundObject s = sounds.Find(x => x.soundId == id );
        if (currentMusic != null)
        {
            currentMusic.SetActive(false);

        }

        if (s != null)
        {
            s.gameObject.SetActive(false);
            currentMusic = s.gameObject;
        }


    }


}
