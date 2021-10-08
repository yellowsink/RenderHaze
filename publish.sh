#!/bin/sh
rm -rf */(bin|obj)
dotnet build -c Release -v q RenderHaze/RenderHaze.csproj