using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SmartB.Core.Extensions;
using SmartB.Core.Models;
namespace SmartB.Core.Utility
{
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;
        public static Angajati User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(Angajati));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }
    }
}
