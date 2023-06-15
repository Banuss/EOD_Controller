using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Media3D;
using System.IO;
using System.Windows.Controls;
using System.Windows.Ink;

namespace EOD_WPF.Model
{
    /// <summary>
    /// Franka Emika Panda - Saxion Hogeschool 457710
    /// </summary>
    class Franka
    {
        private const string MODEL_PATHB = "FRANKA_BASE.STL";
        private const string MODEL_PATH1 = "FRANKA_J1.STL";
        private const string MODEL_PATH2 = "FRANKA_J2.STL";
        private const string MODEL_PATH3 = "FRANKA_J3.STL";
        private const string MODEL_PATH4 = "FRANKA_J4.STL";
        private const string MODEL_PATH5 = "FRANKA_J5.STL";
        private const string MODEL_PATH6 = "FRANKA_J6.STL";
        private const string MODEL_PATH7 = "FRANKA_J7.STL";
        private const string MODEL_PATHG = "FRANKA_G.STL";

        Model3DGroup RA = new Model3DGroup();
        Model3D newLocCircle = null; 
        public List<Joint> joints = null;

        string basePath = "";
        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F4;
        Transform3DGroup F5;
        Transform3DGroup F6;
        Transform3DGroup F7;
        Transform3DGroup F8;
        Transform3DGroup G;
        RotateTransform3D R;
        TranslateTransform3D T;

        public Vector3D reachingPoint;
        public Vector3D headPoint;

        double LearningRate = 0.01;
        double SamplingDistance = 0.15;
        double DistanceThreshold = 20;

        public Franka(HelixViewport3D viewPort3d)
        {
            basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Model\\";
            List<string> modelsNames = new List<string>() { MODEL_PATHB, MODEL_PATH1, MODEL_PATH2, MODEL_PATH3, MODEL_PATH4, MODEL_PATH5, MODEL_PATH6, MODEL_PATH7 };
            ModelVisual3D RoboticArm = new ModelVisual3D();

            var builder = new MeshBuilder(true, true);
            builder.AddSphere(new Point3D(0, 0, 0), 50, 15, 15);
            newLocCircle = new GeometryModel3D(builder.ToMesh(), Materials.Yellow);
            var NewLocation = new ModelVisual3D();
            NewLocation.Content = newLocCircle;

            RoboticArm.Content = Initialize_Environment(modelsNames);
            viewPort3d.Children.Add(RoboticArm);
            viewPort3d.Children.Add(NewLocation);
            execute_fk();
        }

        public void execute_fk()
        {
            double[] angles = {
                joints[0].angle,
                joints[1].angle,
                joints[2].angle,
                joints[3].angle,
                joints[4].angle,
                joints[5].angle,
                joints[6].angle,
                joints[7].angle};
            ForwardKinematics(angles);
        }

        public void execute_ik(Vector3D reachingPoint)
        {
            newLocCircle.Transform = new TranslateTransform3D(reachingPoint);
            this.reachingPoint = reachingPoint;
            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle, joints[6].angle, joints[7].angle };
            angles = InverseKinematics(reachingPoint, angles);
            joints[0].angle = angles[0];
            joints[1].angle = angles[1];
            joints[2].angle = angles[2];
            joints[3].angle = angles[3];
            joints[4].angle = angles[4];
            joints[5].angle = angles[5];
            joints[6].angle = angles[6];
            joints[7].angle = angles[7];
        }

