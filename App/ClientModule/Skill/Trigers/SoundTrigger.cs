using System;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

namespace GameFramework.Skill.Trigers
{
    public class AudioManager
    {
        public bool IsContainAudioSource(string name)
        {
            return m_AudioDict.ContainsKey(name);
        }

        public AudioSource GetAudioSource(string name)
        {
            AudioSource result = null;
            m_AudioDict.TryGetValue(name, out result);
            return result;
        }

        public void AddAudioSource(string name, AudioSource source)
        {
            if (!m_AudioDict.ContainsKey(name)) {
                m_AudioDict.Add(name, source);
            }
        }

        private Dictionary<string, AudioSource> m_AudioDict = new Dictionary<string, AudioSource>();
    }

    public class PlaySoundTriger : AbstractSkillTriger
    {
        protected override ISkillTriger OnClone()
        {
            PlaySoundTriger triger = new PlaySoundTriger();
            
            triger.m_Name = m_Name;
            triger.m_AudioSourceName = m_AudioSourceName;
            triger.m_AudioSourceLifeTime = m_AudioSourceLifeTime;
            triger.m_AudioGroup.AddRange(m_AudioGroup);
            triger.m_IsNeedCollide = m_IsNeedCollide;
            triger.m_IsBoneSound = m_IsBoneSound;
            triger.m_Position = m_Position;
            triger.m_BoneName = m_BoneName;
            triger.m_IsAttach = m_IsAttach;
            triger.m_volume = m_volume;
            triger.m_RealStartTime = StartTime;
            return triger;
        }

        public override void Reset()
        {
            m_IsResourcePreloaded = false;
            m_AudioSource = null;
            m_volume = 1.0f;

            m_RealStartTime = StartTime;
        }

        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            GfxSkillSenderInfo senderObj = sender as GfxSkillSenderInfo;
            if (null == senderObj) return false;
            GameObject obj = senderObj.GfxObj;
            if (null == obj) return false;
            if (m_RealStartTime < 0) {
                m_RealStartTime = TriggerUtil.RefixStartTime((int)StartTime, instance.LocalVariables, senderObj.ConfigData);
            }
            if (!m_IsResourcePreloaded) {
                PreloadResource(obj, instance);
            }
            if (curSectionTime < m_RealStartTime) {
                return true;
            }
            if (m_IsNeedCollide) {
            }
            string random_audio = GetRandomAudio();
            AudioClip clip = ResourceSystem.Instance.GetSharedResource(random_audio) as AudioClip;
            if (null == clip) {
                return false;
            }
            if (m_AudioSource != null) {
                if (m_AudioSource.loop) {
                    m_AudioSource.clip = clip;
                    m_AudioSource.Play();
                } else {
                    m_AudioSource.PlayOneShot(clip);
                }
                m_AudioSource.volume = m_volume;
                m_AudioSource.dopplerLevel = 0f;
            }

            return false;
        }

        private void PreloadResource(GameObject obj, SkillInstance instance)
        {
            m_IsResourcePreloaded = true;
            AudioManager audio_mgr = instance.CustomDatas.GetData<AudioManager>();
            if (audio_mgr == null) {
                audio_mgr = new AudioManager();
                instance.CustomDatas.AddData<AudioManager>(audio_mgr);
                audio_mgr.AddAudioSource(DefaultAudioName, obj.GetComponent<AudioSource>());
            }
            m_AudioSource = audio_mgr.GetAudioSource(m_Name);
            if (m_AudioSource == null) {
                m_AudioSource = CreateNewAudioSource(obj);
                if (m_AudioSource != null) {
                    audio_mgr.AddAudioSource(m_Name, m_AudioSource);
                } else {
                    m_AudioSource = obj.GetComponent<AudioSource>();
                }
            }
        }

        private string GetRandomAudio()
        {
            int random_index = m_Random.Next(0, m_AudioGroup.Count);
            if (0 <= random_index && random_index < m_AudioGroup.Count) {
                return m_AudioGroup[random_index];
            }
            return "";
        }

        private AudioSource CreateNewAudioSource(GameObject obj)
        {
            if (string.IsNullOrEmpty(m_AudioSourceName)) {
                return null;
            }
            GameObject audiosource_obj = ResourceSystem.Instance.NewObject(
                                                   m_AudioSourceName,
                                                   (StartTime + m_AudioSourceLifeTime) / 1000.0f) as GameObject;
            if (audiosource_obj == null) {
                return null;
            }
            if (m_IsBoneSound) {
                Transform attach_node = TriggerUtil.GetChildNodeByName(obj, m_BoneName);
                if (attach_node != null) {
                    audiosource_obj.transform.parent = attach_node;
                    audiosource_obj.transform.rotation = Quaternion.identity;
                    audiosource_obj.transform.position = Vector3.zero;
                    if (!m_IsAttach) {
                        audiosource_obj.transform.parent = null;
                    }
                } else {
                    audiosource_obj.transform.position = obj.transform.TransformPoint(m_Position);
                    if (m_IsAttach) {
                        audiosource_obj.transform.parent = obj.transform;
                    }
                }
            } else {
                audiosource_obj.transform.position = obj.transform.TransformPoint(m_Position);
                if (m_IsAttach) {
                    audiosource_obj.transform.parent = obj.transform;
                }
            }
            return audiosource_obj.GetComponent<AudioSource>();
        }

