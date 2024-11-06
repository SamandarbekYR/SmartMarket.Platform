FROM postgres:16

ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=1234
ENV POSTGRES_DB=smart-market

# COPY ./init.sql /docker-entrypoint-initdb.d/
