@echo off

cd /d %~dp0
cd ..

call Ant.UserView.exe.exe uninstall

echo; 

echo success

echo; 
echo; 


pause