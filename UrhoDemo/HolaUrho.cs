using System;
using Urho;
using Urho.Actions;
using Urho.Gui;

namespace UrhoDemo
{
    public class HolaUrho : Urho.Application
    {
        public HolaUrho(ApplicationOptions options) : base(new ApplicationOptions(assetsFolder: "Data"))
        {
        }
    }
}
