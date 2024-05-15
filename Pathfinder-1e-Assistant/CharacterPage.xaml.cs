using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;

namespace Pathfinder_1e_Assistant;

public partial class CharacterPage : ContentPage
{
	public CharacterPage()
	{
		InitializeComponent();
        Title = $"{CharacterGlobals.LoadedCharacter.CharacterName}'s Page";
	}




    private async void OnMacroPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MacroPage());
    }

}