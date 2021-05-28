FROM node:12
WORKDIR /usr/src/app

COPY package*.json ./
RUN yarn install --production=true

COPY . ./
COPY ./production.server.js ./build

EXPOSE 8080
CMD [ "node", "build/production.server.js" ]