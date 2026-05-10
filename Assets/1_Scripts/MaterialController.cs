using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public static MaterialController Instance { get; private set; }

    [SerializeField] private Material _material;

    private const string HSV_SHIFT = "_HsvShift"; // 0 - 360
    private const string HSV_SATURATION = "_HsvSaturation"; // 0 - 2
    private const string HSV_BRIGHT = "_HsvBright"; // 0 - 2

    private const string PIXELATE_SIZE = "_PixelateSize"; // 0 - 512

    private const string WAVE_AMOUNT = "_WaveAmount"; // 0 - 25
    private const string WAVE_SPEED = "_WaveSpeed"; // 0 - 25
    private const string WAVE_STRENGTH = "_WaveStrength"; // 0 - 25

    private const string ROUND_WAVE_STRENGTH = "_RoundWaveStrength"; // 0 - 1
    private const string ROUND_WAVE_SPEED = "_RoundWaveSpeed"; // 0 - 5

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    #region HSV
    public void Set_HsvShift(float value)
    {
        _material.SetFloat(HSV_SHIFT, value);
        PlayerPrefs.SetFloat(HSV_SHIFT, value);
    }

    public void Set_HsvSaturation(float value)
    {
        _material.SetFloat(HSV_SATURATION, value);
        PlayerPrefs.SetFloat(HSV_SATURATION, value);
    }

    public void Set_HsvBright(float value)
    {
        _material.SetFloat(HSV_BRIGHT, value);
        PlayerPrefs.SetFloat(HSV_BRIGHT, value);
    }
    #endregion

    #region Pixelate
    public void Set_PixelateSize(float value)
    {
        _material.SetFloat(PIXELATE_SIZE, value);
        PlayerPrefs.SetFloat(PIXELATE_SIZE, value);
    }
    #endregion

    #region Wave
    public void Set_WaveAmount(float value)
    {
        _material.SetFloat(WAVE_AMOUNT, value);
        PlayerPrefs.SetFloat(WAVE_AMOUNT, value);
    }

    public void Set_WaveSpeed(float value)
    {
        _material.SetFloat(WAVE_SPEED, value);
        PlayerPrefs.SetFloat(WAVE_SPEED, value);
    }

    public void Set_WaveStrength(float value)
    {
        _material.SetFloat(WAVE_STRENGTH, value);
        PlayerPrefs.SetFloat(WAVE_STRENGTH, value);
    }
    #endregion

    #region Round Wave
    public void Set_RoundWaveStrength(float value)
    {
        _material.SetFloat(ROUND_WAVE_STRENGTH, value);
        PlayerPrefs.SetFloat(ROUND_WAVE_STRENGTH, value);
    }

    public void Set_RoundWaveSpeed(float value)
    {
        _material.SetFloat(ROUND_WAVE_SPEED, value);
        PlayerPrefs.SetFloat(ROUND_WAVE_SPEED, value);
    }
    #endregion

    public void Load()
    {
        #region HSV
        var hue = PlayerPrefs.GetFloat(HSV_SHIFT, 0f);
        Set_HsvShift(hue);

        var saturation = PlayerPrefs.GetFloat(HSV_SATURATION, 0.9349673f);
        Set_HsvSaturation(saturation);

        var bright = PlayerPrefs.GetFloat(HSV_BRIGHT, 0.5816249f);
        Set_HsvBright(bright);
        #endregion

        #region Pixelate
        var pixelateSize = PlayerPrefs.GetFloat(PIXELATE_SIZE, 512f);
        Set_PixelateSize(pixelateSize);
        #endregion

        #region Wave
        var waveAmount = PlayerPrefs.GetFloat(WAVE_AMOUNT, 0f);
        Set_WaveAmount(waveAmount);

        var waveSpeed = PlayerPrefs.GetFloat(WAVE_SPEED, 0f);
        Set_WaveSpeed(waveSpeed);

        var waveStrength = PlayerPrefs.GetFloat(WAVE_STRENGTH, 0f);
        Set_WaveStrength(waveStrength);
        #endregion

        #region Round Wave
        var roundWaveStrength = PlayerPrefs.GetFloat(ROUND_WAVE_STRENGTH, 0f);
        Set_RoundWaveStrength(roundWaveStrength);

        var roundWaveSpeed = PlayerPrefs.GetFloat(ROUND_WAVE_SPEED, 0f);
        Set_RoundWaveSpeed(roundWaveSpeed);
        #endregion

        UI_SettingsMenu.Instance.UpdateUI(hue, saturation, bright, pixelateSize, waveAmount, waveSpeed, waveStrength, roundWaveStrength, roundWaveSpeed);
    }
}
