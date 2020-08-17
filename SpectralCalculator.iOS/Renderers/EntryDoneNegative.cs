using System.Drawing;
using SpectralCalculator.Controls;
using SpectralCalculator.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryDoneNegative), typeof(EntryDoneNegativeRenderer))]
namespace SpectralCalculator.iOS.Renderers
{
	/// <summary>
    /// Used to add a "Done" button to a Numeric keypad so the "Completed" event 
    /// can fire; also to add a "minus" button so negatives can be entered.  
    /// </summary>
	/// <see cref="https://gist.github.com/yuv4ik/4592401836bd6bc54ac85fcc5cdbaa2f"/>
	/// <see cref="https://github.com/xamarin/Xamarin.Forms/issues/6580"/>
	public class EntryDoneNegativeRenderer : EntryDoneRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
				return;

			if (Element.Keyboard == Keyboard.Numeric)
                Control.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
		}
	}
}
