using System;
using System.Drawing;
using SpectralCalculator.Controls;
using SpectralCalculator.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace SpectralCalculator.iOS.Renderers
{
	/// <summary>
    /// Used to add a "Done" button to a Numeric keypad so the "Completed" event 
    /// can fire.  It is RIDICULOUS Apple doesn't support this by default (affects
    /// Swift/XCode too).
    /// </summary>
	/// <see cref="https://gist.github.com/yuv4ik/4592401836bd6bc54ac85fcc5cdbaa2f"/>
	public class ExtendedEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
				return;

			// Check only for Numeric keyboard
			if (this.Element.Keyboard == Keyboard.Numeric)
				this.AddDoneButton();
		}

		/// <summary>
		/// <para>Add toolbar with Done button</para>
		/// </summary>
		protected void AddDoneButton()
		{
			var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
			{
				this.Control.ResignFirstResponder();
				var baseEntry = this.Element.GetType();
				((IEntryController)Element).SendCompleted();
			});

			toolbar.Items = new UIBarButtonItem[] {
				new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
				doneButton
			};
			this.Control.InputAccessoryView = toolbar;
		}
	}
}
