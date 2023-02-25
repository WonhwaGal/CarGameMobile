using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Tool.Bundles.Examples
{
    internal class AssetBundleMyViewBase : MonoBehaviour
    {
        private const string UrlAssetBundleSprite = "https://drive.google.com/uc?export=download&id=1i_w2j-h-xSqQlQ74NlWHupmujfs3D1L3";
        
        [SerializeField] private DataSpriteBundle _dataSpriteBundle;

        private AssetBundle _spritesAssetBundle;

        protected IEnumerator DownloadAndSetAssetBundles()
        {
            yield return GetSpritesAssetBundle();

            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");
        }

        private IEnumerator GetSpritesAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleSprite);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spritesAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle)
        {
            _dataSpriteBundle.Image.sprite = assetBundle.LoadAsset<Sprite>(_dataSpriteBundle.NameAssetBundle);
        }
    }
}

