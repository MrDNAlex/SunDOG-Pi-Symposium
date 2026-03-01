#!/usr/bin/env bash

dotnet publish -r linux-arm64 --self-contained true -p:PublishSingleFile=true -c Release

scp -r ./bin/Release/net8.0/linux-arm64/publish/* mrdna@192.168.0.146:~/Projects/Symposium/
