FROM mcr.microsoft.com/dotnet/sdk:8.0

# Instalar wget, xvfb, unzip e gnupg2
RUN apt-get update -y && apt-get install -y wget xvfb unzip gnupg2

# Configurar o Chrome PPA
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
RUN echo "deb http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list

# Atualizar a lista de pacotes e instalar o Google Chrome
RUN apt-get update -y
RUN apt-get install -y google-chrome-stable

# Configurar variáveis de ambiente do Chromedriver
ENV CHROMEDRIVER_VERSION "126.0.6478.126"
ENV CHROMEDRIVER_DIR /chromedriver
RUN mkdir $CHROMEDRIVER_DIR

# Baixar e instalar o Chromedriver
RUN wget -q --continue -P $CHROMEDRIVER_DIR "https://storage.googleapis.com/chrome-for-testing-public/$CHROMEDRIVER_VERSION/linux64/chromedriver-linux64.zip"
RUN unzip $CHROMEDRIVER_DIR/chromedriver* -d $CHROMEDRIVER_DIR

# Colocar o Chromedriver no PATH
ENV PATH $CHROMEDRIVER_DIR:$PATH

# Adicionar a entrada de comando para iniciar o Xvfb e executar testes
# ENTRYPOINT ["sh", "-c", "xvfb-run --server-args='-screen 0 1920x1080x24'"]
