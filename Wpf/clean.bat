@echo off

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe GetHandle.sln /t:Clean /p:Configuration=Debug
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe GetHandle.sln /t:Clean /p:Configuration=Release

pause
