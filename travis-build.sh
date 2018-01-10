#!/usr/bin/env bash
cd Genie.Tests
dotnet restore
dotnet xunit --fx-version 2.0.3

if [ $? -ne 0 ]; then
    exit 3
fi

cd ..
if [ ! -z "$GENIE_VERSION" -a "$GENIE_VERSION" != " " ]; then
  cd GenieCLI
  dotnet restore
  printf "$GENIE_VERSION" > .version
  cd ..
  dotnet publish GenieCLI -c release -r win10-x64
  dotnet publish GenieCLI -c release -r ubuntu.16.10-x64
  dotnet publish GenieCLI -c release -r osx-x64
fi