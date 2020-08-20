using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpectralCalculator.Views
{
    public class AnimatedEntries
    {
        const int timeInMS = 250;
        const int timeOutMS = 250;

        Dictionary<string, Entry> entries = new Dictionary<string, Entry>();
        DateTime nextAnimationStart = DateTime.Now;

        public AnimatedEntries()
        {
        }

        public void add(string name, Entry e) => entries.Add(name, e);

        public void onPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.PropertyName;
            if (!entries.ContainsKey(name))
                return;

            int delayMS = 0;
            DateTime now = DateTime.Now;
            if (now < nextAnimationStart)
                delayMS = (int)(nextAnimationStart - now).TotalMilliseconds;
            nextAnimationStart = now.AddMilliseconds(delayMS + timeInMS + timeOutMS);

            animateAsync(entries[name], delayMS);
        }

        async void animateAsync(Entry e, int delayMS)
        {
            await Task.Delay(delayMS);
            await e.ScaleTo(2, timeInMS);
            await e.ScaleTo(1, timeOutMS, Easing.SpringOut);
        }
    }
}
