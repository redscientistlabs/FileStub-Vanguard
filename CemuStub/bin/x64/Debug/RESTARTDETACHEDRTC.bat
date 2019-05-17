@echo off

taskkill /F /IM Cemu.exe > nul 2>&1
taskkill /F /IM CemuStub.exe > nul 2>&1

start CemuStub.exe
exit