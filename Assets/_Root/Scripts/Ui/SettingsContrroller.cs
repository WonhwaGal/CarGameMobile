using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

internal class SettingsContrroller : BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/settingsMenu");
    private readonly ProfilePlayer _profilePlayer;
    private readonly SettingsView _view;


    public SettingsContrroller(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(GoBack);
    }

    private SettingsView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<SettingsView>();
    }

    private void GoBack() =>
        _profilePlayer.CurrentState.Value = GameState.Start;
}
