    /// <summary>
    /// Static class for saving and loading ScriptableObject data securely using Easy Save 3.
    /// Provides folder checks, default value initialization, and localization setting support.
    /// </summary>
    public static class SaveManager
    {
        // Path for save file and directory (can be made configurable)
        static string file = "C:\\Users\\User\\OneDrive\\Documents\\KingSlayer\\Player\\SaveTest";
        static string directoryPath = "C:\\Users\\User\\OneDrive\\Documents\\KingSlayer\\Player\\r";

        /// <summary>
        /// Initialize Easy Save settings with AES encryption and password.
        /// </summary>
        static ES3Settings Init()
        {
            ES3Settings saveSettings = new ES3Settings
            {
                encryptionType = ES3.EncryptionType.AES,
                encryptionPassword = "123"
            };
            return saveSettings;
        }

        /// <summary>
        /// Saves the ScriptableObject data to disk using the given key.
        /// Ensures the save directory exists before saving.
        /// </summary>
        public static void SaveData(this ScriptableObject data, string key)
        {
            EnsureDirectoryExists();
            ES3.Save(key, data, file, Init());
        }

        /// <summary>
        /// Loads ScriptableObject data from disk by key.
        /// Creates default file and folder if missing.
        /// </summary>
        public static void LoadTheData(this ScriptableObject data, string key)
        {
            CheckFolderAndLoad(data, key);
        }

        /// <summary>
        /// Checks if save directory exists; if not, creates default data and saves it.
        /// Loads data afterward and sets it to SettingManager.
        /// </summary>
        static void CheckFolderAndLoad(ScriptableObject data, string key)
        {
            if (!Directory.Exists(directoryPath))
            {
                CreateDefaultFile(key, data);
                Directory.CreateDirectory(directoryPath);
                SaveData(data, key);
            }

            ES3.Load(key, file, data, Init());
            SettingManager.Instance.SetLoadedData();
        }

        /// <summary>
        /// Creates the save folder if it doesn't exist.
        /// </summary>
        static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Initializes default values for specific keys, such as GameSettings.
        /// </summary>
        static void CreateDefaultFile(string key, ScriptableObject data)
        {
            if (key.Equals("GameSettings"))
            {
                var _settings = data as GameSettings;

                _settings.frameRate = -1;
                _settings.fullScreenMode = DISPLAY.FULLSCREEN;
                _settings.width = Screen.width;
                _settings.height = Screen.height;
                _settings.indexDisplay = DISPLAY.FULLSCREEN;
                _settings.indexFrameRate = FRAMERATE.UNLIMITED;
                _settings.indexResolution = GetResolutionIndex(Screen.width);
                _settings.masterVolume = 0;
                _settings.musicVolume = 0;
                _settings.sfxVolume = 0;
                _settings.localIndex = SetLocal();
                _settings.indexMasterVol = 0;
                _settings.indexMusicVol = 0;
                _settings.indexSfxVol = 0;
            }
        }

        /// <summary>
        /// Returns resolution index based on screen width for default settings.
        /// </summary>
        static int GetResolutionIndex(int width)
        {
            return width switch
            {
                1080 => 0,
                1440 => 1,
                _ => 0,
            };
        }

        /// <summary>
        /// Maps system language to localization index.
        /// </summary>
        static int SetLocal()
        {
            return Application.systemLanguage switch
            {
                SystemLanguage.German => 1,
                SystemLanguage.French => 2,
                SystemLanguage.Italian => 3,
                SystemLanguage.Spanish => 4,
                SystemLanguage.Portuguese => 5,
                SystemLanguage.Polish => 7,
                SystemLanguage.Russian => 8,
                SystemLanguage.ChineseSimplified => 9,
                SystemLanguage.Japanese => 10,
                SystemLanguage.Korean => 11,
                SystemLanguage.Turkish => 12,
                _ => 0,
            };
        }
    }
