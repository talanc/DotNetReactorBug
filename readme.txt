Notes:
- Requires dotnet SDK and .NET Reactor installed
- Tested on .NET Reactor 6.7.0
- Protected debug builds work, release builds don't
- You must delete bin and obj folders when building and protecting
- I've found that mixing debug/release builds makes it work
- Build from the command line with dotnet as VS plays around with bin/obj folders
- Also found that the build/protect sometimes works, do another z_release build and it should start failing again

Instructions:
1) Run z_release_build_and_protect.cmd
2) Open bin\Release\DotNetReactor_FolderIconIssue.exe
3) Click 'set icon'
4) Observe a node added to the empty tree view
5) Close form
6) Open exe in secure
7) Click 'set icon'
8) Observe form crash
9) Open Event Viewer to see .NET Runtime error
