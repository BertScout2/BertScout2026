using System.ComponentModel;

namespace BertScout2026;

public partial class GlobalViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public string ScouterName
    {
        get
        {
            return Global.ScouterName;
        }
        set
        {
            if (Global.ScouterName != value)
            {
                Global.ScouterName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScouterName)));
            }
        }
    }

    public int AirtableUploadCount
    {
        get
        {
            return Global.AirtableUploadCount;
        }
        set
        {
            if ((Global.AirtableUploadCount != value) && (value >= 0))
            {
                Global.AirtableUploadCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AirtableUploadCount)));
            }
        }
    }
}
