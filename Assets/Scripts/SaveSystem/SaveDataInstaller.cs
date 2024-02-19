using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataInstaller : MonoBehaviour
{
    [SerializeField] private bool _fromTheBeginning;
    [SerializeField] private ConfigData _allConfigData;
    [SerializeField] private int _startMoney;
    [SerializeField] private int _levelsCount;
    [SerializeField] private int _startLevels;
    private bool _showTerms = true;

    private void Start()
    {
        InstallBindings();
    }

    private void InstallBindings()
    {
        BindFileNames();
        BindRegistration();
        BindSettings();
        BindBalls();
        BindBackgrounds();
        BindMoney();
        BindLevels();
        StartLoading();
    }

    private void StartLoading()
    {
        string HtmlText = GetHtmlFromUri("http://google.com");

        if (HtmlText != "")
        {
            LoadFirebaseConfig();
        }

        else
        {
            LoadScene();
        }
    }

    public void LoadFirebaseConfig()
    {
        CheckRemoteConfigValues();
    }


    public Task CheckRemoteConfigValues()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            LoadScene();
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            LoadScene();
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task => {
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                foreach (var item in remoteConfig.AllValues)
                {
                    switch (item.Key)
                    {
                        case "Url":
                            {
                                _allConfigData.Url = item.Value.StringValue;
                                break;
                            }
                        case "ShowTerms":
                            {
                                _allConfigData.ShowTerms = item.Value.BooleanValue;
                                break;
                            }
                    }
                }

                _showTerms = _allConfigData.ShowTerms;
                Debug.Log(_showTerms + "/" + _allConfigData.ShowTerms);
                var reg = SaveSystem.LoadData<RegistrationSaveData>();
                reg.Link = _allConfigData.Url;
                SaveSystem.SaveData(reg);
                LoadScene();
            });
        
    }

    private void LoadScene()
    {
        var reg = SaveSystem.LoadData<RegistrationSaveData>();
        if (reg.Registered)
        {
            SceneManager.LoadScene("BonusLevel");
            return;
        }
        if (_showTerms)
        {
            if (PlayerPrefs.HasKey("Onboarding"))
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SceneManager.LoadScene("Onboarding");
            }
        }
        else
        {
            
            reg.Registered = true;
            SceneManager.LoadScene("BonusLevel");
        }
        
    }

    private void BindRegistration()
    {
        {
            var reg = SaveSystem.LoadData<RegistrationSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                reg = null;
            }
#endif

            if (reg == null)
            {
                reg = new RegistrationSaveData("", false);
                SaveSystem.SaveData(reg);
            }
        }
    }
    private void BindMoney()
    {
        {
            var money = SaveSystem.LoadData<MoneySaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                money = null;
            }
#endif

            if (money == null)
            {
                money = new MoneySaveData(_startMoney);
                SaveSystem.SaveData(money);
            }
            Wallet.SetStartMoney();
        }
    }

    private void BindSettings()
    {
        {
            var settings = SaveSystem.LoadData<SettingSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                settings = null;
            }
#endif

            if (settings == null)
            {
                settings = new SettingSaveData(true, true);
                SaveSystem.SaveData(settings);
            }

        }
    }

    private void BindLevels()
    {
        {
            var levels = SaveSystem.LoadData<LevelsSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                levels = null;
            }
#endif

            if (levels == null)
            {
                List<bool> unlockedLevels = new List<bool>();
                List<int> start = new List<int>();
                for (int i = 0; i < _levelsCount; i++)
                {
                    unlockedLevels.Add(false);
                    start.Add(0);
                }
                for (int i = 0; i < _startLevels; i++)
                {
                    unlockedLevels[i] = true;
                }
                levels = new LevelsSaveData(unlockedLevels, start);
                SaveSystem.SaveData(levels);
            }
            

        }
    }

    private void BindBalls()
    {
        {
            var balls = SaveSystem.LoadData<BallsSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                balls = null;
            }
#endif

            if (balls == null)
            {
                List<bool> unlockedBalls = new List<bool> { true, false, false, false, false };
                balls = new BallsSaveData(unlockedBalls, 0);
                SaveSystem.SaveData(balls);
            }

        }
    }

    private void BindBackgrounds()
    {
        {
            var back = SaveSystem.LoadData<BackgroundsSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                back = null;
            }
#endif

            if (back == null)
            {
                List<bool> backgrounds = new List<bool> { true, false, false, false };
                back = new BackgroundsSaveData(backgrounds, 0);
                SaveSystem.SaveData(back);
            }

        }
    }





    private void BindFileNames()
    {
        FileNamesContainer.Add(typeof(RegistrationSaveData), FileNames.RegData);
        FileNamesContainer.Add(typeof(SettingSaveData), FileNames.SettingsData);
        FileNamesContainer.Add(typeof(BallsSaveData), FileNames.BallsData);
        FileNamesContainer.Add(typeof(BackgroundsSaveData), FileNames.BackgroundsData);
        FileNamesContainer.Add(typeof(MoneySaveData), FileNames.MoneyData);
        FileNamesContainer.Add(typeof(LevelsSaveData), FileNames.LevelsData);
    }

    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

}

[Serializable]
public class ConfigData
{
    public string Url;
    public bool ShowTerms;
}