
FROM node:20-alpine as builder

COPY package.json package-lock.json ./

# --legacy-peer-deps
RUN npm i && mkdir /app && mv ./node_modules ./app 

WORKDIR /app
COPY . /app/

# Build  bundles // should use --omit=dev
RUN npm run build --prod

FROM node:20-alpine
COPY --from=builder /app /app
WORKDIR /app
EXPOSE 4200
CMD npm run ng-serve 