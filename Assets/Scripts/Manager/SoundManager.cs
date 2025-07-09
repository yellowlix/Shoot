using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find("SoundManager");
                instance = root.GetComponent<SoundManager>();
                if (instance == null)
                {
                    instance = root.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField]private AudioSource audioBGMSource;
    [SerializeField] private AudioSource audioSESource;

    //字典：string 文件路径，AudioClip 保存音频文件
    private Dictionary<string, AudioClip> dictAudio;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        audioBGMSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        audioSESource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    void Update()
    {

    }
    public float GetBackgroundMusicVolume()
    {
        return audioBGMSource.volume;
    }

    // 设置音效音量
    public float GetSoundEffectVolume()
    {
        return audioSESource.volume;
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        audioBGMSource.volume = Mathf.Clamp01(volume);
    }

    // 设置音效音量
    public void SetSoundEffectVolume(float volume)
    {
        audioSESource.volume = Mathf.Clamp01(volume);
    }

//辅助函数：加载音频，需要确保音频文件在Resources文件夹下
public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }

    private AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path))
        {
            dictAudio[path] = LoadAudio(path);
        }
        return dictAudio[path];
    }
    /// <summary>
    /// 播放bgm
    /// </summary>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlayBGM(string path, float volume = 1.0f)
    {
        audioBGMSource.Stop();
        audioBGMSource.clip = GetAudio(path);
        audioBGMSource.Play();
    }
    public void StopBGM()
    {
        audioBGMSource.Stop();
    }
    /// <summary>
    /// 播放瞬间音效
    /// </summary>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlaySound(string path, float volumeRate = 1.0f)
    {
        this.audioSESource.PlayOneShot(LoadAudio(path));
        this.audioSESource.volume = audioSESource.volume * volumeRate;
    }

    /// <summary>
    /// 使用挂载在物体身上的AudioSource来播放某些3D音效，来达到音效远近效果。
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlaySound(AudioSource audioSource, string path, float volume = 1.0f)
    {
        audioSource.PlayOneShot(LoadAudio(path));
        audioSource.volume = volume;

    }
}
public class SoundConst
{
    public const string PistolSound = "Audio/ShootingSound/machine_gun";
    public const string LasergunSound = "Audio/ShootingSound/laser_01";
    public const string ShotGunSound = "Audio/ShootingSound/cannon_01";
    public const string Gem_Grab = "Audio/InventorySounds/OGG/Gem_Grab_01";
    public const string Goins_Grab = "Audio/InventorySounds/OGG/Coins_Grab_01";
    public const string Pick_Weapon = "Audio/PickWeapon";
    public const string Click = "Audio/InterfaceSound/V1.0/Interface/Clicks/Click(9)";
}