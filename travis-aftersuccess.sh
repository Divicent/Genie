#!/usr/bin/env bash
dir
[ -f /home/travis/build/rusith/Genie/GenieCLI/bin/release/netcoreapp1.1/win10-x64/GenieCLI.exe ] && echo "Exe file exists" || echo "Exe file does not exist"
zip -r genieCLI_win-x64.zip /home/travis/build/rusith/Genie/GenieCLI/bin/release/netcoreapp1.1/win10-x64
dir
echo "Script executed from: ${PWD}"
echo "Travis Build: ${TRAVIS_BUILD_DIR}"
