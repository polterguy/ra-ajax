@echo off

path=path;"c:\utils\nant-0.85\bin";

nant -buildfile:Ra.build %*

pause