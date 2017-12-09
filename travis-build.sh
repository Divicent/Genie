#!/usr/bin/env bash
dotnet restore GenieCLI 
dotnet build GenieCLI
dotnet publish GenieCLI -c release -r win10-x64
dotnet publish GenieCLI -c release -r ubuntu.16.10-x64
dotnet publish GenieCLI -c release -r osx.10.12-x64
