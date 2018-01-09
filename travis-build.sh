#!/usr/bin/env bash
dotnet restore GenieCLI 
dotnet build GenieCLI

echo "Version : $GENIE_VERSION"

dotnet publish GenieCLI -c release -r win10-x64
dotnet publish GenieCLI -c release -r ubuntu.16.10-x64
dotnet publish GenieCLI -c release -r osx-x64

