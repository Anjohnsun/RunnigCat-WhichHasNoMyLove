using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _deathPanel;

    [SerializeField] PlayerMovement _player;
    [SerializeField] CameraMovement _camera;

    [SerializeField] List<Image> _comicsParts;

    public void OnStart()
    {
/*        StartCoroutine(PlayComics(_comicsParts));
*/
        _startPanel.SetActive(false);
        _player.StartMoving();
    }

    public void OnDeath()
    {
        _deathPanel.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator PlayComics(List<Image> comics)
    {
        Debug.Log("comics");
        foreach (Image image in comics)
        {
            DOTween.ToAlpha(() => image.color, x => image.color = x, 1, 2);
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
