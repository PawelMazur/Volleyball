using UnityEngine;
using System.Collections;

public class StaticPlayerSetup : MonoBehaviour
{

    public string classIdPlayer;
    public int numberPlayer;
    public string namePlayer;
    public Color colorPlayer;

    public void SetClassIdPlayer( string  classIdPlayer)
    {
        this.classIdPlayer = classIdPlayer;
    }

    public string GetclassIdPlayer()
    {
        return classIdPlayer;
    }

    public void SetNumberPlayer(int numberPlayer)
    {
        this.numberPlayer = numberPlayer;
    }

    public int GetNumberPlayer()
    {
        return numberPlayer;
    }

    public void SetNamePlayer(string namePlayer)
    {
        this.namePlayer = namePlayer;
    }

    public string GetNamePlayer()
    {
        return namePlayer;
    }

    public void SetColorPlayer(Color colorPlayer)
    {
        this.colorPlayer = colorPlayer;
    }

    public Color GetColorPlayer()
    {
        return colorPlayer;
    }
}
