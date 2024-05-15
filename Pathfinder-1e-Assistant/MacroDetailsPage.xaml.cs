using System.Diagnostics;
using Pathfinder_1e_Assistant.Lib;

using Pathfinder_1e_Assistant.Databases;
namespace Pathfinder_1e_Assistant;

public partial class MacroDetailsPage : ContentPage
{
    readonly MacroRepo _macroRepo;
    readonly Macro _macro;
	public MacroDetailsPage(Macro macro, MacroRepo macroRepo)
	{
		InitializeComponent();

        _macroRepo = macroRepo;
        _macro = macro;
        MacroName.Text = macro.Name;
        MacroDetails.Text = macro.MacroText;

    }

	private async void OnAcceptChangesClicked(object sender, EventArgs e)
	{
        _macroRepo.UpdateMacro(new Macro { Id = _macro.Id, CharName =_macro.CharName, Name=MacroName.Text, MacroText = MacroDetails.Text});
        
        await Navigation.PopModalAsync();
    }

    private async void OnDeleteMacroClicked(object sender, EventArgs e)
    {
        //Debug.WriteLine(MacroName.Text + "\n");
        //Debug.WriteLine(MacroDetails.Text + "\n");
        bool answer = await this.DisplayAlert("Confirmation", "Are you sure you want to delete this macro?", "Yes", "No");
        if (answer) { _macroRepo.DeleteMacro(_macro); }
        await Navigation.PopModalAsync();
    }

    public async void OnCancelButtonClicked(object sender, EventArgs args)
    {
        await Navigation.PopModalAsync();
    }
}