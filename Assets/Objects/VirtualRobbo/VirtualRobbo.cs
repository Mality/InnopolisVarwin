﻿using UnityEngine;
using Varwin.Public;
using System.Threading;

namespace Varwin.Types.VirtualRobbo_d948cb30690c4f29936b5f7625e2487f
{
    [Locale(SystemLanguage.English,"Virtual ROBBO")]
    [Locale(SystemLanguage.Russian,"Виртуальный РОББО")]
    public class VirtualRobbo : RobboVarwin.Robot
    {
        private void UpdateWheels()
        {
            UpdateWheelVelocity(LeftWheel, MaximumTorgue * _LeftMotor_Percentage / 100F * _LeftMotor_Direction);
            UpdateWheelVelocity(RightWheel, MaximumTorgue * _RightMotor_Percentage / 100F * _RightMotorDirection);
        }

        private void UpdateWheelVelocity(HingeJoint joint, float velocity)
        {
            var motor = joint.motor;
            motor.targetVelocity = velocity;
            joint.motor = motor;
        }

        private float _LeftMotor_Percentage = 30;
        public float LeftMotor_Percentage
        {
            get
            {
                return _LeftMotor_Percentage;
            }
            set
            {
                _LeftMotor_Percentage = value;
                UpdateWheelVelocity(LeftWheel, MaximumTorgue * _LeftMotor_Percentage / 100F * _LeftMotor_Direction);
            }
        }

        private float _RightMotor_Percentage = 30;
        public float RightMotor_Percentage
        {
            get
            {
                return _RightMotor_Percentage;
            }
            set
            {
                _RightMotor_Percentage = value;
                UpdateWheelVelocity(RightWheel, MaximumTorgue * _RightMotor_Percentage / 100F * _RightMotorDirection);
            }
        }

        private float _LeftMotor_Direction = 1;
        public float LeftMotor_Direction
        {
            get
            {
                return _LeftMotor_Direction;
            }
            set
            {
                _LeftMotor_Direction = value;
                UpdateWheelVelocity(LeftWheel, MaximumTorgue * _LeftMotor_Percentage / 100F * _LeftMotor_Direction);
            }
        }

        private float _RightMotorDirection = 1;
        private float RightMotor_Direction
        {
            get
            {
                return _RightMotorDirection;
            }
            set
            {
                _RightMotorDirection = value;
                UpdateWheelVelocity(RightWheel, MaximumTorgue * _RightMotor_Percentage / 100F * _RightMotorDirection);
            }
        }

        private float MaximumTorgue = 700F;

        public HingeJoint LeftWheel;
        public HingeJoint RightWheel;

        public VirtualWheelEncoder LeftEncoder;
        public VirtualWheelEncoder RightEncoder;

        void Start()
        {
            LeftMotor_Direction = 1;
            RightMotor_Direction = 1;
            LeftMotor_Percentage = 30;
            RightMotor_Percentage = 30;
        }

        public override void MotorsOff()
        {
            LeftWheel.useMotor = false;
            RightWheel.useMotor = false;
        }

        public override void MotorsOn()
        {
            LeftWheel.useMotor = true;
            RightWheel.useMotor = true;
        }

        public override void MotorsOnForSeconds(float seconds)
        {
            MotorsOn();
            Thread.Sleep(Mathf.RoundToInt(seconds * 1000F));
            MotorsOff();
        }

        private void SingleStep()
        {
            var leftSteps = LeftEncoder.Steps;
            var rightSteps = RightEncoder.Steps;

            MotorsOn();

            while (true)
            {
                if((LeftEncoder.Steps - leftSteps) >= 1)
                {
                    LeftWheel.useMotor = false;
                }
                if((RightEncoder.Steps - rightSteps) >= 1)
                {
                    RightWheel.useMotor = false;
                }

                if((LeftEncoder.Steps - leftSteps) >= 1 && (RightEncoder.Steps - rightSteps) >= 1)
                {
                    break;
                }
            }

            MotorsOff();
        }

        public override void MotorsOnForSteps(float steps)
        {
            for(int i = 0; i < steps; i ++)
            {
                SingleStep();
            }
        }

        public override void ResetTripMeters()
        {
            LeftEncoder.Steps = 0;
            RightEncoder.Steps = 0;
        }

        private float DirectionToFloat(string direction)
        {
            switch (direction)
            {
                case RobboVarwin.Values.MotorDirection_Forward:
                    return 1F;
                case RobboVarwin.Values.MotorDirection_Backward:
                    return -1F;
                default:
                    return 1F;
            }
        }

        public override void SetMotors(string leftDirection, string rightDirection, float leftPercentage, float rightPercentage)
        {
            LeftMotor_Direction = DirectionToFloat(leftDirection);
            RightMotor_Direction = DirectionToFloat(rightDirection);

            LeftMotor_Percentage = leftPercentage;
            RightMotor_Percentage = rightPercentage;
        }

        public override void SetMotorsPower(float percentage)
        {
            LeftMotor_Percentage = RightMotor_Percentage = Mathf.Abs(percentage);
        }

        public override void SetMotorsPower(float left, float right)
        {
            LeftMotor_Percentage = Mathf.Abs(left);
            RightMotor_Percentage = Mathf.Abs(right);
        }

        public override void SetRobotDirection(string direction)
        {
            switch (direction)
            {
                case RobboVarwin.Values.RobotDirection_Forward:
                    LeftMotor_Direction = RightMotor_Direction = 1F;
                    break;
                case RobboVarwin.Values.RobotDirection_Backward:
                    LeftMotor_Direction = RightMotor_Direction = -1F;
                    break;
                case RobboVarwin.Values.RobotDirection_TurnLeft:
                    LeftMotor_Direction = -1F;
                    RightMotor_Direction = 1F;
                    break;
                case RobboVarwin.Values.RobotDirection_TurnRight:
                    LeftMotor_Direction = 1F;
                    RightMotor_Direction = -1F;
                    break;
            }
        }

        public override void TurnLeftDegrees(float degrees)
        {
            
        }

        public override void TurnRightDegrees(float degrees)
        {
            
        }
    }
}
