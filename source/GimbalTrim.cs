#region GPL-2.0+ARR
/*
 * GimbalTrim (TRIM) for Kerbal Space Program
 * 
 * Copyright (c) 2014 Sébastien GAGGINI AKA Sarbian, France
 * Copyright (c) 2020, 2023 zer0Kerbal
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or (at
 * your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA.
*/
#endregion

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KSP.Localization;

namespace GimbalTrim
{
    [KSPModule("Trim")]
    public class GimbalTrim : PartModule
    {
        // name of the transform of the gimbal we control
        [KSPField(isPersistant = false)]
        public string gimbalTransformName = "thrustTransform";

        // Copy of the default engine rotation
        public List<Quaternion> originalInitalRots;

        private ModuleGimbal gimbal;

        private float lastEditorTime = 0;
        
        private Vector3 currentTrim;
        private Vector3[] oldLocalTrim;

        [KSPField(isPersistant = false)]
        public bool limitToGimbalRange = false;

        // The range have 2 uses :
        // When limitToGimbalRange is false they store the max range of the trimming
        // When limitToGimbalRange is true they store the gimbal initial max range
        
        [KSPField(isPersistant = false)]
        public float trimRange = 30f;

        [KSPField]
        public float trimRangeXP = -1f;

        [KSPField]
        public float trimRangeYP = -1f;

        [KSPField]
        public float trimRangeXN = -1f;

        [KSPField]
        public float trimRangeYN = -1f;

        //[KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-gui-name", groupName = "GimbalTrim", //Plugged In?
        //    groupDisplayName = "GimbalTrim v " + Version.SText, groupStartCollapsed = true)]
        //public bool _isActive = false;

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-100"),		// #TRIM-100 = X-Trim
         UI_FloatRange(minValue = -14f, maxValue = 14f, stepIncrement = 0.5f)]
        public float trimX = 0;

