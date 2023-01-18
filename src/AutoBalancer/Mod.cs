using System;
using Modding;
using Modding.Common;
using Modding.Blocks;
using UnityEngine;

namespace AutoBalancer
{
	public class Mod : ModEntryPoint
	{
		public override void OnLoad()
		{
			Log($"Loaded. Version = {Mods.GetVersion(new Guid("716d4bcf-1143-4d09-bae1-3e353678e37b"))}");
		}
		public static void Log(string msg)
        {
			Debug.Log("Auto Balancer: " + msg);
        }
		public static void Warning(string msg)
        {
			Debug.LogWarning("Auto Balancer: " + msg);
        }
		public static void Error(string msg)
        {
			Debug.LogError("Auto Balancer: " + msg);
        }
	}
	/// <summary>
	/// バランサーに共通するスクリプト
	/// </summary>
	public abstract class Balancer : ModBlockBehaviour
	{
		/// <summary>
		/// 有効化するキー
		/// </summary>
		protected MKey keyActivate;
		/// <summary>
		/// 目標角度をピッチ上方向に移動
		/// </summary>
		protected MKey keyPitchUp;
		/// <summary>
		/// 目標角度をピッチ下方向に移動
		/// </summary>
		protected MKey keyPitchDown;
		/// <summary>
		/// 目標角度をロール右方向に移動
		/// </summary>
		protected MKey keyRollRight;
		/// <summary>
		/// 目標角度をロール左方向に移動
		/// </summary>
		protected MKey keyRollLeft;
		/// <summary>
		/// 目標角度をヨー右方向に移動
		/// </summary>
		protected MKey keyYawRight;
		/// <summary>
		/// 目標角度をヨー左方向に移動
		/// </summary>
		protected MKey keyYawLeft;
		/// <summary>
		/// 目標角度を移動するキーの感度を変えるスライダー
		/// </summary>
		protected MSlider sliderSensitivity;
		/// <summary>
		/// 目標角度
		/// </summary>
		protected Quaternion referenceRotation = Quaternion.identity;

		public override void SafeAwake()
        {
			keyActivate = AddKey("Activate", "activate", KeyCode.B);
			keyPitchUp = AddKey("Pitch Up", "pitch-up", KeyCode.T);
			keyPitchDown = AddKey("Pitch Down", "pitch-down", KeyCode.G);
			keyRollRight = AddKey("Roll Right", "roll-right", KeyCode.H);
			keyRollLeft = AddKey("Roll Left", "roll-left", KeyCode.F);
			keyYawRight = AddKey("Yaw Right", "yaw-right", KeyCode.Y);
			keyYawLeft = AddKey("Yaw Left", "yaw-left", KeyCode.R);
			sliderSensitivity = AddSlider("Sensitivity", "sensitivity", 1f, 0f, 10f);

			referenceRotation = Quaternion.identity;
        }
        public override void SimulateFixedUpdateAlways()
        {
            // キー受付
        }
        public override void KeyEmulationUpdate()
        {
            // キー受付
        }
        public override void SimulateFixedUpdateHost()
        {
            if (keyActivate.IsDown)
            {
				Rigidbody.AddTorque(Force(transform.rotation, referenceRotation), ForceMode.Force);
            }
        }
		/// <summary>
		/// 目標角度に向かうような回転力をかける
		/// </summary>
		/// <param name="currentRotation"></param>
		/// <param name="referenceRotation"></param>
		/// <returns></returns>
		protected abstract Vector3 Force(Quaternion currentRotation, Quaternion referenceRotation);
    }
	/// <summary>
	/// https://gist.github.com/botamochi6277/bf5f54e3c888fb5b7f7b5907409c063d
	/// </summary>
	public class PIDBalancer : Balancer
    {
		/// <summary>
		/// Pゲイン
		/// </summary>
		private MSlider keyPGain;
		/// <summary>
		/// Iゲイン
		/// </summary>
		private MSlider keyIGain;
		/// <summary>
		/// Dゲイン
		/// </summary>
		private MSlider keyDGain;

        public override void SafeAwake()
        {
            base.SafeAwake();
			keyPGain = AddSlider("P Gain", "p-gain", 1f, 0f, 10f);
        }
    }
}
