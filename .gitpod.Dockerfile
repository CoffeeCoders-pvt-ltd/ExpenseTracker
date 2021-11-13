FROM gitpod/workspace-full:latest

USER gitpod
#.NET installed via .gitpod.yml task until the following issue is fixed: https://github.com/gitpod-io/gitpod/issues/5090
ENV DOTNET_VERSION=5.0
ENV DOTNET_ROOT=/workspace/.dotnet
ENV PATH=$PATH:$DOTNET_ROOT

RUN apt-key adv --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys B97B0AFCAA1A47F044F244A07FCC7D46ACCC4CF8
RUN echo "deb http://apt.postgresql.org/pub/repos/apt/ precise-pgdg main" > /etc/apt/sources.list.d/pgdg.list
RUN apt-get update
RUN apt-get install -y postgresql-12 postgresql-client-12

USER postgres

RUN /etc/init.d/postgresql start &&\
    psql --command "ALTER USER postgres WITH ENCRYPTED PASSWORD 'admin';"

# RUN echo "host all  all    0.0.0.0/0  md5" >> /etc/postgresql/12/main/pg_hba.conf

# RUN echo "listen_addresses='*'" >> /etc/postgresql/12/main/postgresql.conf

EXPOSE 5432
