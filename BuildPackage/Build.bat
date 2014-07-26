Call ..\.nuget\nuget.exe sources add -Name Umb7Nightly -Source https://www.myget.org/F/umbraco7nightly -NonInteractive
Call ..\.nuget\nuget.exe restore ..\Our.Umbraco.PropertyValueConverters.sln -source "https://www.nuget.org/api/v2;https://www.myget.org/F/umbraco7nightly"
Call C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe Package.build.xml