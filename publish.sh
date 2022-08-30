#!/bin/sh
dotnet clean -v q
dotnet build -c Release -v q RenderHaze/RenderHaze.csproj