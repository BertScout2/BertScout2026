using System.ComponentModel;

namespace BertScout2026;

public partial class GlobalViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public string ScouterName
    {
        get
        {
            return field ?? "";
        }
        set
        {
            if (field != value)
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScouterName)));
            }
        }
    }
}
