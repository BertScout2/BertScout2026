namespace BertScout2026.Models;

public interface IGlobalModel
{
    string ScouterName { get; set; }
}

public class GlobalModel : IGlobalModel
{
    public string ScouterName { get; set; }

    public GlobalModel()
    {
        ScouterName = "";
    }
}
