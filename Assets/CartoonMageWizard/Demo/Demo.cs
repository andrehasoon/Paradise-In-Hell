using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Demo : MonoBehaviour 
{
#if UNITY_EDITOR
	private string[] animNames = null;

	private Animation anim = null;

    private void Awake()
    {
		AnimationClip[] clips = AnimationUtility.GetAnimationClips(gameObject);
		anim = GetComponent<Animation>();
		animNames = new string[clips.Length];
        for (int i = 0; i < clips.Length; i++)
        {
			animNames[i] = clips[i].name;
        }

		System.Array.Sort(animNames);
	}

    private void OnGUI()
	{
		Color guiColor = GUI.color;
		GUI.color = Color.yellow;

		float size = 8;
		float btnSize = Screen.height / size;
		
		for(int i = 0; i < animNames.Length; ++i)
		{
			int col = (int)(i / size);
			if(GUI.Button(new Rect(Screen.width - btnSize * (col + 1), (i - col * size) * btnSize, btnSize, btnSize), animNames[i]))
			{
				anim.CrossFade(animNames[i], 0.2f);
			}
		}

		GUI.color = guiColor;
	}
#endif
}
