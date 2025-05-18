using UnityEngine;

public class Main : MonoBehaviour
{
    private const string urlTrailer = "https://www.youtube.com/watch?v=YJ85K-zxAcE";
    private const string urlGoogleAndroidStore = "https://play.google.com/store/apps/details?id=com.DanteGames.Hellmaster&pli=1";
    private const string urlPCStore = "https://elixir.games/browse/hellmaster";

    public void OpenTrailer()
    {
        Application.OpenURL(urlTrailer);
    }

    public void OpenGoogleAndroidStore()
    {
        Application.OpenURL(urlGoogleAndroidStore);
    }

    public void OpenPC()
    {
        Application.OpenURL(urlPCStore);
    }
}
