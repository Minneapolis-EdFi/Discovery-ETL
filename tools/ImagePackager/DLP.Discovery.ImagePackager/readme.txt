Follow this guide to
Extract images from Discovery and compress them in a Zip file 
for EDFI Dashboard upload.

1) Look into DLP.Discovery.ImagePackager.exe.config file and adjust connection string "DiscoveryDB" information accordingly.
2) Open and edit 'ExtracImages.bat' file and adjust the following variables:

	-SOURCE_FOLDER
		Set this folder to point to Discovery Student Images Folder
	-DESTINATION_FOLDER
		Set this location for the output zip file.

3) Execute 'ExtractImages.bat' by double click on it.

4) Pick 'students.zip' file from the designated folder at step 2, 
   and upload it into Ed-Fi Dashboard UI for importing student images.