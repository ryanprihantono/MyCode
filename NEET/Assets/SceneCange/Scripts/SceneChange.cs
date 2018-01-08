using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*	@author	田原
 *	使用方法 
 * ボタンスクリプトにアタッチして、遷移先のシーンを、
 * SCENE_NOをインスペクターでドロップダウンリストから選択
 * ButtonスクリプトのOnClickにこのButtonオブジェクトをアタッチ、
 * 最後に、sceneChange()を選択すれば、ボタンクリックで遷移します。
 * 
 * シーン設定方法
 * SCENE_NOとList_SceneName(実際のシーン名)に追加
 * 注意：番号を対応させないと正しく動きません
 */

/// <summary>
/// シーンの番号
/// </summary>
public enum SCENE_NO
{
    NULL	= -1,
    START	= 0,
    SCENE_A,
    SCENE_B
}

/// <summary>
/// シーン遷移マネージャー
/// </summary>
public class SceneChange : MonoBehaviour {

	//	インスペクターで遷移先のシーンを選択
    public SCENE_NO sceneNo;

	/// <summary>
	/// シーンの名前
	/// SCENE_NOと対応させる
	/// </summary>
    public List<string> List_sceneList = new List<string>
    {
        "startScene",
        "A_Scene",
        "B_Scene"
    };

	/// <summary>
	///	シーン名を取得
	/// </summary>
	/// <param name="_sceneNo">インスペクターで設定したシーン</param>
	/// <returns>string型のシーン名</returns>
    private string getSceneName_str(SCENE_NO _sceneNo)
    {
		return List_sceneList[(int)_sceneNo];
    }

	/// <summary>
	/// シーン遷移
	/// </summary>
	public void sceneChange()
	{
		Application.LoadLevel(getSceneName_str(sceneNo));
	}
    

}
