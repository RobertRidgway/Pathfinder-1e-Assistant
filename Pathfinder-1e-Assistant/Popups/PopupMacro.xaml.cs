using CommunityToolkit.Maui.Views;

namespace Pathfinder_1e_Assistant.Popups;
 
public partial class PopupMacro : Popup
{
	// For rolls
	readonly Color bgColor = Colors.Yellow;
    readonly Color critHitColor = Colors.Green;
    readonly Color critFailColor = Colors.Red;
    readonly Color rollColor = Colors.Black;

    public PopupMacro(string[] macroSnippets, int[] critFlags)
	{
		InitializeComponent();

		FormattedString formattedString = new();

		for (int i = 0; i < macroSnippets.Length; i++)
		{
			Span span = new() { Text = macroSnippets[i] };
			switch (critFlags[i]) 
			{
				case 0: // Regular roll, no crit
					span.BackgroundColor = bgColor;
					span.FontAttributes = FontAttributes.Bold;
					span.TextColor = rollColor;
					break;
				case 1: // Critical Hit!
                    span.BackgroundColor = critHitColor;
                    span.FontAttributes = FontAttributes.Bold;
                    span.TextColor = rollColor;
                    break;
				case -1: // Critical Fail!
					span.BackgroundColor = critFailColor;
                    span.FontAttributes = FontAttributes.Bold;
                    span.TextColor = rollColor;
                    break;
            }
			formattedString.Spans.Add(span);
		}

		MacroLabel.FormattedText = formattedString;
		BindingContext = this;
		//Size = new Size(MacroLabel.Width * 1.1, MacroLabel.Height * 1.1);
	}
}