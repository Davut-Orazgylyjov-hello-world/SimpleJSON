using UnityEngine;

namespace OrazgylyjovFuteres.SimpleJSON
{
    public enum FolderType
    {
        StreamingAssets,
        Resources
    }

    [CreateAssetMenu(fileName = "FileSystemSettingsSO", menuName = "Settings/File System Settings")]
    public class FileSystemSettingsSO : ScriptableObject
    {
        [Tooltip("Specify the folder type to use.")]
        public FolderType useFilesFolder = FolderType.StreamingAssets;

        // Статическое свойство для удобного доступа к настройкам
        private static FileSystemSettingsSO _instance;

        public static FolderType CurrentFolderType
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<FileSystemSettingsSO>(nameof(FileSystemSettingsSO));
                    if (_instance == null)
                    {
                        Debug.LogWarning("FileSystemSettingsSO asset not found in Resources!. Please Create one and move to Resources/FileSystemSettingsSO");
                    }
                }

                return _instance?.useFilesFolder ?? FolderType.StreamingAssets;
            }
            set
            {
                if (_instance == null) return;
                _instance.useFilesFolder = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(_instance);
#endif
            }
        }
    }
}