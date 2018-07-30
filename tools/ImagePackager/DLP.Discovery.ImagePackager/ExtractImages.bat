
REM Set this folder to point to Discovery Student Images Folder:
SET SOURCE_FOLDER=C:\Temp\Source

REM Set this location for output zip file:
SET DESTINATION_FOLDER=C:\Temp\Images

DLP.Discovery.ImagePackager.exe --source=%SOURCE_FOLDER% --destination=%DESTINATION_FOLDER%

7za a %DESTINATION_FOLDER%\students.zip %DESTINATION_FOLDER%\students\*

PAUSE