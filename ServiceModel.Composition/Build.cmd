@ECHO OFF
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe "%~dp0\ServiceModel.Composition.proj" /v:diag /maxcpucount /nodeReuse:false %*