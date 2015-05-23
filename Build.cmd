@ECHO OFF
"%ProgramFiles%\MSBuild\12.0\bin\MSBuild.exe" "%~dp0\build.proj" /v:m /maxcpucount /nodeReuse:false %*