rmdir /s /q bin
rmdir /s /q obj

dotnet build -c Release

"C:\Program Files (x86)\Eziriz\.NET Reactor\dotNET_Reactor.exe" -file bin\Release\DotNetReactor_FolderIconIssue.exe  -suppressildasm 1 -obfuscation 1 -mapping_file 1 -antistrong 1 -stringencryption 1 -control_flow_obfuscation 1 -flow_level 5 -exclude_enums 1