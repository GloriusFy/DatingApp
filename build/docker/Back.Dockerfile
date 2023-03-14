FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env

WORKDIR /sln

COPY ./API/API.csproj \
    ./Application/Application.csproj \
    ./Domain/Domain.csproj \
    ./Infrastructure/Infrastructure.csproj \
    ./UnitTests/UnitTests.csproj \
    ./DatingApp.sln \
    ./

RUN for file in *.csproj; do \
        filename=$(basename $file) && \
        dirname=${filename%.*} && \
        mkdir $dirname && \
        mv $filename ./$dirname/; \
    done

RUN dotnet restore DatingApp.sln

COPY ./ ./

RUN dotnet build ./DatingApp.sln -c Release --no-restore

RUN dotnet test ./UnitTests -c Release --no-build

RUN dotnet publish ./API/API.csproj -o ./published/API -c Release --no-build

FROM scratch as transfer
WORKDIR /sln
COPY --from=build-env ./sln/published ./published