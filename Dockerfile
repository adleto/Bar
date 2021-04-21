FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "Bar.API/Bar.API.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk add -U tzdata \
  && cp /usr/share/zoneinfo/Europe/Belgrade /etc/localtime \
  && echo "Europe/Belgrade" > /etc/timezone \
  && apk del tzdata \
  && rm -rf \
  /var/cache/apk/*


ENTRYPOINT ["dotnet", "Bar.API.dll"]