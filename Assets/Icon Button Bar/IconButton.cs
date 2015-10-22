﻿using UnityEngine;
using System.Collections;

public class IconButton : MonoBehaviour
{
  public string buttonID = "none";
  public Material mat;
  MeshRenderer buttonRenderer;
  Color defaultColor;
  Color fadeStartColor;
  public IconButtonBar buttonBar;
  public GameObject textLabel;

  TextMesh labelTextMesh;

  // Use this for initialization
  void Start()
  {
    gameObject.tag = Crosshair3D.kCrosshairTargetable;

    buttonRenderer = gameObject.AddComponent<MeshRenderer>();
    buttonRenderer.material = mat;

    defaultColor = CurrentColor();

    MeshUtilities.AddMeshComponent(gameObject, .15f, .15f);

    // start off hidden
    // SetColorAlpha(0);

    labelTextMesh = textLabel.GetComponent<TextMesh>();
    labelTextMesh.color = new Color(0f, 0f, 0f, .9f);
  }

  void SetColorAlpha(float alpha)
  {
    buttonRenderer.material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
  }

  Color CurrentColor()
  {
    return buttonRenderer.material.GetColor("_Color");
  }

  public void OnClick()
  {
    buttonBar.OnButtonClick(buttonID);
  }

  public void OnHoverStart()
  {
    transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

    // play click sound
    if (gameObject.GetComponent<AudioSource>() != null) gameObject.GetComponent<AudioSource>().Play();
  }

  public void OnHoverEnd()
  {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }

  public void FadeIn(bool fadeIn)
  {
    float start = 1f;
    float end = 0f;

    if (fadeIn)
    {
      start = 0f;
      end = 1f;
    }

    iTween.ValueTo(gameObject, iTween.Hash("from", start, "to", end, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "FadeUpdate", "time", .5f));
  }

  void FadeUpdate(float alpha)
  {
    SetColorAlpha(alpha);
  }

}
