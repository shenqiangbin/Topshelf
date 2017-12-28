@echo off

cd /d %~dp0
cd ..

call Law.Monitor.exe install

echo; 

echo success

echo; 
echo; 


pause