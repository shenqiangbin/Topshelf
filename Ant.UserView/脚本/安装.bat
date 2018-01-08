@echo off

cd /d %~dp0
cd ..

call Ant.UserView.exe install

echo; 

echo success

echo; 
echo; 


pause