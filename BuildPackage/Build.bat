ECHO APPVEYOR_REPO_BRANCH: %APPVEYOR_REPO_BRANCH%
ECHO APPVEYOR_BUILD_NUMBER : %APPVEYOR_BUILD_NUMBER%
ECHO APPVEYOR_BUILD_VERSION : %APPVEYOR_BUILD_VERSION%
cd ..\BuildPackage\
Call Tools\nuget.exe restore  ..\Our.Umbraco.PropertyValueConverters.sln
Call "%programfiles(x86)%\MSBuild\12.0\Bin\MsBuild.exe" Package.build.xml