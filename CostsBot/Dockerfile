FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# copy all sources
WORKDIR /app
COPY . /app/sources
WORKDIR /app/sources

# build all projects
RUN dotnet restore
RUN dotnet publish -c Release -o /app/CostsBot --no-restore 

WORKDIR /app
CMD ["dotnet", "CostsBot.dll"]
#ENTRYPOINT ["tail", "-f", "/dev/null"]