FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Api/*.csproj ./Api/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Domain.Core/*.csproj ./Domain.Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Common/*.csproj ./Common/

# Copy everything else and build
COPY . ./

RUN dotnet restore Api/Api.csproj

RUN dotnet publish Api/Api.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Api/out .

#RUN mkdir Dynamic
ENTRYPOINT ["dotnet", "Api.dll"]

# docker build -t dyraapi .
# docker run -d -p 8081:80 -e Database__Mongodb__ConnectionString=mongodb://host.docker.internal:27017 --name dyra-local dyraapi