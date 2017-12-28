@echo off

cd /d %~dp0
cd ..

call Law.Monitor.exe.exe uninstall

echo; 

echo success

echo; 
echo; 


pause