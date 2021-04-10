using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geometry.TwoPointsOnSphere
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();
        }


        #region Input Events
        private void Th0_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            t0 = th0.Value * 2 * Math.PI / th0.Maximum;
            DefineModel(MainModel3Dgroup);
        }

        private void Ph0_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            p0 = ph0.Value * 2 * Math.PI / ph0.Maximum;
            DefineModel(MainModel3Dgroup);
        }

        private void Th1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            t1 = th1.Value * 2 * Math.PI / th1.Maximum;
            DefineModel(MainModel3Dgroup);
        }

        private void Ph1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            p1 = ph1.Value * 2 * Math.PI / ph1.Maximum;
            DefineModel(MainModel3Dgroup);
        }

        private void ChBSphere_Checked(object sender, RoutedEventArgs e)
        {
            draw_sphere = true;
            DefineModel(MainModel3Dgroup);
        }

        private void ChBSphere_Unchecked(object sender, RoutedEventArgs e)
        {
            draw_sphere = false;
            DefineModel(MainModel3Dgroup);
        }

        private void AddRotationControl(RotationInfo ri)
        {
            var DockPanel = new DockPanel() { Tag = ri };
            var Label = new Label() { MinWidth = 30 };
            Label.Content = ri.Axis;
            var ctm = new ContextMenu();
            var rm = new MenuItem();
            rm.Header = "Remove";
            rm.Tag = DockPanel;
            rm.Click += RotationSlider_Remove;
            ctm.Items.Add(rm);
            DockPanel.ContextMenu = ctm;
            DockPanel.SetDock(Label, Dock.Left);
            var Slider = new Slider()
            {
                Maximum = 100,
                TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight,
                TickFrequency = 5,
                Focusable = false,
                IsSnapToTickEnabled = true,
                Tag = ri
            };
            Slider.ValueChanged += RotationSlider_ValueChanged;
            DockPanel.Children.Add(Label);
            DockPanel.Children.Add(Slider);
            Rotations.Add(ri);
            RotationsList.Children.Add(DockPanel);
        }

        private void RotationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var el = sender as Slider;
            var ri = el.Tag as RotationInfo;
            ri.Value = el.Value * 2 * Math.PI / el.Maximum;
            DefineModel(MainModel3Dgroup);
        }

        private void RotationSlider_Remove(object sender, EventArgs e)
        {
            var el = sender as MenuItem;
            var dp = el.Tag as DockPanel;
            var ri = dp.Tag as RotationInfo;
            Rotations.Remove(ri);
            RotationsList.Children.Remove(dp);
            DefineModel(MainModel3Dgroup);
        }

        private void RXAddAction_Click(object sender, RoutedEventArgs e) => AddRotationControl(new RotationInfo('X', 0));

        private void RYAddAction_Click(object sender, RoutedEventArgs e) => AddRotationControl(new RotationInfo('Y', 0));

        private void RZAddAction_Click(object sender, RoutedEventArgs e) => AddRotationControl(new RotationInfo('Z', 0));

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Button button = sender as Button;
                ContextMenu contextMenu = button.ContextMenu;
                contextMenu.PlacementTarget = button;
                contextMenu.IsOpen = true;
            }
            e.Handled = true;
        }

        #endregion

        #region 3D Mechanics                   
        private Model3DGroup MainModel3Dgroup = new Model3DGroup();

        // The camera.
        private PerspectiveCamera Camera;

        // The camera's current location.
        private double CameraPhi = Math.PI / 6.0;
        private double CameraTheta = Math.PI / 6.0;
        private double CameraR = 3.5;

        private const double CameraDPhi = 0.1;
        private const double CameraDTheta = 0.1;
        private const double CameraDR = 0.1;

        private void PositionCamera()
        {
            double y = CameraR * Math.Sin(CameraPhi);
            double hyp = CameraR * Math.Cos(CameraPhi);
            double x = hyp * Math.Cos(CameraTheta);
            double z = hyp * Math.Sin(CameraTheta);
            Camera.Position = new Point3D(x, y, z);
            Camera.LookDirection = new Vector3D(-x, -y, -z);
            Camera.UpDirection = new Vector3D(0, 1, 0);
        }

        private void DefineLights()
        {
            AmbientLight ambient_light = new AmbientLight(Colors.Gray);
            DirectionalLight directional_light =
                new DirectionalLight(Colors.Gray, new Vector3D(-1.0, -3.0, -2.0));
            MainModel3Dgroup.Children.Add(ambient_light);
            MainModel3Dgroup.Children.Add(directional_light);
        }

        #endregion


        #region Simulation Logic       

        public double t0 = 0, p0 = 0;
        public double t1 = 0, p1 = 0;
        bool draw_sphere = true;
        private List<RotationInfo> Rotations = new List<RotationInfo>();
        Func<double, Point3D> Trajectory;
        private double alpha = 0;

        double gamma0 = 0, gamma1 = 0;

        void GetAlpha()
        {
            var pt = new Polar(t1, p1).ToPoint3D().RotY(-t0).RotZ(-p0);
            double x = pt.X, y = pt.Y, z = pt.Z;
            alpha = -Math.Atan2(y, z);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            Camera = new PerspectiveCamera();
            Camera.FieldOfView = 60;
            MainViewport.Camera = Camera;
            PositionCamera();
            DefineModel(MainModel3Dgroup);

            ModelVisual3D model_visual = new ModelVisual3D();
            model_visual.Content = MainModel3Dgroup;

            // Display the main visual to the viewport.
            MainViewport.Children.Add(model_visual);

            Keyboard.Focus(this);
        }     

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    CameraPhi += CameraDPhi;
                    if (CameraPhi > Math.PI / 2.0) CameraPhi = Math.PI / 2.0;
                    break;
                case Key.Down:
                    CameraPhi -= CameraDPhi;
                    if (CameraPhi < -Math.PI / 2.0) CameraPhi = -Math.PI / 2.0;
                    break;
                case Key.Left:
                    CameraTheta += CameraDTheta;
                    break;
                case Key.Right:
                    CameraTheta -= CameraDTheta;
                    break;
                case Key.Add:
                case Key.OemPlus:
                    CameraR -= CameraDR;
                    if (CameraR < 3.5) CameraR = 3.5;
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    CameraR += CameraDR;
                    if (CameraR > 10) CameraR = 10;
                    break;
            }

            // Update the camera's position.
            PositionCamera();
        }

        void GetGammas()
        {
            var pt0 = new Polar(t0, p0).ToPoint3D().RotY(-t0).RotZ(-p0).RotX(-alpha);
            var pt1 = new Polar(t1, p1).ToPoint3D().RotY(-t0).RotZ(-p0).RotX(-alpha);
            double x = pt0.X, z = pt0.Z;
            gamma0 = Math.Atan2(z, x);
            x = pt1.X; z = pt1.Z;
            gamma1 = Math.Atan2(z, x);
        }

        private double delta_gamma
        {
            get
            {
                double lambda = gamma1 - gamma0, pi = Math.PI;
                var f = Math.Sign(lambda) * Math.Sign(pi - Math.Abs(lambda)) * Math.Min(Math.Abs(lambda), 2 * Math.PI - Math.Abs(lambda));
                return (f == 0) ? (p0 == p1 && t0 == t1) ? 0 : Math.PI : f;
            }
        }

        private void DefineModel(Model3DGroup model_group)
        {
            #region Clear the scene
            MainModel3Dgroup.Children.Clear();
            DefineLights();
            #endregion

            GetAlpha();
            GetGammas();

            DiffuseMaterial surface_material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(120, 255, 128, 0)));

            #region Draw Axes
            model_group.Children.Add(new GeometryModel3D(
                new Segment(new Point3D(-1.2, 0, 0), new Point3D(1.2, 0, 0), new Vector3D(0, 0.1, 0)).ToMesh(true),
                new DiffuseMaterial(Brushes.Blue))
                );

            model_group.Children.Add(new GeometryModel3D(
               new Segment(new Point3D(0, 0, -1.2), new Point3D(0, 0, 1.2), new Vector3D(0.1, 0.1, 0.1)).ToMesh(true),
               new DiffuseMaterial(Brushes.Green))
               );

            model_group.Children.Add(new GeometryModel3D(
               new Segment(new Point3D(0, -1.2, 0), new Point3D(0, 1.2, 0), new Vector3D(0.1, 0.1, 0.1)).ToMesh(true),
               new DiffuseMaterial(Brushes.Red))
               );
            #endregion

            #region Draw Sphere
            if (draw_sphere)
            {
                model_group.Children.Add(new GeometryModel3D(new Sphere(new Point3D(0, 0, 0), 1, 50, 50).ToMesh(), surface_material)
                { BackMaterial = surface_material });
            }
            #endregion

            #region Draw First Position      
            var computed_position_0 = new Polar(t0, p0).ToPoint3D().Rot(Rotations);
            model_group.Children.Add(new GeometryModel3D(
              new Segment(computed_position_0.Scale(0.9), new Point3D(0, 0, 0), new Vector3D(0, 0.1, 0)).ToMesh(true),
              new DiffuseMaterial(Brushes.Black))
              );
            model_group.Children.Add(new GeometryModel3D(new Sphere(computed_position_0, 0.05, 5, 5).ToMesh(), new DiffuseMaterial(Brushes.Fuchsia)));
            #endregion

            #region Draw Last Position

            var computed_position_1 = new Polar(t1, p1).ToPoint3D().Rot(Rotations);
            model_group.Children.Add(new GeometryModel3D(
             new Segment(computed_position_1.Scale(0.9), new Point3D(0, 0, 0), new Vector3D(0, 0.1, 0)).ToMesh(true),
             new DiffuseMaterial(Brushes.Black))
             );
            model_group.Children.Add(new GeometryModel3D(new Sphere(computed_position_1, 0.05, 5, 5).ToMesh(), new DiffuseMaterial(Brushes.Gainsboro)));
            #endregion

            #region Compute And Draw Trajectory
            Func<double, double> theta = t => gamma0 + t * delta_gamma;
            Func<double, double> phi = t => 0;
            Trajectory = t => new Point3D(Math.Cos(phi(t)) * Math.Cos(theta(t)), Math.Sin(phi(t)), Math.Cos(phi(t)) * Math.Sin(theta(t))).RotX(alpha).RotZ(p0).RotY(t0).Rot(Rotations);
            model_group.Children.Add(new GeometryModel3D(
                new Trajectory(Trajectory, 0, 1).ToMesh(),
                new DiffuseMaterial(new SolidColorBrush(Colors.Black))));
            #endregion         
        }

        #endregion
    }
}
