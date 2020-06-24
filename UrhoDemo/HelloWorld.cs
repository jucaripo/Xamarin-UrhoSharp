using System;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;
using Urho.Gui;

namespace UrhoDemo
{
    public class HelloWorld : Urho.Application
    {
        public HelloWorld(ApplicationOptions options) : base(new ApplicationOptions(assetsFolder: "Data"))
        {
        }

        protected override async void Start()
        {
            base.Start();
            CreateText();
            await Create3DObject();
        }

        private void CreateText()
        {
            // Create Text Element
            var text = new Text()
            {
                Value = "UrhoSharp \n Comunidad Xamarin en español",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            text.SetColor(Color.Green);
            text.SetFont(font: ResourceCache.GetFont("Fonts/Anonymous Pro.ttf"), size: 30);
            // Add to UI Root
            UI.Root.AddChild(text);
        }

        private async Task Create3DObject()
        {
            // Scene
            var scene = new Scene();
            scene.CreateComponent<Octree>();

            // Node (Rotation and Position)
            Node node = scene.CreateChild();
            node.Position = new Vector3(0, 0, 5);
            node.Rotation = new Quaternion(10, 60, 10);
            node.SetScale(.5f);

            // Pyramid Model
            StaticModel modelObject = node.CreateComponent<StaticModel>();
            //modelObject.Model = ResourceCache.GetModel("Models/Pyramid.mdl");
            modelObject.Model = ResourceCache.GetModel("Models/Text.mdl");
            
            

            // Light
            Node light = scene.CreateChild(name: "light");
            light.SetDirection(new Vector3(0.4f, -0.5f, 0.3f));
            light.CreateComponent<Light>();

            // Camera
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            // Viewport
            var viewport = new Viewport(Context, scene, camera, null);
            Renderer.SetViewport(0, viewport);
            viewport.SetClearColor( Color.FromHex("#2d96da"));

            // Action
            await node.RunActionsAsync(
                new RepeatForever(new RotateBy(duration: 1,
                    deltaAngleX: 0, deltaAngleY: 190, deltaAngleZ: 0)));

        }
    }
}
