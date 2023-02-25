using TMPro; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Bundles.Examples
{
    internal class MyLoadButtonView : AssetBundleMyViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private TextMeshProUGUI _buttonText;

        [Header("Addressables")]
        [SerializeField] private Image _backgroundPanel;
        [SerializeField] private AssetReference _backgroundAsset;
        [SerializeField] private Button _setBackground;
        [SerializeField] private Button _removeBackground;

        private AsyncOperationHandle<Sprite> _addressablePrefab;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _setBackground.onClick.AddListener(LoadBackground);
            _removeBackground.onClick.AddListener(RemoveBackground);

            _removeBackground.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _setBackground.onClick.RemoveAllListeners();
            _removeBackground.onClick.RemoveAllListeners();

            RemoveBackground();
        }


        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            _buttonText.text = string.Empty;

            StartCoroutine(DownloadAndSetAssetBundles());
        }


        private void LoadBackground()
        {
            _addressablePrefab = Addressables.LoadAssetAsync<Sprite>(_backgroundAsset);

            _addressablePrefab.Completed += ReplaceBackgroundSprite;
        }

        private void ReplaceBackgroundSprite(AsyncOperationHandle<Sprite> handle)
        {
            Debug.Log("Sprite load completed");
            _backgroundPanel.sprite = handle.Result;

            _removeBackground.gameObject.SetActive(true);
        }


        private void RemoveBackground()
        {
            _removeBackground.gameObject.SetActive(false);
            _backgroundPanel.sprite = null;
            //Addressables.Release<AsyncOperationHandle<Sprite>>(_addressablePrefab);
        }
    }
}

