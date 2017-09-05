#!/usr/bin/env bash
dir
[ -f ./GenieCLI/bin/Release/netcoreapp1.1/win10-x64/publish/GenieCLI.exe ] && echo "Exe file exists" || echo "Exe file does not exist"
zip -r genieCLI_win-x64.zip ./GenieCLI/bin/Release/netcoreapp1.1/win10-x64/publish
dir
echo "Script executed from: ${PWD}"
echo "Travis Build: ${TRAVIS_BUILD_DIR}"
