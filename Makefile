all:
	@echo "This app is built via Visual Studio."

clean:
	@rm -rf SpectralCalculator/{bin,obj} \
	        SpectralCalculator.{iOS,Android}/{bin,obj} \
            *.{apk,ipa} \
            .vs
