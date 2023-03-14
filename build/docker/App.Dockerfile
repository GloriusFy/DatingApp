ARG BUILD_NUMBER

FROM dating-app/back:$BUILD_NUMBER AS back
FROM dating-app/front:$BUILD_NUMBER as front

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs
# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=back ./sln/published/API ./
COPY --from=front ./src/dist/ ./wwwroot/

ENTRYPOINT ["./API"]