using CommunityToolkit.Maui.Views;
using System.Diagnostics;

namespace Pathfinder_1e_Assistant.Popups;
 
public partial class PopupMacro : Popup
{
	// For rolls
	readonly static Color bgColor = Colors.Yellow;
    readonly static Color critHitColor = Colors.Green;
    readonly static Color critFailColor = Colors.Red;
    readonly static Color rollColor = Colors.Black;
	readonly static Color textColor = Colors.White;

    readonly static Color bgLabelColor = Colors.Black;
    readonly static Color regularBgColor = Colors.Transparent;

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
				case 2: // Text
					span.BackgroundColor = regularBgColor;
					//span.FontAttributes = FontAttributes.Bold;
					span.TextColor = textColor;
					break;
			}
			formattedString.Spans.Add(span);
		}

		// Create frame for roll result
		Frame frame = new() 
		{ 
			BackgroundColor = bgLabelColor,
            Content = new Label { FormattedText = formattedString, BackgroundColor = bgLabelColor }
        };
		MacroControl.Children.Add(frame);
		//BindingContext = this;
	}
}