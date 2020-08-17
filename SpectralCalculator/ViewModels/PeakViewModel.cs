﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using SpectralCalculator.Models;

namespace SpectralCalculator.ViewModels
{
    public class PeakViewModel : INotifyPropertyChanged
    {
        PeakModel pm = new PeakModel();

        public PeakViewModel()
        {
            openWebsite = new Command(async () => await Browser.OpenAsync("https://wasatchphotonics.com"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand openWebsite { get; }

        public string Title
        {
            get => "Raman Peak Calculator";
        }

        public double laserWavelength
        {
            get => pm.laserWavelength;
            private set
            {
                Console.WriteLine($"PVM.laserWavelength = {value}");
                pm.laserWavelength = value;
                OnPropertyChanged();
            }
        }

        public double peakWavelength 
        {
            get => pm.peakWavelength;
            private set
            {
                Console.WriteLine($"PVM.peakWavelength = {value}");
                pm.peakWavelength = value;
                OnPropertyChanged();
            }
        }

        public double peakWavenumber
        {
            get => pm.peakWavenumber;
            private set
            {
                Console.WriteLine($"PVM.peakWavenumber = {value}");
                pm.peakWavenumber = value;
                OnPropertyChanged();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // accept keyboard "completed" events from View code-behind
        ////////////////////////////////////////////////////////////////////////

        public void setLaserWavelength(string s)
        {
            Console.WriteLine($"PVM.setLaserWavelength -> {s}");
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                laserWavelength = value;

                if (peakWavelength > 0)
                    computePeakWavenumber();
                else
                    computePeakWavelength();
            }
        }

        public void setPeakWavelength(string s)
        {
            Console.WriteLine($"PVM.setPeakWavelength -> {s}");
            if (float.TryParse(s, out float value))
            {
                if (value <= 0)
                    return;

                peakWavelength = value;
                computePeakWavenumber();
            }
        }

        public void setPeakWavenumber(string s)
        {
            Console.WriteLine($"PVM.setPeakWavenumber -> {s}");
            if (float.TryParse(s, out float value))
            {
                peakWavenumber = value;
                computePeakWavelength();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // Computations
        ////////////////////////////////////////////////////////////////////////

        void computePeakWavenumber()
        {
            if (laserWavelength <= 0)
                return;

            peakWavenumber = SpectralMath.computeWavenumber(laserWavelength, peakWavelength);
        }

        void computePeakWavelength()
        {
            if (laserWavelength <= 0)
                return;

            peakWavelength = SpectralMath.computeWavelength(laserWavelength, peakWavenumber);
        }

        ////////////////////////////////////////////////////////////////////////
        // Notifications
        ////////////////////////////////////////////////////////////////////////

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}