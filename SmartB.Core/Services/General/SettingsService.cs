using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SmartB.Core.Contracts.Services.General;
namespace SmartB.Core.Services.General
{
    public class SettingsService : ISettingsService
    {
        private const string CommessaBarcode = "CommessaBarcode";
        private const string Counter = "Counter";
        private const string EfficiencyIds = "EfficiciencyIds";
        private const string H10 = "H10";
        private const string H10Efficiency = "H10Efficiency";
        private const string H11 = "H11";
        private const string H11Efficiency = "H11Efficiency";
        private const string H12 = "H12";
        private const string H12Efficiency = "H12Efficiency";
        private const string H13 = "H13";
        private const string H13Efficiency = "H13Efficiency";
        private const string H14 = "H14";
        private const string H14Efficiency = "H14Efficiency";
        private const string H15 = "15";
        private const string H15Efficiency = "H15Efficiency";
        private const string H16 = "H16";
        private const string H16Efficiency = "H16Efficiency";
        private const string H17 = "H17";
        private const string H17Efficiency = "H17Efficiency";
        private const string H18 = "H18";
        private const string H18Efficiency = "H18Efficiency";
        private const string H19 = "H19";
        private const string H19Efficiency = "H19Efficiency";
        private const string H20 = "H20";
        private const string H20Efficiency = "H20Efficiency";
        private const string H21 = "H21";
        private const string H21Efficiency = "H21Efficiency";
        private const string H22 = "H22";
        private const string H22Efficiency = "H22Efficiency";
        private const string H23 = "H23";
        private const string H23Efficiency = "H23Efficiency";
        private const string H6 = "H6";
        private const string H6Efficiency = "H6Efficiency";
        private const string H7 = "H7";
        private const string H7Efficiency = "H7Efficiency";
        private const string H8 = "H8";
        private const string H8Efficiency = "H8Efficiency";
        private const string H9 = "H9";
        private const string H9Efficiency = "H9Efficiency";
        private const string IsNormCalculated = "IsNormCalculated";
        private const string JobId = "JobId";
        private const string JobsIds = "JobsIds";

