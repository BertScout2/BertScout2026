using BertScout2026.Models;

namespace BertScout2026;

public partial class ListMatchesPage : ContentPage
{
	public List<MatchTeam> Matches = [];

	public ListMatchesPage()
	{
		InitializeComponent();
	}
}