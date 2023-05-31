using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Media3D;
using System.IO;

namespace EOD_WPF.Model
{
    /// <summary>
    /// Franka Emika Panda - Saxion Hogeschool 457710
    /// </summary>
    class Franka
    {
        private const string MODEL_PATHB = "Franka_B.STL";
        private const string MODEL_PATH1 = "Franka_J1.STL";
        private const string MODEL_PATH2 = "Franka_J2.STL";
        private const string MODEL_PATH3 = "Franka_J3.STL";
        private const string MODEL_PATH4 = "Franka_J4.STL";

        Model3DGroup RA = new Model3DGroup();
        public List<Joint> joints = null;

        string basePath = "";
        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F4;
        Transform3DGroup F5;
        Transform3DGroup F6;
        RotateTransform3D R;
        TranslateTransform3D T;
   
        public Franka(HelixViewport3D viewPort3d)
        {
            basePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Model\\";
            List<string> modelsNames = new List<string>() { MODEL_PATHB, MODEL_PATH1, MODEL_PATH2, MODEL_PATH3, MODEL_PATH4 };
            ModelVisual3D RoboticArm = new ModelVisual3D();
            RoboticArm.Content = Initialize_Environment(modelsNames);
            RoboticArm.Content.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90)); //Draai de hele robot
            viewPort3d.Children.Add(RoboticArm);
            execute_fk();
        }

        public void execute_fk()
        {
            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle };
            ForwardKinematics(angles);
        }

        public Vector3D ForwardKinematics(double[] angles)
        {
            //Base
            F1 = new Transform3DGroup();
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ), angles[0]), new Point3D(joints[0].rotPointX, joints[0].rotPointY, joints[0].rotPointZ));
            F1.Children.Add(R);

            //Joint1
            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(30, 110, 20);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[1].rotAxisX, joints[1].rotAxisY, joints[1].rotAxisZ), angles[1]), new Point3D(joints[1].rotPointX, joints[1].rotPointY, joints[1].rotPointZ));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);

            //Joint2
            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(-180, 0, 20);
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
            //F6 = new Transform3DGroup();
            //T = new TranslateTransform3D(0, 0, 0);
            //R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[5].rotAxisX, joints[5].rotAxisY, joints[5].rotAxisZ), angles[5]), new Point3D(joints[5].rotPointX, joints[5].rotPointY, joints[5].rotPointZ));
            //F6.Children.Add(T);
            //F6.Children.Add(R);
            //F6.Children.Add(F5);


            //Gripper Rotate

            //Gripper Claw

            joints[0].model.Transform = F1; 
            joints[1].model.Transform = F2; 
            joints[2].model.Transform = F3;
            joints[3].model.Transform = F4;
            joints[4].model.Transform = F5;
            //joints[5].model.Transform = F6;
            //joints[6].model.Transform = F7;
            //joints[7].model.Transform = F8;

            return new Vector3D(joints[4].model.Bounds.Location.X, joints[4].model.Bounds.Location.Y, joints[4].model.Bounds.Location.Z);

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

                joints[0].angleMin = -180;
                joints[0].angleMax = 180;
                joints[0].rotAxisX = 0;
                joints[0].rotAxisY = 0;
                joints[0].rotAxisZ = 0;
                joints[0].rotPointX = 0;
                joints[0].rotPointY = 0;
                joints[0].rotPointZ = 0;

                joints[1].angleMin = -180;
                joints[1].angleMax = 180;
                joints[1].rotAxisX = 0;
                joints[1].rotAxisY = 0;
                joints[1].rotAxisZ = 1;
                joints[1].rotPointX = 90;
                joints[1].rotPointY = 180;
                joints[1].rotPointZ = 0;

                joints[2].angleMin = -90;
                joints[2].angleMax = 90;
                joints[2].rotAxisX = 0;
                joints[2].rotAxisY = 1;
                joints[2].rotAxisZ = 0;
                joints[2].rotPointX = 0;
                joints[2].rotPointY = 0;
                joints[2].rotPointZ = 0;

                joints[3].angleMin = -180;
                joints[3].angleMax = 180;
                joints[3].rotAxisX = 1;
                joints[3].rotAxisY = 0;
                joints[3].rotAxisZ = 0;
                joints[3].rotPointX = 0;
                joints[3].rotPointY = 0;
                joints[3].rotPointZ = 0;

                joints[4].angleMin = -180;
                joints[4].angleMax = 180;
                joints[4].rotAxisX = 0;
                joints[4].rotAxisY = 1;
                joints[4].rotAxisZ = 0;
                joints[4].rotPointX = 0;
                joints[4].rotPointY = 0;
                joints[4].rotPointZ = 0;

                //joints[5].angleMin = -180;
                //joints[5].angleMax = 180;
                //joints[5].rotAxisX = 1;
                //joints[5].rotAxisY = 0;
                //joints[5].rotAxisZ = 0;
                //joints[5].rotPointX = 1405;
                //joints[5].rotPointY = 0;
                //joints[5].rotPointZ = 1765;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
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
