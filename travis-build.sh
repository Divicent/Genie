#!/usr/bin/env bash
dotnet restore GenieCLI 
dotnet build GenieCLI
dotnet publish GenieCLI -c release -r win10-x64