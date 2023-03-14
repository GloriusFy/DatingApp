FROM node:12-alpine

WORKDIR /src

RUN apk add --no-cache  chromium --repository=http://dl-cdn.alpinelinux.org/alpine/v3.10/main

COPY ./package.json .
COPY ./package-lock.json ./

RUN npm ci

COPY ./ .

RUN npm run build:prod

RUN export CHROME_BIN='/usr/bin/chromium-browser'; npm run test:ci

ENTRYPOINT []