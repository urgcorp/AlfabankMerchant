#!/bin/bash

PROJECT_PATH="./AlfabankMerchant.RestClient/AlfabankMerchant.RestClient.csproj"
SOURCE_URL="http://localhost:7665/v3/index.json"

echo "--- Build latest NuGet Release ---"

rm -rf AlfabankMerchant.RestClient/bin/Release/*.nupkg
dotnet clean "$PROJECT_PATH" -c Release
dotnet build "$PROJECT_PATH" -c Release --no-incremental
dotnet pack "$PROJECT_PATH" -c Release --no-build
dotnet nuget push "AlfabankMerchant.RestClient/bin/Release/*.nupkg" -s "$SOURCE_URL" --allow-insecure-connections --skip-duplicate

echo "--- Ready ---"
