#!/usr/bin/env bash
zip -r genieCLI_win-x64-${TRAVIS_TAG}.zip /home/travis/build/rusith/Genie/GenieCLI/bin/release/netcoreapp1.1/win10-x64/publish
zip -r genieCLI_linux-x64-${TRAVIS_TAG}.zip /home/travis/build/rusith/Genie/GenieCLI/bin/release/netcoreapp1.1/ubuntu.16.10-x64/publish
zip -r genieCLI_osx-x64-${TRAVIS_TAG}.zip /home/travis/build/rusith/Genie/GenieCLI/bin/release/netcoreapp1.1/osx.10.11-x64/publish
echo "${PWD}"