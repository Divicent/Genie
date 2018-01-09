#!/usr/bin/env bash
dotnet restore GenieCLI
cd Genie.Tests
dotnet xunit --fx-version 2.0.3
cd ..

if [ ! -z "$GENIE_VERSION" -a "$GENIE_VERSION" != " " ]; then
  cd GenieCLI
  printf "$GENIE_VERSION" > .version
  cd ..
  dotnet publish GenieCLI -c release -r win10-x64
  dotnet publish GenieCLI -c release -r ubuntu.16.10-x64
  dotnet publish GenieCLI -c release -r osx-x64
fi