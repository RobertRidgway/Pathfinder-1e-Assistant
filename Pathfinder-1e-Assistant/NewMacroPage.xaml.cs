using System.Diagnostics;
using Pathfinder_1e_Assistant.Lib;

using Pathfinder_1e_Assistant.Databases;
namespace Pathfinder_1e_Assistant;

public partial class NewMacroPage : ContentPage
{
    MacroRepo _macroRepo;
	public NewMacroPage(MacroRepo macroRepo)
	{
		InitializeComponent();

        _macroRepo = macroRepo;
        
    }

	private async void OnAddMacroClicked(object sender, EventArgs e)
	{
        _macroRepo.AddNewMacro(CharacterGlobals.LoadedCharacter.CharacterName, NewMacroName.Text, NewMacroDetails.Text);
        
        await Navigation.PopModalAsync();
    }


    public async void OnCancelButtonClicked(object sender, EventArgs args)
    {
        await Navigation.PopModalAsync();
    }
}