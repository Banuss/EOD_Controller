using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Media3D;
using System.IO;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Linq;
using System.Windows.Documents;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private const string MODEL_PATHG1 = "FRANKA_G1.STL";
        private const string MODEL_PATHG2 = "FRANKA_G2.STL";
        private const string TIP = "TIP.STL";

        int jointcount = 10;

        Model3DGroup RA = new Model3DGroup();
        Model3D newLocCircle = null; 

        public List<Joint> joints = null;
        List<Transform3DGroup> tfg = null;

        string basePath = "";

        public Vector3D reachingPoint;
        public Vector3D headPoint;

        double LearningRate = 0.1;
        double SamplingDistance = 0.15;
        double DistanceThreshold = 10;

        public Franka(HelixViewport3D viewPort3d)
        {
            basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Model\\";
            List<string> modelsNames = new List<string>() { MODEL_PATHB, MODEL_PATH1, MODEL_PATH2, MODEL_PATH3, MODEL_PATH4, MODEL_PATH5, MODEL_PATH6, MODEL_PATH7, MODEL_PATHG1, MODEL_PATHG2};
            ModelVisual3D RoboticArm = new ModelVisual3D();

            var builder = new MeshBuilder(true, true);
            builder.AddSphere(new Point3D(0, 0, 0), 10);
            newLocCircle = new GeometryModel3D(builder.ToMesh(), Materials.Yellow);
            var NewLocation = new ModelVisual3D();
            NewLocation.Content = newLocCircle;
            RoboticArm.Content = Initialize_Environment(modelsNames);
            viewPort3d.Children.Add(RoboticArm);
            viewPort3d.Children.Add(NewLocation);
            tfg = new List<Transform3DGroup>(jointcount);
            //init lists
            execute_fk();
        }

        public void execute_fk()
        {
            ForwardKinematics(joints.Select(x => x.angle).ToArray());
        }

        public void updatecircle(Vector3D reachingPoint)
        {
            newLocCircle.Transform = new TranslateTransform3D(reachingPoint);
            this.reachingPoint = reachingPoint;
        }

        public Vector3D ForwardKinematics(double[] angles)
        {
            tfg.Clear();
            for (int i = 0; i < jointcount; i++)
            {
                Transform3DGroup tf = new Transform3DGroup();
                RotateTransform3D R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[i].rotAxisX, joints[i].rotAxisY, joints[i].rotAxisZ), angles[i]), new Point3D(joints[i].rotPointX, joints[i].rotPointY, joints[i].rotPointZ));
                tf.Children.Add(R);
                if (i > 0)
                {
                    tf.Children.Add(tfg.ElementAt(i-1));
                }
                joints[i].model.Transform = tf;
                tfg.Add(tf);
            }
          // MessageBox.Show(joints.Last().model.Bounds.Location.);
            //headPoint =  new Vector3D(joints.Last().model.Bounds.Location.X, joints.Last().model.Bounds.Location.Y, joints.Last().model.Bounds.Location.Z);
            headPoint = new Vector3D(joints.Last().model.Bounds.Location.X +73, joints.Last().model.Bounds.Location.Y+94, joints.Last().model.Bounds.Location.Z);

            return headPoint;
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

                joints.ForEach(x => RA.Children.Add(x.model));

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
                joints[3].enable = false; //cant use this to simulate 6DOF

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
                //joints[7].rotPointZ = 926;
                joints[7].rotPointZ = 1033;

                joints[9].angleMax = RadToDeg(0.5);
                joints[9].angleMin = RadToDeg(-0.5);
                joints[9].rotAxisX = 0;
                joints[9].rotAxisY = 0;
                joints[9].rotAxisZ = 1;
                joints[9].rotPointX = 0;
                joints[9].rotPointY = 88;
                joints[9].rotPointZ = 691;

             


            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
        }


        public void InverseKinematics()
        {
            var target = reachingPoint;
            var angles = joints.Select(x => x.angle).ToArray();
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
                return;
            for (int i = 0; i < jointcount; i++)
            {
                if (joints[i].enable)
                {
                    double gradient = PartialGradient(target, angles, i);
                    angles[i] -= LearningRate * gradient;
                    if (DistanceFromTarget(target, angles) < DistanceThreshold)
                        return;
                }
            }
            joints.ForEach(x => x.angle = angles[joints.IndexOf(x)]);
            
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


        public double DistanceFromTarget(Vector3D target, double[] angles)
        {
            Vector3D point = ForwardKinematics (angles);      
            var ans = Math.Sqrt(Math.Pow((point.X - target.X), 2.0) + Math.Pow((point.Y - target.Y), 2.0) + Math.Pow((point.Z - target.Z), 2.0));
            return ans;
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
        public bool enable = true;

        public Joint(Model3D pModel)
        {
            model = pModel;
        }
    }
}
