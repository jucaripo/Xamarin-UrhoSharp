using System;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;
using Urho.Gui;

namespace UrhoDemo
{
    /// <summary>
    /// Hello World  hereda de Urho.Application
    /// </summary>
    public class HelloWorld : Urho.Application
    {
        /// <summary>
        /// Constructor con Opciones de Aplicación 
        /// </summary>
        /// <param name="options">assetsFolder desde main</param>
        /// Base:  pasamos un options por default
        public HelloWorld(ApplicationOptions options) : base(new ApplicationOptions(assetsFolder: "Data"))
        {
        }

        /// <summary>
        /// Start  clase para iniciar nuestra urho aplicación
        /// </summary>
        protected override async void Start()
        {
            base.Start();
            CreateText(); // crear un texto en la vista  ver la función
            await Create3DObject();
        }

        /// <summary>
        /// función demo de como crear un texto en la vista de urhosharp
        /// </summary>
        private void CreateText()
        {
            // Create Text Element
            var text = new Text()
            {
                Value = "UrhoSharp \n Comunidad Xamarin en español",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            text.SetColor(Color.Green);
            text.SetFont(font: ResourceCache.GetFont("Fonts/Anonymous Pro.ttf"), size: 30);
            // Add to UI Root
            UI.Root.AddChild(text);
        }

        /// <summary>
        /// Funcion Create3DObject  que muestra como contruir una escena simple en Urho
        /// </summary>
        /// <returns></returns>
        private async Task Create3DObject()
        {
            // Crear la escena
            var scene = new Scene();
            scene.CreateComponent<Octree>();

            // Node (Rotation and Position) de la escena
            Node node = scene.CreateChild();
            node.Position = new Vector3(0, 0, 5);
            node.Rotation = new Quaternion(10, 60, 10);
            node.SetScale(.5f);

            // Modelo traer la geometria de un objeto para su uso en el motor 3d
            StaticModel modelObject = node.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel("Models/Text.mdl");
            
            

            // Luces crear una nueva luz
            Node light = scene.CreateChild(name: "light");
            light.SetDirection(new Vector3(0.4f, -0.5f, 0.3f));
            light.CreateComponent<Light>();

            // Camara crear la camara de la escena
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            // Viewport  unir los elementos a la vista
            var viewport = new Viewport( scene, camera, null);
            Renderer.SetViewport(0, viewport);
            viewport.SetClearColor( Color.FromHex("#2d96da"));

            // Action
            await node.RunActionsAsync(
                new RepeatForever(new RotateBy(duration: 1,
                    deltaAngleX: 0, deltaAngleY: 190, deltaAngleZ: 0)));

        }
    }
}
