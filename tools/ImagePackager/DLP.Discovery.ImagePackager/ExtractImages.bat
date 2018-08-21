
REM Set this folder to point to Discovery Student Images Folder:
SET SOURCE_FOLDER=C:\Temp\Source

REM Set this location for output zip file:
SET DESTINATION_FOLDER=C:\Temp\Images

SET INPUT_FILE="C:\Projects\dlp\Minneapolis-ETL\Tools\tools\ImagePackager\Assets\student_images.csv"

DLP.Discovery.ImagePackager.exe --source=%SOURCE_FOLDER% --destination=%DESTINATION_FOLDER% --inputfile=%INPUT_FILE%

7za a %DESTINATION_FOLDER%\students.zip %DESTINATION_FOLDER%\students\*

PAUSE