namespace SmartB.Core.Contracts.Services.General
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);

        void RemoveSettings();

        string UserNameSetting { get; set; }
        string UserIdSetting { get; set; }
        string UserSectorSettings { get; set; }
        string UserLineSettings { get; set; }
        string UserLoginDateSettings { get; set; }

        string MachineIdSettings { get; set;}
        string MachineCodeSettings { get; set;}
        string MachineLineSettings { get; set; }
        string MachineNameSettings { get;set;}

        string JobIdSettings { get; set; }
        string JobsIdSettings { get; set; }
        string CommessaFromBarcode { get; set; }
        string SelectedPhaseSettings { get; set; }
        string JobNormSettings { get; set; }
        string OneClickWorthSettings { get; set; }

        string TotalEfficiencySettings { get; set; }
        string CounterSettings { get; set; }

        string H6Settings { get; set; }
        string H7Settings { get; set; }
        string H8Settings { get; set; }
        string H9Settings { get; set; }
        string H10Settings { get; set; }
        string H11Settings { get; set; }
        string H12Settings { get; set; }
        string H13Settings { get; set; }
        string H14Settings { get; set; }
        string H15Settings { get; set; }
        string H16Settings { get; set; }
        string H17Settings { get; set; }
        string H18Settings { get; set; }
        string H19Settings { get; set; }
        string H20Settings { get; set; }
        string H21Settings { get; set; }
        string H22Settings { get; set; }
        string H23Settings { get; set; }

        string H6EfficiencySettings { get; set; }
        string H7EfficiencySettings { get; set; }
        string H8EfficiencySettings { get; set; }
        string H9EfficiencySettings { get; set; }
        string H10EfficiencySettings { get; set; }
        string H11EfficiencySettings { get; set; }
        string H12EfficiencySettings { get; set; }
        string H13EfficiencySettings { get; set; }
        string H14EfficiencySettings { get; set; }
        string H15EfficiencySettings { get; set; }
        string H16EfficiencySettings { get; set; }
        string H17EfficiencySettings { get; set; }
        string H18EfficiencySettings { get; set; }
        string H19EfficiencySettings { get; set; }
        string H20EfficiencySettings { get; set; }
        string H21EfficiencySettings { get; set; }
        string H22EfficiencySettings { get; set; }
        string H23EfficiencySettings { get; set; }

        string IsNormCalculatedSettings { get; set; }
        string LastClickSetting { get; set; }
        string EfficiencyIdsSettings { get; set; }
        string DeviceIdSettings { get; set; }

        string BadPiecesSettings { get; set; }
        string GoodPiecesSettings { get; set; }
        string TotalPiecesSettings { get; set; }

    }
}
