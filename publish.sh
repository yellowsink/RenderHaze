#!/bin/sh
rm -rf */bin */obj > /dev/null
dotnet build -c Release -v q RenderHaze/RenderHaze.csproj