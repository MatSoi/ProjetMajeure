using UnityEngine;

/**
 * Class CloseOptions
 * Classe rapide qui ferme et affiche les options quand on appuie sur la croix
 * */
public class CloseOptions : MonoBehaviour {
    public GameObject[] Menus;

    bool visible = false;

    public void closeOptions()
    {
        foreach (GameObject menu in Menus)
        {
            menu.SetActive(visible);
        }
        visible = !visible;
    }
}
