using System;
using Core.Services.CameraTransition.Configuration;
using Core.Services.GameFactory;
using Core.Services.ResourceProvider;
using Core.Services.SoundService;
using Core.Services.WindowAnimation;
using Game.Player;
using UI.Windows.HUDWindow;

namespace Shared.Constants
{
    public static class ResourceNames
    {
        private static ResourceInfo HUDView = new(typeof(HudView), "UI/Hud/HudView");
        
        private static ResourceInfo WindowSettings = new(typeof(WindowAnimationSettings), "Configurations/Window Animation Settings");
        private static ResourceInfo PlayerConfig = new(typeof(PlayerConfiguration), "Configurations/Player/Player Configuration");
        private static ResourceInfo CameraProfiles = new(typeof(CameraConfiguration), "Configurations/Camera Profiles");
        private static ResourceInfo LayerMaskConfig = new(typeof(LayerMaskConfiguration), "Configurations/LayerMask Configuration");
        private static ResourceInfo SoundConfigurations = new(typeof(SoundConfiguration), "Configurations/Sound Configurations");

        private static ResourceInfo Player = new(typeof(GameCharacter), "Prefabs/Player/GameCharacter");
        
            
        private static readonly ResourceInfo[] Resources =
        {
            // Windows
            HUDView,
            
            // Configurations
            WindowSettings,
            PlayerConfig,
            CameraProfiles,
            LayerMaskConfig,
            SoundConfigurations,
            
            // Characters
            Player,
        };
        
        public static string GetPath<TResource>(bool baseType = false) where TResource : IResource
        {
            var path = string.Empty;
            foreach (var resource in Resources)
            {
                var typeToSearch = resource.Type;
                
                if(typeToSearch == typeof(TResource))
                {
                    path = resource.Path;
                }
                
                if (!baseType) continue;
                if (typeToSearch.IsSubclassOf(typeof(TResource)))
                {
                    path = resource.Path;
                }
                
            }

            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException($"The is no path for resource with type {typeof(TResource)}.");

            return path;
        }
        
        public static string GetPath(Type type, bool baseType = false)
        {
            var path = string.Empty;
            foreach (var resource in Resources)
            {
                var typeToSearch = resource.Type;
                
                if(typeToSearch == type)
                {
                    path = resource.Path;
                }
                
                if (!baseType) continue;
                if (typeToSearch.IsSubclassOf(type))
                {
                    path = resource.Path;
                }
                
            }

            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException($"The is no path for resource with type {type}.");

            return path;
        }
    }
}