#!/usr/bin/env bash
cd ${TRAVIS_BUILD_DIR}/GenieCLI/bin/release/netcoreapp1.1/win10-x64/publish
zip -r ${TRAVIS_BUILD_DIR}/genieCLI_win-x64-${TRAVIS_TAG}.zip ./*
cd ${TRAVIS_BUILD_DIR}/GenieCLI/bin/release/netcoreapp1.1/ubuntu.16.10-x64/publish
zip -r ${TRAVIS_BUILD_DIR}/genieCLI_linux-x64-${TRAVIS_TAG}.zip ./*
cd ${TRAVIS_BUILD_DIR}/GenieCLI/bin/release/netcoreapp1.1/osx.10.11-x64/publish
zip -r ${TRAVIS_BUILD_DIR}/genieCLI_osx-x64-${TRAVIS_TAG}.zip ./*
cd  ${TRAVIS_BUILD_DIR}
