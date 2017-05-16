FROM mono:latest
RUN apt-get update && apt-get -y install mono-xsp4

ADD /bin /app/bin
ADD default.htm /app
ADD Global.asax /app
ADD Web.config /appd

WORKDIR /app

EXPOSE 9000
ENTRYPOINT ["xsp4", "--port=9000", "--nonstop", "--printlog"]
