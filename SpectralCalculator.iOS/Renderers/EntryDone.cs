using System.Drawing;
using SpectralCalculator.Controls;
using SpectralCalculator.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryDone), typeof(EntryDoneRenderer))]
namespace SpectralCalculator.iOS.Renderers
{
	/// <summary>
    /// Used to add a "Done" button to a Numeric keypad so the "Completed" event 
    /// can fire; also to add a "minus" button so negatives can be entered.  
    /// </summary>
	/// <see cref="https://gist.github.com/yuv4ik/4592401836bd6bc54ac85fcc5cdbaa2f"/>
	/// <see cref="https://github.com/xamarin/Xamarin.Forms/issues/6580"/>
	public class EntryDoneRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
				return;

			if (Element.Keyboard == Keyboard.Numeric)
				AddDoneButton();
		}

		/// <summary>
		/// <para>Add toolbar with Done button</para>
		/// </summary>
		protected void AddDoneButton()
		{
			var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
			{
				Control.ResignFirstResponder();
				var baseEntry = this.Element.GetType();
				((IEntryController)Element).SendCompleted();
			});

			toolbar.Items = new UIBarButtonItem[] {
				new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
				doneButton
			};
			Control.InputAccessoryView = toolbar;
		}
	}
}