        protected override void Load(Dsl.CallData callData, int dslSkillId)
        {
            int num = callData.GetParamNum();
            if (num >= 6) {
                StartTime = long.Parse(callData.GetParamId(0));
                m_Name = callData.GetParamId(1);
                m_AudioSourceName = callData.GetParamId(2);
                m_AudioSourceLifeTime = long.Parse(callData.GetParamId(3));
                m_AudioGroup.Add(callData.GetParamId(4));
                m_IsNeedCollide = bool.Parse(callData.GetParamId(5));
            }
            m_RealStartTime = StartTime;
        }

        protected override void Load(Dsl.FunctionData funcData, int dslSkillId)
        {
            Dsl.CallData callData = funcData.Call;
            if (null == callData) {
                return;
            }
            Load(callData, dslSkillId);
            for (int i = 0; i < funcData.Statements.Count; i++) {
                Dsl.CallData stCall = funcData.Statements[i] as Dsl.CallData;
                if (null == stCall) {
                    continue;
                }
                if (stCall.GetId() == "position") {
                    LoadPositionConfig(stCall);
                } else if (stCall.GetId() == "bone") {
                    LoadBoneConfig(stCall);
                } else if (stCall.GetId() == "audiogroup") {
                    LoadAudioGroup(stCall);
                } else if (stCall.GetId() == "volume") {
                    if (stCall.GetParamNum() >= 1) {
                        m_volume = float.Parse(stCall.GetParamId(0));
                    } else {
                        m_volume = 1.0f;
                    }
                }
            }
        }

        private void LoadAudioGroup(Dsl.CallData stCall)
        {
            for (int i = 0; i < stCall.GetParamNum(); i++) {
                m_AudioGroup.Add(stCall.GetParamId(i));
            }
        }

        private void LoadPositionConfig(Dsl.CallData stCall)
        {
            if (stCall.GetParamNum() >= 2) {
                m_IsBoneSound = false;
                m_Position = DslUtility.CalcVector3(stCall.GetParam(0) as Dsl.CallData);
                m_IsAttach = bool.Parse(stCall.GetParamId(1));
            }
        }

        private void LoadBoneConfig(Dsl.CallData stCall)
        {
            if (stCall.GetParamNum() >= 2) {
                m_IsBoneSound = true;
                m_Position = DslUtility.CalcVector3(stCall.GetParam(0) as Dsl.CallData); ;
                m_IsAttach = bool.Parse(stCall.GetParamId(1));
            }
        }

        public static string DefaultAudioName = "default";

        private string m_Name;
        private string m_AudioSourceName;
        private long m_AudioSourceLifeTime;
        private List<string> m_AudioGroup = new List<string>();
        private bool m_IsNeedCollide = false;
        private float m_volume = 1.0f;
        private System.Random m_Random = new System.Random();
        private bool m_IsBoneSound = false;
        private Vector3 m_Position = new Vector3(0, 0, 0);
        private string m_BoneName = "";
        private bool m_IsAttach = true;

        private AudioSource m_AudioSource = null;
        private bool m_IsResourcePreloaded = false;

        private long m_RealStartTime = 0;
    }

    public class StopSoundTrigger : AbstractSkillTriger
    {
        protected override ISkillTriger OnClone()
        {
            StopSoundTrigger copy = new StopSoundTrigger();
            
            copy.m_Name = m_Name;
            copy.m_RealStartTime = m_RealStartTime;
            return copy;
        }

        public override void Reset()
        {
            m_RealStartTime = StartTime;
        }

        protected override void Load(Dsl.CallData callData, int dslSkillId)
        {
            if (callData.GetParamNum() >= 2) {
                StartTime = long.Parse(callData.GetParamId(0));
                m_Name = callData.GetParamId(1);
            }
            m_RealStartTime = StartTime;
        }

        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            GfxSkillSenderInfo senderObj = sender as GfxSkillSenderInfo;
            if (null == senderObj) return false;
            GameObject obj = senderObj.GfxObj;
            if (null == obj) return false;
            if (m_RealStartTime < 0) {
                m_RealStartTime = TriggerUtil.RefixStartTime((int)StartTime, instance.LocalVariables, senderObj.ConfigData);
            }
            if (curSectionTime < m_RealStartTime) {
                return true;
            }
            if (obj.GetComponent<AudioSource>() == null) {
                return false;
            }
            AudioManager mgr = instance.CustomDatas.GetData<AudioManager>();
            if (mgr == null) {
                return false;
            }
            AudioSource source = mgr.GetAudioSource(m_Name);
            if (source != null) {
                source.Stop();
            }
            return false;
        }

        private string m_Name;
        private long m_RealStartTime = 0;
    }
}
