if not exist .git\hooks\pre-push copy _tools\pre-push .git\hooks\

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" _tools\BuildTasks.Dev.targets /fl /flp:logfile=build.log;verbosity=normal

exit