        private const string JobNorm = "JobNorm";
        private const string LastClick = "LastClick";
        private const string MachineCode = "MachineCode";
        private const string MachineId = "MachineId";
        private const string MachineLine = "MachineLine";
        private const string MachineName = "MachineName";
        private const string OneClickWorth = "OneClickWorth";
        private const string SelectedPhase = "SelectedPhase";
        private const string TotalEfficiency = "TotalEfficiency";
        private const string UserId = "UserId";
        private const string UserLine = "UserLine";
        private const string UserLoginDate = "UserPassword";
        private const string UserName = "UserName";
        private const string UserSector = "UserSector";
        private const string DeviceId = "DeviceId";
        private const string TotalPieces = "TotalPieces";
        private const string GoodPieces = "GoodPieces";
        private const string BadPieces = "BadPieces";
        private readonly ISettings _settings;
        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }
        public string JobsIdSettings
        {
            get => GetItem(JobsIds);
            set => AddItem(JobsIds, value);
        }
        public string CommessaFromBarcode
        {
            get => GetItem(CommessaBarcode);
            set => AddItem(CommessaBarcode, value);
        }
        public string CounterSettings
        {
            get => GetItem(Counter);
            set => AddItem(Counter, value);
        }
        public string EfficiencyIdsSettings
        {
            get => GetItem(EfficiencyIds);
            set => AddItem(EfficiencyIds, value);
        }
        public string DeviceIdSettings
        {
            get=>GetItem(DeviceId);
            set => AddItem(DeviceId, value);
        }
        public string BadPiecesSettings
        {
            get => GetItem(BadPieces);
            set => AddItem(BadPieces, value);
        }
        public string GoodPiecesSettings
        {
            get => GetItem(GoodPieces);
            set => AddItem(GoodPieces, value);
        }
        public string TotalPiecesSettings
        {
            get => GetItem(TotalPieces);
            set => AddItem(TotalPieces, value);
        }
        public string H10EfficiencySettings
        {
            get => GetItem(H10Efficiency);
            set => AddItem(H10Efficiency, value);
        }
        public string H10Settings
        {
            get => GetItem(H10);
            set => AddItem(H10, value);
        }
        public string H11EfficiencySettings
        {
            get => GetItem(H11Efficiency);
            set => AddItem(H11Efficiency, value);
        }
        public string H11Settings
        {
            get => GetItem(H11);
            set => AddItem(H11, value);
        }
        public string H12EfficiencySettings
        {
            get => GetItem(H12Efficiency);
            set => AddItem(H12Efficiency, value);
        }
        public string H12Settings
        {
            get => GetItem(H12);
            set => AddItem(H12, value);
        }
        public string H13EfficiencySettings
        {
            get => GetItem(H13Efficiency);
            set => AddItem(H13Efficiency, value);
        }
        public string H13Settings
        {
            get => GetItem(H13);
            set => AddItem(H13, value);
        }
        public string H14EfficiencySettings
        {
            get => GetItem(H14Efficiency);
            set => AddItem(H15Efficiency, value);
        }
        public string H14Settings
        {
            get => GetItem(H14);
            set => AddItem(H14, value);
        }
        public string H15EfficiencySettings
        {
            get => GetItem(H15Efficiency);
            set => AddItem(H15Efficiency, value);
        }
        public string H15Settings
        {
            get => GetItem(H15);
            set => AddItem(H15, value);
        }
        public string H16EfficiencySettings
        {
            get => GetItem(H16Efficiency);
            set => AddItem(H16Efficiency, value);
        }
        public string H16Settings
        {
            get => GetItem(H16);
            set => AddItem(H16, value);
        }
        public string H17EfficiencySettings
        {
            get => GetItem(H17Efficiency);
            set => AddItem(H17Efficiency, value);
        }
        public string H17Settings
        {
            get => GetItem(H17);
            set => AddItem(H17, value);
        }
        public string H18EfficiencySettings
        {
            get => GetItem(H18Efficiency);
            set => AddItem(H18Efficiency, value);
        }
        public string H18Settings
        {
            get => GetItem(H18);
            set => AddItem(H18, value);
        }
        public string H19EfficiencySettings
        {
            get => GetItem(H19Efficiency);
            set => AddItem(H19Efficiency, value);
        }
        public string H19Settings
        {
            get => GetItem(H19);
            set => AddItem(H19, value);
        }
        public string H20EfficiencySettings
        {
            get => GetItem(H20Efficiency);
            set => AddItem(H20Efficiency, value);
        }
        public string H20Settings
        {
            get => GetItem(H20);
            set => AddItem(H20, value);
        }
        public string H21EfficiencySettings
        {
            get => GetItem(H21Efficiency);
            set => AddItem(H21Efficiency, value);
        }
        public string H21Settings
        {
            get => GetItem(H21);
            set => AddItem(H21, value);
        }
        public string H22EfficiencySettings
        {
            get => GetItem(H22Efficiency);
            set => AddItem(H22Efficiency, value);
        }
        public string H22Settings
        {
            get => GetItem(H22);
            set => AddItem(H22, value);
        }
        public string H23EfficiencySettings
        {
            get => GetItem(H23Efficiency);
            set => AddItem(H23Efficiency, value);
        }
        public string H23Settings
        {
            get => GetItem(H23);
            set => AddItem(H23, value);
        }
        public string H6EfficiencySettings
        {
            get => GetItem(H6Efficiency);
            set => AddItem(H6Efficiency, value);
        }
        public string H6Settings
        {
            get => GetItem(H6);
            set => AddItem(H6, value);
        }
        public string H7EfficiencySettings
        {
            get => GetItem(H7Efficiency);
            set => AddItem(H7Efficiency, value);
        }
        public string H7Settings
        {
            get => GetItem(H7);
            set => AddItem(H7, value);
        }
        public string H8EfficiencySettings
        {
            get => GetItem(H8Efficiency);
            set => AddItem(H8Efficiency, value);
        }
        public string H8Settings
        {
            get => GetItem(H8);
            set => AddItem(H8, value);
        }
        public string H9EfficiencySettings
        {
            get => GetItem(H9Efficiency);
            set => AddItem(H9Efficiency, value);
        }
        public string H9Settings
        {
            get => GetItem(H9);
            set => AddItem(H9, value);
        }
        public string IsNormCalculatedSettings
        {
            get => GetItem(IsNormCalculated);
            set => AddItem(IsNormCalculated, value);
        }
        public string JobIdSettings
        {
            get => GetItem(JobId);
            set => AddItem(JobId, value);
        }
        public string JobNormSettings
        {
            get => GetItem(JobNorm);
            set => AddItem(JobNorm, value);
        }
        public string LastClickSetting
        {
            get => GetItem(LastClick);
            set => AddItem(LastClick, value);
        }
        public string MachineCodeSettings
        {
            get => GetItem(MachineCode);
            set => AddItem(MachineCode, value);
        }
        public string MachineIdSettings
        {
            get => GetItem(MachineId);
            set => AddItem(MachineId, value);
        }
        public string MachineLineSettings
        {
            get => GetItem(MachineLine);
            set => AddItem(MachineLine, value);
        }
        public string MachineNameSettings
        {
            get => GetItem(MachineName);
            set => AddItem(MachineName, value);
        }
        public string OneClickWorthSettings
        {
            get => GetItem(OneClickWorth);
            set => AddItem(OneClickWorth, value);
        }
        public string SelectedPhaseSettings
        {
            get => GetItem(SelectedPhase);
            set => AddItem(SelectedPhase, value);
        }
        public string TotalEfficiencySettings
        {
            get => GetItem(TotalEfficiency);
            set => AddItem(TotalEfficiency, value);
        }
        public string UserIdSetting
        {
            get => GetItem(UserId);
            set => AddItem(UserId, value);
        }
        public string UserLineSettings
        {
            get => GetItem(UserLine);
            set => AddItem(UserLine, value);
        }
        public string UserLoginDateSettings
        {
            get => GetItem(UserLoginDate);
            set => AddItem(UserLoginDate, value);
        }
        public string UserNameSetting
        {
            get => GetItem(UserName);
            set => AddItem(UserName, value);
        }
        public string UserSectorSettings
        {
            get => GetItem(UserSector);
            set => AddItem(UserSector, value);
        }
        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }
        public string GetItem(string key)
        {
            return _settings.GetValueOrDefault(key, string.Empty);
        }
        public void RemoveSettings()
        {
            _settings.Clear();
        }
    }
}
