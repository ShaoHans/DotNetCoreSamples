#FROM microsoft/aspnetcore:2.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0 AS build
#WORKDIR /src
#COPY Member.API/Member.API.csproj Member.API/
#RUN dotnet restore Member.API/Member.API.csproj
#COPY . .
#WORKDIR /src/Member.API
#RUN dotnet build Member.API.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish Member.API.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Member.API.dll"]
#

FROM microsoft/aspnetcore-build as build-env
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o output

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=build-env /code/output ./

EXPOSE 80
ENTRYPOINT ["dotnet","Member.API.dll"]