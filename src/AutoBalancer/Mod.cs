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
	/// �o�����T�[�ɋ��ʂ���X�N���v�g
	/// </summary>
	public abstract class Balancer : ModBlockBehaviour
	{
		/// <summary>
		/// �L��������L�[
		/// </summary>
		protected MKey keyActivate;
		/// <summary>
		/// �ڕW�p�x���s�b�`������Ɉړ�
		/// </summary>
		protected MKey keyPitchUp;
		/// <summary>
		/// �ڕW�p�x���s�b�`�������Ɉړ�
		/// </summary>
		protected MKey keyPitchDown;
		/// <summary>
		/// �ڕW�p�x�����[���E�����Ɉړ�
		/// </summary>
		protected MKey keyRollRight;
		/// <summary>
		/// �ڕW�p�x�����[���������Ɉړ�
		/// </summary>
		protected MKey keyRollLeft;
		/// <summary>
		/// �ڕW�p�x�����[�E�����Ɉړ�
		/// </summary>
		protected MKey keyYawRight;
		/// <summary>
		/// �ڕW�p�x�����[�������Ɉړ�
		/// </summary>
		protected MKey keyYawLeft;
		/// <summary>
		/// �ڕW�p�x���ړ�����L�[�̊��x��ς���X���C�_�[
		/// </summary>
		protected MSlider sliderSensitivity;
		/// <summary>
		/// �ڕW�p�x
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
            // �L�[��t
        }
        public override void KeyEmulationUpdate()
        {
            // �L�[��t
        }
        public override void SimulateFixedUpdateHost()
        {
            if (keyActivate.IsDown)
            {
				Rigidbody.AddTorque(Force(transform.rotation, referenceRotation), ForceMode.Force);
            }
        }
		/// <summary>
		/// �ڕW�p�x�Ɍ������悤�ȉ�]�͂�������
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
		/// P�Q�C��
		/// </summary>
		private MSlider keyPGain;
		/// <summary>
		/// I�Q�C��
		/// </summary>
		private MSlider keyIGain;
		/// <summary>
		/// D�Q�C��
		/// </summary>
		private MSlider keyDGain;

        public override void SafeAwake()
        {
            base.SafeAwake();
			keyPGain = AddSlider("P Gain", "p-gain", 1f, 0f, 10f);
        }
    }
}
