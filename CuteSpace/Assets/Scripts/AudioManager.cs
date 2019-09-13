using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Scripts.Audio
{
	public class AudioManager : MonoBehaviour
	{
        [SerializeField]
        private List<string> mKeys;
        [SerializeField]
        private List<AudioClip> mAudioClips;
        [SerializeField]
        private object[] mAudioClipArray;
        private char[] mAudioKeyFilter = new char[1];

        public Dictionary<string, AudioClip> mAudioDictionary { get; private set; }

        private AudioSource mMusicSource;
        private AudioSource mMusicSource2;
        private AudioSource m2DSoundSource;
        private AudioSource mAmbienceSource;
        private AudioSource mAmbienceSource2;

        private bool mIsCrossfading;

        private static AudioManager sAudioManager;

        public static AudioManager Instance
        {
            get
            {
                if (!sAudioManager)
                {
                    sAudioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                    if (!sAudioManager)
                    {
                        Debug.LogError("There needs to be one active AudioManager script on a GameObject in your scene.");
                    }
                }
                else
                {
                    //sAudioManager.Init();
                }

                return sAudioManager;
            }
        }

        /// <summary>
        /// Load all .wav files form the Resources.Audio folder (as objects) into an Obj Array. Then cast those objects as audioclips
        /// and load those into mAudioClips (a list of audioclips). Then get the name of those audioclips, cut off the naming convention and load those
        /// into a list of strings. Then create a dictionary (string, audioclip) with the list of strings as the keys and the list of audioclips as the entries.
        /// </summary>
        private void Init()
        {
            //mMusicSource = GetComponent<AudioSource>();
            mAudioKeyFilter[0] = '_';
            mAudioClipArray = Resources.LoadAll("Audio");
            //mIsInside = true;
            foreach (Object obj in mAudioClipArray)
            {
                mAudioClips.Add(obj as AudioClip);
            }
            foreach (AudioClip clip in mAudioClips)
            {
                string clipName = clip.name;
                string[] tempArray = clipName.Split(mAudioKeyFilter);
                mKeys.Add(tempArray[0]);
            }

            if (mAudioDictionary == null)
            {
                mAudioDictionary = new Dictionary<string, AudioClip>();
                for (int i = 0; i < mAudioClips.Count; ++i)
                {
                    mAudioDictionary.Add(mKeys[i], mAudioClips[i]);
                }
            }
        }

        private void Update()
        {

        }

        private void Awake()
		{
            Init();
            mMusicSource = gameObject.AddComponent<AudioSource>();
            mMusicSource2 = gameObject.AddComponent<AudioSource>();

            mMusicSource.loop = true;

        }

        private void Start()
        {
            InitializeAmbience();
        }

        public void StartNormalMusic()
        {

        }

        private void InitializeAmbience()
        {
            if (mAmbienceSource == null)
            {
                mAmbienceSource = gameObject.AddComponent<AudioSource>();
            }
            if (mAmbienceSource2 == null)
            {
                mAmbienceSource2 = gameObject.AddComponent<AudioSource>();
            }

            mAmbienceSource.loop = true;
            if (!mAmbienceSource.isPlaying)
            {
                mAmbienceSource.Play();
            }

        }

        public void PlayUISound(string key)
        {
            AudioSource tempAudio = gameObject.AddComponent<AudioSource>();
            tempAudio.clip = mAudioDictionary[key];
            tempAudio.Play();
            Destroy(tempAudio, tempAudio.clip.length);
        }

		public void Play2DSound(string key, bool doLoop = false, bool isMusic = false)
		{
            if (isMusic)
            {
                mMusicSource.clip = mAudioDictionary[key];
                mMusicSource.volume = 1;
                mMusicSource.Play();
            }
            else
            {
                AudioSource tempAudio = gameObject.AddComponent<AudioSource>();
                tempAudio.clip = mAudioDictionary[key];
                tempAudio.Play();
                if (doLoop)
                {
                    tempAudio.loop = true;
                }
                else
                {
                    Destroy(tempAudio, tempAudio.clip.length);
                }
            }
        }     
        /// <summary>
        /// Calls the Coroutine CrossfadeMusic.
        /// </summary>
        /// <param name="newClipKey"></param>
        /// the string name of the .wav file for the new clip to play
        /// <param name="transitionTime"></param>
        /// the time it takes to full transition (at the end of transitionTime, only the new clip will play )

        public void CrossfadeAmbience(string newClipKey, float transitionTime)
        {
           StartCoroutine(CrossfadeAmbience(ReturnAudioClip(newClipKey), transitionTime));
        }

        /// <summary>
        /// Creates a temporary gameobject with an audiosource. Sets the Clip of the temporary source to the new clip to be played. Over an amount of time (transitionTime), the volume 
        /// of MusicSource (this object's AudioSource) fades. At the same time, the volume of the temporary source increases. When the old clip volume is 0, the MusicSource becomes the new gameobject's Audiosource
        /// </summary>
        /// <param name="newClip"></param>
        /// <param name="transitionTime"></param>
        /// <returns></returns>

        IEnumerator CrossfadeAmbience(AudioClip newClip, float transitionTime)
        {
            mAmbienceSource2.clip = newClip;
            mAmbienceSource2.volume = 0;
            mAmbienceSource2.Play();

            while (mAmbienceSource.volume > 0)
            {
                mAmbienceSource.volume -= Time.deltaTime / transitionTime;
                mAmbienceSource2.volume += Time.deltaTime / transitionTime;
                yield return null;
            }
            mAmbienceSource2.volume = 1f;
            mAmbienceSource2.loop = true;
            AudioSource holderSource = mAmbienceSource;
            mAmbienceSource = mAmbienceSource2;
            mAmbienceSource2 = holderSource;
        }

        public void CrossfadeMusic(string newClipKey, float transitionTime)
        {
            if (!mIsCrossfading)
            {
                StartCoroutine(CrossfadeMusic(ReturnAudioClip(newClipKey), transitionTime));
                mIsCrossfading = true;
            }
        }

        /// <summary>
        /// Creates a temporary gameobject with an audiosource. Sets the Clip of the temporary source to the new clip to be played. Over an amount of time (transitionTime), the volume 
        /// of MusicSource (this object's AudioSource) fades. At the same time, the volume of the temporary source increases. When the old clip volume is 0, the MusicSource becomes the new gameobject's Audiosource
        /// </summary>
        /// <param name="newClip"></param>
        /// <param name="transitionTime"></param>
        /// <returns></returns>

        IEnumerator CrossfadeMusic(AudioClip newClip, float transitionTime)
        {
            mMusicSource2.clip = newClip;
            mMusicSource2.volume = 0;
            mMusicSource2.Play();

            while (mMusicSource.volume > 0)
            {
                mMusicSource.volume -= Time.deltaTime / transitionTime;
                mMusicSource2.volume += Time.deltaTime / transitionTime;
                yield return null;
            }
            mMusicSource2.volume = 1f;
            mMusicSource2.loop = true;
            AudioSource holderSource = mMusicSource;
            mMusicSource = mMusicSource2;
            mMusicSource2 = holderSource;
            mIsCrossfading = false;
        }

        /*
        public void FadeAmbience(float transitionTime)
        {
            if (mAmbienceSource.isPlaying == false)
            {
                mAmbienceSource.Play();
            }
            StartCoroutine(AmbienceFade(transitionTime));
        }

        IEnumerator AmbienceFade(float transitionTime)
        {
            if (mIsInside)
            {
                while (mAmbienceSource.volume < 1)
                {
                    mAmbienceSource.volume += Time.deltaTime / transitionTime;
                    yield return null;
                }
                mAmbienceSource.volume = 1;
                //mIsInside = false;
            }
            else
            {
                while (mAmbienceSource.volume > 0)
                {
                    mAmbienceSource.volume -= Time.deltaTime / transitionTime;
                    yield return null;
                }
                mAmbienceSource.volume = 0;
                //mIsInside = true;
            }
        }

        public void FadeMusicIn (float transitionTime)
        {
            StartCoroutine(FadeInMusic(transitionTime));
        }

        IEnumerator FadeInMusic(float transitionTime)
        {
            while (mMusicSource.volume < 1)
            {
                mMusicSource.volume += Time.deltaTime / transitionTime;
                yield return null;
            }
            mMusicSource.volume = 1;
        }

    */

        public void FadeMusicOut(float transitionTime)
        {
            StartCoroutine(FadeOutMusic(transitionTime));
        }

        IEnumerator FadeOutMusic(float transitionTime)
        {
            while (mMusicSource.volume > 0)
            {
                mMusicSource.volume -= Time.deltaTime / transitionTime;
                yield return null;
            }
            
            mMusicSource.volume = 0;
        }

        public void PlaySoundOnDelay(string key, float delayAmount)
        {
            StartCoroutine(PlaySoundDelayed(key, delayAmount));
        }

        IEnumerator PlaySoundDelayed(string key, float delayAmount)
        {
            yield return new WaitForSeconds(delayAmount);
            Play2DSound(key);
        }

        public AudioClip ReturnAudioClip(string key)
        {
            return mAudioDictionary[key];
        }
	}
}
