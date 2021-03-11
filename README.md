# Spectral Calculator

This is a simple cross-platform (Android / iOS) Xamarin app allowing you to 
quickly convert between wavelength (nm) and wavenumber (cm⁻¹) when working
with Raman shifts.

# Store Links

- Android: https://play.google.com/store/apps/details?id=com.wasatchphotonics.spectral_calculator
- Apple: https://apps.apple.com/app/id1528209782

# Changelog

- see [Changelog](README_CHANGELOG.md)

# Globalization
- add setting to switch between 0,000.00 number format and 0.000,00 number format
Right now this has turned out to be a greater challenge than anticipated. It comes in part from the fact that it's not just displayed text that needs to handle a new format but entry as well. The main approach used here was to include a switch that when toggled on set the app culture to France, (a country that uses 0.000,00 format) and then refresh the shell. This worked fine on iOS but resulted in a graphical glitch on Android where the shell tab bar rendered initally white then changed to the proper color, which cause a white streak slide effect across the tab bar. However, entry was the issue on both platforms. The default keyboard for any entry is controlled by the phone itself not the app so even if the current culture was changed and a user now saw 0.000,00 format numbers they could not enter them because their default keyboard still only managed 0,000.00 numbers. Both keyboards on iOS and Android restrict entry to proper formatting for numeric keyboards. This means to solve the issue a custom render keyboard needs to be written. 
