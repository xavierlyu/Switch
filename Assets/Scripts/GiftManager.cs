using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GiftManager : MonoBehaviour {

	/// <summary>
	/// the time interval between gifts
	/// </summary>
	/// <example>
	/// "1:00:00" = one hour
	/// </example>
	public string timeInterval;
	public Button button;
	public Sprite[] sprites;

	void Start () {
		if (PlayerPrefs.GetString ("GiftTimestamp").Equals (null)) {
			DateTime now = DateTime.UtcNow;
			PlayerPrefs.SetString ("GiftTimestamp", now.ToString());
		}
		DateTime last = DateTime.Parse(PlayerPrefs.GetString ("GiftTimestamp"));
		SpriteState ss = button.spriteState;
		if (DateTime.UtcNow - last >= TimeSpan.Parse (timeInterval)) {
			button.image.sprite = sprites [0];
			ss.pressedSprite = sprites [1];
		} else {
			button.image.sprite = sprites [2];
			ss.pressedSprite = sprites [3];
		}
		button.spriteState = ss;
	}

	public void ReceiveGift(){
		if (PlayerPrefs.GetString ("GiftTimestamp").Equals (null)) {
			DateTime now = DateTime.UtcNow;
			PlayerPrefs.SetString ("GiftTimestamp", now.ToString());
		}
		DateTime last = DateTime.Parse(PlayerPrefs.GetString ("GiftTimestamp"));
		if (DateTime.UtcNow - last >= TimeSpan.Parse (timeInterval)) {
			//receive coins
			DateTime now = DateTime.UtcNow;
			PlayerPrefs.SetString ("GiftTimestamp", now.ToString());
			SpriteState ss = button.spriteState;
			button.image.sprite = sprites [2];
			ss.pressedSprite = sprites [3];
			button.spriteState = ss;
		}
	}
}