        public float TrimX
        {
            get { return trimX; }
            set { trimX = Mathf.Clamp(value, -trimRangeXN, trimRangeXP); }
        }

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-101"),		// #TRIM-101 = Y-Trim
         UI_FloatRange(minValue = -14f, maxValue = 14f, stepIncrement = 0.5f)]
        public float trimY = 0;

        public float TrimY
        {
            get { return trimY; }
            set { trimY = Mathf.Clamp(value, -trimRangeYN, trimRangeYP); }
        }

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-102"),		// #TRIM-102 = Trim
         UI_Toggle(disabledText = "#TRIM-103", enabledText = "#TRIM-104")]		// #TRIM-103 = Disabled		// #TRIM-104 = Enabled
        public bool enableTrim = true;

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-105"),		// #TRIM-105 = Method
         UI_Toggle(disabledText = "#TRIM-107", enabledText = "#TRIM-106")]		// #TRIM-106 = Smooth		// #TRIM-107 = Precise
        public bool useTrimResponseSpeed = false;

        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#TRIM-108"),		// #TRIM-108 = Speed
         UI_FloatRange(minValue = 1f, maxValue = 100.0f, stepIncrement = 1f)]
        public float trimResponseSpeed = 60;

        [KSPAction("#TRIM-110")]		// #TRIM-110 = X Trim +
        public void plusTrimX(KSPActionParam param)
        { TrimX += 1f; }

        [KSPAction("#TRIM-111")]		// #TRIM-111 = X Trim -
        public void minusTrim(KSPActionParam param)
        { TrimX -= 1f; }

        [KSPAction("#TRIM-112")]		// #TRIM-112 = X Trim +5
        public void plus5Trim(KSPActionParam param)
        { TrimX += 5f; }

        [KSPAction("#TRIM-113")]		// #TRIM-113 = X Trim -5
        public void minus5Trim(KSPActionParam param) 
        { TrimX -= 5f; }

        [KSPAction("#TRIM-114")]		// #TRIM-114 = Y Trim +
        public void plusTrimY(KSPActionParam param)
        { TrimY += 1f; }

        [KSPAction("#TRIM-115")]		// #TRIM-115 = Y Trim -
        public void minusTrimY(KSPActionParam param)
        { TrimY -= 1f; }

        [KSPAction("#TRIM-116")]		// #TRIM-116 = Y Trim +5
        public void plus5TrimY(KSPActionParam param)
        { TrimY += 5f; }

        [KSPAction("#TRIM-117")]		// #TRIM-117 = Y Trim -5
        public void minus5TrimY(KSPActionParam param)
        { TrimY -= 5f; }

        [KSPAction("#TRIM-118")]		// #TRIM-118 = Toggle Trim
        public void toggleTrim(KSPActionParam param)
        { enableTrim = !enableTrim; }

        /// <summary>InitRange</summary>
        public void InitRange()
        {
            if (limitToGimbalRange)
            {
                trimRange = gimbal.gimbalRange;
                trimRangeXP = gimbal.gimbalRangeXP;
                trimRangeYP = gimbal.gimbalRangeYP;
                trimRangeXN = gimbal.gimbalRangeXN;
                trimRangeYN = gimbal.gimbalRangeYN;
            }

            //print("Ranges = " + trimRange.ToString("F1") + " " + trimRangeXN.ToString("F1") + " " + trimRangeXP.ToString("F1") + " " + trimRangeYN.ToString("F1") + " " + trimRangeYP.ToString("F1"));

            if (trimRangeXP < 0f)
            {
                trimRangeXP = trimRange;
            }
            if (trimRangeYP < 0f)
            {
                trimRangeYP = trimRange;
            }
            if (trimRangeXN < 0f)
            {
                trimRangeXN = trimRangeXP;
            }
            if (trimRangeYN < 0f)
            {
                trimRangeYN = trimRangeYP;
            }

            //print("Ranges = " + trimRangeXN.ToString("F1") + " " + trimRangeXP.ToString("F1") + " " + trimRangeYN.ToString("F1") + " " + trimRangeYP.ToString("F1"));
        }

        /// <summary>OnStart</summary>
        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            gimbal = part.Modules.GetModules<ModuleGimbal>().FirstOrDefault(g => g.gimbalTransformName == gimbalTransformName);

            if (gimbal == null)
            {
                print(Localizer.Format("#TRIM-109") + gimbalTransformName);		// #TRIM-109 = Could not find a ModuleGimbal with gimbalTransformName = 
                return;
            }

            InitRange();

            for (int i = 0; i < gimbal.initRots.Count; i++)
            {
                originalInitalRots.Add(gimbal.initRots[i]);
            }
            oldLocalTrim = new Vector3[gimbal.initRots.Count];

            BaseField trimXField = Fields["trimX"];
            trimXField.guiActive = trimRangeXP + trimRangeXN > 0;
            trimXField.guiActiveEditor = trimRangeXP + trimRangeXN > 0;
            UI_FloatRange trimXRange = (UI_FloatRange) (state == StartState.Editor ? trimXField.uiControlEditor : trimXField.uiControlFlight);
            trimXRange.minValue = -trimRangeXN;
            trimXRange.maxValue = trimRangeXP;
            trimXRange.stepIncrement = trimRangeXP + trimRangeXN >= 20f ? 1f : trimRangeXP + trimRangeXN >= 10f ? 0.5f : 0.25f;

            BaseField trimYField = Fields["trimY"];
            trimYField.guiActive = trimRangeYP + trimRangeYN > 0;
            trimYField.guiActiveEditor = trimRangeYP + trimRangeYN > 0;
            UI_FloatRange trimYRange = (UI_FloatRange) (state == StartState.Editor ? trimYField.uiControlEditor : trimYField.uiControlFlight);
            trimYRange.minValue = -trimRangeYN;
            trimYRange.maxValue = trimRangeYP;
            trimYRange.stepIncrement = trimRangeXN + trimRangeYN >= 10f ? 1f : trimRangeXN + trimRangeYN >= 5f ? 0.5f : 0.25f;
        }

        /// <summary>Update</summary>
        public void Update()
        {
            if (gimbal == null)
                return;

            if (HighLogic.LoadedSceneIsEditor && Time.time > lastEditorTime + TimeWarp.fixedDeltaTime)
            {
                lastEditorTime = Time.time;
                DoTrim(EditorLogic.RootPart.transform, EditorMarker_CoM.CraftCoM);
                int count = originalInitalRots.Count;
                for (int i = 0; i < count; i++)
                {
                    gimbal.gimbalTransforms[i].localRotation = gimbal.initRots[i];
                }
            }
        }

        /// <summary>FixedUpdate</summary>
        public void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsEditor || gimbal == null)
                return;
           
            DoTrim(vessel.ReferenceTransform, vessel.CurrentCoM);
        }

        private void DoTrim(Transform vesselTransform, Vector3 CoM)
        {
            Vector3 localCoM = vesselTransform.transform.InverseTransformPoint(CoM);

            int count = originalInitalRots.Count;
            for (int i = 0; i < count; i++)
            {
                currentTrim.x = enableTrim ? trimX : 0;
                currentTrim.y = enableTrim ? trimY : 0;

                Transform gimbalTransform = gimbal.gimbalTransforms[i];

                Vector3 localPos = vesselTransform.InverseTransformPoint(gimbalTransform.position);
                float sign = 1f;
                if (localCoM.y < localPos.y)
                {
                    sign = -1f;
                }

                Vector3 localTrim = sign * currentTrim;

                if (useTrimResponseSpeed)
                {
                    float timeFactor = trimResponseSpeed * TimeWarp.fixedDeltaTime;
                    localTrim.x = Mathf.Lerp(oldLocalTrim[i].x, localTrim.x, timeFactor);
                    localTrim.y = Mathf.Lerp(oldLocalTrim[i].y, localTrim.y, timeFactor);
                    oldLocalTrim[i] = localTrim;
                }

                //print(localPos.ToString("F2") + " " + currentTrim.ToString("F2") + "  " + localTrim.ToString("F2") + " " + sign.ToString("F0"));

                gimbal.initRots[i] = originalInitalRots[i] * Quaternion.AngleAxis(localTrim.x, Vector3.right) * Quaternion.AngleAxis(localTrim.y, Vector3.up);

                if (limitToGimbalRange)
                {
                    gimbal.gimbalRangeXP = trimRangeXP - localTrim.x;
                    gimbal.gimbalRangeXN = trimRangeXN + localTrim.x;
                    gimbal.gimbalRangeYP = trimRangeYP - localTrim.y;
                    gimbal.gimbalRangeYN = trimRangeYN + localTrim.y;
                }
            }
        }

        public new static void print(object message)
        {
            MonoBehaviour.print("[GimbalTrim] " + message);
        }
    }
}