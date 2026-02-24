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
}