        public Vector3D ForwardKinematics(double[] angles)
        {
            //Base
            F1 = new Transform3DGroup();
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ), angles[0]), new Point3D(joints[0].rotPointX, joints[0].rotPointY, joints[0].rotPointZ));
            F1.Children.Add(R);

            //Joint1
            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[1].rotAxisX, joints[1].rotAxisY, joints[1].rotAxisZ), angles[1]), new Point3D(joints[1].rotPointX, joints[1].rotPointY, joints[1].rotPointZ));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);

            //Joint2
            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[2].rotAxisX, joints[2].rotAxisY, joints[2].rotAxisZ), angles[2]), new Point3D(joints[2].rotPointX, joints[2].rotPointY, joints[2].rotPointZ));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);

            //Joint3
            F4 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0); 
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[3].rotAxisX, joints[3].rotAxisY, joints[3].rotAxisZ), angles[3]), new Point3D(joints[3].rotPointX, joints[3].rotPointY, joints[3].rotPointZ));
            F4.Children.Add(T);
            F4.Children.Add(R);
            F4.Children.Add(F3);

            //Joint4
            F5 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[4].rotAxisX, joints[4].rotAxisY, joints[4].rotAxisZ), angles[4]), new Point3D(joints[4].rotPointX, joints[4].rotPointY, joints[4].rotPointZ));
            F5.Children.Add(T);
            F5.Children.Add(R);
            F5.Children.Add(F4);

            //Joint5
            F6 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[5].rotAxisX, joints[5].rotAxisY, joints[5].rotAxisZ), angles[5]), new Point3D(joints[5].rotPointX, joints[5].rotPointY, joints[5].rotPointZ));
            F6.Children.Add(T);
            F6.Children.Add(R);
            F6.Children.Add(F5);

            //Joint6
            F7 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[6].rotAxisX, joints[6].rotAxisY, joints[6].rotAxisZ), angles[6]), new Point3D(joints[6].rotPointX, joints[6].rotPointY, joints[6].rotPointZ));
            F7.Children.Add(T);
            F7.Children.Add(R);
            F7.Children.Add(F6);

            //Joint7
            F8 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[7].rotAxisX, joints[7].rotAxisY, joints[7].rotAxisZ), angles[7]), new Point3D(joints[7].rotPointX, joints[7].rotPointY, joints[7].rotPointZ));
            F8.Children.Add(T);
            F8.Children.Add(R);
            F8.Children.Add(F7);

            //Gripper Rotate

            //Gripper Claw

            joints[0].model.Transform = F1; 
            joints[1].model.Transform = F2; 
            joints[2].model.Transform = F3;
            joints[3].model.Transform = F4;
            joints[4].model.Transform = F5;
            joints[5].model.Transform = F6;
            joints[6].model.Transform = F7;
            joints[7].model.Transform = F8;

            var goal =  new Vector3D(joints[7].model.Bounds.Location.X, joints[7].model.Bounds.Location.Y, joints[7].model.Bounds.Location.Z);
            headPoint = goal;
            return goal;


        }

        private Model3DGroup Initialize_Environment(List<string> modelsNames)
        {
            try
            {
                ModelImporter import = new ModelImporter();
                joints = new List<Joint>();

                foreach (string modelName in modelsNames)
                {
                    var materialGroup = new MaterialGroup();
                    Color mainColor = Colors.White;
                    materialGroup.Children.Add(new EmissiveMaterial(new SolidColorBrush(mainColor)));
                    materialGroup.Children.Add(new DiffuseMaterial(new SolidColorBrush(mainColor)));
                    materialGroup.Children.Add(new SpecularMaterial(new SolidColorBrush(mainColor), 200));
                    var link = import.Load(basePath + modelName);
                    GeometryModel3D model = link.Children[0] as GeometryModel3D;
                    model.Material = materialGroup;
                    model.BackMaterial = materialGroup;
                    joints.Add(new Joint(link));
                }

                RA.Children.Add(joints[0].model);
                RA.Children.Add(joints[1].model);
                RA.Children.Add(joints[2].model);
                RA.Children.Add(joints[3].model);
                RA.Children.Add(joints[4].model);
                RA.Children.Add(joints[5].model);
                RA.Children.Add(joints[6].model);
                RA.Children.Add(joints[7].model);

                //base
                joints[0].rotAxisX = 0;
                joints[0].rotAxisY = 0;
                joints[0].rotAxisZ = 0;
                joints[0].rotPointX = 0;
                joints[0].rotPointY = 0;
                joints[0].rotPointZ = 0;

                joints[1].angleMax = RadToDeg(2.8973);
                joints[1].angleMin = RadToDeg(-2.8973);
                joints[1].rotAxisX = 0;
                joints[1].rotAxisY = 0;
                joints[1].rotAxisZ = 1;
                joints[1].rotPointX = 0;
                joints[1].rotPointY = 0;
                joints[1].rotPointZ = 333;

                joints[2].angleMax = RadToDeg(1.7628);
                joints[2].angleMin = RadToDeg(-1.7628);
                joints[2].rotAxisX = 1;
                joints[2].rotAxisY = 0;
                joints[2].rotAxisZ = 0;
                joints[2].rotPointX = 0;
                joints[2].rotPointY = 0;
                joints[2].rotPointZ = 333;

                joints[3].angleMax = RadToDeg(2.8973);
                joints[3].angleMin = RadToDeg(-2.8973);
                joints[3].rotAxisX = 0;
                joints[3].rotAxisY = 0;
                joints[3].rotAxisZ = 1;
                joints[3].rotPointX = 0;
                joints[3].rotPointY = 0;
                joints[3].rotPointZ = 649;

                joints[4].angleMax = RadToDeg(-0.0698);
                joints[4].angleMin = RadToDeg(-3.0718);
                joints[4].rotAxisX = 1;
                joints[4].rotAxisY = 0;
                joints[4].rotAxisZ = 0;
                joints[4].rotPointX = 0;
                joints[4].rotPointY = 83;
                joints[4].rotPointZ = 649;

                joints[5].angleMax = RadToDeg(2.8973);
                joints[5].angleMin = RadToDeg(-2.8973);
                joints[5].rotAxisX = 0;
                joints[5].rotAxisY = 0;
                joints[5].rotAxisZ = 1;
                joints[5].rotPointX = 0;
                joints[5].rotPointY = 0;
                joints[5].rotPointZ = 1033;

                joints[6].angleMax = RadToDeg(3.7525);
                joints[6].angleMin = RadToDeg(-0.0175);
                joints[6].rotAxisX = 1;
                joints[6].rotAxisY = 0;
                joints[6].rotAxisZ = 0;
                joints[6].rotPointX = 0;
                joints[6].rotPointY = 0;
                joints[6].rotPointZ = 1033;

                joints[7].angleMax = RadToDeg(2.8973);
                joints[7].angleMin = RadToDeg(-2.8973);
                joints[7].rotAxisX = 0;
                joints[7].rotAxisY = 0;
                joints[7].rotAxisZ = 1;
                joints[7].rotPointX = 0;
                joints[7].rotPointY = 88;
                joints[7].rotPointZ = 926;

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
        }


        public double[] InverseKinematics(Vector3D target, double[] angles)
        {
            //Check if location is location
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
            {
                return angles;
            }

            double[] oldAngles = new double[8];
            angles.CopyTo(oldAngles, 0);
            for (int i = 0; i <= 8; i++)
            {
                double gradient = PartialGradient(target, angles, i);
                angles[i] -= LearningRate * gradient;
                angles[i] = Clamp(angles[i], joints[i].angleMin, joints[i].angleMax);
                if (DistanceFromTarget(target, angles) < DistanceThreshold || checkAngles(oldAngles, angles))
                {
                    return angles;
                }
            }
            return angles;
        }

        public double DistanceFromTarget(Vector3D target, double[] angles)
        {
            Vector3D point = ForwardKinematics(angles);
            return Math.Sqrt(Math.Pow((point.X - target.X), 2.0) + Math.Pow((point.Y - target.Y), 2.0) + Math.Pow((point.Z - target.Z), 2.0));
        }

        public double PartialGradient(Vector3D target, double[] angles, int i)
        {
            double angle = angles[i];
            double f_x = DistanceFromTarget(target, angles);
            angles[i] += SamplingDistance;
            double f_x_plus_d = DistanceFromTarget(target, angles);
            double gradient = (f_x_plus_d - f_x) / SamplingDistance;
            angles[i] = angle;
            return gradient;
        }

        public bool checkAngles(double[] oldAngles, double[] angles)
        {
            for (int i = 0; i <= angles.Length-1; i++)
            {
                if (oldAngles[i] != angles[i])
                    return false;
            }
            return true;
        }

        public static T Clamp<T>(T value, T min, T max)
            where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        public int RadToDeg(double rad)
        {
            return (int)(rad * 180 / double.Pi);
        }
    }

   

    class Joint
    {
        public Model3D model = null;
        public double angle = 0;
        public double angleMin = -180;
        public double angleMax = 180;
        public int rotPointX = 0;
        public int rotPointY = 0;
        public int rotPointZ = 0;
        public int rotAxisX = 0;
        public int rotAxisY = 0;
        public int rotAxisZ = 0;

        public Joint(Model3D pModel)
        {
            model = pModel;
        }
    }
}
